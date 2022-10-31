using Domain.ResultDomain;
using Domain.UserDomain;

namespace Domain;

public class Game
{
    public int Id { get; set; }
    public DateTime StartTime { get; set; }
    public virtual Sport Sport { get; set; }
    public GameState State { get; set; }
    public virtual ICollection<BetType> Bets { get; set; } = new List<BetType>();
    public virtual Specialist Specialist { get; set; }

    public Game(DateTime startTime, Sport sport, ICollection<BetType> bets)
    {
        StartTime = startTime;
        Sport = sport;
        Bets = bets;
        State = GameState.Open;
    }
}
