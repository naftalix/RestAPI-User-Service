using System;
using System.Security.Cryptography;
using UserManagment.Models;

namespace UserManagment.Services
{
    public interface IKeysService
    {
        public void GenerateKey(User user);
    }
}
