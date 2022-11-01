using Domain;
using Domain.ResultDomain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GameOddPersistance;
public partial class GameOddContext : DbContext
{
    public DbSet<Game> Game { get; set; }
    public DbSet<Sport> Sport { get; set; }
    public DbSet<BetType> BetType { get; set; }
    public DbSet<Odd> Odd { get; set; }

}
