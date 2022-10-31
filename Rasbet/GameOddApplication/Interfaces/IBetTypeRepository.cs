using Domain;
using Domain.ResultDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOddApplication.Interfaces;

public interface IBetTypeRepository
{
    public Task<BetType> CreateH2h(double oddHomeTeam, double oddDraw, double oddAwayTeam, Game game);

    public Task<BetType> CreateIndividualResult(Dictionary<string, double> results, Game game);
}
