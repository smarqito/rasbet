using Domain;
using Domain.ResultDomain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GameOddPersistance;
public partial class GameOddContext
{
    public DbSet<Game> Games { get; set; }
    public DbSet<Sport> Sports { get; set; }
    public DbSet<BetType> BetsType { get; set; }


}
