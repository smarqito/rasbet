namespace DTO.UserDTO;

public class BetDTO 
{
    public string UserId { get; set; }
    public int BetId { get; set; }
    public double Value { get; set; }
    public double Odd { get; set; }
  

    protected BetDTO()
    {

    }
    public BetDTO(string userId, int betId, double value, double odd) 
    {
        UserId = userId;
        BetId = betId;
        Value = value;
        Odd = odd;
    }
}