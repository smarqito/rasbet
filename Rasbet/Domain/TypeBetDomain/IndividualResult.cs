using Domain.ResultDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain;

public class IndividualResult : BetType
{
    public ICollection<Odd> PlayerOdds { get; set; }

    protected IndividualResult() : base()
    {
    }

    public IndividualResult(ICollection<Odd> playerOdds, DateTime lastUpdate) : base(lastUpdate)
    {
        PlayerOdds = playerOdds;
    }

    public override ICollection<Odd> GetWinningOdd()
    {
        ICollection<Odd> result = new List<Odd>();
        foreach (Odd odd in PlayerOdds)
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
