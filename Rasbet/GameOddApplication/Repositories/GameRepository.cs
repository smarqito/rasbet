using Domain;
using Domain.ResultDomain;
using GameOddApplication.Interfaces;
using GameOddPersistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOddApplication.Repositories;

public class GameRepository : IGameRepository
{
    private readonly GameOddContext gameOddContext;

    public GameRepository(GameOddContext gameOddContext)
    {
        this.gameOddContext = gameOddContext;
    }

    public Task<Game> CreateCollectiveGame(Sport sport, DateTime date, string HomeTeam, string AwayTeam, ICollection<BetType> bets)
    {
        Game g = new CollectiveGame(HomeTeam, AwayTeam, date, sport, bets);
        throw new NotImplementedException();

    }

    public Task<Game> CreateIndividuallGame(Sport sport, DateTime date, ICollection<string> Players, ICollection<BetType> bets)
    {
        Game g = new IndividualGame(Players, date, sport, bets);
        throw new NotImplementedException();
    }
}
