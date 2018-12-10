using Microsoft.EntityFrameworkCore;
using ParKing.Data.Engine;
using ParKing.Utils.Configuration;

namespace ParKing.Data
{
    public class ParKingContext : DbContext
    {
        private Config Config { get; }

        public ParKingContext(Config config)
        {
            Config = config;
        }

        public ParKingContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseMySql(Config.DatabaseConnectionString);
        }

        public DbSet<ParkingLot> ParkingLots { get; set; }
        public DbSet<ParkingAvailability> ParkingAvailabilities { get; set; }
        public DbSet<ParkingLocation> ParkingLocations { get; set; }

    }
}
