using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;

namespace Services
{
    public interface IUserService
    {
        Task<List<User>> GetUsersAsync();
        List<User> GetUsers();
    }
}