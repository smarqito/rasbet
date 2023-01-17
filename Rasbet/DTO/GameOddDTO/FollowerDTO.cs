using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.GameOddDTO
{
    public class FollowerDTO
    {
        public string UserId { get; set; }
        public int GameId { get; set; }

        public FollowerDTO()
        {
        }

        public FollowerDTO(string userId, int gameId)
        {
            UserId = userId;
            GameId = gameId;
        }
    }
}
