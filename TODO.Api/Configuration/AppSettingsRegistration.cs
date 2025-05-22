using TODO.Api.Application.AppSettings;

namespace TODO.Api.Configuration
{
    public static class AppSettingsRegistration
    {
        public static IServiceCollection AddAppSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtAuthConfiguration>(configuration.GetSection("AuthConfiguration"));
            services.Configure<MinioSettings>(configuration.GetSection("MinIO"));
            
            return services;
        }
    }
}
