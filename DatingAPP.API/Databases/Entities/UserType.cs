using System.ComponentModel.DataAnnotations;

namespace DatingApp.API.Databases.Entities
{
    public class UserType
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual List<User> Users { get; set; } = new List<User>();
    }
}