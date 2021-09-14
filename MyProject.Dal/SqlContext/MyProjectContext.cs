using Microsoft.EntityFrameworkCore;
using MyProject.Common.Models;
using MyProject.Common.Models.Domain;
using MyProject.Dal.Configuration;
using System;


namespace MyProject.Dal.SqlContext
{
    public class MyProjectContext : DbContext
    {
        public MyProjectContext(DbContextOptions<MyProjectContext> options)
            : base(options)
        {
            try
            {
                Database.EnsureCreated();
            }
            catch (Exception e)
            {
                e.GetBaseException();
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyModelConfiguration();
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
