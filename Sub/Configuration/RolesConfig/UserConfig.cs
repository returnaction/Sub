using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sub.Models.Entities.User.User;

namespace Sub.Configuration.RolesConfig
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            var admin = new User
            {
                Id = Guid.Parse("1E1C54F5-7105-4582-B8A8-AB88EDA4B7DB").ToString(),
                Email = "obergannikita@gmail.com",
                NormalizedEmail = "OBERGANNIKITA@GMAIL.COM",
                UserName = "Admin",
                NormalizedUserName = "ADMIN",
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var adminPasswordHash = PasswordHash(admin, "2752985BBnn!");
            admin.PasswordHash = adminPasswordHash;
            builder.HasData(admin);

            var member = new User
            {
                Id = Guid.Parse("64CA7758-4F3D-47B1-87F6-3B3EBF20FFE8").ToString(),
                Email = "obergannikita2@gmail.com",
                NormalizedEmail = "OBERGANNIKITA2@GMAIL.COM",
                UserName = "TestMember",
                NormalizedUserName = "TESTMEMBER",
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var memberPasswordHash = PasswordHash(member, "2752985BBnn!");
            member.PasswordHash = memberPasswordHash;
            builder.HasData(member);
        }

        private string PasswordHash(User user, string password)
        {
            var passwordHasher = new PasswordHasher<User>();
            return passwordHasher.HashPassword(user, password);
        }
    }
}
