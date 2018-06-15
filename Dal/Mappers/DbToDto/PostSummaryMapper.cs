using Core.Entities;
using DbPostSummary = DB.Models.PostSummary;

namespace Dal.Mappers.DbToDto
{
    public class PostSummaryMapper : IMapper<DbPostSummary, PostSummary>
    {
        public PostSummary Map(DbPostSummary source)
        {
            if (source == null)
            {
                return null;
            }
            return new PostSummary
            {
                Id = source.Id,
                Title = source.Title
            };
        }
    }
}