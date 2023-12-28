namespace DatingApp.API.DTOs
{
    public class MemberDto
    {
        public int Id { get; set; }

        public string Username { get; set; } = null!;

        public string? Email { get; set; }

        public string KnownAs { get; set; } = null!;

        public int Age { get; set; }

        public string Gender { get; set; } = null!;

        public string Avatar { get; set; } = null!;

        public string City { get; set; } = null!;

        public int UserTypeId { get; set; }

        public string UserTypeName { get; set; } = null!;
    }
}