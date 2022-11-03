using BetApplication.Interfaces;
using Domain;
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
            //Verificar se a odd esta dentro dos parâmetros aceitaveis (comparar com a odd atual do bettype)
            double odd = await APIService.GetOdd(selection.BetTypeId, selection.OddId);

            //Buscar user por Id

            //Verificar se o user tem dinheiro primeiro
            BetSimple bet = await BetRepository.CreateBetSimple(amount, start, userId, selection);
            //await TransactionRepository.WithdrawBalance(user, amount);

            return bet;
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

            //Verificar se o User tem dinheiro
            BetMultiple bet = await BetRepository.CreateBetMultiple(amount, start, userId, oddMultiple, selections);
            //await TransactionRepository.WithdrawBalance(user, amount);

            return bet;
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

        double serverOdd = await APIService.GetOdd(betTypeId, oddId);
            
        try
        {
            return await SelectionRepository.CreateSelection(serverOdd, odd, betTypeId, oddId, 1);
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
