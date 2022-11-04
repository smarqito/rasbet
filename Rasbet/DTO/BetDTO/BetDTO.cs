using Domain;

namespace DTO.BetDTO;

public class BetDTO
{
    public string Id { get; set; }
    public double Amount { get; set; }
    public double WonValue { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public BetState State { get; set; }
    public string User { get; set; }
    
}