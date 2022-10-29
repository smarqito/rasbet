﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.UserDTO;

public class RegisterAppUserDTO
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Nif { get; set; }
    public DateTime BirthDate { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
}