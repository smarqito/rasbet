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

    public CollectiveGame() : base()
    {
    }

    public CollectiveGame(string homeTeam, string awayTeam, string idSync, DateTime startTime, Sport sport) : base(idSync, startTime, sport)
    {
        HomeTeam = homeTeam;
        AwayTeam = awayTeam;
    }

    
}
