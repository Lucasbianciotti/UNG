// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Models.EntityFrameworks
{
    public partial class UNG_Context : DbContext
    {
        public UNG_Context()
        {
        }

        public UNG_Context(DbContextOptions<UNG_Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Clients> Clients { get; set; }
        public virtual DbSet<Data> Data { get; set; }
        public virtual DbSet<Emails> Emails { get; set; }
        public virtual DbSet<Equipments> Equipments { get; set; }
        public virtual DbSet<Logs_Errors> Logs_Errors { get; set; }
        public virtual DbSet<Logs_SystemMoves> Logs_SystemMoves { get; set; }
        public virtual DbSet<Stations> Stations { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=.\\sqlexpress;Initial Catalog=UNG;Persist Security Info=True;User ID=nexus-sql;Password=Nexus@1699");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<Data>(entity =>
            {
                entity.HasOne(d => d.IDclientNavigation)
                    .WithMany(p => p.Data)
                    .HasForeignKey(d => d.IDclient)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Data_Clients");

                entity.HasOne(d => d.IDequipmentNavigation)
                    .WithMany(p => p.Data)
                    .HasForeignKey(d => d.IDequipment)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Data_Equipments");

                entity.HasOne(d => d.IDstationNavigation)
                    .WithMany(p => p.Data)
                    .HasForeignKey(d => d.IDstation)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Data_Stations");
            });

            modelBuilder.Entity<Emails>(entity =>
            {
                entity.Property(e => e.ID).ValueGeneratedNever();
            });

            modelBuilder.Entity<Equipments>(entity =>
            {
                entity.HasOne(d => d.IDclientNavigation)
                    .WithMany(p => p.Equipments)
                    .HasForeignKey(d => d.IDclient)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Equipments_Clients");

                entity.HasOne(d => d.IDstationNavigation)
                    .WithMany(p => p.Equipments)
                    .HasForeignKey(d => d.IDstation)
                    .HasConstraintName("FK_Equipments_Stations");

                entity.HasOne(d => d.Modify_IDuserNavigation)
                    .WithMany(p => p.Equipments)
                    .HasForeignKey(d => d.Modify_IDuser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Equipments_Users");
            });

            modelBuilder.Entity<Logs_Errors>(entity =>
            {
                entity.HasOne(d => d.IDclientNavigation)
                    .WithMany(p => p.Logs_Errors)
                    .HasForeignKey(d => d.IDclient)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Logs_Errors_Companies");

                entity.HasOne(d => d.IDuserNavigation)
                    .WithMany(p => p.Logs_Errors)
                    .HasForeignKey(d => d.IDuser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Logs_Errors_Users");
            });

            modelBuilder.Entity<Logs_SystemMoves>(entity =>
            {
                entity.HasOne(d => d.IDclientNavigation)
                    .WithMany(p => p.Logs_SystemMoves)
                    .HasForeignKey(d => d.IDclient)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Logs_SystemMoves_Companies");

                entity.HasOne(d => d.IDuserNavigation)
                    .WithMany(p => p.Logs_SystemMoves)
                    .HasForeignKey(d => d.IDuser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Logs_SystemMoves_Users");
            });

            modelBuilder.Entity<Stations>(entity =>
            {
                entity.HasOne(d => d.IDclientNavigation)
                    .WithMany(p => p.Stations)
                    .HasForeignKey(d => d.IDclient)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Stations_Companies");

                entity.HasOne(d => d.Modify_IDuserNavigation)
                    .WithMany(p => p.Stations)
                    .HasForeignKey(d => d.Modify_IDuser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Stations_Users");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasOne(d => d.IDclientNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.IDclient)
                    .HasConstraintName("FK_Users_Clients");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}