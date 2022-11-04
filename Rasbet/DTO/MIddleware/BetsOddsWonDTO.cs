namespace DTO;
public class BetsOddsWonDTO
{
    public int BetTypeId { get; set; }
    public ICollection<int> WinnerOddIds { get; set; }

    public BetsOddsWonDTO()
    {
    }

    public BetsOddsWonDTO(int betTypeId, ICollection<int> winnerOddIds)
    {
        BetTypeId = betTypeId;
        WinnerOddIds = winnerOddIds;
    }
}
