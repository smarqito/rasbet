using Domain;

namespace DTO.BetDTO;
public class CreateBetMultipleDTO : CreateBetDTO
{
    public double OddMultiple { get; set; }
    public ICollection<Selection> Selections { get; set; }
}
