﻿using Domain;
using DTO;
using DTO.BetDTO;

namespace BetFacade;

public interface IBetFacade
{
    Task<BetSimple> CreateBetSimple(double amount, string userId, CreateSelectionDTO selectionDTO);
    Task<BetMultiple> CreateBetMultiple(double amount, string userId, ICollection<CreateSelectionDTO> selectionsDTOS);
    Task<ICollection<BetDTO>> GetUserBetsByState(string user, BetState state, DateTime start, DateTime end);
    Task<ICollection<BetDTO>> GetUserBetsByStates(string user, BetState state1, BetState state2, DateTime start, DateTime end);
    Task<bool> UpdateBets(ICollection<BetsOddsWonDTO> finsihedGames);
    Task<Selection> CreateSelection(int betTypeId, int oddId, double odd, int gameId, double server_odd);
    Task<ICollection<Selection>> GetSelectionByGame(int game);
    Task<ICollection<Selection>> GetSelectionByType(int bettype);
    Task<StatisticsDTO> GetStatisticsByGame(ICollection<int> oddIds);
}
