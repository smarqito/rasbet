using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.GameOddDTO;

public class BetTypeDTO
{
    public int Id { get; set; }
    public ICollection<OddDTO> Odds { get; set; }
}
