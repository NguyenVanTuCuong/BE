﻿// <auto-generated />
using System;
using BussinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BussinessObjects.Migrations
{
    [DbContext(typeof(OrchidAuctionContext))]
    partial class OrchidAuctionContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BussinessObjects.Models.DepositRequest", b =>
                {
                    b.Property<Guid>("DepositRequestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("SYSDATETIME()");

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<Guid>("OrchidId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("RequestStatus")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<string>("Title")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("SYSDATETIME()");

                    b.Property<string>("WalletAddress")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("DepositRequestId");

                    b.HasIndex("OrchidId");

                    b.ToTable("DepositRequest", (string)null);
                });

            modelBuilder.Entity("BussinessObjects.Models.Orchid", b =>
                {
                    b.Property<Guid>("OrchidId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ApprovalStatus")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<string>("Color")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("SYSDATETIME()");

                    b.Property<int>("DepositedStatus")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

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

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("SYSDATETIME()");

                    b.HasKey("OrchidId");

                    b.HasIndex("OwnerId");

                    b.ToTable("Orchid", (string)null);
                });

            modelBuilder.Entity("BussinessObjects.Models.Transaction", b =>
                {
                    b.Property<Guid>("TransactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("SYSDATETIME()");

                    b.Property<Guid>("OrchidId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("TransactionHash")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("TransactionId");

                    b.HasIndex("OrchidId");

                    b.ToTable("Transaction", (string)null);
                });

            modelBuilder.Entity("BussinessObjects.Models.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("Birthday")
                        .HasColumnType("date");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("FirstName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("WalletAddress")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("UserId");

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("BussinessObjects.Models.DepositRequest", b =>
                {
                    b.HasOne("BussinessObjects.Models.Orchid", "Orchid")
                        .WithMany("DepositRequests")
                        .HasForeignKey("OrchidId")
                        .IsRequired()
                        .HasConstraintName("FK_DepositRequest_Orchid");

                    b.Navigation("Orchid");
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

            modelBuilder.Entity("BussinessObjects.Models.Transaction", b =>
                {
                    b.HasOne("BussinessObjects.Models.Orchid", "Orchid")
                        .WithMany("Transactions")
                        .HasForeignKey("OrchidId")
                        .IsRequired()
                        .HasConstraintName("FK_Transaction_Orchid");

                    b.Navigation("Orchid");
                });

            modelBuilder.Entity("BussinessObjects.Models.Orchid", b =>
                {
                    b.Navigation("DepositRequests");

                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("BussinessObjects.Models.User", b =>
                {
                    b.Navigation("Orchids");
                });
#pragma warning restore 612, 618
        }
    }
}
