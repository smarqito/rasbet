﻿using Domain;
using Microsoft.EntityFrameworkCore;

namespace BetPersistence;
public partial class BetContext : DbContext
{
    public DbSet<Bet> Bets { get; set; }
    public DbSet<Selection> Selections { get; set; }

    public BetContext(DbContextOptions<BetContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BetSimple>();
        modelBuilder.Entity<BetMultiple>();
        base.OnModelCreating(modelBuilder);
    }
}
