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

    public BetDTO (double amount, double wonValue, DateTime start, DateTime end, string state, ICollection<SelectionDTO> selections)
    {
        Amount = amount;
        WonValue = wonValue;
        Start = start;
        End = end;
        State = state;
        Selections = selections;
    }
}