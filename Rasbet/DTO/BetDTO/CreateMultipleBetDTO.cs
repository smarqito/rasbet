
namespace DTO.BetDTO;

public class CreateMultipleBetDTO 
{
    public double Amount { get; set; }
    public DateTime Start { get; set; }
    public string UserId { get; set; }

    public ICollection<CreateSelectionDTO> selections;
}
