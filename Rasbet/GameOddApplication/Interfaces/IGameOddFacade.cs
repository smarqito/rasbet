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
    public Task<Unit> UpdateGameOdd(ICollection<GameDTO> games, string sportName);
    public Task<Unit> FinishGame(string id, string result);
    public Task<Unit> FinishGame(string id, string result, string? specialistId);
    public Task<Unit> SuspendGame(string gameId, string specialistId);
    public Task<ICollection<ActiveGameDTO>> GetActiveGames();
    public Task<double> GetOddValue(int oddId, int betTypeId);
    public Task<Unit> ChangeOdds(string specialistId, int betTypeId, Dictionary<int, double> newOdds);
}
