using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.GetGamesRespDTO;

public class BookmakerDTO
{
    public string Key { get; set; }
    public DateTime LastUpdate { get; set; }
    public ICollection<MarketDTO> Markets { get; set; }
}
