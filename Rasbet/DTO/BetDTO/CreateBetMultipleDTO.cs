using Domain;

namespace DTO.BetDTO;
public class CreateBetMultipleDTO : CreateBetDTO
{
    public ICollection<int> SelectionIds { get; set; }
}
