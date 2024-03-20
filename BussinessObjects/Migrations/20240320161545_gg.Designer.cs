﻿// <auto-generated />
using System;
using BussinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BussinessObjects.Migrations
{
    [DbContext(typeof(OrchidAuctionContext))]
    [Migration("20240320161545_gg")]
    partial class gg
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BussinessObjects.Models.Orchid", b =>
                {
                    b.Property<Guid>("OrchidId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Color")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("SYSDATETIME()");

                    b.Property<int>("DepositedStatus")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("ImageUrl")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Origin")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Species")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("SYSDATETIME()");

                    b.HasKey("OrchidId");

                    b.HasIndex("OwnerId");

                    b.ToTable("Orchid", (string)null);
                });

            modelBuilder.Entity("BussinessObjects.Models.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.HasKey("UserId");

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("BussinessObjects.Models.Orchid", b =>
                {
                    b.HasOne("BussinessObjects.Models.User", "Owner")
                        .WithMany("Orchids")
                        .HasForeignKey("OwnerId")
                        .IsRequired()
                        .HasConstraintName("FK_Orchid_User");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("BussinessObjects.Models.User", b =>
                {
                    b.Navigation("Orchids");
                });
#pragma warning restore 612, 618
        }
    }
}