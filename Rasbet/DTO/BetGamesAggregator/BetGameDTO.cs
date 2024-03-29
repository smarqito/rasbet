﻿using DTO.BetDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.BetDTO;

namespace DTO.BetGamesAggregator
{
    public class BetGameDTO
    {
        public double Amount { get; set; }
        public double WonValue { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string State { get; set; }
        public ICollection<SelectionGameDTO> Selections { get; set; }
        public double Odd { get; set; }

        public BetGameDTO(BetDTO.BetDTO bet)
        {
            Amount = bet.Amount;
            WonValue = bet.WonValue;
            Start = bet.Start;
            End = bet.End;
            State = bet.State;
            Odd = bet.Odd;
            Selections = new List<SelectionGameDTO>();
        }
    }
}
