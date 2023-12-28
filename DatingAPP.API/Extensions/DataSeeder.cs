using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using DatingApp.API.Databases;
using DatingApp.API.Databases.Entities;

namespace DatingApp.API.Extensions
{
    public static class DataSeeder
    {
        public static void SeedData(DataContext context)
        {
            if (context.Users.Any() || context.UserTypes.Any()) return;
            var type = new UserType
            {
                Name = "Default"
            };

            var jsonContent = File.ReadAllText("Databases/users.json");
            var users = JsonSerializer.Deserialize<List<User>>(jsonContent);
            if (users is null || !users.Any()) return;
            foreach (var user in users)
            {
                user.Username = user.Email ?? user.Username;
                using var hashFn = new HMACSHA256();
                var passwordInBytes = Encoding.UTF8.GetBytes("Password$");
                user.PasswordHash = hashFn.ComputeHash(passwordInBytes);
                user.PasswordSalt = hashFn.Key;
            }
            type.Users.AddRange(users);
            context.UserTypes.Add(type);
            context.SaveChanges();
        }
    }
}