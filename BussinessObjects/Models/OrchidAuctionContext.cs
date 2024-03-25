using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BussinessObjects.Models;

public partial class OrchidAuctionContext : DbContext
{
    public OrchidAuctionContext()
    {
    }

    public OrchidAuctionContext(DbContextOptions<OrchidAuctionContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Orchid> Orchids { get; set; }

    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<DepositRequest> DepositRequests { get; set; }
    public virtual DbSet<Transaction> Transactions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=(local);uid=sa;pwd=12345;database=OrchidAuction;TrustServerCertificate=True");

        private string GetConnectionString()
        {
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .Build();
        return config.GetConnectionString("Db");
        }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Orchid>(entity =>
        {
            entity.ToTable("Orchid");

            entity.Property(e => e.OrchidId).ValueGeneratedOnAdd();
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.ImageUrl).HasMaxLength(500);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Color).HasMaxLength(50);
            entity.Property(e => e.Species).HasMaxLength(50);
            entity.Property(e => e.Origin).HasMaxLength(50);
            entity.Property(e => e.DepositedStatus).HasDefaultValue(Enums.DepositStatus.Available);
            entity.Property(e => e.ApprovalStatus).HasDefaultValue(Enums.ApprovalStatus.Available);

            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime2")
                .HasDefaultValueSql("SYSDATETIME()")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime2")
                .HasDefaultValueSql("SYSDATETIME()")
                .ValueGeneratedOnAdd();

            entity.HasOne(d => d.Owner).WithMany(p => p.Orchids)
                .HasForeignKey(d => d.OwnerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Orchid_User");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.UserId).ValueGeneratedOnAdd();
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.Username).HasMaxLength(50);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.WalletAddress).HasMaxLength(50);
            entity.Property(e => e.Birthday).HasColumnType("date");
            entity.Property(e => e.Status).HasColumnType("int");
            entity.Property(e => e.Role).HasColumnType("int");
        });

        modelBuilder.Entity<DepositRequest>(entity =>
        {
            entity.ToTable("DepositRequest");

            entity.Property(e => e.DepositRequestId).ValueGeneratedOnAdd();
            entity.Property(e => e.Title).HasMaxLength(255);
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.RequestStatus).HasDefaultValue(Enums.RequestStatus.Pending);
            entity.Property(e => e.WalletAddress).HasMaxLength(100);

            entity.Property(e => e.CreatedAt)
               .HasColumnType("datetime2")
               .HasDefaultValueSql("SYSDATETIME()")
               .ValueGeneratedOnAdd();

            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime2")
                .HasDefaultValueSql("SYSDATETIME()")
                .ValueGeneratedOnAdd();

            entity.HasOne(d => d.Orchid).WithMany(p => p.DepositRequests)
                .HasForeignKey(d => d.OrchidId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DepositRequest_Orchid");
        });


        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.ToTable("Transaction");

            entity.Property(e => e.TransactionId).ValueGeneratedOnAdd();
            entity.Property(e => e.Amount);
            entity.Property(e => e.TransactionHash);
            entity.Property(e => e.OrchidId);
            entity.Property(e => e.CreatedAt)
               .HasColumnType("datetime2")
               .HasDefaultValueSql("SYSDATETIME()")
               .ValueGeneratedOnAdd();

            entity.HasOne(d => d.Orchid).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.OrchidId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Transaction_Orchid");
        });

    OnModelCreatingPartial(modelBuilder);
    }


    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
