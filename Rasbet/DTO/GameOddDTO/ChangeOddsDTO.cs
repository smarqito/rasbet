using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.GameOddDTO;

public class ChangeOddsDTO
{
    public string SpecialistId { get; set; }
    public int BetTypeId { get; set; }
    public Dictionary<int, double> NewOdds { get; set; }
}
