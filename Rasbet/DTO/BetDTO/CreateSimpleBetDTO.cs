using Domain;

namespace DTO.BetDTO;
public abstract class CreateSimpleBetDTO 
{
    public double Amount { get; set; }
    public DateTime Start { get; set; }
    public int UserId { get; set; }
    public CreateSelectionDTO selectionDTO { get; set; }
}
