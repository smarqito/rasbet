using Domain;
using System.Collections.ObjectModel;

namespace BetApplication.Interfaces;

public interface IBetRepository
{
    Task<BetSimple> CreateBetSimple(double amount, DateTime start, AppUser user, Selection selection);
    Task<BetMultiple> CreateBetMultiple(double amount, DateTime start, AppUser user, double oddMultiple, ICollection<Selection> selections);
    Task<ICollection<Bet>> GetUserBetsByState(AppUser user, BetState state);
    Task<bool> updateBets(Collection<int> bets, BetState state);
}