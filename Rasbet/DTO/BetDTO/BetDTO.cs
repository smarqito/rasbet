using Domain;

namespace DTO.BetDTO;

public class BetDTO
{
    public double Amount { get; set; }
    public double WonValue { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public string State { get; set; }
    public ICollection<SelectionDTO> Selections { get; set; }
    public double Odd { get; set; }
}