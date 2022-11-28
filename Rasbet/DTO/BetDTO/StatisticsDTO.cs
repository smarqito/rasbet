using Domain;

namespace DTO.BetDTO;

public class StatisticsDTO
{
    public int BetCount;
    public Dictionary<int, float> Statistics;

    public StatisticsDTO(int betCount, Dictionary<int, float> statistics)
    {
        BetCount = betCount;
        Statistics = statistics;
    }
}