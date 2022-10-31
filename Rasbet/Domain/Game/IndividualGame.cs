using Domain.ResultDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain;

public class IndividualGame : Game
{
    public ICollection<string> Players;

    public IndividualGame(ICollection<string> players, DateTime startTime, Sport sport, ICollection<BetType> bets) : base(startTime, sport, bets)
    {
        Players = players;
    }
}
