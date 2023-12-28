using System.ComponentModel.DataAnnotations;

namespace DatingApp.API.DTOs
{
    public class RegisterUserDto
    {
        [StringLength(100)]
        public string Username { get; set; } = null!;

        [StringLength(256)]
        public string Password { get; set; } = null!;
    }

    public class LoginUserDto
    {
        [StringLength(100)]
        public string Username { get; set; } = null!;

        [StringLength(256)]
        public string Password { get; set; } = null!;
    }
}