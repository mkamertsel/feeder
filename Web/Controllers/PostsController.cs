using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Core.Entities;
using Dal.Repositories;

namespace Web.Controllers
{
    public class PostsController : ApiController
    {
        private readonly IPostRepository postRepository;

        public PostsController(IPostRepository postRepository)
        {
            this.postRepository = postRepository;
        }

        [HttpGet]
        public PostDetails GetPostDetails(int id)
        {
            PostDetails postDetails;
            try
            {
                postDetails = postRepository.GetPost(id);
            }
            catch (Exception e)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    ReasonPhrase = e.Message
                });
            }
            return postDetails;
        }

        [HttpGet]
        public IEnumerable<PostSummary> GetPosts()
        {
            IEnumerable<PostSummary> postSummaries;
            try
            {
                postSummaries = postRepository.GetAllPosts();
            }
            catch (Exception e)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    ReasonPhrase = e.Message
                });
            }
            return postSummaries;
        }
    }
}