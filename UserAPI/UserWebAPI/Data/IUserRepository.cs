using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserWebAPI.Models;

namespace UserWebAPI.Data
{
    public interface IUserRepository
    {
        Task<User> Create(User user);
        Task<bool> Update();

        Task<bool> Delete(int id);

        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUser(int id);
        Task<bool> UserExists(string username);
    }
}