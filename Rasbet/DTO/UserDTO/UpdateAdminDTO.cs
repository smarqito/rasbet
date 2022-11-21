using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.UserDTO;

public class UpdateAdminDTO
{
    public string Email { get; set;}
    public string Name { get; set; }
    public string Language { get; set; }

    public UpdateAdminDTO(string name, string language, string email)
    {
        Email = email;
        Name = name;
        Language = language;
    }
}