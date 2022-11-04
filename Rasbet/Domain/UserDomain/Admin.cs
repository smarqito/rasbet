using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UserDomain;

public class Admin : User
{
    public Admin(string name, string email, string language) : base(name, email, language)
    {
    }
}
