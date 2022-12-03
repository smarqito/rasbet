namespace Domain.UserDomain;

public class AppUserBetHistory
{
    public string UserId { get; set; }
    public int BetId { get; set; }
    public AppUserBetHistory()
    {
    }
    public AppUserBetHistory(string userId, int betId)
    {
        UserId = userId;
        BetId = betId;
    }
}
