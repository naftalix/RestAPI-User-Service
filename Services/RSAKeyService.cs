using System;
using System.Security.Cryptography;
using UserManagment.Models;
using UserManagment.Services;

namespace UserManagment
{
    public class RSAKeyService : IKeysService
    {

        public void GenerateKey(User user)
        {
            //Generate a public/private key pair.  
            var rsa = new RSACryptoServiceProvider();
            //Save the public key information to an RSAParameters structure.  
            var rsaKeyInfo = rsa.ExportParameters(false);

            user.RSAKey = rsaKeyInfo;
        }
    }
}