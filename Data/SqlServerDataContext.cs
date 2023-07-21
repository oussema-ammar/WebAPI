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
    }
}
