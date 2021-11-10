using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ServerBE.Models;
using Shared.Enum;

namespace ServerBE.Data.SeedData
{
    public static class InitialData
    {
        public static void SeedData(this ModelBuilder modelBuilder)
        {
            var NikeId = Guid.NewGuid().ToString();
            var AdidasId = Guid.NewGuid().ToString();
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = NikeId,
                    Name = "Nike",
                    Description = "USA brand",
                },
                new Category
                {
                    Id = AdidasId,
                    Name = "Adidas",
                    Description = "German brand",
                }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = Guid.NewGuid().ToString(),
                    CategoryId = AdidasId,
                    Name = "Adidas 1",
                    Description = "Good Shoes for good runner",
                    Price = 3400000,
                    Gender = (int)GenderEnum.Male,
                    Size = (int)SizeEnum.Size39,
                    //Brand = (int)BrandEnum.Adidas,
                    CreatedDate = DateTime.Now
                },
                new Product
                {
                    Id = Guid.NewGuid().ToString(),
                    CategoryId = NikeId,
                    Name = "Nike 2",
                    Description = "Good Shoes for good runner",
                    Price = 3400000,
                    Gender = (int)GenderEnum.Male,
                    Size = (int)SizeEnum.Size40,
                    //Brand = (int)BrandEnum.Adidas,
                    CreatedDate = DateTime.Now
                },
                new Product
                {
                    Id = Guid.NewGuid().ToString(),
                    CategoryId = AdidasId,
                    Name = "Adidas 3",
                    Description = "Good Shoes for good runner",
                    Price = 3400000,
                    Gender = (int)GenderEnum.Female,
                    Size = (int)SizeEnum.Size39,
                    //Brand = (int)BrandEnum.Adidas,
                    CreatedDate = DateTime.Now
                }
            );

            //var roleId = Guid.NewGuid().ToString();
            //var adminId = Guid.NewGuid().ToString();
            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "user",
                    NormalizedName = "USER",
                    ConcurrencyStamp = "abc"

                },
                new Role
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = "xyz"
                }
            );

            var hash = new PasswordHasher<User>();
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "user1",
                    NormalizedUserName = "USER1",
                    Email = "user1@email.com",
                    NormalizedEmail = "USER1@EMAIL.COM",
                    PasswordHash = hash.HashPassword(null, "User1^^"),

                },
                new User
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "admin1",
                    NormalizedUserName = "ADMIN1",
                    Email = "admin1@email.com",
                    NormalizedEmail = "ADMIN1@EMAIL.COM",
                    PasswordHash = hash.HashPassword(null, "Admin1^^"),
                }
            );

            //modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(
            //    new IdentityUserRole<Guid>
            //    {
            //        RoleId = roleId,
            //        UserId = adminId
            //    });
        }
    }
}
