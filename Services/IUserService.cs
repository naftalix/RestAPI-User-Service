using System;
using System.Collections.Generic;
using UserManagment.Models;

namespace UserManagment.Services
{
    public interface IUserService
    {
        public List<User> GetUsers();
        public User GetUser(string userId);
        public void UpdateUser(User user);
        public void DeleteUser(string userId);
        public void CreateUser(User user);
        public bool IsUserExist(string userId);


    }
}
