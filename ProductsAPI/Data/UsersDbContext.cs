﻿using Microsoft.EntityFrameworkCore;
using ProductsAPI.Models;

namespace ProductsAPI.Data
{
    public class UsersDbContext : DbContext
    {
        public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        
    }
}
