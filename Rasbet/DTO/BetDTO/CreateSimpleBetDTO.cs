﻿using Domain;

namespace DTO.BetDTO;
public class CreateSimpleBetDTO 
{
    public double Amount { get; set; }
    public string UserId { get; set; }
    public CreateSelectionDTO Selection { get; set; }

}
