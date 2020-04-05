using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace UserManagment.Models
{
    public class FileModel : EntityBase
    {
        [BindNever]
        public string FilePath { get; set; }

        [BindNever]
        public byte[] Content { get; set; }

        [BindNever]
        public string UniqueFileName { get; set; }


        [BindNever]
        public string UserId { get; set; }
    }
}
