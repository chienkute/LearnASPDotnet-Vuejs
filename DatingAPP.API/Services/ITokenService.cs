using DatingApp.API.Databases.Entities;

namespace DatingApp.API.Services
{
    public interface ITokenService
    {
        string GenerateJWT(User user);
    }
}