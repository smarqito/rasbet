using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.GameOddDTO
{
    public class GameDTO
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public string SportName { get; set; }
        public BetTypeDTO MainBet { get; set; }

        public GameDTO(GameDTO g)
        {
            Id = g.Id;
            StartTime = g.StartTime;
            SportName = g.SportName;
            MainBet = g.MainBet;
        }
    }
}
