using Domain;
using DTO;
using DTO.BetDTO;

namespace BetFacade;

public interface IBetFacade
{
    Task<BetSimple> CreateBetSimple(double amount, DateTime start, string userId, CreateSelectionDTO selectionDTO);
    Task<BetMultiple> CreateBetMultiple(double amount, DateTime start, string userId, ICollection<CreateSelectionDTO> selectionsDTOS);
    Task<ICollection<Bet>> GetUserBetsByState(string user, BetState state);
    Task<ICollection<Bet>> GetUserBetsByStart(string user, DateTime start);
    Task<ICollection<Bet>> GetUserBetsByAmount(string user, double amount);
    Task<ICollection<Bet>> GetUserBetsByEnd(string user, DateTime end);
    Task<ICollection<Bet>> GetUserBetsByWonValue(string user, double wonValue);
    Task<bool> UpdateBets(ICollection<BetsOddsWonDTO> finsihedGames);
    Task<Selection> CreateSelection(int betTypeId, int oddId, double odd, int gameId);
    Task<ICollection<Selection>> GetSelectionByGame(int game);
    Task<ICollection<Selection>> GetSelectionByType(int bettype);
}
