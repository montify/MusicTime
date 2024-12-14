using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MusicTimeServa.Model;


namespace MusicTimeServa.Database
{
    public class UserIdentityDbContext : IdentityDbContext<User>
    {
        protected readonly IConfiguration Configuration;

        public UserIdentityDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sqlite database
            options.UseSqlite(Configuration.GetConnectionString("WebApiDatabase"));
        }

    }
}
