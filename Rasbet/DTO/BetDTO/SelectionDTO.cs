using Domain.ResultDomain;

namespace DTO.BetDTO;

public class SelectionDTO
{
    public int OddId { get; set; }
    public double Odd { get; set; }
    public BetType Result { get; set; }
}
