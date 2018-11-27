using Microsoft.EntityFrameworkCore;
using ParKing.Utils.Configuration;

namespace ParKing.Data
{
    public class ParKingContext : DbContext
    {
        protected ParKingContext(Config config)
        {
            Config = config;
        }

        private Config Config { get; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(Config.DatabaseConnectionString);
        }
    }
}
