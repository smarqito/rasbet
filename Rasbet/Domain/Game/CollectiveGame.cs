using Domain.ResultDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain;

public class CollectiveGame : Game
{
    public string HomeTeam { get; set; }
    public string AwayTeam { get; set; }
    public CollectiveGame(string homeTeam, string awayTeam, DateTime startTime, Sport sport, ICollection<BetType> bets) : base(startTime, sport, bets)
    {
        HomeTeam = homeTeam;
        AwayTeam = awayTeam;
    }

    
}
