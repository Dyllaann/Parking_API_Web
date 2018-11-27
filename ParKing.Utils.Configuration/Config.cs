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

        public string DatabaseConnectionString => GetConfigurationOption(Main.Database.ConnectionString, "Database Connectionstring");

        private static string GetConfigurationOption(string configValue, string configKey)
        {
            if (string.IsNullOrEmpty(configValue))
            {
                throw new MissingConfigurationException($"Missing config value for {configKey}");
            }

            return configValue;
        }
    }
}
