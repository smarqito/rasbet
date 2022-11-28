using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.GameOddDTO
{
    public class CollectiveGameDTO : GameDTO
    {
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }

        public CollectiveGameDTO(GameDTO g) : base(g) { }
    }
}
