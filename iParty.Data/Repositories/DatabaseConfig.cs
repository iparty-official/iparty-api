using System;

namespace iParty.Data.Repositories
{
    public class DatabaseConfig
    {
        public DatabaseConfig()
        {
            ConnectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");
            DatabaseAlias = Environment.GetEnvironmentVariable("DATABASE_ALIAS");
        }
        public string ConnectionString { get; }
        public string DatabaseAlias { get; }
    }
}
