using BetApplication.Errors;
using BetApplication.Interfaces;
using BetPersistence;
using Domain;

namespace BetApplication.Repositories;

public class BetRepository : IBetRepository
{
    private readonly BetContext _context;

    public BetRepository(BetContext context)
    {
        _context = context;
    }

    public async Task<Bet> GetBetById(int betId)
    {
        try
        {
            var bet = await _context.Bets.FindAsync(betId);
            if(bet != null)
                return bet;

            throw new BetNotFoundException("Não foi possível encontrar a aposta!");
        }
        catch (Exception)
        {
            throw new Exception("Ocorreu um erro interno!");
        }
    }

    public async Task<BetSimple> CreateBetSimple(
        double amount, DateTime start, int user, Selection selection)
    {
        try
        {
            if(amount > 1.20)
            {
                BetSimple b = new BetSimple(selection, amount, start, user);
                await _context.Bets.AddAsync(b);    
                await _context.SaveChangesAsync();
                return b;
            }
            else
            {
                throw new BetTooLowException("A aposta tem um valor muito baixo!");
            }
        } 
        catch(Exception)
        {
            throw new Exception("Aconteceu um erro interno!");
        }
    }

    public async Task<BetMultiple> CreateBetMultiple(double amount, DateTime start, int user, double oddMultiple, ICollection<Selection> selections)
    {
        bool duplicateBets = false;
        foreach (var selection in selections)
        {
            Game g = selection.Result.Game;

            foreach(var selection2 in selections)
            {
                if(selection2 != selection)
                {
                    Game g2 = selection2.Result.Game;

                    if(g.Id == g2.Id)
                        duplicateBets = true;
                }
            }
        }

        if(!duplicateBets)
        {
            try
            {
                if(amount > 1.20)
                {
                    BetMultiple b = new BetMultiple(amount, start, user, oddMultiple, selections);
                    await _context.Bets.AddAsync(b);
                    await _context.SaveChangesAsync();
                    return b;

                }
                else
                {
                    throw new BetTooLowException("A aposta tem um valor demasiado baixo!");
                }

            }
            catch (Exception) 
            {
                throw new Exception("Aconteceu um erro interno!");

            }
        }

        throw new BetsInTheSameGameException("Apostas efetuadas no mesmo jogo!");
    }

    public Task<ICollection<Bet>> GetUserBetsByState(int user, BetState state)
    {
        ICollection<Bet> bets = _context.Bets.Where(b => b.UserId == user && b.State == state).ToList();
        return (Task<ICollection<Bet>>) bets;
    }

    public async Task<bool> UpdateBets(ICollection<int> bets, BetState state)
    {
        if(bets != null)
        {
            foreach (var id in bets)
            {
                var bet = await _context.Bets.FindAsync(id);

                if (bet != null)
                    bet.State = state;

                else throw new BetNotFoundException("A aposta não foi localizada!");
            }

            await _context.SaveChangesAsync();

            return true;

        }
        return false;
    }

    public async Task<bool> DeleteBet(int betId)
    {
        try
        {
            var bet = await _context.Bets.FindAsync(betId);

            if(bet != null)
            {
                _context.Bets.Remove(bet);
                return true;
            }

            throw new BetNotFoundException("A aposta não foi localizada!");
        }
        catch (Exception)
        {
            throw new Exception("Ocorreu um erro interno!");
        }
    }
}
