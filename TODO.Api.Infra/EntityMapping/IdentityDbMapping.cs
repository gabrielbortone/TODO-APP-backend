using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace TODO.Api.Infra.EntityMapping
{
    public static class IdentityDbMapping
    {
        public static void MapIdentity(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUser<string>>(e =>
            {
                e.ToTable("Users");
            });
            modelBuilder.Entity<IdentityRole<string>>(e =>
            {
                e.ToTable("Roles");
            });
            modelBuilder.Entity<IdentityUserRole<string>>(b =>
            {
                b.ToTable("UserRoles");
            });
            modelBuilder.Entity<IdentityUserClaim<string>>(b =>
            {
                b.ToTable("UserClaims");
            });
            modelBuilder.Entity<IdentityUserLogin<string>>(b =>
            {
                b.ToTable("UserLogins");
            });
            modelBuilder.Entity<IdentityRoleClaim<string>>(b =>
            {
                b.ToTable("RoleClaims");
            });
            modelBuilder.Entity<IdentityUserToken<string>>(b =>
            {
                b.ToTable("UserTokens");
            });


        }
    }
}
