using DatingApp.API.Databases.Entities;

namespace DatingApp.API.Services
{
    public interface IUsersService
    {
        List<User> GetUsers();
        User? GetUserById(int id);
        void CreateUser(User user);
        void UpdateUser(User user);
        void DeleteUser(int id);
        bool CheckUsernameExist(string username);
        User? GetUserByUsername(string username);
    }
}