using Microsoft.EntityFrameworkCore;
using MusicTimeServa.Model;

namespace MusicTimeServa.Services
{
    public class DataContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sqlite database
            options.UseSqlite(Configuration.GetConnectionString("WebApiDatabase"));
        }

        public DbSet<Entry> Entries { get; set; }
        public DbSet<User> Users { get; set; }
    }
}