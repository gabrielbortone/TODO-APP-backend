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
                e.Property(u=> u.FirstName)
                    .IsRequired()
                    .HasMaxLength(80);
                e.Property(u => u.LastName)
                   .IsRequired()
                   .HasMaxLength(120);
                e.Property(u => u.PictureUrl)
                    .IsRequired(false)
                    .HasMaxLength(1024);
            });
            
        }
    }
}
