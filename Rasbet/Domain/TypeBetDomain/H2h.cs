using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ResultDomain;

public class H2h : BetType
{

    public Odd OddHomeTeam { get; set; }
    public Odd OddDraw { get; set; }
    public Odd OddAwayTeam { get; set; }
    public H2h(Odd oddHomeTeam, Odd oddDray, Odd oddAwayTeam,Game game) : base(game)
    {
        OddHomeTeam = oddHomeTeam;
        OddDraw = oddDray;
        OddAwayTeam = oddAwayTeam;
    }
}
