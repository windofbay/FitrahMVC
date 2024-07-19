using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FitrahDataAccess.Models
{
    public partial class FitrahContext : DbContext
    {
        public FitrahContext()
        {
        }

        public FitrahContext(DbContextOptions<FitrahContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<History> Histories { get; set; } = null!;
        public virtual DbSet<Recap> Recaps { get; set; } = null!;

//         protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//         {
//             if (!optionsBuilder.IsConfigured)
//             {
// #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                 optionsBuilder.UseSqlServer("Data Source = LAPTOP-26EQ94MU; Initial Catalog= Fitrah; Trusted_Connection = True; User = basilisktf; Password = basilisk; TrustServerCertificate = True");
//             }
//         }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.Username)
                    .HasName("PK__Accounts__536C85E5163BCFF6");

                entity.Property(e => e.Username)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(300)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<History>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("PK__Historie__A25C5AA6C40AB357");

                entity.Property(e => e.Code)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Address)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AmilUsername)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.FidiyaMoney).HasColumnType("money");

                entity.Property(e => e.FidiyaRice).HasColumnType("decimal(6, 2)");

                entity.Property(e => e.FitrahMoney).HasColumnType("money");

                entity.Property(e => e.FitrahRice).HasColumnType("decimal(6, 2)");

                entity.Property(e => e.InfaqMoney).HasColumnType("money");

                entity.Property(e => e.InfaqRice).HasColumnType("decimal(6, 2)");

                entity.Property(e => e.MaalMoney).HasColumnType("money");

                entity.Property(e => e.MaalRice).HasColumnType("decimal(6, 2)");

                entity.Property(e => e.MuzakkiName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Note)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.HasOne(d => d.AmilUsernameNavigation)
                    .WithMany(p => p.Histories)
                    .HasForeignKey(d => d.AmilUsername)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Histories_Accounts");
            });

            modelBuilder.Entity<Recap>(entity =>
            {
                entity.HasKey(e => e.Date);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Image)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
