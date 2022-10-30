using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UserDomain;

public class Administrator : User
{
    public Administrator(string name, string email) : base(name, email)
    {
    }
}
