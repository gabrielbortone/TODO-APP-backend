using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TODO.Api.Domain.Entities;
using TODO.Api.Infra.EntityMapping;

namespace TODO.Api.Infra.Context
{
    public class TodoItemDbContext : IdentityDbContext
    {
        public TodoItemDbContext(DbContextOptions<TodoItemDbContext> options)
            : base(options)
        {
        }
        
        public DbSet<TodoItem> TodoItems { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.MapCategory();
            modelBuilder.MapTodoItem();
            modelBuilder.MapIdentity();
            modelBuilder.MapUser();
            base.OnModelCreating(modelBuilder);
        }
    }
}
