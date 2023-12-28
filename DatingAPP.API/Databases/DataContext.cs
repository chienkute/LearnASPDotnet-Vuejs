using DatingApp.API.Databases.Entities;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Databases
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<UserType> UserTypes { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserType>()
                .Property(t => t.Name)
                .HasMaxLength(100);

            modelBuilder.Entity<User>()
                .HasOne(u => u.UserType)
                .WithMany(t => t.Users)
                .HasForeignKey(u => u.UserTypeId);

            base.OnModelCreating(modelBuilder);
        }
    }
}