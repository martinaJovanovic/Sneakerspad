using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OnlineShopping.Areas.Identity.Data;
using OnlineShopping.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.Models
{
    public class SeedData
    {
        public static async Task CreateUserRoles(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<OnlineShoppingUser>>();
            IdentityResult roleResult;
            var roleCheck = await RoleManager.RoleExistsAsync("Admin");
            if (!roleCheck) { roleResult = await RoleManager.CreateAsync(new IdentityRole("Admin")); }
            OnlineShoppingUser user = await UserManager.FindByEmailAsync("admin@shop.com");
            if (user == null)
            {
                var User = new OnlineShoppingUser();
                User.Email = "admin@shop.com";
                User.UserName = "admin@shop.com";
                User.Role = "Администратор";
                User.FirstName = "Администратор";
                string userPWD = "Admin123";
                IdentityResult chkUser = await UserManager.CreateAsync(User, userPWD);
                if (chkUser.Succeeded) { var result1 = await UserManager.AddToRoleAsync(User, "Admin"); }
            }

            var roleCheck1 = await RoleManager.RoleExistsAsync("Manager");
            if (!roleCheck1) { roleResult = await RoleManager.CreateAsync(new IdentityRole("Manager")); }
            OnlineShoppingUser user1 = await UserManager.FindByEmailAsync("manager@shop.com");
            if (user1 == null)
            {
                var User1 = new OnlineShoppingUser();
                User1.Email = "manager@shop.com";
                User1.UserName = "manager@shop.com";
                User1.Role = "Менаџер";
                User1.FirstName = "Мартина";
                User1.LastName = "Јовановиќ";
                string userPWD = "Manager123";
                IdentityResult chkUser = await UserManager.CreateAsync(User1, userPWD);
                if (chkUser.Succeeded) { var result1 = await UserManager.AddToRoleAsync(User1, "Manager"); }
            }

            var roleCheck2 = await RoleManager.RoleExistsAsync("Client");
            if (!roleCheck2) { roleResult = await RoleManager.CreateAsync(new IdentityRole("Client")); }
            OnlineShoppingUser user2 = await UserManager.FindByEmailAsync("martina@shop.com");
            if (user2 == null)
            {
                var User2 = new OnlineShoppingUser();
                User2.Email = "martina@shop.com";
                User2.UserName = "martina@shop.com";
                User2.Role = "Клиент";
                User2.FirstName = "Мартина";
                User2.LastName = "Јовановиќ";
                string userPWD = "Martina123";
                IdentityResult chkUser = await UserManager.CreateAsync(User2, userPWD);
                if (chkUser.Succeeded) { var result2 = await UserManager.AddToRoleAsync(User2, "Client"); }
            }
        }


        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new OnlineShoppingContext(serviceProvider.GetRequiredService<DbContextOptions<OnlineShoppingContext>>()))
            {
                CreateUserRoles(serviceProvider).Wait();
                if (context.Product.Any() && context.Size.Any() && context.Client.Any() && context.Manager.Any())
                {
                    return;   // DB has been seeded
                }

                context.Product.AddRange(
                    new Product { Name = "Nike Air Max 90", Color = "Мултиколор", Price = 5990, PictureUrl = "/images/airmax.jpg", Sex = "Машки" },
                    new Product { Name = "Puma Suede", Color = "Зелена", Price = 4990, PictureUrl = "/images/suede.jpg", Sex = "Машки" },
                    new Product { Name = "Adidas Superstar", Color = "Сина", Price = 5990, PictureUrl = "/images/superstar.jpg", Sex = "Машки" },
                    new Product { Name = "Reebok Royal EC Ride 4", Color = "Мултиколор", Price = 2990, PictureUrl = "/images/reebok.jpg", Sex = "Машки" },
                    new Product { Name = "Adidas Stan Smith", Color = "Бела", Price = 5590, PictureUrl = "/images/stansmith.jpg", Sex = "Машки" },
                    new Product { Name = "Nike Air Max 97", Color = "Розева", Price = 5590, PictureUrl = "/images/airmax97.jpg", Sex = "Женски" },
                    new Product { Name = "Nike Wearallday", Color = "Розева", Price = 3990, PictureUrl = "/images/wearallday.jpg", Sex = "Женски" },
                    new Product { Name = "Reebok Classic", Color = "Бела", Price = 4290, PictureUrl = "/images/classic.jpg", Sex = "Женски" },
                    new Product { Name = "Puma RSX Reinventation", Color = "Мултиколор", Price = 6190, PictureUrl = "/images/rsxreinvent.jpg", Sex = "Женски" },
                    new Product { Name = "Reebok Falcon", Color = "Мултиколор", Price = 5590, PictureUrl = "/images/falcon.jpg", Sex = "Женски" },
                    new Product { Name = "Nike Air Force 1", Color = "Розева", Price = 5590, PictureUrl = "/images/airforcew.jpg", Sex = "Женски" },
                    new Product { Name = "Nike Revolution", Color = "Црна", Price = 5590, PictureUrl = "/images/revolution.jpg", Sex = "Деца" },
                    new Product { Name = "Nike Air Max Excee", Color = "Мултиколор", Price = 5590, PictureUrl = "/images/airmaxexcee.jpg", Sex = "Деца" },
                    new Product { Name = "Adidas Superstar", Color = "Мултиколор", Price = 5590, PictureUrl = "/images/superstark.jpg", Sex = "Деца" },
                    new Product { Name = "Adidas Ultraboost", Color = "Црна", Price = 5590, PictureUrl = "/images/ultraboost.jpg", Sex = "Деца" },
                    new Product { Name = "Reebook Classic", Color = "Мултиколор", Price = 5590, PictureUrl = "/images/classicnylon.jpg", Sex = "Деца" },
                    new Product { Name = "Reebok RX Unity", Color = "Мултиколор", Price = 5590, PictureUrl = "/images/rxunity.jpg", Sex = "Деца" }
                );

                context.SaveChanges();

                context.Size.AddRange(
                    new Size { ProductSize = 20 }, 
                    new Size { ProductSize = 21 }, 
                    new Size { ProductSize = 22 },
                    new Size { ProductSize = 23 }, 
                    new Size { ProductSize = 24 }, 
                    new Size { ProductSize = 25 },
                    new Size { ProductSize = 26 }, 
                    new Size { ProductSize = 27 }, 
                    new Size { ProductSize = 28 },
                    new Size { ProductSize = 29 }, 
                    new Size { ProductSize = 30 }, 
                    new Size { ProductSize = 31 },
                    new Size { ProductSize = 32 }, 
                    new Size { ProductSize = 33 }, 
                    new Size { ProductSize = 34 },
                    new Size { ProductSize = 35 }, 
                    new Size { ProductSize = 36 }, 
                    new Size { ProductSize = 37 },
                    new Size { ProductSize = 38 }, 
                    new Size { ProductSize = 39 }, 
                    new Size { ProductSize = 40 },
                    new Size { ProductSize = 41 }, 
                    new Size { ProductSize = 42 }, 
                    new Size { ProductSize = 43 },
                    new Size { ProductSize = 44 }, 
                    new Size { ProductSize = 45 }, 
                    new Size { ProductSize = 46 },
                    new Size { ProductSize = 47 }, 
                    new Size { ProductSize = 48 }, 
                    new Size { ProductSize = 49 },
                    new Size { ProductSize = 50 }
                    );

                context.SaveChanges();

                context.Client.AddRange(
                    new Client { FirstName = "Мартина", LastName = "Јовановиќ" });
                context.SaveChanges();

                context.Manager.AddRange(
                    new Manager { FirstName = "Мартина", LastName = "Јовановиќ" });
                context.SaveChanges();

            }
        }
    }
}
