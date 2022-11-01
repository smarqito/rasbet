using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Domain.ResultDomain;

public class H2h : BetType
{

    public Odd OddHomeTeam { get; set; }
    public Odd OddDraw { get; set; }
    public Odd OddAwayTeam { get; set; }
    public H2h(Odd oddHomeTeam, Odd oddDray, Odd oddAwayTeam, Game game) : base(game)
    {
        OddHomeTeam = oddHomeTeam;
        OddDraw = oddDray;
        OddAwayTeam = oddAwayTeam;
    }

    public override ICollection<Odd> GetWinningOdd()
    {
        ICollection<Odd> result = new List<Odd>();
        if (OddHomeTeam.Win)
        {
            result.Add(OddHomeTeam);
        } else if (OddDraw.Win)
        {
            result.Add(OddDraw);
        }
        else
        {
            result.Add(OddAwayTeam);
        }
        return result;
    }

    public override void SetWinningOdd(string result)
    {
        Regex regex = new Regex(@"\s*(\d+)\s*x\s*(\d+)\s*");
        Match match = regex.Match(result);
        if (match.Success)
        {
            if (int.Parse(match.Groups[1].Value) > int.Parse(match.Groups[2].Value))
            {
                OddHomeTeam.Win = true;
            }
            else if(int.Parse(match.Groups[1].Value) < int.Parse(match.Groups[2].Value))
            {
                OddAwayTeam.Win = true;
            }
            else
            {
                OddDraw.Win = true;
            }
        }
    }
}
