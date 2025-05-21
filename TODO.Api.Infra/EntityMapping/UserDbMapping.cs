using Microsoft.EntityFrameworkCore;
using TODO.Api.Domain.Entities;

namespace TODO.Api.Infra.EntityMapping
{
    public static class UserDbMapping
    {
        public static void MapUser(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(e =>
            {
                e.ToTable("ToDoUsers");
                e.HasKey(u => u.Id);
                e.Property(u=> u.UserName)
                    .IsRequired()
                    .HasMaxLength(256);
                e.Property(u=> u.NormalizedUserName)
                    .IsRequired()
                    .HasMaxLength(256);
                e.Property(u => u.PictureUrl)
                    .IsRequired(false)
                    .HasMaxLength(1024);
            });
            
        }
    }
}
