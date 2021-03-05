using Microsoft.EntityFrameworkCore;
using API.Configuration;
using API.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace API.Services
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().Property(e => e.Price).HasPrecision(10, 2);
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
        }

        public virtual DbSet<Product> Products { get; set; }
    }
}
