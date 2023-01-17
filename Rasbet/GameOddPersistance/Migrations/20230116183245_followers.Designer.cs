﻿// <auto-generated />
using System;
using GameOddPersistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GameOddPersistance.Migrations
{
    [DbContext(typeof(GameOddContext))]
    [Migration("20230116183245_followers")]
    partial class followers
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Domain.Follower", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("GameId")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.ToTable("Follower");
                });

            modelBuilder.Entity("Domain.Game", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IdSync")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("SpecialistId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SportId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdSync");

                    b.HasIndex("SportId");

                    b.ToTable("Game");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Game");
                });

            modelBuilder.Entity("Domain.Odd", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("BetTypeId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("OddValue")
                        .HasColumnType("float");

                    b.Property<bool>("Win")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("BetTypeId");

                    b.ToTable("Odd");
                });

            modelBuilder.Entity("Domain.ResultDomain.BetType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GameId")
                        .HasColumnType("int");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("datetime2");

                    b.Property<string>("SpecialistId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.ToTable("BetType");

                    b.HasDiscriminator<string>("Discriminator").HasValue("BetType");
                });

            modelBuilder.Entity("Domain.Sport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Sport");
                });

            modelBuilder.Entity("Domain.CollectiveGame", b =>
                {
                    b.HasBaseType("Domain.Game");

                    b.Property<string>("AwayTeam")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HomeTeam")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("CollectiveGame");
                });

            modelBuilder.Entity("Domain.IndividualGame", b =>
                {
                    b.HasBaseType("Domain.Game");

                    b.HasDiscriminator().HasValue("IndividualGame");
                });

            modelBuilder.Entity("Domain.IndividualResult", b =>
                {
                    b.HasBaseType("Domain.ResultDomain.BetType");

                    b.HasDiscriminator().HasValue("IndividualResult");
                });

            modelBuilder.Entity("Domain.ResultDomain.H2h", b =>
                {
                    b.HasBaseType("Domain.ResultDomain.BetType");

                    b.Property<string>("AwayTeam")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("H2h");
                });

            modelBuilder.Entity("Domain.Follower", b =>
                {
                    b.HasOne("Domain.Game", null)
                        .WithMany("FollowersIds")
                        .HasForeignKey("GameId");
                });

            modelBuilder.Entity("Domain.Game", b =>
                {
                    b.HasOne("Domain.Sport", "Sport")
                        .WithMany()
                        .HasForeignKey("SportId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Sport");
                });

            modelBuilder.Entity("Domain.Odd", b =>
                {
                    b.HasOne("Domain.ResultDomain.BetType", null)
                        .WithMany("Odds")
                        .HasForeignKey("BetTypeId");
                });

            modelBuilder.Entity("Domain.ResultDomain.BetType", b =>
                {
                    b.HasOne("Domain.Game", null)
                        .WithMany("Bets")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Game", b =>
                {
                    b.Navigation("Bets");

                    b.Navigation("FollowersIds");
                });

            modelBuilder.Entity("Domain.ResultDomain.BetType", b =>
                {
                    b.Navigation("Odds");
                });
#pragma warning restore 612, 618
        }
    }
}
