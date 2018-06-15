using System.Net.Http;

namespace CustomClient
{
    public interface IClient
    {
        string GetActionPath(AvailableActions action, int? id = null);

        HttpClient GetHttpClient();
    }
}