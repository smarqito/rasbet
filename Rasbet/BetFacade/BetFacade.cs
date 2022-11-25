using AutoMapper;
using BetApplication.Errors;
using BetApplication.Interfaces;
using Domain;
using DTO;
using DTO.BetDTO;
using DTO.GameOddDTO;
using DTO.UserDTO;

namespace BetFacade;

public class BetFacade : IBetFacade
{
    public IBetRepository BetRepository;
    public ISelectionRepository SelectionRepository;
    public IMapper mapper;
    public APIService APIService = new();

    public BetFacade(IBetRepository betRepository,
                     ISelectionRepository selectionRepository,
                     IMapper mapper)
    {
        BetRepository = betRepository;
        SelectionRepository = selectionRepository;
        this.mapper= mapper;

    }

    public bool GameAvailable(DateTime start, string state)
    {
        if (start < DateTime.Now && state.Equals(GameState.Open))
            return true;
        else
            return false;
    }
    // Métodos para o BetController
    public async Task<BetSimple> CreateBetSimple(double amount,
                                                 string userId,
                                                 CreateSelectionDTO selectionDTO)
    {
        BetSimple? bet;
        try
        {
            GameInfoDTO game = await APIService.GetGame(selectionDTO.GameId, false);
            if (!GameAvailable(game.StartTime, game.State))
            {
                throw new GameNotAvailableException($"Game {selectionDTO.GameId} is not available");
            }
            

            double server_odd = await APIService.GetOdd(selectionDTO.BetTypeId, selectionDTO.OddId);
            
            Selection newS = await CreateSelection(selectionDTO.BetTypeId, selectionDTO.OddId, selectionDTO.Odd, selectionDTO.GameId, server_odd);

            bet = await BetRepository.CreateBetSimple(amount, userId, newS, server_odd);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }

        try
        {
            await APIService.WithdrawUserBalance(new DTO.UserDTO.TransactionDTO(userId, amount));
            return bet;
        }
        catch (Exception e)
        {
            await BetRepository.DeleteBet(bet.Id);
            throw new Exception(e.Message);
        }
    }

    public async Task<BetMultiple> CreateBetMultiple(double amount,
                                                     string userId,
                                                     ICollection<CreateSelectionDTO> selectionDTOs)
    {
        ICollection<Selection> selections = new List<Selection>();
        double oddMultiple = 1.0;
        foreach (var selectionDTO in selectionDTOs)
        {
            try
            {
                GameInfoDTO game = await APIService.GetGame(selectionDTO.GameId, false);
                if (!GameAvailable(game.StartTime, game.State))
                {
                    throw new GameNotAvailableException($"Game {selectionDTO.GameId} is not available");
                }

                double server_odd = await APIService.GetOdd(selectionDTO.BetTypeId, selectionDTO.OddId);

                Selection newS = await CreateSelection(selectionDTO.BetTypeId,
                                                       selectionDTO.OddId,
                                                       selectionDTO.Odd,
                                                       selectionDTO.GameId,
                                                       server_odd);
                selections.Add(newS);
                oddMultiple *= newS.Odd;
            }
            catch (Exception e)
            {
                await SelectionRepository.RemoveSelections(selections);
                throw new Exception(e.Message);
            }
        }

        BetMultiple? bet;
        try
        {
            bet = await BetRepository.CreateBetMultiple(amount,
                                                        userId,
                                                        oddMultiple,
                                                        selections);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
        try
        {
            await APIService.WithdrawUserBalance(new DTO.UserDTO.TransactionDTO(userId, amount));
            return bet;
        }
        catch (Exception e)
        {
            await BetRepository.DeleteBet(bet.Id);
            throw new Exception(e.Message);
        }
    }

    public async Task<ICollection<BetDTO>> GetUserBetsByState(string user, BetState state)
    {
        try
        {
            ICollection<Bet> bets = await BetRepository.GetUserBetsByState(user, state);
            return mapper.Map<ICollection<BetDTO>>(bets);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    //o método retorna true se existirem apostas vencedoras no update
    public async Task<bool> UpdateBets(ICollection<BetsOddsWonDTO> finishedGames)
    {
        bool resp = false;
        try
        {
            ICollection<Bet> won_bets;

            if (finishedGames.Count > 0)
            {
                foreach (var finishedGame in finishedGames)
                {
                    ICollection<Selection> selecs = await SelectionRepository.GetSelectionByType(finishedGame.BetTypeId);
                    won_bets = await BetRepository.UpdateBets(selecs, finishedGame.WinnerOddIds);

                    if (won_bets.Count > 0)
                    {
                        foreach (var bet in won_bets)
                        {
                            await APIService.DepositUserBalance(new DTO.UserDTO.TransactionDTO(bet.UserId, bet.WonValue));
                        }
                        resp = true;
                    }
                }

                return resp;
            }

            else throw new FinishedGamesInvalidException("Os jogos enviados são inválidos!");
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    // Métodos para o SelectionRepository
    public async Task<Selection> CreateSelection(int betTypeId, int oddId, double odd, int gameId, double serverOdd)
    {
        // pedir GameOddApi com betTypeId + oddId
        // receber odd atual do servidor
        // comparar com odd do cliente e aplicar um threshold max
        //      caso threshold exceda, enviar erro
        //      caso contrario, criar selection
        return await SelectionRepository.CreateSelection(serverOdd, odd, betTypeId, oddId, gameId);
    }

    public async Task<ICollection<Selection>> GetSelectionByGame(int game)
    {
        try
        {
            return await SelectionRepository.GetSelectionByGame(game);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<ICollection<Selection>> GetSelectionByType(int bettype)
    {
        try
        {
            return await SelectionRepository.GetSelectionByType(bettype);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

}
