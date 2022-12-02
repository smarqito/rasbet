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
    public class GameStatisticsDTO : GameOddDTO.GameDTO
    {
        public StatisticsDTO Statistics { get; set; }

        public GameStatisticsDTO(int id, DateTime startTime, string sportName, BetTypeDTO mainBet) : base(id, startTime, sportName, mainBet)
        {
        }

        public GameStatisticsDTO(GameOddDTO.GameDTO game) : base(game)
        {
        }
    }
}
