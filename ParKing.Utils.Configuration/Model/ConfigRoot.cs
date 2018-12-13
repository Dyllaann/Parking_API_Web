namespace ParKing.Utils.Configuration.Model
{
    public class ConfigRoot
    {
        public Database Database { get; set; }
        public Logging Logging { get; set; }
        public Authentication Authentication { get;set; }
    }

    public class Database
    {
        public string ConnectionString { get; set; }
    }

    public class Logging
    {
        public string LogzIoToken { get; set; }
    }

    public class Authentication
    {
        public string Username { get;set; }
        public string Password { get; set; }
        
    }
}
