namespace ParKing.Utils.Configuration.Model
{
    public class ConfigRoot
    {
        public Database Database { get; set; }
    }

    public class Database
    {
        public string ConnectionString { get; set; }
    }
}
