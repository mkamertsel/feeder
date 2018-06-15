using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Configurations;

namespace CustomClient
{
    public class Client : IClient
    {
        private static HttpClient client;
        private readonly string mediaType = "application/json";


        public Client(IConfiguration configuration)
        {
            client = new HttpClient {BaseAddress = new Uri(configuration.HttpBaseAdress) };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue(mediaType));
        }

        public string GetActionPath(AvailableActions action, int? id = null)
        {
            switch (action)
            {
                case AvailableActions.GetCommentsByPostId:
                    return ActionGetCommentsByPostId(id);
                case AvailableActions.GetPostById:
                    return ActionGetPostById(id);
                case AvailableActions.GetPosts:
                    return ActionGetPosts();
                case AvailableActions.GetUsers:
                    return ActionGetUsers();
            }
            return null;
        }

        public HttpClient GetHttpClient()
        {
            return client;
        }


        private static string ActionGetUsers()
        {
            return "users/";
        }

        private static string ActionGetPosts()
        {
            return "posts/";
        }

        private static string ActionGetCommentsByPostId(int? id)
        {
            var action = string.Empty;
            if (id.HasValue)
            {
                action = $"comments/{id.Value}";
            }
            return action;
        }

        private static string ActionGetPostById(int? id)
        {
            var action = string.Empty;

            if (id.HasValue)
            {
                action = $"posts/{id.Value}";
            }
            return action;
        }
    }
}