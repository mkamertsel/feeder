using System.Collections.Generic;
using Core.Entities;

namespace Dal.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsers();
    }
}