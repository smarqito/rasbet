using Domain.ResultDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain;

public class IndividualResult : BetType
{

    public Dictionary<string, double> PlayerOdds{ get; set; }
    public IndividualResult(Dictionary<string, double> playerOdds, Game game) : base(game)
    {
        PlayerOdds = playerOdds;
    }
}
