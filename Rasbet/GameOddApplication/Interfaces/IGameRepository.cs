using Domain;
using Domain.ResultDomain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOddApplication.Interfaces;

public interface IGameRepository
{
    public Task<Game> GetGame(int id);
    public Task<Game> GetGame(string idSync);
    public Task<CollectiveGame> GetCollectiveGame(string idSync);
    public Task<bool> HasGame(string idSync);
    public Task<Game> CreateCollectiveGame(Sport sport, string idSync, DateTime date, string HomeTeam, string AwayTeam);
    public Task<Unit> CreateIndividuallGame(Sport sport, string idSync, DateTime date, ICollection<string> Players, ICollection<BetType> bets);
    public Task<Unit> ChangeGameState(Game game, string specialistId, GameState state);
    public Task<Unit> ChangeGameState(Game g, GameState state);
    public Task<ICollection<Game>> GetActiveAndSuspendedGames();
}
