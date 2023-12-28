using DatingApp.API.Databases;
using DatingApp.API.Databases.Entities;

namespace DatingApp.API.Services
{
    public class UsersService : IUsersService
    {
        private readonly DataContext _context;

        public UsersService(DataContext context)
        {
            _context = context;
        }

        public bool CheckUsernameExist(string username)
        {
            return _context.Users.Any(user => user.Username == username);
        }

        public void CreateUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void DeleteUser(int id)
        {
            var user = GetUserById(id);
            if (user is null) return;
            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        public User? GetUserById(int id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }

        public User? GetUserByUsername(string username)
        {
            return _context.Users.FirstOrDefault(u => u.Username == username);
        }

        public List<User> GetUsers()
        {
            return _context.Users.Take(20).ToList();
        }

        public void UpdateUser(User user)
        {
            var updateUser = GetUserById(user.Id);
            if (updateUser is null) return;
            updateUser.Email = user.Email;
            updateUser.Username = user.Username;
            _context.SaveChanges();
        }
    }
}