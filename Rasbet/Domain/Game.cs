using Domain.ResultDomain;

namespace Domain;

public class Game
{
    public int Id { get; set; }
    public DateTime StartTime { get; set; }
    public virtual Sport Sport { get; set; }
    public GameState State { get; set; }
    public virtual ICollection<Result> Results { get; set; } = new List<Result>();
}
