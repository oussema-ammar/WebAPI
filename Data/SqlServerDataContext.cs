using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Data
{
    public class SqlServerDataContext : DbContext
    {
        public SqlServerDataContext(DbContextOptions<SqlServerDataContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Sensor> Sensors { get; set; }
        public DbSet<SensorCategory> SensorCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the primary key for the junction entity
            modelBuilder.Entity<SensorCategory>()
                .HasKey(sc => new { sc.SensorId, sc.CategoryId });

            // Configure the foreign keys and relationships
            modelBuilder.Entity<SensorCategory>()
                .HasOne(sc => sc.Sensor)
                .WithMany(s => s.SensorCategories)
                .HasForeignKey(sc => sc.SensorId);

            modelBuilder.Entity<SensorCategory>()
                .HasOne(sc => sc.Category)
                .WithMany(c => c.SensorCategories)
                .HasForeignKey(sc => sc.CategoryId);
        }
    }
}
