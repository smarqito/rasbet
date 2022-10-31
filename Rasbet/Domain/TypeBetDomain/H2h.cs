using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ResultDomain;

public class H2h : BetType
{
    public double OddHomeTeam { get; set; }
    public double oddDraw { get; set; }
    public double oddAwayTeam { get; set; }
}
