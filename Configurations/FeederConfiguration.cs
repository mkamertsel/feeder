using System.Configuration;

namespace Configurations
{
    public class FeederConfiguration
    {
        public string ConnectionString => ConfigurationManager.AppSettings["feederConnection"];
        public string HttpBaseAdress => ConfigurationManager.AppSettings["httpBaseAdress"];
    }
}