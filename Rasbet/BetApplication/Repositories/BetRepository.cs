using BetApplication.Errors;
using BetApplication.Interfaces;
using BetPersistence;
using Domain;
using DTO;
using Microsoft.EntityFrameworkCore;

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
        double amount, DateTime start, int user, Selection selection, double serverOdd)
    {
        double threshold = serverOdd / selection.Odd;
        BetSimple b;

        if (threshold >= 0.05 && threshold <= 0.95 )
        {
            throw new OddTooDiferentException("A odd atual diverge demaisado da odd escolhida!");
        }
        if(amount > 0.10 )
        {
            if(selection.Odd > 0.1)
            {
                b = new BetSimple(selection, amount, start, user);
                await _context.Bets.AddAsync(b);    
            }
            else
            {
                throw new OddTooLowException("A odd da seleção é demasiado baixa!");
            }
        }
        else
        {
            throw new BetTooLowException("O valor da aposta tem um valor muito baixo!");
        }

        try
        {
                await _context.SaveChangesAsync();
                return b;
        } 
        catch(Exception)
        {
            throw new Exception("Aconteceu um erro interno!");
        }
    }

    public async Task<BetMultiple> CreateBetMultiple(double amount, DateTime start, int user, double oddMultiple, ICollection<Selection> selections)
    {
        double oddMultipleChosen = 1.0;
        if(selections.Count > 0) { 
            bool duplicateBets = false;
            foreach (var selection in selections)
            {
                oddMultipleChosen *= selection.Odd; 
                foreach(var selection2 in selections)
                {
                    if(selection2 != selection)
                    {
                        if(selection.GameId == selection2.GameId)
                            duplicateBets = true;
                    }
                }
            }

            double threshold = oddMultiple / oddMultipleChosen;

            if (threshold >= 0.05 && threshold <= 0.95)
            {
                throw new OddTooDiferentException("A odd atual diverge demaisado da odd escolhida!");
            }

            if (!duplicateBets)
            {
                try
                {
                    if(amount > 0.10)
                    {
                        if(oddMultiple > 1.20)
                        {
                            BetMultiple b = new BetMultiple(amount, start, user, oddMultiple, selections);
                            await _context.Bets.AddAsync(b);
                            await _context.SaveChangesAsync();
                            return b;
                        }
                        else
                        {
                            throw new OddTooLowException("A odd da seleção é demasiado baixa!");
                        }
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

        throw new InvalidSelectionsException("As selecções estão inválidas!");
    }

    public async Task<ICollection<Bet>> GetUserBetsByState(int user, BetState state)
    {
        ICollection<Bet> bets = await _context.Bets.Where(b => b.UserId == user && b.State == state).ToListAsync();

        if(bets.Count == 0)
        {
            throw new UserWithoutBetsException("O utilizador não tem bets associadas com o estado!");
        }

        return bets;
    }

    public async Task<ICollection<Bet>> GetUserBetsByStart(int user, DateTime start)
    {
        ICollection<Bet> bets = await _context.Bets.Where(b => b.UserId == user && b.Start == start).ToListAsync();

        if (bets.Count == 0)
        {
            throw new UserWithoutBetsException("O utilizador não tem bets associadas com o estado!");
        }

        return bets;
    }

    //Via buscar todas as bets com aquele amount ou menos
    public async Task<ICollection<Bet>> GetUserBetsByAmount(int user, double amount)
    {
        ICollection<Bet> bets = await _context.Bets.Where(b => b.UserId == user && b.Amount <= amount).ToListAsync();

        if (bets.Count == 0)
        {
            throw new UserWithoutBetsException("O utilizador não tem bets associadas com o estado!");
        }

        return bets;
    }

    public async Task<ICollection<Bet>> GetUserBetsByEnd(int user, DateTime end)
    {
        ICollection<Bet> bets = await _context.Bets.Where(b => b.UserId == user && b.State != BetState.Open && b.End <= end).ToListAsync();

        if (bets.Count == 0)
        {
            throw new UserWithoutBetsException("O utilizador não tem bets associadas com o estado!");
        }

        return bets;
    }

    public async Task<ICollection<Bet>> GetUserBetsByWonValue(int user, double wonValue)
    {
        ICollection<Bet> bets = await _context.Bets.Where(b => b.UserId == user && b.State != BetState.Open && b.WonValue == wonValue).ToListAsync();

        if (bets.Count == 0)
        {
            throw new UserWithoutBetsException("O utilizador não tem bets associadas com o estado!");
        }

        return bets;
    }

    public async Task<bool> UpdateBets(ICollection<BetsOddsWonDTO> finishedGames)
    {
        if(finishedGames.Count != 0)
        {
            var bets = await _context.Bets.ToListAsync();
            foreach (var bet in bets)
            {
                foreach(var finished in finishedGames)
                {
                    bet.SetFinishBet(finished.BetTypeId, finished.WinnerOddIds.ToList());
                }
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
