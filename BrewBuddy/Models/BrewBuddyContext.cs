using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BrewBuddy.Models;
//Denne brewbuddycontext klasse er vores primære bindeled mellem vores applikation og database. 
public partial class BrewBuddyContext : DbContext
{
    public BrewBuddyContext()
    {
    }

    public BrewBuddyContext(DbContextOptions<BrewBuddyContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CoffieMachine> CoffieMachines { get; set; }

    public virtual DbSet<MachineInfo> MachineInfos { get; set; }

    public virtual DbSet<Assignment> Assignments { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=BrewBuddy;Integrated Security=True;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CoffieMachine>(entity =>
        {
            entity.HasKey(e => e.MachineId).HasName("PK__CoffieMa__44EE5B38A8A30860");

            entity.ToTable("CoffieMachine");

            entity.Property(e => e.Location).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<MachineInfo>(entity =>
        {
            entity.HasKey(e => e.InfoId).HasName("PK__MachineI__4DEC9D7A52EF3225");

            entity.ToTable("MachineInfo");

            entity.HasOne(d => d.Machine).WithMany(p => p.MachineInfos)
                .HasForeignKey(d => d.MachineId)
                .HasConstraintName("FK__MachineIn__Machi__2D27B809");

            entity.HasOne(d => d.Assignment).WithMany(p => p.MachineInfos)
                .HasForeignKey(d => d.AssignmentId)
                .HasConstraintName("FK__MachineIn__TaskI__2F10007B");

            entity.HasOne(d => d.User).WithMany(p => p.MachineInfos)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__MachineIn__UserI__2E1BDC42");
        });

        modelBuilder.Entity<Assignment>(entity =>
        {
            entity.HasKey(e => e.AssignmentId).HasName("PK__Tasks__7C6949B1D001C0CB");

            entity.ToTable(tb => tb.HasTrigger("SetDateAndTimeOnComplete"));

            entity.Property(e => e.FinishedDateAndTime).HasColumnType("datetime");
            entity.Property(e => e.AssignmentName).HasMaxLength(50);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4CD5065536");

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
