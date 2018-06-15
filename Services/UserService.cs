using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Core.Entities;
using CustomClient;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly IClient client;
        private readonly HttpClient httpClient;

        public UserService(IClient client)
        {
            this.client = client;
            httpClient = client.GetHttpClient();
        }

        public async Task<List<User>> GetUsersAsync()
        {
            var usersLink = client.GetActionPath(AvailableActions.GetUsers);

            IEnumerable<User> usersList = new List<User>();
            var response = await httpClient.GetAsync(usersLink);
            if (response.IsSuccessStatusCode)
            {
                usersList = await response.Content.ReadAsAsync<IEnumerable<User>>();
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
            return usersList.ToList();
        }

        public List<User> GetUsers()
        {
            var usersLink = client.GetActionPath(AvailableActions.GetUsers);

            return Task.Run(() => GetUsers(usersLink, httpClient)).Result;
        }

        private async Task<List<User>> GetUsers(string path, HttpClient httpClient)
        {
            IEnumerable<User> usersList = new List<User>();
            var response = await httpClient.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                usersList = await response.Content.ReadAsAsync<IEnumerable<User>>();
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
            return usersList.ToList();
        }
    }
}