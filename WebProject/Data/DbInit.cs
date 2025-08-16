using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebProject.Models;

namespace WebProject.Data
{
    public static class DbInit
    {
        public static async Task SeedRolesAndAdminUser(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            // Define roles
            string[] roleNames = { "Admin", "User" };

            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // Create Admin User
            string adminUsername = "admin";
            string adminEmail = "admin@example.com";
            string adminPassword = "Admin@123"; // Change to a more secure password

            var adminUser = await userManager.FindByNameAsync(adminUsername);
            if (adminUser == null)
            {
                adminUser = new User
                {
                    UserName = adminUsername,
                    Email = adminEmail,
                    Name = "Administrator",
                    ImgURL = "https://static.vecteezy.com/system/resources/previews/009/292/244/non_2x/default-avatar-icon-of-social-media-user-vector.jpg",
                    EmailConfirmed = true // Mark email as confirmed
                };

                var result = await userManager.CreateAsync(adminUser, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }
        public static async Task InitializeTagAsync(MyAppContext context)
        {
            await context.Database.EnsureCreatedAsync();

            if (await context.Tags.AnyAsync())
            {
                return;   // DB has been seeded
            }
            var tags = new List<Tag>
            {
                new Tag { Name = "ASP.NET" },
                new Tag { Name = "EF Core" },
                new Tag { Name = "C#" }
            };

            context.Tags.AddRange(tags);
            await context.SaveChangesAsync();
        }
    }
}
