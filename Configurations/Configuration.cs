using System;

namespace Configurations
{
    public class Configuration : IConfiguration
    {
        private const string defaultConnection = "local";
        private static FeederConfiguration configuration;
        private readonly string baseAdress = "http://localhost:51790/api/";

        public Configuration()
        {
            try
            {
                configuration = new FeederConfiguration();
            }
            catch (Exception)
            {
                configuration = null;
            }
        }

        public string DbConnectionString => configuration.ConnectionString ?? defaultConnection;
        public string HttpBaseAdress => configuration.HttpBaseAdress ?? baseAdress;
    }
}