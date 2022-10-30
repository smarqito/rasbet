using Domain.ResultDomain;

namespace Domain;

public class Selection
{
    public double Odd { get; set; }
    public virtual Result Result { get; set; }

    public Selection(double odd, Result result)
    {
        Odd = odd;
        Result = result;
    }


}
