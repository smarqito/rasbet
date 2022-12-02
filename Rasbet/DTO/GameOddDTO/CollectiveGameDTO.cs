using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.GameOddDTO
{
    public class CollectiveGameDTO
    {
        public int Id { get; set; }
        public string State { get; set; }
        public DateTime StartTime { get; set; }
        public string SportName { get; set; }
        public BetTypeDTO MainBet { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }

        public CollectiveGameDTO()
        {
        }

        public CollectiveGameDTO(int id, string state, DateTime startTime, string sportName, BetTypeDTO mainBet, string homeTeam, string awayTeam)
        {
            Id = id;
            State = state;
            StartTime = startTime;
            SportName = sportName;
            MainBet = mainBet;
            HomeTeam = homeTeam;
            AwayTeam = awayTeam;
        }

        public CollectiveGameDTO(CollectiveGameDTO game)
        {
            this.Id = game.Id;
            this.State = game.State;
            this.StartTime = game.StartTime;
            this.SportName= game.SportName;
            this.MainBet= game.MainBet;
            this.HomeTeam= game.HomeTeam;
            this.AwayTeam= game.AwayTeam;
        }
    }
}
