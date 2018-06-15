using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Core.Entities;
using CustomClient;

namespace Services
{
    public class CommentService : ICommentService
    {
        private readonly IClient client;
        private readonly HttpClient httpClient;

        public CommentService(IClient client)
        {
            this.client = client;
            httpClient = client.GetHttpClient();
        }

        public IEnumerable<Comment> GetComments(int postId)
        {
            var commentsLink = client.GetActionPath(AvailableActions.GetCommentsByPostId, postId);

            return Task.Run(() => GetComments(commentsLink)).Result;
        }

        public async Task<IEnumerable<Comment>> GetCommentsAsync(int postId)
        {
            var commentsLink = client.GetActionPath(AvailableActions.GetCommentsByPostId, postId);

            IEnumerable<CommentExtended> commentsList = new List<CommentExtended>();
            var response = await httpClient.GetAsync(commentsLink);
            if (response.IsSuccessStatusCode)
            {
                commentsList = await response.Content.ReadAsAsync<IEnumerable<CommentExtended>>();
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
            return commentsList.ToList();
        }

        private async Task<List<CommentExtended>> GetComments(string path)
        {
            IEnumerable<CommentExtended> commentsList = new List<CommentExtended>();
            var response = await httpClient.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                commentsList = await response.Content.ReadAsAsync<IEnumerable<CommentExtended>>();
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
            return commentsList.ToList();
        }
    }
}