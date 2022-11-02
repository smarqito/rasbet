using Domain;

namespace DTO.BetDTO;
public abstract class CreateBetDTO 
{
    public double Amount { get; set; }
    public DateTime Start { get; set; }
    public int UserId { get; set; }
    
}
