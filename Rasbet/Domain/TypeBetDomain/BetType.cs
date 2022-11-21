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
    public DateTime LastUpdate { get; set; }
    public BetTypeState State { get; set; }
    public string SpecialistId { get; set; }
    public int GameId { get; set; }
    public virtual ICollection<Odd> Odds { get; set; } = new List<Odd>();

    protected BetType()
    {
    }

    public BetType(DateTime lastUpdate, int gameId)
    {
        LastUpdate = lastUpdate;
        GameId = gameId;
        State = BetTypeState.UNFINISHED;
    }

    public abstract ICollection<Odd> GetWinningOdd();
    public abstract ICollection<Odd> SetWinningOdd(string result);
}
