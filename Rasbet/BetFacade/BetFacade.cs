using BetApplication.Errors;
using BetApplication.Repositories;
using Domain;
using Domain.ResultDomain;
using GameOddApplication.Repositories;
using UserApplication.Repositories;

namespace BetFacade;

public class BetFacade : IBetFacade
{
    public BetRepository BetRepository;
    public SelectionRepository SelectionRepository;
    public TransactionRepository TransactionRepository;
    public UserRepository UserRepository;
    public BetTypeRepository BetTypeRepository;

    public BetFacade(BetRepository betRepository, 
                     SelectionRepository selectionRepository, 
                     TransactionRepository transactionRepository, 
                     UserRepository userRepository,
                     BetTypeRepository betTypeRepository)
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

            BetSimple bet = await BetRepository.CreateBetSimple(amount, start, userId, selection);
            //await TransactionRepository.WithdrawBalance(user, amount);

            return bet;
        }
        catch(UserBalanceTooLowException e) {
            //await BetRepository.DeleteBet(bet.Id);
            throw new Exception(e.Message);

        }
        catch(BetTooLowException e)
        {
            throw new Exception(e.Message);
        }
        catch(SelectionNotExistException e)
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
            catch(SelectionNotExistException e)
            {
                throw new Exception(e.Message);
            }
        }

        try
        {
            BetMultiple bet = await BetRepository.CreateBetMultiple(amount, start, userId, oddMultiple, selections);
            //await TransactionRepository.WithdrawBalance(user, amount);

            return bet;
        }
        catch (UserBalanceTooLowException e)
        {
            //await BetRepository.DeleteBet(bet.Id);
            throw new Exception(e.Message);

        }
        catch (BetTooLowException e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<ICollection<Bet>> GetUserBetsByState(int user, BetState state)
    {
        return await BetRepository.GetUserBetsByState(user, state);
    }

    public async Task<bool> UpdateBets(ICollection<int> bets, BetState state)
    {
        return await BetRepository.UpdateBets(bets, state);
    }

    // Métodos para o SelectionRepository
    public async Task<Selection> CreateSelection(int betTypeId, int oddId)
    {
        Odd oddChosen;
        // get odd by id 
        BetType bettype;

        try
        {
            //return await SelectionRepository.CreateSelection(bettype, oddChosen);
            throw new NotImplementedException();
        }
        catch(Exception e)
        {
            throw new Exception(e.Message);
        }


    }

    public async Task<ICollection<Selection>> GetSelectionByGame(int game)
    {
        return await SelectionRepository.GetSelectionByGame(game);
    }

    public async Task<ICollection<Selection>> GetSelectionByType(int bettype)
    {
        return await SelectionRepository.GetSelectionByType(bettype);
    }
}
