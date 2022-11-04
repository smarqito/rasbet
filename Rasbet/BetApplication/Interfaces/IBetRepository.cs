using Domain;
using DTO;
using System.Collections.ObjectModel;

namespace BetApplication.Interfaces;

public interface IBetRepository
{
    Task<Bet> GetBetById(int betId);
    Task<BetSimple> CreateBetSimple(double amount, DateTime start, int user, Selection selection, double serOdd);
    Task<BetMultiple> CreateBetMultiple(double amount, DateTime start, int user, double oddMultiple, ICollection<Selection> selections);
    Task<bool> DeleteBet(int betId);
    Task<ICollection<Bet>> GetUserBetsByState(int user, BetState state);
    Task<ICollection<Bet>> GetUserBetsByStart(int user, DateTime start);
    Task<ICollection<Bet>> GetUserBetsByAmount(int user, double amount);
    Task<ICollection<Bet>> GetUserBetsByEnd(int user, DateTime end);
    Task<ICollection<Bet>> GetUserBetsByWonValue(int user, double wonValue);
    Task<ICollection<Bet>> UpdateBets(ICollection<BetsOddsWonDTO> finishedGames);
}