using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace UserManagment.Models
{
    public abstract class EntityBase 
    {
        [Key]
        public string Id { get; set; }

        public string Name { get; set; }

        [BindNever]
        [JsonIgnore]
        public EntityStatus Status { get; set; }

    }
}
