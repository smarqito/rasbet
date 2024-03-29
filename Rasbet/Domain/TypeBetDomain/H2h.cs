﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Domain.ResultDomain;

public class H2h : BetType
{
    public string AwayTeam { get; set; }

    protected H2h() : base()
    {
    }

    public H2h(string awayTeam, DateTime lastUpdate, int gameId) : base(lastUpdate, gameId)
    {
        AwayTeam = awayTeam;
    }

    public override ICollection<Odd> GetWinningOdd()
    {
        return Odds.Where(x => x.Win).ToList();
    }

    public Odd GetHomeOdd()
    {
        return Odds.Where(x => !x.Name.Equals("Draw") && !x.Name.Equals(AwayTeam)).First();
    }
    public Odd GetAwayOdd()
    {
        return Odds.Where(x => x.Name.Equals(AwayTeam)).First();
    }
    public Odd GetDrawOdd()
    {
        return Odds.Where(x => x.Name.Equals("Draw")).First();
    }
    public override ICollection<Odd> SetWinningOdd(string result)
    {
        ICollection<Odd> res = new List<Odd>();
        Regex regex = new Regex(@"\s*(\d+)\s*x\s*(\d+)\s*");
        Match match = regex.Match(result);
        if (match.Success)
        {
            if (int.Parse(match.Groups[1].Value) > int.Parse(match.Groups[2].Value))
            {
                GetHomeOdd().Win = true;
                res.Add(GetHomeOdd());
            }
            else if(int.Parse(match.Groups[1].Value) < int.Parse(match.Groups[2].Value))
            {
                GetAwayOdd().Win = true;
                res.Add(GetAwayOdd());
            }
            else
            {
                GetDrawOdd().Win = true;
                res.Add(GetDrawOdd());
            }
        }
        return res;
    }
}
