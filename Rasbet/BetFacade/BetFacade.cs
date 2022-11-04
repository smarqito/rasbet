﻿using BetApplication.Interfaces;
using Domain;
using DTO;
using DTO.BetDTO;
using DTO.UserDTO;

namespace BetFacade;

public class BetFacade : IBetFacade
{
    public IBetRepository BetRepository;
    public ISelectionRepository SelectionRepository;
    public APIService APIService = new ();

    public BetFacade(IBetRepository betRepository, 
                     ISelectionRepository selectionRepository)
    {
        BetRepository = betRepository;
        SelectionRepository = selectionRepository;
    }

    // Métodos para o BetController
    public async Task<BetSimple> CreateBetSimple(double amount,
                                                 DateTime start,
                                                 string userId,
                                                 CreateSelectionDTO selectionDTO)
    {
        BetSimple? bet;
        try
        {
            Selection newS = await CreateSelection(selectionDTO.BetTypeId, selectionDTO.OddId, selectionDTO.Odd, selectionDTO.GameId);
            double odd = await APIService.GetOdd(selectionDTO.BetTypeId, selectionDTO.OddId);

            bet = await BetRepository.CreateBetSimple(amount, start, userId, newS, odd);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
        try 
        {
            await APIService.WithdrawUserBalance(new TransactionDTO(userId,amount));
            return bet;
        }
        catch(Exception e)
        {
            await BetRepository.DeleteBet(bet.Id);
            throw new Exception(e.Message);
        }
    }

    public async Task<BetMultiple> CreateBetMultiple(double amount,
                                                     DateTime start,
                                                     string userId,
                                                     ICollection<CreateSelectionDTO> selectionDTOs)
    {
        ICollection<Selection> selections = new List<Selection>();
        double oddMultiple = 1.0;
        foreach (var selectionDTO in selectionDTOs)
        {
            try
            {
                Selection newS = await CreateSelection(selectionDTO.BetTypeId,
                                                       selectionDTO.OddId,
                                                       selectionDTO.Odd,
                                                       selectionDTO.GameId);
                selections.Add(newS);
                oddMultiple *= newS.Odd;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        BetMultiple? bet;
        try
        {
            bet = await BetRepository.CreateBetMultiple(amount,
                                                        start,
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
            await APIService.WithdrawUserBalance(new TransactionDTO(userId, amount));
            return bet;
        }
        catch(Exception e)
        {
            await BetRepository.DeleteBet(bet.Id);
            throw new Exception(e.Message);
        }
    }

    public async Task<ICollection<Bet>> GetUserBetsByState(string user, BetState state)
    {
        try
        {
            return await BetRepository.GetUserBetsByState(user, state);
        }
        catch(Exception e)
        {
            throw new Exception(e.Message);
        }
    }
    public async Task<ICollection<Bet>> GetUserBetsByStart(string user, DateTime start)
    {
        try
        {
            return await GetUserBetsByStart(user, start);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message); 
        }
    }

    public async Task<ICollection<Bet>> GetUserBetsByAmount(string user, double amount)
    {
        try
        {
            return await GetUserBetsByAmount(user, amount);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<ICollection<Bet>> GetUserBetsByEnd(string user, DateTime end)
    {
        try
        {
            return await GetUserBetsByEnd(user, end);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<ICollection<Bet>> GetUserBetsByWonValue(string user, double wonValue)
    {
        try
        {
            return await GetUserBetsByWonValue(user, wonValue);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    //o método retorna true se existirem apostas vencedoras no update
    public async Task<bool> UpdateBets(ICollection<BetsOddsWonDTO> finishedGames)
    {
        try
        {
            ICollection<Bet> won_bets = await BetRepository.UpdateBets(finishedGames);

            if(won_bets.Count > 0)
            {
                foreach(var bet in won_bets)
                {
                    await APIService.DepositUserBalance(new TransactionDTO(bet.UserId, bet.WonValue));
                }
                return true;
            }
            return false;
        }
        catch(Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    // Métodos para o SelectionRepository
    public async Task<Selection> CreateSelection(int betTypeId, int oddId, double odd, int gameId)
    {
        // pedir GameOddApi com betTypeId + oddId
        // receber odd atual do servidor
        // comparar com odd do cliente e aplicar um threshold max
        //      caso threshold exceda, enviar erro
        //      caso contrario, criar selection
        try
        {
            double serverOdd = await APIService.GetOdd(betTypeId, oddId);
            return await SelectionRepository.CreateSelection(serverOdd, odd, betTypeId, oddId, gameId);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<ICollection<Selection>> GetSelectionByGame(int game)
    {
        try
        {
            return await SelectionRepository.GetSelectionByGame(game);
        }
        catch(Exception e)
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
        catch(Exception e)
        {
            throw new Exception(e.Message);
        }
    }

}
