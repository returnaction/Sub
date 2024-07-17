using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sub.Models.Entities.User.Roles;

namespace Sub.Configuration.RolesConfig
{
    public class UserRoleConfig : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.HasData(
                new UserRole
                {
                    UserId = Guid.Parse("1E1C54F5-7105-4582-B8A8-AB88EDA4B7DB").ToString(),
                    RoleId = Guid.Parse("5EDF8313-66DD-448B-A111-E7058AAF6BD6").ToString()

                },
                new UserRole
                {
                    UserId = Guid.Parse("64CA7758-4F3D-47B1-87F6-3B3EBF20FFE8").ToString(),
                    RoleId = Guid.Parse("C49E8523-B93C-4872-B2CB-D92F4843C98D").ToString()
                });
        }
    }
}
