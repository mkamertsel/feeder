using DB.Models;

namespace Dal.Mappers.DbToDto
{
    public class UserMapper : IMapper<User, Core.Entities.User>
    {
        public Core.Entities.User Map(User source)
        {
            if (source == null)
            {
                return null;
            }
            return new Core.Entities.User
            {
                UserId = source.UserId,
                Name = source.Name,
                Email = source.Email,
                ImageUrl = source.ImageUrl
            };
        }
    }
}