using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TODO.Api.Infra.Context;

namespace TODO.Api.Configuration
{
    public static class IdentityRegistration
    {
        public static void AddAuthAppConfiguration(this WebApplication app, IConfiguration configuration)
        {
            app.UseAuthentication();
            app.UseAuthorization();
        }


        public static IServiceCollection AddAuthConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
;
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["AuthConfiguration:Issuer"],
                        ValidAudience = configuration["AuthConfiguration:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["AuthConfiguration:Secret"]))
                    };
                });

            services.AddAuthorization();

            return services;
        }
    }
}
