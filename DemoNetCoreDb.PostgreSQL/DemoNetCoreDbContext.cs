using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DemoNetCoreDb.PostgreSQL
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
                optionsBuilder.UseNpgsql("Host=localhost;Database=DemoNetCoreDb;Username=postgres;Password=1234;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(entity =>
            {
                entity.HasKey(e => e.Row);

                entity.ToTable("Address");

                entity.Property(e => e.Row).UseIdentityAlwaysColumn();

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Text)
                    .HasMaxLength(100)
                    .IsFixedLength();

                entity.HasOne(d => d.IdNavigation)
                    .WithMany(p => p.Addresses)
                    .HasPrincipalKey(p => p.Id)
                    .HasForeignKey(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Address_Id");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasKey(e => e.Row);

                entity.ToTable("Person");

                entity.HasIndex(e => e.Id, "UNIQUE_Person_Id")
                    .IsUnique();

                entity.Property(e => e.Row).UseIdentityAlwaysColumn();

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Remark)
                    .HasMaxLength(100)
                    .IsFixedLength();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
