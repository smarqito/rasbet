using Domain.ResultDomain;

namespace Domain;

public class Selection
{
    public int id { get; set; }
    public int OddId { get; set; }
    public double Odd { get; set; }
    public virtual BetType Result { get; set; }

}
