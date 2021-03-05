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
                },
                new Product
                {
                    Id = 3,
                    Name = "Test 3",
                    ImgUri = "https://via.placeholder.com/600x400",
                    Price = 499.50M,
                    Description = "Test 3 Test 3 Test"
                },
                new Product
                {
                    Id = 4,
                    Name = "Test 4",
                    ImgUri = "https://via.placeholder.com/600x400",
                    Price = 88,
                    Description = "Test 4 Test 4 Test"
                },
                new Product
                {
                    Id = 5,
                    Name = "Test 5",
                    ImgUri = "https://via.placeholder.com/600x400",
                    Price = 201.60M,
                    Description = "Test 5 Test 5 Test"
                },
                new Product
                {
                    Id = 6,
                    Name = "Test 6",
                    ImgUri = "https://via.placeholder.com/600x400",
                    Price = 499,
                    Description = "Test 6 Test 6 Test"
                },
                new Product
                {
                    Id = 7,
                    Name = "Test 7",
                    ImgUri = "https://via.placeholder.com/600x400",
                    Price = 301,
                    Description = "Test 7 Test 7 Test"
                },
                new Product
                {
                    Id = 8,
                    Name = "Test 8",
                    ImgUri = "https://via.placeholder.com/600x400",
                    Price = 1113,
                    Description = "Test 8 Test 8 Test"
                },
                new Product
                {
                    Id = 9,
                    Name = "Test 9",
                    ImgUri = "https://via.placeholder.com/600x400",
                    Price = 433.90M,
                    Description = "Test 9 Test 9 Test"
                },
                new Product
                {
                    Id = 10,
                    Name = "Test 10",
                    ImgUri = "https://via.placeholder.com/600x400",
                    Price = 349.22M,
                    Description = "Test 10 Test 10 Test"
                },
                new Product
                {
                    Id = 11,
                    Name = "Test 11",
                    ImgUri = "https://via.placeholder.com/600x400",
                    Price = 3039.99M,
                    Description = "Test 11 Test 11 Test"
                }
            );
        }
    }
}
