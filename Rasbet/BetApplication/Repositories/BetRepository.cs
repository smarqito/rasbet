﻿using BetApplication.Errors;
using BetApplication.Interfaces;
using BetPersistence;
using Domain;
using DTO;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;

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
        Bet? bet;
        try
        {
            bet = await _context.Bets.FindAsync(betId);
        }
        catch (Exception)
        {
            throw new Exception("Ocorreu um erro interno!");
        }

        if(bet != null)
            return bet;

        throw new BetNotFoundException("Não foi possível encontrar a aposta!");
    }

    public async Task<BetSimple> CreateBetSimple(double amount,
                                                 string user,
                                                 Selection selection,
                                                 double serverOdd)
    {
        double threshold = serverOdd / selection.Odd;
        BetSimple b;

        if(amount > 0.10 )
        {
            if(selection.Odd > 1.2)
            {
                b = new BetSimple(selection, amount, user);
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
        catch(Exception ex)
        {
            throw new Exception("Aconteceu um erro interno!");
        }
    }

    public async Task<BetMultiple> CreateBetMultiple(double amount,
                                                     string user,
                                                     double oddMultiple,
                                                     ICollection<Selection> selections)
    {
        double oddMultipleChosen = 1.0;
        if(selections.Count > 1) { 
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
                BetMultiple b;
                if (amount > 0.10)
                {
                    if(oddMultiple > 1.20)
                    {
                        b = new BetMultiple(amount, user, oddMultiple, selections);
                        await _context.Bets.AddAsync(b);
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

                try
                {
                    await _context.SaveChangesAsync();
                    return b;
                }
                catch (Exception) 
                {
                    throw new Exception("Aconteceu um erro interno!");

                }
            }
            else
                throw new BetsInTheSameGameException("Apostas efetuadas no mesmo jogo!");
        }
        else
            throw new InvalidSelectionsException("Não existem pelo menos 2 seleções!");
    }

    public async Task<ICollection<Bet>> GetUserBetsByState(string user, BetState state, DateTime start, DateTime end)
    {
        ICollection<Bet> bets = new List<Bet>();
        if (state == BetState.Open)   
           bets = await _context.Bets.Where(b => b.UserId == user 
                                            && b.State == state
                                            && b.Start > start).ToListAsync();
        else
           bets = await _context.Bets.Where(b => b.UserId == user 
                                            && b.State == state
                                            && b.Start > start && b.End < end).ToListAsync();

        //if (bets.Count == 0)
        //{
        //    throw new UserWithoutBetsException("O utilizador não tem bets associadas com o estado!");
        //}

        return bets;
    }

    public async Task<ICollection<Bet>> GetUserBetsByStates(string user, BetState state1, BetState state2, DateTime start, DateTime end)
    {
        ICollection<Bet> bets = new List<Bet>();
        if (state1 == BetState.Open || state2 == BetState.Open)
            bets = await _context.Bets.Where(b => b.UserId == user
                                             && (b.State == state1 || b.State == state2)
                                             && b.Start > start).ToListAsync();
        else
            bets = await _context.Bets.Where(b => b.UserId == user
                                             && (b.State == state1 || b.State == state2)
                                             && b.Start > start && b.End < end).ToListAsync();

        //if (bets.Count == 0)
        //{
        //    throw new UserWithoutBetsException("O utilizador não tem bets associadas com o estado!");
        //}

        return bets;
    }


    public async Task<ICollection<Bet>> GetUserBetsByStart(string user, DateTime start)
    {
        ICollection<Bet> bets = await _context.Bets.Where(b => b.UserId == user && b.Start == start).ToListAsync();

        if (bets.Count == 0)
        {
            throw new UserWithoutBetsException("O utilizador não tem bets associadas com o estado!");
        }

        return bets;
    }

    //Via buscar todas as bets com aquele amount ou menos
    public async Task<ICollection<Bet>> GetUserBetsByAmount(string user, double amount)
    {
        ICollection<Bet> bets = await _context.Bets.Where(b => b.UserId == user && b.Amount <= amount).ToListAsync();

        if (bets.Count == 0)
        {
            throw new UserWithoutBetsException("O utilizador não tem bets associadas com o estado!");
        }

        return bets;
    }

    public async Task<ICollection<Bet>> GetUserBetsByEnd(string user, DateTime end)
    {
        ICollection<Bet> bets = await _context.Bets.Where(b => b.UserId == user && b.State != BetState.Open && b.End <= end).ToListAsync();

        if (bets.Count == 0)
        {
            throw new UserWithoutBetsException("O utilizador não tem bets associadas com o estado!");
        }

        return bets;
    }

    public async Task<ICollection<Bet>> GetUserBetsByWonValue(string user, double wonValue)
    {
        ICollection<Bet> bets = await _context.Bets.Where(b => b.UserId == user && b.State != BetState.Open && b.WonValue == wonValue).ToListAsync();

        if (bets.Count == 0)
        {
            throw new UserWithoutBetsException("O utilizador não tem bets associadas com o estado!");
        }

        return bets;
    }


    public async Task<ICollection<Bet>> UpdateBets(ICollection<Selection> selections, ICollection<int> odds)
    {
        ICollection<Bet> finished_bets = new List<Bet>();

        foreach (Selection selection in selections)
        {
            Bet? bet = await _context.Bets.OfType<BetSimple>().Where(x => x.Selection.Id == selection.Id).FirstOrDefaultAsync();
            if (bet == null)
            {
                bet = await _context.Bets.OfType<BetMultiple>()
                                         .Where(x => x.Selections.Any(s => s.Id == selection.Id))
                                         .FirstOrDefaultAsync();
            }

            bet.SetFinishBet(selection.BetTypeId, odds.ToList());

            if (bet.State == BetState.Won || bet.State == BetState.Lost)
                finished_bets.Add(bet);
        }
        
        await _context.SaveChangesAsync();

        return finished_bets;
    }

    public async Task<bool> DeleteBet(int betId)
    {
        var bet = await _context.Bets.FindAsync(betId);

        if(bet != null)
        {
            _context.Bets.Remove(bet);
        }
        else
            throw new BetNotFoundException("A aposta não foi localizada!");

        try
        {
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            throw new Exception("Ocorreu um erro interno!");
        }
    }
}
