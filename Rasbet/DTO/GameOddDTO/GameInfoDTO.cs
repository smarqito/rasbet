using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.GameOddDTO;

public class GameInfoDTO
{
    public DateTime StartTime { get; set; }
    public string State { get; set; }
    public ICollection<BetInfoDTO> Bets { get; set; }

}
