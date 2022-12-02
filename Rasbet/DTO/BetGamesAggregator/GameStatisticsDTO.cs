using DTO.BetDTO;
using DTO.GameOddDTO;
using DTO.GetGamesRespDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.BetGamesAggregator
{
    public class GameStatisticsDTO : CollectiveGameDTO
    {
        public StatisticsDTO Statistics { get; set; }

        public GameStatisticsDTO(int id, DateTime startTime, string sportName, BetTypeDTO mainBet, string homeTeam, string awayTeam) : base(id, startTime, sportName, mainBet, homeTeam, awayTeam)
        {
        }

        public GameStatisticsDTO(CollectiveGameDTO game) : base(game)
        {
        }
    }
}
