using System;
using Microsoft.EntityFrameworkCore;
using UserManagment.Models;

namespace UserManagment.Persistence.Context
{
    public class UserManagmentContext : DbContext
    {
        public UserManagmentContext(DbContextOptions<UserManagmentContext> options)
            : base(options)
        {
        }

        public DbSet<FileModel> Files { get; set; }

        public DbSet<User> Users { get; set; }

    }
}
