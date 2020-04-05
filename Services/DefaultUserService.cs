using System;
using System.Collections.Generic;
using System.Linq;
using UserManagment.Infrastructure;
using UserManagment.Models;
using UserManagment.Persistence.Context;
using UserManagment.Persistence.Repositories;

namespace UserManagment.Services
{
    public class DefaultUserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public DefaultUserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public bool IsUserExist(string userId)
        {
            return _userRepository.IsUserExist(userId);
        }

        public void CreateUser(User user)
        {
            var dbUser = _userRepository.GetUser(user.Id);

            if (dbUser.IsNotNull())
            {
                throw new ArgumentException($"The user id: {dbUser.Id} is already exist");
            }

            _userRepository.CreateUser(user);
        }

        public void DeleteUser(string userId)
        {
            var user = _userRepository.GetUser(userId);

            if (user.IsNull() || !user.ValidateStatus())
            {
                throw new ArgumentException($"The user id: {userId} is not exist");
            }

            _userRepository.DeleteUser(userId);
        }

        public User GetUser(string userId)
        {
            var user = _userRepository.GetUser(userId);

            if (user.IsNull() || !user.ValidateStatus())
            {
                throw new  ArgumentException($"The user id: {userId} is not exist");
            }

            return user;
        }

        public List<User> GetUsers()
        {
            var users = _userRepository.GetUsers();
            return users;
        }

        public void UpdateUser(User user)
        {
            var dbUser = GetUser(user.Id);

            MapUser(user, dbUser);

            _userRepository.UpdateUser(user);
        }

        private void MapUser(User user, User dbUser)
        {
            user.Name = user.Name != string.Empty ? user.Name : dbUser.Name;
            user.Address = user.Address != string.Empty ? user.Address : dbUser.Address;
            user.RSAKey = user.RSAKey.IsNotNull() ? user.RSAKey : dbUser.RSAKey;
        }

    }
}
