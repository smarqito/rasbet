using Domain;
using System.Collections.ObjectModel;

namespace BetApplication.Interfaces;

public interface IBetRepository
{
    Task<Bet> GetBetById(int betId);
    Task<BetSimple> CreateBetSimple(double amount, DateTime start, int user, Selection selection);
    Task<BetMultiple> CreateBetMultiple(double amount, DateTime start, int user, double oddMultiple, ICollection<Selection> selections);
    Task<bool> DeleteBet(int betId);
    Task<ICollection<Bet>> GetUserBetsByState(int user, BetState state);
    Task<bool> UpdateBets(ICollection<int> bets, BetState state);
}