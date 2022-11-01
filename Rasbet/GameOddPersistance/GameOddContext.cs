using Domain;
using Domain.ResultDomain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GameOddPersistance;
public partial class GameOddContext : DbContext
{
    public DbSet<Game> Games { get; set; }
    public DbSet<Sport> Sports { get; set; }
    public DbSet<BetType> BetsType { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

    }
}
