using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Core.Entities;
using CustomClient;

namespace Services
{
    public class PostService : IPostService
    {
        private readonly IClient client;
        private readonly HttpClient httpClient;

        public PostService(IClient client)
        {
            this.client = client;
            httpClient = client.GetHttpClient();
        }

        public IEnumerable<PostSummary> GetAllPosts()
        {
            var link = client.GetActionPath(AvailableActions.GetPosts);
            return Task.Run(() => GetPostsAsync(link, httpClient)).Result;
        }

        public PostDetails GetPostDetails(int postId)
        {
            var link = client.GetActionPath(AvailableActions.GetPostById, postId);
            return Task.Run(() => GetPostDetails(link)).Result;
        }

        public async Task<IEnumerable<PostSummary>> GetAllPostsAsync()
        {
            var path = client.GetActionPath(AvailableActions.GetPosts);

            IEnumerable<PostSummary> posts;
            var response = await httpClient.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                posts = await response.Content.ReadAsAsync<IEnumerable<PostSummary>>();
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
            return posts;
        }

        public async Task<PostDetails> GetPostDetailsAsync(int postId)
        {
            var path = client.GetActionPath(AvailableActions.GetPostById, postId);

            PostDetails postDetails;
            var response = await httpClient.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                postDetails = await response.Content.ReadAsAsync<PostDetails>();
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
            return postDetails;
        }

        private static async Task<IEnumerable<PostSummary>> GetPostsAsync(string path, HttpClient client)
        {
            IEnumerable<PostSummary> posts;
            var response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                posts = await response.Content.ReadAsAsync<IEnumerable<PostSummary>>();
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
            return posts;
        }

        private async Task<PostDetails> GetPostDetails(string path)
        {
            PostDetails postDetails;
            var response = await httpClient.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                postDetails = await response.Content.ReadAsAsync<PostDetails>();
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
            return postDetails;
        }
    }
}