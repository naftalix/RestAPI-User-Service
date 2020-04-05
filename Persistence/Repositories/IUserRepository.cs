using System;
using System.Collections.Generic;
using UserManagment.Models;
using UserManagment.Persistence.Context;

namespace UserManagment.Persistence.Repositories
{
    public interface IUserRepository
    {
        public List<User> GetUsers();
        public User GetUser(string userId);
        public void UpdateUser(User user);
        public void DeleteUser(string userId);
        public void CreateUser(User user);
        public bool IsUserExist(string userId);

    }
}
