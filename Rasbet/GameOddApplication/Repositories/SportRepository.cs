using GameOddApplication.Interfaces;
using GameOddPersistance;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace GameOddApplication.Repositories;

public class SportRepository : ISportRepository
{
    private readonly GameOddContext gameOddContext;

    public SportRepository(GameOddContext gameOddContext)
    {
        this.gameOddContext = gameOddContext;
    }

    public async Task<Sport> GetSport(string Name)
    {
        Sport s = await gameOddContext.Sport.Where(s => s.Name.Equals(Name))
            .FirstOrDefaultAsync();
        if (s == null)
            throw new Exception();
        return s;
    }

    public async Task<Sport> CreateSport(string name)
    {
        Sport s = new Sport(name);
        try
        {
            await gameOddContext.Sport.AddAsync(s);
            await gameOddContext.SaveChangesAsync();
            return s;
        }
        catch (Exception)
        {
            throw new Exception("Aconteceu um erro interno");
        }
    }

}
