using Domain.ResultDomain;

namespace DTO;

public class SelectionDTO
{
    public int Id { get; set; }
    public int OddId { get; set; }
    public double Odd { get; set; }
    public BetType Result { get; set; }
}
