using Org.Feeder.FeederDb;

namespace Dal.Mappers
{
    public class UserMapper : IMapper<User, DB.Models.User>
    {
        public DB.Models.User Map(User source)
        {
            if (source == null)
            {
                return null;
            }
            return new DB.Models.User
            {
                UserId = source.UserId,
                Name = source.Name,
                Email = source.Email,
                ImageUrl = source.ImageUrl
            };
        }
    }
}