using Domain;
using Microsoft.EntityFrameworkCore;

namespace BetPersistence;
public partial class BetContext : DbContext
{
    public DbSet<Bet> Bets { get; set; }
    public DbSet<Selection> Selections { get; set; }
}
