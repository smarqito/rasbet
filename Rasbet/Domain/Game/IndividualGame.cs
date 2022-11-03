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

    public IndividualGame() : base()
    {
    }

    public IndividualGame(ICollection<string> players, string idSync, DateTime startTime, Sport sport, ICollection<BetType> bets) : base(idSync, startTime, sport, bets)
    {
        Players = players;
    }
}
