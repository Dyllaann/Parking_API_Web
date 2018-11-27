using Microsoft.EntityFrameworkCore;
using ParKing.Utils.Configuration;

namespace ParKing.Data
{
    public class ParKingContext : DbContext
    {
        private Config Config { get; }


        protected ParKingContext(Config config)
        {
            Config = config;
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(Config.DatabaseConnectionString);
        }
    }
}
