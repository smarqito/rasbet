using BetApplication.Interfaces;
using BetPersistence;
using Domain;
using System.Collections.ObjectModel;

namespace BetApplication.Repositories;

public class BetRepository : IBetRepository
{
    private readonly BetContext _context;

    public BetRepository(BetContext context)
    {
        this._context = context;
    }

    public async Task<BetSimple> CreateBetSimple(
        double amount, DateTime start, AppUser user, Selection selection)
    {
        try
        {
            BetSimple b = new BetSimple(selection, amount, start, user);
            await _context.Bets.AddAsync(b);    
            await _context.SaveChangesAsync();
            return b;
        } 
        catch(Exception)
        {
            throw new Exception("Aconteceu um erro interno!");
        }
    }

    public async Task<BetMultiple> CreateBetMultiple(double amount, DateTime start, AppUser user, double oddMultiple,ICollection<Selection> selections)
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
                BetMultiple b = new BetMultiple(amount, start, user, oddMultiple, selections);
                await _context.Bets.AddAsync(b);
                await _context.SaveChangesAsync();
                return b;
            }
            catch (Exception) 
            {
                throw new Exception("Aconteceu um erro interno!");

            }
        }

        throw new Exception("Apostas efetuadas no mesmo jogo!");
    }

    public Task<ICollection<Bet>> GetUserBetsByState(AppUser user, BetState state)
    {
        ICollection<Bet> bets = _context.Bets.Where(b => b.User.Equals(user) && b.State == state).ToList();
        return (Task<ICollection<Bet>>) bets;
    }

    public async Task<bool> updateBets(Collection<int> bets, BetState state)
    {
        if(bets != null)
        {
            foreach (var id in bets)
            {
                var bet = await _context.Bets.FindAsync(id);

                if (bet != null)
                    bet.State = state;

                else throw new Exception("A aposta não foi localizada!");
            }

            await _context.SaveChangesAsync();

            return true;

        }
        return false;
    }
}
