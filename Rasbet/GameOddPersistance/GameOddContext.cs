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

    public GameOddContext(DbContextOptions<GameOddContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<H2h>();
        modelBuilder.Entity<IndividualResult>();

        modelBuilder.Entity<BetType>().Property(b => b.SpecialistId).IsRequired(false);
        modelBuilder.Entity<Game>().Property(b => b.SpecialistId).IsRequired(false);
        base.OnModelCreating(modelBuilder);
    }
}
