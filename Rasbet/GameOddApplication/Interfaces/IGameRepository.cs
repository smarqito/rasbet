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
    public Task<Game> GetGame(string idSync);
    public Task<bool> HasGame(string idSync);
    public Task<Unit> CreateCollectiveGame(Sport sport, string idSync, DateTime date, string HomeTeam, string AwayTeam, ICollection<BetType> bets);
    public Task<Unit> CreateIndividuallGame(Sport sport, string idSync, DateTime date, ICollection<string> Players, ICollection<BetType> bets);
    public Task<Unit> ChangeGameState(string idSync, GameState state, string specialistId);
}
