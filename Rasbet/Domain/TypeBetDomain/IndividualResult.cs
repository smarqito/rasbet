using Domain.ResultDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain;

public class IndividualResult : BetType
{
    protected IndividualResult() : base()
    {
    }

    public override ICollection<Odd> GetWinningOdd()
    {
        ICollection<Odd> result = new List<Odd>();
        foreach (Odd odd in Odds)
        {
            if (odd.Win)
            {
                result.Add(odd);
                return result;
            }
        }
        return result;
    }

    public override void SetWinningOdd(string result)
    {
        throw new NotImplementedException();
    }
}
