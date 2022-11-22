using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.GameOddDTO;

public class BetInfoDTO
{
    public int Id { get; set; }
    public ICollection<int> OddsId { get; set; }
}
