namespace Domain;
public class BetSimple : Bet
{
    public virtual Selection Selection { get; set; }
    
    public BetSimple(Selection selection, double value, DateTime start, AppUser user) : base(value, start, user)
    {
        Selection = selection;
    }
}
