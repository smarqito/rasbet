using Domain;

namespace BetFacade;

public interface IBetFacade
{
    Task<BetSimple> CreateBetSimple(double amount, DateTime start, int userId, int selectionId);
    Task<BetMultiple> CreateBetMultiple(double amount, DateTime start, int userId, double oddMultiple, ICollection<int> selectionIds);
    Task<ICollection<Bet>> GetUserBetsByState(int user, BetState state);
    Task<bool> UpdateBets(ICollection<int> bets, BetState state);
    Task<Selection> CreateSelection(int betTypeId, int oddId);
    Task<ICollection<Selection>> GetSelectionByGame(int game);
    Task<ICollection<Selection>> GetSelectionByType(int bettype);
}
