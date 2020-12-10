using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.Configuration
{
    public class ContextBase : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<ToDoList> ToDoLists { get; set; }

        public ContextBase()
        { }

        public ContextBase(DbContextOptions<ContextBase> options) : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            if (!optionBuilder.IsConfigured)
            {
                optionBuilder.UseInMemoryDatabase(GetStringConectionConfig());
                base.OnConfiguring(optionBuilder);
            }
        }
        private string GetStringConectionConfig()
        {
            return "DatabaseMemory";
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    IdUser = 1,
                    UserName = "test",
                    FirstName = "Test",
                    LastName = "Testing",
                    Password = "pwd123"
                }
            );
        }
    }
}
