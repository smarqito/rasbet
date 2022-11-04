using Domain.ResultDomain;

namespace Domain;

public class Selection
{
    public int Id { get; set; }
    public int OddId { get; set; }
    public double Odd { get; set; }
    public int BetTypeId { get; set; }
    public int GameId { get; set; }
    public bool Win { get; set; } = false;
    protected Selection ()
    {
    }

    public Selection(int oddId, double odd, int betTypeId, int gameId)
    {
        OddId = oddId;
        Odd = odd;
        BetTypeId = betTypeId;
        GameId = gameId;
    }
}
