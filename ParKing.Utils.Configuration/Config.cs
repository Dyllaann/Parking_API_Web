using ParKing.Utils.Configuration.Model;
using Serilog;

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
        public string LogzIoToken => GetConfigurationOption(Main.Logging.LogzIoToken, "LogzIoToken");


        private static string GetConfigurationOption(string configValue, string configKey)
        {
            if (!string.IsNullOrEmpty(configValue)) return configValue;

            Log.Logger.Fatal($"Missing Configuration for {configKey}");
            throw new MissingConfigurationException(configKey);
        }
    }
}
