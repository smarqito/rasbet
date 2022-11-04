
namespace DTO.BetDTO;

public class CreateMultipleBetDTO 
{
    public double Amount { get; set; }
    public DateTime Start { get; set; }
    public int UserId { get; set; }

    public ICollection<CreateSelectionDTO> selections;
}
