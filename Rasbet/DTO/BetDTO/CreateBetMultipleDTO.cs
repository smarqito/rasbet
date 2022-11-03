using Domain;

namespace DTO.BetDTO;
public class CreateBetMultipleDTO : CreateBetDTO
{
    public double OddMultiple { get; set; }
    public ICollection<int> SelectionIds { get; set; }
}
