namespace Domain;
public class BetSimple : Bet
{
    public virtual Selection Selection { get; set; }
    
    public BetSimple(Selection selection, double value, DateTime start, int userId) : base(value, start, userId)
    {
        Selection = selection;
    }
}
