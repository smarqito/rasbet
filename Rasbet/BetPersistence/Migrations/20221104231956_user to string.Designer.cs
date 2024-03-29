﻿// <auto-generated />
using System;
using BetPersistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BetPersistence.Migrations
{
    [DbContext(typeof(BetContext))]
    [Migration("20221104231956_user to string")]
    partial class usertostring
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Domain.Bet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("End")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Start")
                        .HasColumnType("datetime2");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("WonValue")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Bets");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Bet");
                });

            modelBuilder.Entity("Domain.Selection", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("BetMultipleId")
                        .HasColumnType("int");

                    b.Property<int>("BetTypeId")
                        .HasColumnType("int");

                    b.Property<int>("GameId")
                        .HasColumnType("int");

                    b.Property<double>("Odd")
                        .HasColumnType("float");

                    b.Property<int>("OddId")
                        .HasColumnType("int");

                    b.Property<bool>("Win")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("BetMultipleId");

                    b.ToTable("Selections");
                });

            modelBuilder.Entity("Domain.BetMultiple", b =>
                {
                    b.HasBaseType("Domain.Bet");

                    b.Property<double>("OddMultiple")
                        .HasColumnType("float");

                    b.Property<int>("OddsFinished")
                        .HasColumnType("int");

                    b.Property<int>("OddsWon")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue("BetMultiple");
                });

            modelBuilder.Entity("Domain.BetSimple", b =>
                {
                    b.HasBaseType("Domain.Bet");

                    b.Property<int>("SelectionId")
                        .HasColumnType("int");

                    b.HasIndex("SelectionId");

                    b.HasDiscriminator().HasValue("BetSimple");
                });

            modelBuilder.Entity("Domain.Selection", b =>
                {
                    b.HasOne("Domain.BetMultiple", null)
                        .WithMany("Selections")
                        .HasForeignKey("BetMultipleId");
                });

            modelBuilder.Entity("Domain.BetSimple", b =>
                {
                    b.HasOne("Domain.Selection", "Selection")
                        .WithMany()
                        .HasForeignKey("SelectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Selection");
                });

            modelBuilder.Entity("Domain.BetMultiple", b =>
                {
                    b.Navigation("Selections");
                });
#pragma warning restore 612, 618
        }
    }
}
