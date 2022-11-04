using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.UserDTO;

public class RegisterSpecialistDTO
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Language { get; set; }

    public RegisterSpecialistDTO (string name, string email, string password, string language)
    {
        Name = name;
        Email = email;
        Password = password;
        Language = language;
    }
}