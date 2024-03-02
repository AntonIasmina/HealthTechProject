using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HealthTech331.Models
{
    // HealthTechContext represents the database context for the health tech application
    public partial class HealthTechContext : DbContext
    {
        public HealthTechContext()
        {
        }

        public HealthTechContext(DbContextOptions<HealthTechContext> options)
            : base(options)
        {
        }

        // DbSet properties representing tables in the database
        public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; } = null!;
        public virtual DbSet<Appointment> Appointments { get; set; } = null!;
        public virtual DbSet<BusinessInterval> BusinessIntervals { get; set; } = null!;
        public virtual DbSet<Doctor> Doctors { get; set; } = null!;
        public virtual DbSet<Speciality> Specialities { get; set; } = null!;

        // Configures the database connection and options
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=. ;Initial Catalog=HealthTech;Integrated Security=True;TrustServerCertificate=True;encrypt=false;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__Applicat__1788CC4C02CF37A5");

                entity.ToTable("ApplicationUser");

                // Property configurations for ApplicationUser entity
                entity.Property(e => e.Cnp).HasColumnName("CNP");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            // Configuration for the Appointment entity
            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.ToTable("Appointment");

                // Property configurations for Appointment entity
                entity.Property(e => e.AppointmentId).ValueGeneratedNever();

                entity.Property(e => e.AppointmentDate).HasColumnType("datetime");

                entity.Property(e => e.AppointmentDescription)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.DoctorId)
                    .HasConstraintName("FK_Appointment_Doctor");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.PatientId)
                    .HasConstraintName("FK_Appointment_Patient");
            });

            // Configuration for the BusinessInterval entity
            modelBuilder.Entity<BusinessInterval>(entity =>
            {
                entity.ToTable("BusinessInterval");
            });

            // Configuration for the Doctor entity
            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.ToTable("Doctor");

                // Property configurations for Doctor entity
                entity.Property(e => e.Cnp).HasColumnName("CNP");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.HasOne(d => d.BusinessInterval)
                    .WithMany(p => p.Doctors)
                    .HasForeignKey(d => d.BusinessIntervalId)
                    .HasConstraintName("FK_Doctor_BusinessInterval");

                entity.HasOne(d => d.Speciality)
                    .WithMany(p => p.Doctors)
                    .HasForeignKey(d => d.SpecialityId)
                    .HasConstraintName("FK_User_Speciality");
            });

            // Configuration for the Speciality entity
            modelBuilder.Entity<Speciality>(entity =>
            {
                entity.ToTable("Speciality");

                entity.Property(e => e.SpecialityDescription)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.SpecialityName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            // Customization hook for additional model configuration
            OnModelCreatingPartial(modelBuilder);
        }

        // Placeholder for any additional model configuration
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
