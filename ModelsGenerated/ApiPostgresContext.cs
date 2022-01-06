using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WebApiDeneme14.ModelsGenerated
{
    public partial class ApiPostgresContext : DbContext
    {
        public ApiPostgresContext()
        {
        }

        public ApiPostgresContext(DbContextOptions<ApiPostgresContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Bolgeler> Bolgelers { get; set; }
        public virtual DbSet<Sehirler> Sehirlers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Server=192.168.33.10;Database=postgres;User Id=postgres;Password=myPassword;Port=49153");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bolgeler>(entity =>
            {
                entity.ToTable("bolgeler");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Isim)
                    .HasColumnType("character varying")
                    .HasColumnName("isim");
            });

            modelBuilder.Entity<Sehirler>(entity =>
            {
                entity.ToTable("sehirler");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Bolgeid).HasColumnName("bolgeid");

                entity.Property(e => e.Isim)
                    .HasColumnType("character varying")
                    .HasColumnName("isim");

                entity.HasOne(d => d.Bolge)
                    .WithMany(p => p.Sehirlers)
                    .HasForeignKey(d => d.Bolgeid)
                    .HasConstraintName("sehirler_fk");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
