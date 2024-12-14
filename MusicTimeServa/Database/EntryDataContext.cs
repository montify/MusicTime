using Microsoft.EntityFrameworkCore;
using MusicTimeServa.Model;

namespace MusicTimeServa.Database
{
    public class EntryDataContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public EntryDataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sqlite database
            options.UseSqlite(Configuration.GetConnectionString("WebApiDatabase"));
        }

        public DbSet<Entry> Entries { get; set; }
    }
}