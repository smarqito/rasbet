﻿using Domain;
using DTO.BetDTO;

namespace BetApplication.Interfaces;

public interface ISelectionRepository
{
    Task<Selection> GetSelectionById(int id);
    Task<Selection> CreateSelection(double serverOdd, double chosenOdd, int betTypeId, int oddID, int gameId); 
    Task<ICollection<Selection>> GetSelectionByGame(int game);
    Task<ICollection<Selection>> GetSelectionByType(int bettype);
    Task RemoveSelections(ICollection<Selection> selections);
    Task<StatisticsDTO> GetStatisticsByGame(ICollection<int> oddIds);
}
