using Domain;
using GameOddApplication.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOddApplication.Repositories;

public class SportRepository : ISportRepository
{
    public Task<Sport> CreateSport(string name)
    {
        throw new NotImplementedException();
    }
}
