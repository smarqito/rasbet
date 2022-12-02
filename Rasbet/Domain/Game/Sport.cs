using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Game;

public class Sport
{
    public int Id { get; set; }
    public string Name { get; set; }

    protected Sport()
    {
    }

    public Sport(string name)
    {
        Name = name;
    }
}

