using BetApplication.Errors;
using BetApplication.Interfaces;
using Domain;
using Domain.ResultDomain;
using DTO;


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
    public async Task<BetSimple> CreateBetSimple(double amount, DateTime start, int userId, int selectionId)
    {
        try
        {
            Selection selection = await SelectionRepository.GetSelectionById(selectionId);
            double odd = await APIService.GetOdd(selection.BetTypeId, selection.OddId);
            bool valid = await APIService.WithdrawUserBalance(userId, amount);

            if (valid)
            {
                BetSimple bet = await BetRepository.CreateBetSimple(amount, start, userId, selection, odd);

                return bet;
            }
            throw new Exception("Ocorreu um erro interno!");
        }
        catch (OddTooDiferentException e)
        {
            await APIService.DepositUserBalance(userId, amount);
            throw new Exception(e.Message);
        }
        catch(BetTooLowException e)
        {
            await APIService.DepositUserBalance(userId, amount);
            throw new Exception(e.Message);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<BetMultiple> CreateBetMultiple(double amount, DateTime start, int userId, double oddMultiple, ICollection<int> selectionIds)
    {
        ICollection<Selection> selections = new List<Selection>();
        foreach (int selectionId in selectionIds)
        {
            try
            {
                var selection = await SelectionRepository.GetSelectionById(selectionId);
                selections.Add(selection);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        try
        {

            bool valid = await APIService.WithdrawUserBalance(userId, amount);
            if (valid)
            {
                BetMultiple bet = await BetRepository.CreateBetMultiple(amount, start, userId, oddMultiple, selections);
                return bet;
            }

            throw new Exception("Ocorreu um erro interno!");

        }
        catch (OddTooDiferentException e)
        {
            await APIService.DepositUserBalance(userId, amount);
            throw new Exception(e.Message);
        }
        catch (BetTooLowException e)
        {
            await APIService.DepositUserBalance(userId, amount);
            throw new Exception(e.Message);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<ICollection<Bet>> GetUserBetsByState(int user, BetState state)
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
    public async Task<ICollection<Bet>> GetUserBetsByStart(int user, DateTime start)
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

    public async Task<ICollection<Bet>> GetUserBetsByAmount(int user, double amount)
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

    public async Task<ICollection<Bet>> GetUserBetsByEnd(int user, DateTime end)
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

    public async Task<ICollection<Bet>> GetUserBetsByWonValue(int user, double wonValue)
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

    public async Task<bool> UpdateBets(ICollection<BetsOddsWonDTO> finishedGames)
    {
        try
        {
            return await BetRepository.UpdateBets(finishedGames);
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
        return await SelectionRepository.GetSelectionByType(bettype);
    }

}
