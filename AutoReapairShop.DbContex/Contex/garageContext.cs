using System;
using System.Collections.Generic;
using AutoReapairShop.DbContex.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AutoReapairShop.DbContex.Contex
{
    public partial class garageContext : DbContext
    {
        public garageContext()
        {
        }

        public garageContext(DbContextOptions<garageContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Master> Masters { get; set; } = null!;
        public virtual DbSet<Record> Records { get; set; } = null!;
        public virtual DbSet<Schedule> Schedules { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=6.tcp.eu.ngrok.io;Port=15449;Database=garage;Username=postgres;Password=EfgraF_0256");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Master>(entity =>
            {
                entity.ToTable("master");

                entity.Property(e => e.MasterId).HasColumnName("master_id");

                entity.Property(e => e.Email).HasColumnName("email");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(128)
                    .HasColumnName("first_name");

                entity.Property(e => e.LastName)
                    .HasMaxLength(128)
                    .HasColumnName("last_name");

                entity.Property(e => e.Patronymic)
                    .HasMaxLength(128)
                    .HasColumnName("patronymic");

                entity.Property(e => e.Phone).HasColumnName("phone");
            });

            modelBuilder.Entity<Record>(entity =>
            {
                entity.ToTable("record");

                entity.Property(e => e.RecordId).HasColumnName("record_id");

                entity.Property(e => e.AutoMark)
                    .HasMaxLength(200)
                    .HasColumnName("auto_mark");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Title)
                    .HasMaxLength(200)
                    .HasColumnName("title");
            });

            modelBuilder.Entity<Schedule>(entity =>
            {
                entity.ToTable("schedule");

                entity.Property(e => e.ScheduleId).HasColumnName("schedule_id");

                entity.Property(e => e.DateOfWeek).HasColumnName("date_of_week");

                entity.Property(e => e.FkMasterId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("fk_master_id");

                entity.Property(e => e.FkRecordId).HasColumnName("fk_record_id");

                entity.Property(e => e.TimeOfWeek).HasColumnName("time_of_week");

                entity.HasOne(d => d.FkMaster)
                    .WithMany(p => p.Schedules)
                    .HasForeignKey(d => d.FkMasterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("schedule_fk_master_id_fkey");

                entity.HasOne(d => d.FkRecord)
                    .WithMany(p => p.Schedules)
                    .HasForeignKey(d => d.FkRecordId)
                    .HasConstraintName("schedule_fk_record_id_fkey");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
