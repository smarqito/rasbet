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
    public Task<Game> CreateCollectiveGame(Sport sport, DateTime date, string HomeTeam, string AwayTeam, ICollection<BetType> bets);
    public Task<Game> CreateIndividuallGame(Sport sport, DateTime date, ICollection<string> Players, ICollection<BetType> bets);
}
