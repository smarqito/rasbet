namespace Domain;

public class BetMultiple : Bet
{
   public double OddMultiple { get; set; }
   public virtual ICollection<Selection> Selections { get; set; }

    public BetMultiple(double value, DateTime start, AppUser user, double oddMultiple, ICollection<Selection> selections) : base(value, start, user)
    {
        OddMultiple = oddMultiple;
        Selections = selections;
    }
}
