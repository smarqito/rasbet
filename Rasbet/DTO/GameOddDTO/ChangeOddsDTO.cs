using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.GameOddDTO;

public class NewODD
{
    public int OddId { get; set; }
    public double OddValue { get; set; }
}
public class ChangeOddsDTO
{
    public string SpecialistId { get; set; }
    public int BetTypeId { get; set; }
    public ICollection<NewODD> NewOdds { get; set; }
}
