using Microsoft.EntityFrameworkCore;
using WorkshopCDA.Models;

namespace WorkshopCDA.Data
{
    public partial class FinalWorkshopContext : DbContext
    {
        public FinalWorkshopContext()
        {
        }

        public FinalWorkshopContext(DbContextOptions<FinalWorkshopContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Animal> Animals { get; set; }
        public virtual DbSet<Race> Races { get; set; }
        public virtual DbSet<Client> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseMySql("server=localhost;port=3306;database=final_workshop;user=root;password=root", ServerVersion.Parse("5.7.24-mysql"));

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .UseCollation("utf8_general_ci")
                .HasCharSet("utf8");

            modelBuilder.Entity<Animal>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PRIMARY");

                entity.ToTable("animal");

                entity.Property(e => e.Id).HasColumnType("int(11)").ValueGeneratedOnAdd();
                entity.Property(e => e.RaceId).HasColumnType("int(11)");
                entity.Property(e => e.Description).HasMaxLength(255);
                entity.Property(e => e.Name).HasMaxLength(255);

                entity.HasOne(d => d.Race)
                    .WithMany(p => p.Animals)
                    .HasForeignKey(d => d.RaceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Animal_Race");
            });

            modelBuilder.Entity<Race>(entity =>
            {
                entity.HasKey(e => e.RaceId).HasName("PRIMARY");

                entity.ToTable("race");

                entity.Property(e => e.RaceId).HasColumnType("int(11)");
                entity.Property(e => e.Description).HasMaxLength(255);
                entity.Property(e => e.Name).HasMaxLength(255);
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PRIMARY");

                entity.ToTable("users");

                entity.Property(e => e.Id).HasColumnType("int(11)");
                entity.Property(e => e.Email).HasMaxLength(255);
                entity.Property(e => e.Password).HasMaxLength(255);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
