using Domain.UserDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ResultDomain;

public abstract class BetType
{
    public int Id { get; set; }
    public int NumberOfBets { get; set; }
    public DateTime LastUpdate { get; set; }
    public virtual Game Game { get; set; }
    public BetTypeState State { get; set; }
    public virtual Specialist Specialist { get; set; }

    public BetType(Game game)
    {
        NumberOfBets = 0;
        Game = game;
        State = BetTypeState.UNFINISHED;
    }

    public abstract ICollection<Odd> GetWinningOdd();
    public abstract void SetWinningOdd(string result);
}
