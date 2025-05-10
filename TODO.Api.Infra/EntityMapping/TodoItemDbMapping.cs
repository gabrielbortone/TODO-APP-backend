using Microsoft.EntityFrameworkCore;
using TODO.Api.Domain.Entities;

namespace TODO.Api.Infra.EntityMapping
{
    public static class TodoItemDbMapping
    {
        public static void MapTodoItem(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TodoItem>(entity =>
            {
                entity.ToTable("TodoItems");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Title).IsRequired().HasMaxLength(200);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Priority).IsRequired();

                entity.Property(e => e.CategoryId).IsRequired();

                entity.Property(e => e.CreatedAt).IsRequired();

                entity.Property(e => e.UpdatedAt).IsRequired();

                entity.Property(e => e.IsDeleted).IsRequired();

                entity.Property(e => e.DueDate).IsRequired(false);

                entity.Property(e => e.FinishDate).IsRequired(false);

                entity.HasOne(e => e.Category)
                    .WithMany(e => e.TodoItems)
                    .HasForeignKey(e => e.CategoryId)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(e => e.User)
                    .WithMany(e => e.TodoItems)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.NoAction);
            });
        }
    }
}
