using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOddApplication.Interfaces;

public interface ISportRepository
{
    public Task<Sport> GetSport(string Name);
    public Task<Sport> CreateSport(string name);
}
