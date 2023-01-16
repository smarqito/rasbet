namespace DTO.GameOddDTO;
public class ChangeGameDTO
{
    public ICollection<string> UsersToNotify { get; set; }
    public string? GameState {get; set;}
    public string HomeTeam {get; set;}
    public string AwayTeam {get; set;}
    public DateTime StartTime {get; set;}
    public ICollection<OddDTO>? NewOdds { get; set; }

    public ChangeGameDTO()
    {
    }



    public ChangeGameDTO(ICollection<string> UsersToNotify,
                         string GameState,
                         string HomeTeam,
                         string AwayTeam, 
                         DateTime StartTime,
                         ICollection<OddDTO> NewOdds)
    {
        this.UsersToNotify = UsersToNotify;
        this.GameState = GameState;
        this.HomeTeam = HomeTeam;
        this.AwayTeam = AwayTeam;
        this.StartTime = StartTime;
        this.NewOdds = NewOdds;
    }

    public ChangeGameDTO(ICollection<string> UsersToNotify,
                         string HomeTeam,
                         string AwayTeam, 
                         DateTime StartTime,
                         ICollection<OddDTO> NewOdds)
    {
        this.UsersToNotify = UsersToNotify;
        this.HomeTeam = HomeTeam;
        this.AwayTeam = AwayTeam;
        this.StartTime = StartTime;
        this.NewOdds = NewOdds;
    }

    public ChangeGameDTO(ICollection<string> UsersToNotify,
                         string GameState,
                         string HomeTeam,
                         string AwayTeam, 
                         DateTime StartTime)
    {
        this.UsersToNotify = UsersToNotify;
        this.GameState = GameState;
        this.HomeTeam = HomeTeam;
        this.AwayTeam = AwayTeam;
        this.StartTime = StartTime;
    }
}