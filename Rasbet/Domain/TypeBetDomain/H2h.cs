using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ResultDomain;

public class H2h : BetType
{

    public double OddHomeTeam { get; set; }
    public double OddDraw { get; set; }
    public double OddAwayTeam { get; set; }
    public H2h(double oddHomeTeam, double oddDray, double oddAwayTeam,Game game) : base(game)
    {
        OddHomeTeam = oddHomeTeam;
        OddDraw = oddDray;
        OddAwayTeam = oddAwayTeam;
    }
}
