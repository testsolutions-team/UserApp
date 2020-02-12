using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserWebAPI.Models;

namespace UserWebAPI.Data
{
    public class UserRepository : IUserRepository
    {
        public readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<User> Create(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }


        public async Task<bool> Delete(int id)
        {
            _context.Users.Remove(_context.Users.Single(a => a.UserID == id));
            await _context.SaveChangesAsync();
            return true;
        }



        public async Task<User> GetUser(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserID == id);
            return user;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            return users;
        }

        public async Task<bool> Update()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UserExists(string username)
        {
            if (await _context.Users.AnyAsync(x => x.UserName == username))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
