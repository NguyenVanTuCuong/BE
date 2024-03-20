using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(local);uid=sa;pwd=12345;database=OrchidAuction;TrustServerCertificate=True;");

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

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
