
namespace DTO.BetDTO;

public class CreateMultipleBetDTO 
{
    public double Amount { get; set; }
    public string UserId { get; set; }
    public List<CreateSelectionDTO> Selections { get; set; }
}
