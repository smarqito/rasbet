namespace DTO.BetDTO;

public class StatisticsDTO
{
    public int BetCount { get; set; }
    public IDictionary<int, double> Statistics { get; set; }

    public StatisticsDTO(int betCount, IDictionary<int, double> statistics)
    {
        BetCount = betCount;
        Statistics = statistics;
    }
}