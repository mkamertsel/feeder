namespace Configurations
{
    public interface IConfiguration
    {
        string DbConnectionString { get; }
        string HttpBaseAdress { get; }
    }
}