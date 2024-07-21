using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Sub.Configuration.RolesConfig;
using Sub.Models.Entities.Company;
using Sub.Models.Entities.Employee;
using Sub.Models.Entities.Invitation;
using Sub.Models.Entities.User.Roles;
using Sub.Models.Entities.User.User;

namespace Sub.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Invitation> Invitations { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new RoleConfig());
            builder.ApplyConfiguration(new UserConfig());
            builder.ApplyConfiguration(new UserRoleConfig());
        }
    }
}
