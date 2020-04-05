using System;
using System.Collections.Generic;
using System.Linq;
using UserManagment.Infrastructure;
using UserManagment.Models;
using UserManagment.Persistence.Context;

namespace UserManagment.Persistence.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {

        public UserRepository(UserManagmentContext context) : base(context)
        {

        }

        public void CreateUser(User user)
        {
            user.Status = EntityStatus.Active;
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void DeleteUser(string userId)
        {
            var user = _context.Users.Find(userId);
            user.Status = EntityStatus.Deleted;
            UpdateUser(user);
        }

        public User GetUser(string userId)
        {
            var user = _context.Users.Find(userId);

            return user;
        }

        public List<User> GetUsers()
        {
            var users = _context.Users.ToList();
            return users;
        }

        public bool IsUserExist(string userId)
        {
            var user = _context.Users.Find(userId);
            return user.IsNotNull();
        }

        public void UpdateUser(User user)
        {
            _context.Update(user);
            _context.SaveChanges();
        }
    }
}
