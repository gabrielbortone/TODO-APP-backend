using TODO.Api.Application.Services;
using TODO.Api.Application.UseCases.Categories;
using TODO.Api.Application.UseCases.TodoItems;
using TODO.Api.Application.UseCases.Users;

namespace TODO.Api.Configuration
{
    public static class ServicesRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<ITokenService, TokenService>();

            services.AddScoped<ILoginUserUseCase, LoginUserUseCase>();
            services.AddScoped<IRegisterNewUserUseCase, RegisterNewUserUseCase>();
            services.AddScoped<IDeleteUserUseCase, DeleteUserUseCase>();
            services.AddScoped<IUpdateUserUseCase, UpdateUserUseCase>();
            services.AddScoped<IGetUserUseCase, GetUserUseCase>();

            services.AddScoped<IGetCategoriesUseCase, GetCategoriesUseCase>();

            services.AddScoped<ICreateToDoItemUseCase, CreateToDoItemUseCase>();
            services.AddScoped<IGetTodoUserCase, GetTodoUserCase>();
            services.AddScoped<IUpdateTodoItemUseCase, UpdateTodoItemUseCase>();
            services.AddScoped<IDeleteTodoItemUseCase, DeleteTodoItemUseCase>();


            return services;
        }
    }
}
