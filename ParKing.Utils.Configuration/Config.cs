using ParKing.Utils.Configuration.Model;

namespace ParKing.Utils.Configuration
{
    public class Config
    {
        private ConfigRoot Main { get; }

        public Config(ConfigRoot cfg)
        {
            Main = cfg;
        }

        public string Test()
        {
            var main = Main;
            var db = main.Database;
            var conn = db.ConnectionString;
            return conn;
        }

    }
}
