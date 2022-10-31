using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace UserPersistence;

public class UserContext : IdentityDbContext<Domain.User>
{
    public UserContext(DbContextOptions<UserContext> options) : base(options)
    {
    }

    public DbSet<Wallet> Wallet { get; set; }

    //bets?????????????

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

    }
}