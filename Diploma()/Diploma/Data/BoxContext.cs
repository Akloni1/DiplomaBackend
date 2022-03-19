using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Diploma
{
    public partial class BoxContext : DbContext
    {
        public BoxContext()
        {
        }

        public BoxContext(DbContextOptions<BoxContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Boxers> Boxers { get; set; }
        public virtual DbSet<BoxingClubs> BoxingClubs { get; set; }
        public virtual DbSet<Coaches> Coaches { get; set; }
        public virtual DbSet<Competitions> Competitions { get; set; }
        public virtual DbSet<CompetitionsBoxers> CompetitionsBoxers { get; set; }
        public virtual DbSet<CompetitionsClubs> CompetitionsClubs { get; set; }
        public virtual DbSet<EmployeesClub> EmployeesClub { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-JUI9V07;Database=Competitions;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Boxers>(entity =>
            {
                entity.HasKey(e => e.BoxerId);

                entity.HasIndex(e => e.BoxingClubId);

                entity.HasIndex(e => e.CoachId);

                entity.Property(e => e.CoachId).HasColumnName("CoachID");

                entity.Property(e => e.DateOfBirth).HasColumnType("datetime");

                entity.Property(e => e.Discharge)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.BoxingClub)
                    .WithMany(p => p.Boxers)
                    .HasForeignKey(d => d.BoxingClubId)
                    .HasConstraintName("FK_Boxers_BoxingClubs");

                entity.HasOne(d => d.Coach)
                    .WithMany(p => p.Boxers)
                    .HasForeignKey(d => d.CoachId)
                    .HasConstraintName("FK_Boxers_Coaches");
            });

            modelBuilder.Entity<BoxingClubs>(entity =>
            {
                entity.HasKey(e => e.BoxingClubId);

                entity.Property(e => e.ClubAddress)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ClubName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Coaches>(entity =>
            {
                entity.HasKey(e => e.CoachId);

                entity.HasIndex(e => e.BoxingClubId);

                entity.Property(e => e.CoachId).HasColumnName("CoachID");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SportsTitle)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.BoxingClub)
                    .WithMany(p => p.Coaches)
                    .HasForeignKey(d => d.BoxingClubId)
                    .HasConstraintName("FK_Coaches_BoxingClubs");
            });

            modelBuilder.Entity<Competitions>(entity =>
            {
                entity.Property(e => e.CompetitionsId).HasColumnName("CompetitionsID");

                entity.Property(e => e.CompetitionsAddress)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CompetitionsName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CompetitionsBoxers>(entity =>
            {
                entity.HasKey(e => new { e.CompetitionsId, e.BoxerId });

                entity.Property(e => e.CompetitionsId).HasColumnName("CompetitionsID");

                entity.HasOne(d => d.Boxer)
                    .WithMany(p => p.CompetitionsBoxers)
                    .HasForeignKey(d => d.BoxerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CompetitionsBoxers_Boxers");

                entity.HasOne(d => d.Competitions)
                    .WithMany(p => p.CompetitionsBoxers)
                    .HasForeignKey(d => d.CompetitionsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CompetitionsBoxers_Competitions");
            });

            modelBuilder.Entity<CompetitionsClubs>(entity =>
            {
                entity.HasKey(e => new { e.CompetitionsId, e.BoxingClubId });

                entity.Property(e => e.CompetitionsId).HasColumnName("CompetitionsID");

                entity.HasOne(d => d.BoxingClub)
                    .WithMany(p => p.CompetitionsClubs)
                    .HasForeignKey(d => d.BoxingClubId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CompetitionsClubs_BoxingClubs");

                entity.HasOne(d => d.Competitions)
                    .WithMany(p => p.CompetitionsClubs)
                    .HasForeignKey(d => d.CompetitionsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CompetitionsClubs_Competitions");
            });

            modelBuilder.Entity<EmployeesClub>(entity =>
            {
                entity.HasKey(e => e.EmployeeId);

                entity.HasIndex(e => e.BoxingClubId);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Post)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.BoxingClub)
                    .WithMany(p => p.EmployeesClub)
                    .HasForeignKey(d => d.BoxingClubId)
                    .HasConstraintName("FK_EmployeesClub_BoxingClubs");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
