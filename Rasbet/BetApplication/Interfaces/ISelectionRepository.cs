using Domain;
using Domain.ResultDomain;

namespace BetApplication.Interfaces;

public interface ISelectionRepository
{
    Task<Selection> GetSelectionById(int id);
    Task<Selection> CreateSelection(BetType bettype, Odd chosenOdd); 
    Task<ICollection<Selection>> GetSelectionByGame(int game);
    Task<ICollection<Selection>> GetSelectionByType(int bettype);
}
