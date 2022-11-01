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
}
