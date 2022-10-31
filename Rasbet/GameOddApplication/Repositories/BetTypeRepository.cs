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

public class BetTypeRepository : IBetTypeRepository
{
    private readonly GameOddContext gameOddContext;

    public BetTypeRepository(GameOddContext gameOddContext)
    {
        this.gameOddContext = gameOddContext;
    }

    public Task<BetType> CreateH2h(double oddHomeTeam, double oddDraw, double oddAwayTeam, Game g)
    {
        BetType betType = new H2h(oddHomeTeam, oddDraw, oddAwayTeam, g);
        throw new NotImplementedException();
    }

    public Task<BetType> CreateIndividualResult(Dictionary<string, double> results, Game g)
    {
        BetType betType = new IndividualResult(results, g);
        throw new NotImplementedException();
    }
}
