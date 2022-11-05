using Domain;

namespace DTO.BetDTO;
public class CreateSimpleBetDTO 
{
    public double Amount { get; set; }
    public DateTime Start { get; set; }
    public string UserId { get; set; }
    public CreateSelectionDTO selectionDTO { get; set; }

}
