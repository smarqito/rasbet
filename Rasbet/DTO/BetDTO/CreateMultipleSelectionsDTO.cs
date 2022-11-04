
namespace DTO.BetDTO;

public class CreateMultipleSelectionsDTO : CreateBetDTO
{
    public ICollection<CreateSelectionDTO> selections;
}
