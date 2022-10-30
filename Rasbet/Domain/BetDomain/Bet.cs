namespace Domain;

public abstract class Bet
{
    private double amount { get; set; }
    private double wonValue { get; set; }

    public int Id { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }

    // utilizador que realizou a aposta
    public virtual User User { get; set; }

    public BetState State { get; set; } = BetState.Open;

    public double Amount
    {
        get { return amount; }
        set { amount = Math.Max(1.20, value); }
    }

    public double WonValue
    {
        get { return wonValue; }
        set { wonValue = Math.Max(0, value); }
    }

    public Bet(double amount, DateTime start, User user)
    {
        Amount = amount;
        User = user;
        Start = start;
    }

}
