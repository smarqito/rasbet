namespace Domain;
public class BetSimple : Bet
{
    public virtual Selection Selection { get; set; }

    protected BetSimple() : base()
    {
    }

    public BetSimple(Selection selection, double value, DateTime start, int userId) : base(value, start, userId)
    {
        Selection = selection;
    }

    public override void SetFinishBet(int betTypeId, List<int> odds)
    {
        if (Selection.BetTypeId.Equals(betTypeId))
        {

            bool won = odds.Contains(Selection.OddId);
            Selection.Win = won;
            base.FinishBet(won);

            if (won) WonValue = Selection.Odd * Amount;
        }
    }
}
