using API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(
                new Category
                {
                    Id = Guid.NewGuid(),
                    Name = "test-category-1"
                },
                new Category
                {
                    Id = Guid.NewGuid(),
                    Name = "test-category-2"
                },
                new Category
                {
                    Id = Guid.NewGuid(),
                    Name = "test-category-3"
                }
            );
        }
    }
}
