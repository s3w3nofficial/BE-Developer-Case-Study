using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using API.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace API.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(
                new Product
                {
                    Id = 1,
                    Name = "Test",
                    ImgUri = "https://via.placeholder.com/600x400",
                    Price = 39,
                    Description = "Test Test Test"
                },
                new Product
                {
                    Id = 2,
                    Name = "Test 2",
                    ImgUri = "https://via.placeholder.com/600x400",
                    Price = 339,
                    Description = "Test 2 Test 2 Test"
                }
            );
        }
    }
}
