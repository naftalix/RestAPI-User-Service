using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text.Json.Serialization;

namespace UserManagment.Models
{
    public class User : EntityBase
    {
        [JsonIgnore]
        public RSAParameters? RSAKey { get; set; }

        public string Address { get; set; }

        public int Age { get; set; }

    }       
}
