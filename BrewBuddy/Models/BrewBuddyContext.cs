using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BrewBuddy.Models;

public partial class BrewBuddyContext : DbContext
{
    public BrewBuddyContext()
    {
    }

    public BrewBuddyContext(DbContextOptions<BrewBuddyContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Assignment> Assignments { get; set; }

    public virtual DbSet<CoffieMachine> CoffieMachines { get; set; }

    public virtual DbSet<Statistik> Statistiks { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=BrewBuddy;Integrated Security=True;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Assignment>(entity =>
        {
            entity.HasKey(e => e.AssignmentId).HasName("PK__Assignme__32499E770FB98F4E");

            entity.ToTable(tb => tb.HasTrigger("UpdateStat"));

            entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.DailyDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FinishedDateAndTime).HasColumnType("datetime");
            entity.Property(e => e.IntervalType).HasMaxLength(50);
            entity.Property(e => e.Type).HasMaxLength(50);

            entity.HasOne(d => d.Machine).WithMany(p => p.Assignments)
                .HasForeignKey(d => d.MachineId)
                .HasConstraintName("FK__Assignmen__Machi__2E1BDC42");

            entity.HasOne(d => d.User).WithMany(p => p.Assignments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Assignmen__UserI__2F10007B");
        });

        modelBuilder.Entity<CoffieMachine>(entity =>
        {
            entity.HasKey(e => e.MachineId).HasName("PK__CoffieMa__44EE5B385B002544");

            entity.ToTable("CoffieMachine");

            entity.Property(e => e.Location).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Statistik>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Statistik");

            entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.MonthlyAmount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.StatId).ValueGeneratedOnAdd();
            entity.Property(e => e.Type).HasMaxLength(50);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4C703976DF");

            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.PhoneNumber).HasMaxLength(50);
            entity.Property(e => e.RegistrationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Role).HasMaxLength(10);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
