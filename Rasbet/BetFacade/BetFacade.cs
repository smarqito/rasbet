using BetApplication.Interfaces;
using Domain;
using DTO;
using GameOddApplication.Interfaces;
using UserApplication.Errors;
using UserApplication.Interfaces;

namespace BetFacade;

public class BetFacade : IBetFacade
{
    public IBetRepository BetRepository;
    public ISelectionRepository SelectionRepository;
    public ITransactionRepository TransactionRepository;
    public IUserRepository UserRepository;
    public IBetTypeRepository BetTypeRepository;

    public BetFacade(IBetRepository betRepository, 
                     ISelectionRepository selectionRepository, 
                     ITransactionRepository transactionRepository, 
                     IUserRepository userRepository,
                     IBetTypeRepository betTypeRepository)
    {
        BetRepository = betRepository;
        SelectionRepository = selectionRepository;
        TransactionRepository = transactionRepository;
        UserRepository = userRepository;
        BetTypeRepository = betTypeRepository;
    }

    // Métodos para o BetController
    public async Task<BetSimple> CreateBetSimple(double amount, DateTime start, int userId, int selectionId)
    {
        try
        {
            Selection selection = await SelectionRepository.GetSelectionById(selectionId);
            //Verificar se a odd esta dentro dos parâmetros aceitaveis (comparar com a odd atual do bettype)

            //Verificar se o user é válido
            AppUser user = new AppUser("teste", "teste", "teste", "teste");

            BetSimple bet = await BetRepository.CreateBetSimple(amount, start, userId, selection);
            await TransactionRepository.WithdrawBalance(user, amount);

            return bet;
        }
        catch(UserBalanceTooLowException e) {
            //await BetRepository.DeleteBet(bet.Id);
            throw new Exception(e.Message);
        }
        catch(Exception e)
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
            //Buscar user por id
            AppUser user = new AppUser("teste", "teste", "teste", "teste");

            BetMultiple bet = await BetRepository.CreateBetMultiple(amount, start, userId, oddMultiple, selections);
            await TransactionRepository.WithdrawBalance(user, amount);

            return bet;
        }
        catch(UserBalanceTooLowException e)
        {
            //await BetRepository.DeleteBet(bet.Id);
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
    public async Task<Selection> CreateSelection(int betTypeId, int oddId, double odd)
    {
        // pedir GameOddApi com betTypeId + oddId
        // receber odd atual do servidor
        // comparar com odd do cliente e aplicar um threshold max
        //      caso threshold exceda, enviar erro
        //      caso contrario, criar selection

        Odd serverOdd;
        // get odd by id 

        try
        {
            //return await SelectionRepository.CreateSelection(serverOdd, odd, betTypeId, bettype.Game.Id);
            throw new NotImplementedException();
        }
        catch(Exception e)
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
