using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TODO.Api.Infra.Context;
using TODO.Api.Infra.Repositories.Abstract;
using TODO.Api.Infra.Repositories.Concrete;

namespace TODO.Api.Configuration
{
    public static class InfraDataRegistration
    {
        public static IServiceCollection AddInfraData(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TodoItemDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<IdentityUser<string>, IdentityRole<string>>()
                .AddEntityFrameworkStores<TodoItemDbContext>()
                .AddDefaultTokenProviders();

            services.AddHealthChecks().AddNpgSql(configuration.GetConnectionString("DefaultConnection"), name: "postgresql");

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ITodoRepository, TodoRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
