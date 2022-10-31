using Domain.ResultDomain;

namespace Domain;

public class Selection
{
    public double Odd { get; set; }
    public virtual BetType Result { get; set; }

    public Selection(double odd, BetType result)
    {
        Odd = odd;
        Result = result;
    }


}
