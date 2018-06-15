using System;
using System.Collections.Generic;
using Configurations;
using Org.Feeder.FeederDb;

namespace Dal
{
    public sealed class DataBaseAccessor: IDataBaseAccessor
    {
        private readonly Database db;

        public DataBaseAccessor(IConfiguration configuration)
        {
            db = new Database(configuration.DbConnectionString);
        }

        public IEnumerable<PostSummary> GetPostSummaries()
        {
            IEnumerable<PostSummary> data;
            try
            {
                data = db.GetPostSummaries();
            }
            catch (Exception)
            {
                data = db.GetPostSummaries();
            }
            return data;
        }

        public IList<Comment> GetComments(int postId)
        {
            return db.GetComments(postId);
        }

        public Post GetPost(int postId)
        {
            return db.GetPost(postId);
        }

        public IEnumerable<User> GetUsers()
        {
            return db.GetUsers();
        }
    }
}