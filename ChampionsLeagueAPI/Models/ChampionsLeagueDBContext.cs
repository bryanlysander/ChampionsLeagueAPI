using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using ChampionsLeagueAPI.Models;

namespace ChampionsLeagueAPI.Models
{
    public class ChampionsLeagueDBContext:DbContext
    {
        protected readonly IConfiguration Configuration;
        public ChampionsLeagueDBContext(DbContextOptions<ChampionsLeagueDBContext> options, IConfiguration configuration) : base(options)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var connectionString = Configuration.GetConnectionString("ChampionsLeague");
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }

        public DbSet<Player> Players { get; set; } = null;
        public DbSet<Club> Clubs { get; set; } = null;
    }
}
