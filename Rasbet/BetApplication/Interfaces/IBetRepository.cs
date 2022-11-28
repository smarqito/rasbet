using Domain;
using DTO;
using System.Collections.ObjectModel;

namespace BetApplication.Interfaces;

public interface IBetRepository
{
    Task<Bet> GetBetById(int betId);
    Task<BetSimple> CreateBetSimple(double amount, string user, Selection selection, double serOdd);
    Task<BetMultiple> CreateBetMultiple(double amount, string user, double oddMultiple, ICollection<Selection> selections);
    Task<bool> DeleteBet(int betId);
    Task<ICollection<Bet>> GetUserBetsByState(string user, BetState state, DateTime start, DateTime end);
    Task<ICollection<Bet>> GetUserBetsByStart(string user, DateTime start);
    Task<ICollection<Bet>> GetUserBetsByAmount(string user, double amount);
    Task<ICollection<Bet>> GetUserBetsByEnd(string user, DateTime end);
    Task<ICollection<Bet>> GetUserBetsByWonValue(string user, double wonValue);
    Task<ICollection<Bet>> UpdateBets(ICollection<Selection> selections, ICollection<int> odds);
}