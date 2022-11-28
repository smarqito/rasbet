using DTO.GameOddDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class SelectionGameDTO
    {
        public double Odd { get; set; }
        public GameDTO Game { get; set; }
        public bool Win { get; set; }

        public SelectionGameDTO(double odd, GameDTO game, bool win)
        {
            Odd = odd;
            Game = game;
            Win = win;
        }
    }
}
