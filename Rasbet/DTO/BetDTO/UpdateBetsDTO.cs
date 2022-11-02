using Domain;

namespace DTO.BetDTO;

public class UpdateBetsDTO
{
    public ICollection<int> Bets { get; set; }
}
