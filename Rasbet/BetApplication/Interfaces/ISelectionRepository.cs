using Domain;

namespace BetApplication.Interfaces;

public interface ISelectionRepository
{
    Task<Selection> createSelection(); 
}
