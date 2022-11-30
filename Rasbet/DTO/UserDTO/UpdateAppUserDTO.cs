using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.UserDTO;

public class UpdateAppUserDTO
{
    public string? Name { get; set; }
    public string Email { get; set; }   
    public string? Language { get; set; }
    public string? Coin { get; set; }
    public bool Notifications { get; set; }

}