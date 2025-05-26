using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TODO.Api.Domain.Entities;
using TODO.Api.Infra.EntityMapping;

namespace TODO.Api.Infra.Context
{
    public class TodoItemDbContext : IdentityDbContext<IdentityUser<string>, IdentityRole<string>, string>, IDbContextExtension
    {
        public TodoItemDbContext(DbContextOptions<TodoItemDbContext> options)
            : base(options)
        {
        }
        
        public DbSet<TodoItem> TodoItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> ToDoUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.MapIdentity();
            modelBuilder.MapCategory();
            modelBuilder.MapTodoItem();
            modelBuilder.MapUser();
        }

        public async Task<bool> Commit()
        {
            return (await SaveChangesAsync()) > 0;
        }
    }
}
