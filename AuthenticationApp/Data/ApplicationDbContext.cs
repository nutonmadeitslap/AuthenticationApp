using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityUser>(entity =>
            {
                entity.Property(u => u.UserName).HasMaxLength(256);
                entity.Property(u => u.NormalizedUserName).HasMaxLength(256);
                entity.Property(u => u.Email).HasMaxLength(256);
                entity.Property(u => u.NormalizedEmail).HasMaxLength(256);
            });

            builder.Entity<IdentityRole>(entity =>
            {
                entity.Property(r => r.Name).HasMaxLength(256);
                entity.Property(r => r.NormalizedName).HasMaxLength(256);
            });
        }
    }
}
