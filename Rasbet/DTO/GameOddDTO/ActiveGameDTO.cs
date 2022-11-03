using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.GameOddDTO;

public class ActiveGameDTO
{
    public DateTime StartTime { get; set; }
    public string SportName { get; set; }
    public ICollection<BetTypeDTO> Bets { get; set; }
}
