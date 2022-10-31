using Domain.ResultDomain;
using Domain.UserDomain;

namespace Domain;

public class Game
{
    public int Id { get; set; }
    public DateTime StartTime { get; set; }
    public virtual Sport Sport { get; set; }
    public GameState State { get; set; }
    public virtual ICollection<BetType> Results { get; set; } = new List<BetType>();
    public virtual Specialist Specialist { get; set; }
}
