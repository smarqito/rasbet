using Domain.ResultDomain;
using Domain.UserDomain;

namespace Domain;

public class Game
{
    public int Id { get; set; }
    public string IdSync { get; set; }
    public DateTime StartTime { get; set; }
    public virtual Sport Sport { get; set; }
    public GameState State { get; set; }
    public virtual ICollection<BetType> Bets { get; set; } = new List<BetType>();
    public string SpecialistId { get; set; }

    public Game(string idSync, DateTime startTime, Sport sport, ICollection<BetType> bets)
    {
        IdSync = idSync;
        StartTime = startTime;
        Sport = sport;
        Bets = bets;
        State = GameState.Open;
    }
}
