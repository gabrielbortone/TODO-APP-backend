using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TODO.Api.Infra.Context;

namespace TODO.Api.Configuration
{
    public static class InfraDataRegistration
    {
        public static IServiceCollection AddInfraData(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TodoItemDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<TodoItemDbContext>()
                .AddDefaultTokenProviders();
            
            return services;
        }
    }
}
