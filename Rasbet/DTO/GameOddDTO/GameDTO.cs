namespace DTO.GameOddDTO
{
    public class GameDTO
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public string SportName { get; set; }
        public BetTypeDTO MainBet { get; set; }

        public GameDTO()
        {
        }

        public GameDTO(int id, DateTime startTime, string sportName, BetTypeDTO mainBet)
        {
            Id = id;
            StartTime = startTime;
            SportName = sportName;
            MainBet = mainBet;
        }

        public GameDTO(GameDTO g)
        {
            Id = g.Id;
            StartTime = g.StartTime;
            SportName = g.SportName;
            MainBet = g.MainBet;
        }
    }
}
