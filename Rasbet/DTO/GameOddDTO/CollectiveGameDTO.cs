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
        public DateTime StartTime { get; set; }
        public string SportName { get; set; }
        public BetTypeDTO MainBet { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }

    }
}
