using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.GetGamesRespDTO;

public class MarketDTO
{
    public string Key { get; set; }
    public ICollection<OutcomesDTO> Outcomes { get; set; }
}
