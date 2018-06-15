using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Core.Entities;
using Dal.Repositories;

namespace Web.Controllers
{
    public class CommentsController : ApiController
    {
        private readonly ICommentRepository commentRepository;

        public CommentsController(ICommentRepository commentRepository)
        {
            this.commentRepository = commentRepository;
        }

        [HttpGet]
        public IEnumerable<Comment> GetComments(int id)
        {
            IEnumerable<Comment> comments;
            try
            {
                comments = commentRepository.GetComments(id);
            }
            catch (Exception e)
            {
                // the error will be processed further

                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    ReasonPhrase = e.Message
                });
            }
            return comments;
        }
    }
}