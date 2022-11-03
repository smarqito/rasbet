namespace DTO;
public class BetsOddsWonDTO
{
    public int BetTypeId { get; set; }
    public ICollection<int> WinnerOddIds { get; set; }
}
