using Microsoft.AspNetCore.Mvc;
using TODO.Api.Application.DTOs;

namespace TODO.Api.Configuration
{
    public static class ModelStateConfiguration
    {
        public static void ConfigureModelState(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var validationResult = new FinalValidationResultDto();

                    foreach(var error in context.ModelState.Values.SelectMany(v => v.Errors))
                    {
                        var propertyName = context.ModelState.FirstOrDefault(x => x.Value.Errors.Contains(error)).Key;
                        var errorMessage = error.ErrorMessage;
                        validationResult.AddError(propertyName, errorMessage, "InvalidModelState");
                    }

                    return (IActionResult)Results.UnprocessableEntity(validationResult);
                };
            });
        }
    }
}
