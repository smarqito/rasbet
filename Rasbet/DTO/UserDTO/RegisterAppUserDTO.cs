using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.UserDTO;

public class RegisterAppUserDTO
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string NIF { get; set; }
    public DateTime DOB { get; set; }
    public string Password { get; set; }
    public bool Notifications { get; set; }
    public string Language { get; set; }

    public RegisterAppUserDTO(string name, string email, string password, string nIF, DateTime dOB, bool notifications, string language)
    {
        Name = name;
        Email = email;
        Password = password;
        NIF = nIF;
        DOB = dOB;
        Notifications = notifications;
        Language = language;
    }

}