using System.Security.Cryptography;
using System.Text;
using DatingApp.API.Databases.Entities;
using DatingApp.API.DTOs;
using DatingApp.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly ITokenService _tokenService;

        public AuthController(
            IUsersService usersService,
            ITokenService tokenService)
        {
            _usersService = usersService;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterUserDto registerUser)
        {
            // Lowercase username
            registerUser.Username = registerUser.Username.ToLower();
            // Check if username already registered
            if (_usersService.CheckUsernameExist(registerUser.Username))
            {
                return BadRequest("Username already exists");
            }
            // Generate the password
            using var hashFn = new HMACSHA256();
            var passwordInBytes = Encoding.UTF8.GetBytes(registerUser.Password);
            var newUser = new User()
            {
                Username = registerUser.Username,
                PasswordHash = hashFn.ComputeHash(passwordInBytes),
                PasswordSalt = hashFn.Key
            };
            _usersService.CreateUser(newUser);
            return Ok(_tokenService.GenerateJWT(newUser));
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginUserDto loginUser)
        {
            // Lowercase username
            loginUser.Username = loginUser.Username.ToLower();
            // Check if username already registered
            var existedUser = _usersService.GetUserByUsername(loginUser.Username);
            if (existedUser is null)
            {
                return Unauthorized("Invalid username");
            }
            using var hashFn = new HMACSHA256(existedUser.PasswordSalt);
            var passwordInBytes = Encoding.UTF8.GetBytes(loginUser.Password);
            var passwordHash = hashFn.ComputeHash(passwordInBytes);
            for (int i = 0; i < passwordHash.Length; i++)
            {
                if (passwordHash[i] != existedUser.PasswordHash[i])
                {
                    return Unauthorized("Invalid password");
                }
            }
            return Ok(_tokenService.GenerateJWT(existedUser));
        }
    }
}