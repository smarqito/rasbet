﻿namespace Domain;

public class BetMultiple : Bet
{
    public double OddMultiple { get; set; }
    public virtual ICollection<Selection> Selections { get; set; }
    public int OddsFinished { get; set; } = 0;
    public int OddsWon { get; set; } = 0;

    protected BetMultiple() : base()
    {
    }

    public BetMultiple(double value, string userId, double oddMultiple, ICollection<Selection> selections) : base(value, userId)
    {
        OddMultiple = oddMultiple;
        Selections = selections;
    }

    public override void SetFinishBet(int betTypeId, List<int> odds)
    {
        var s = Selections.Where(x => x.BetTypeId == betTypeId);
        OddsFinished += s.Count();
        var t = s.Where(x => odds.Contains(x.OddId));
        OddsWon += t.Count();
        foreach (var item in t)
        {
            item.Win = true;
        }
        if (OddsFinished == Selections.Count)
        {
            base.FinishBet(OddsFinished == OddsWon);

            if(OddsFinished == OddsWon)
            {
                WonValue = OddMultiple * Amount;
            }

        }
    }
}
