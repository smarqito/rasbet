namespace DTO.UserDTO;

public class WonBetDTO
{
    public string UserId { get; set; }
    public int BetId { get; set; }
    public double Value { get; set; }
    public double Odd { get; set; }

    protected WonBetDTO()
    {

    }
    public WonBetDTO(string userId, int betId, double value, double odd)
    {
        UserId = userId;
        BetId = betId;
        Value = value;
        Odd = odd;
    }
}