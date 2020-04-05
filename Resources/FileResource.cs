using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace UserManagment.Resources
{
    public class FileResource
    {
        [BindNever]
        public string FilePath { get; set; }

        [BindNever]
        [JsonIgnore]
        public byte[] Content { get; set; }

        [BindNever]
        public string UserId { get; set; }

        public string Name { get; set; }

        public string Id { get; set; }

    }
}
