using DB.Models;
using DbPostSummary = Org.Feeder.FeederDb.PostSummary;

namespace Dal.Mappers
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