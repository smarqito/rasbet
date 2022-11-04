namespace Domain;

public abstract class Bet
{
    public int Id { get; set; }
    private double amount { get; set; }
    private double wonValue { get; set; }

    public DateTime Start { get; set; }
    public DateTime ?End { get; set; }

    // utilizador que realizou a aposta
    public string UserId { get; set; }

    public BetState State { get; set; } = BetState.Open;

    public double Amount
    {
        get { return amount; }
        set { amount = Math.Max(0.1, value); }
    }

    public double WonValue
    {
        get { return wonValue; }
        set { wonValue = Math.Max(0, value); }
    }

    protected Bet()
    {
    }

    public Bet(double amount, DateTime start, string userId)
    {
        Amount = amount;
        Start = start;
        UserId = userId;
    }

    public abstract void SetFinishBet(int betTypeId, List<int> odds);
    public void FinishBet(bool won)
    {
        State = won ? BetState.Won : BetState.Lost;
    }
}
