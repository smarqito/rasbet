namespace DTO.BetDTO;

public class StatisticsDTO
{
    public int BetCount { get; set; }
    public IDictionary<int, int> Statistics { get; set; }

    public StatisticsDTO(int betCount, IDictionary<int, int> statistics)
    {
        BetCount = betCount;
        Statistics = statistics;
    }
}