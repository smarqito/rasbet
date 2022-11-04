using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.GetGamesRespDTO;

public class GameDTO
{
    public string Id { get; set; }
    public string HomeTeam { get; set; }
    public string AwayTeam { get; set; }
    public DateTime CommenceTime { get; set; }
    public bool Completed { get; set; }
    public string Scores { get; set; }
    public ICollection<BookmakerDTO> Bookmakers { get; set; }
}
