using Domain.ResultDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain;

public class IndividualResult : BetType
{
    public ICollection<Odd> PlayerOds { get; set; }

    public IndividualResult(ICollection<Odd> playerOds, Game game) : base(game)
    {
        PlayerOds = playerOds;
    }

    public override ICollection<Odd> GetWinningOdd()
    {
        ICollection<Odd> result = new List<Odd>();
        foreach (Odd odd in PlayerOds)
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
