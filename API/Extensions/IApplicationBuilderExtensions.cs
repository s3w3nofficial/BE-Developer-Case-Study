using API.Models;
using API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Extensions
{
    public static class IApplicationBuilderExtensions
    {
        public static void Seed(this IApplicationBuilder applicationBuilder, 
            ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            // apply ef migrations
            db.Database.Migrate();

            // add category to first product
            var prodcut = db.Products.AsNoTracking().FirstOrDefault();
            if (prodcut != null && prodcut.Category == null)
            {
                var updatedProduct = prodcut with
                {
                    Category = db.Categories.FirstOrDefault()
                };
                db.Products.Update(updatedProduct);
                db.SaveChanges();
            }

            var exists = userManager.FindByEmailAsync("admin@admin.cz").Result;
            if (exists is null)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = "admin@admin.cz",
                    Email = "admin@admin.cz"
                };

                IdentityResult result = userManager.CreateAsync(user, "Heslo1234.").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }

            exists = userManager.FindByEmailAsync("test@test.cz").Result;
            if (exists is null)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = "test@test.cz",
                    Email = "test@test.cz"
                };

                userManager.CreateAsync(user, "Heslo1234.").Wait();
            }

            exists = userManager.FindByEmailAsync("test2@test.cz").Result;
            if (exists is null)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = "test2@test.cz",
                    Email = "test2@test.cz"
                };

                userManager.CreateAsync(user, "Heslo1234.").Wait();
            }
        }
    }
}
