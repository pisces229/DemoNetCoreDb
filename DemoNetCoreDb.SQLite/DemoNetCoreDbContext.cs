using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DemoNetCoreDb.SQLite
{
    public partial class DemoNetCoreDbContext : DbContext
    {
        public DemoNetCoreDbContext()
        {
        }

        public DemoNetCoreDbContext(DbContextOptions<DemoNetCoreDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Addresses { get; set; } = null!;
        public virtual DbSet<Person> People { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=d:/Database/SQLite/DemoNetCoreDb.db;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(entity =>
            {
                entity.HasKey(e => e.Row);

                entity.ToTable("Address");

                entity.HasOne(d => d.IdNavigation)
                    .WithMany(p => p.Addresses)
                    .HasPrincipalKey(p => p.Id)
                    .HasForeignKey(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasKey(e => e.Row);

                entity.ToTable("Person");

                entity.HasIndex(e => e.Id, "IX_Person_Id")
                    .IsUnique();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
