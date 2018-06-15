using System.Collections.Generic;
using System.Linq;
using Dal.Mappers;
using Org.Feeder.FeederDb;

namespace Dal.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDataBaseAccessor dbAccessor;
        private readonly IMapper<User, DB.Models.User> dbUserMapper;
        private readonly IMapper<DB.Models.User, Core.Entities.User> dtoUserMapper;

        public UserRepository(IDataBaseAccessor dbAccessor, IMapper<User, DB.Models.User> dbUserMapper,
            IMapper<DB.Models.User, Core.Entities.User> dtoUserMapper)
        {
            this.dbAccessor = dbAccessor;
            this.dbUserMapper = dbUserMapper;
            this.dtoUserMapper = dtoUserMapper;
        }

        public IEnumerable<Core.Entities.User> GetUsers()
        {
            return dbAccessor.GetUsers().Select(x => dbUserMapper.Map(x)).Select(x => dtoUserMapper.Map(x));
        }
    }
}