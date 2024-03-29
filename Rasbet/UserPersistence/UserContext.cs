﻿using Domain;
using Domain.UserDomain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System;

namespace UserPersistence;

public class UserContext : IdentityDbContext<Domain.User>
{
    public UserContext(DbContextOptions<UserContext> options) : base(options)
    {
    }

    public DbSet<Wallet> Wallet { get; set; }
    public DbSet<Transaction> Transaction { get; set; }

    public DbSet<AppUser> AppUsers { get; set; }
    public DbSet<Specialist> Specialists { get; set;}
    public DbSet<Admin> Admins { get; set;}
    
    public DbSet<UpdateInfo> Updates {get; set;}
    public DbSet<AppUserBetHistory> UserBetHistory {get; set;}

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<User>();

        builder.Entity<AppUserBetHistory>()
            .HasKey(x => new { x.UserId, x.BetId });

        builder.Entity<AppUser>().HasMany(x => x.BetHistory).WithOne().HasForeignKey(x => x.UserId);

        builder.Entity<Withdraw>();
        builder.Entity<Deposit>();

        builder.Entity<UpdateInfo>().HasKey(x => x.Email);

    }
}