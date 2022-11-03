using Domain;
using DTO;

namespace BetFacade;

public interface IBetFacade
{
    Task<BetSimple> CreateBetSimple(double amount, DateTime start, int userId, int selectionId);
    Task<BetMultiple> CreateBetMultiple(double amount, DateTime start, int userId, double oddMultiple, ICollection<int> selectionIds);
    Task<ICollection<Bet>> GetUserBetsByState(int user, BetState state);
    Task<ICollection<Bet>> GetUserBetsByStart(int user, DateTime start);
    Task<ICollection<Bet>> GetUserBetsByAmount(int user, double amount);
    Task<ICollection<Bet>> GetUserBetsByEnd(int user, DateTime end);
    Task<ICollection<Bet>> GetUserBetsByWonValue(int user, double wonValue);
    Task<bool> UpdateBets(ICollection<BetsOddsWonDTO> finsihedGames);
    Task<Selection> CreateSelection(int betTypeId, int oddId, double odd);
    Task<ICollection<Selection>> GetSelectionByGame(int game);
    Task<ICollection<Selection>> GetSelectionByType(int bettype);
}
