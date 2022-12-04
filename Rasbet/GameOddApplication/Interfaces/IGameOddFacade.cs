using Domain;
using DTO.GameOddDTO;
using DTO.GetGamesRespDTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOddApplication.Interfaces;

public interface IGameOddFacade
{
    public Task<Unit> UpdateGameOdd(ICollection<DTO.GetGamesRespDTO.GameDTO> games, string sportName);
    public Task<Unit> FinishGame(int gameId, string result, string specialistId);
    public Task<Unit> FinishGame(string gameId, string result);
    public Task<Unit> FinishGame(string gameId, string result, string? specialistId);
    public Task<Unit> ActivateGame(int gameId, string specialistId);
    public Task<Unit> SuspendGame(int gameId, string specialistId);
    public Task<ICollection<CollectiveGameDTO>> GetActiveGames();
    public Task<double> GetOddValue(int oddId, int betTypeId);
    public Task<Unit> ChangeOdds(string specialistId, int betTypeId, ICollection<NewODD> newOdds);
    public Task<GameInfoDTO> GetGameInfo(int gameId, bool detailed);
    public Task<DTO.GameOddDTO.GameDTO> GetGame(int gameId);
    public Task<ICollection<SportDTO>> GetSports();
    public Task<ICollection<CollectiveGameDTO>> GetActiveAndSuspendedGames();
}
