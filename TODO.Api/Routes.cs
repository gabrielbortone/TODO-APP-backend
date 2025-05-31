using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TODO.Api.Application.DTOs;
using TODO.Api.Application.UseCases.Categories;
using TODO.Api.Application.UseCases.TodoItems;
using TODO.Api.Application.UseCases.Users;

namespace TODO.Api
{
    public static class Routes
    {
        public static void MapRoutes(this WebApplication app)
        {
            app.MapUsersRoutes();
            app.MapCategory();
            app.MapToDoRoutes();
            app.MapHealthCheckRoutes();
        }

        private static void MapToDoRoutes(this WebApplication app)
        {
            app.MapGet("/todos/", async (
                [FromServices] IGetTodoUserCase useCase,
                HttpContext context,
                [FromQuery] string? search = "",
                [FromQuery] string orderBy = "Title",
                [FromQuery] string orderDirection = "asc",
                [FromQuery] int page = 1,
                [FromQuery] int itemsPerPage = 10,
                [FromQuery] int? priority = null,
                [FromQuery] DateTime? dueDate = null,
                [FromQuery] DateTime? finishDate = null,
                [FromQuery] bool? includeCompleted = null) =>
            {

                var queryParameters = new TodoQueryParametersDto
                {
                    Search = search,
                    OrderBy = orderBy,
                    OrderDirection = orderDirection,
                    Page = page,
                    ItemsPerPage = itemsPerPage,
                    Priority = priority,
                    DueDate = dueDate,
                    FinishDate = finishDate,
                    IncludeCompleted = includeCompleted
                };

                var claimsPrincipal = context.User;
                var userId = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var result = await useCase.Process(userId, queryParameters);

                if (result.ValidationResult.IsValid)
                {
                    if (result.Data == null || !result.Data.Any())
                    {
                        result.ValidationResult.AddError("Data", "No To-Do items found.", "NoItemsFound");
                        return Results.NotFound(result.ValidationResult);
                    }
                    return Results.Ok(result);
                }
                else
                {
                    return Results.BadRequest(result.ValidationResult);
                }

            }).RequireAuthorization()
                .WithName("Get all To-Dos")
                .WithOpenApi();

            app.MapPost("/todos/", async (
                [FromServices] ICreateToDoItemUseCase useCase,
                [FromBody] CreateToDoItemRequestDto request,
                HttpContext httpContext) =>
            {
                var claimsPrincipal = httpContext.User;
                var userId = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var (result,validationResult) = await useCase.Process(userId, request);

                if(validationResult.IsValid && result != null)
                {
                    return Results.Created($"/todos/{result.Id}", result);
                }
                else
                {
                    return Results.BadRequest(validationResult);
                }
            }).RequireAuthorization()
                .WithName("Post To-Do")
                .WithOpenApi();

            app.MapPut("/todos/{id}", async ([FromRoute] Guid? id) =>
            {
                return Results.Ok();

            }).RequireAuthorization()
                .WithName("Put To-Do")
                .WithOpenApi();

            app.MapDelete("/todos/{id}", async ([FromRoute] Guid? id) =>
            {
                return Results.Ok();

            }).RequireAuthorization()
                .WithName("Delete To-Do")
                .WithOpenApi();
        }

        private static void MapCategory(this WebApplication app)
        {
            app.MapGet("/categories/", async ([FromServices] IGetCategoriesUseCase useCase) =>
            {
                var result = await useCase.Process();

                if(result.ValidationResult.IsValid && result.Data != null)
                {
                    return Results.Ok(result);
                }
                else
                {
                    return Results.BadRequest(result.ValidationResult);
                }

            }).RequireAuthorization()
                .WithName("Get all Categories")
                .WithOpenApi();
        }

        private static void MapHealthCheckRoutes(this WebApplication app)
        {
            app.MapGet("/health", async (HttpContext context) =>
            {
                var healthCheckService = context.RequestServices.GetRequiredService<Microsoft.Extensions.Diagnostics.HealthChecks.HealthCheckService>();
                var report = await healthCheckService.CheckHealthAsync();

                var status = report.Status.ToString();
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync($"{{\"status\":\"{status}\"}}");
            })
                .WithName("Get Health-Check status")
                .WithOpenApi();
        }

        private static void MapUsersRoutes(this WebApplication app)
        {
            app.MapPost("/login", async (
                [FromServices] ILoginUserUseCase useCase,
                [FromBody] LoginRequestDto request) =>
            {
                var (validationResult, tokenResult) = await useCase.Process(request.UserName, request.Password);

                if (validationResult.IsValid && tokenResult != null)
                {
                    return Results.Ok(tokenResult);
                }
                else
                {
                    return Results.BadRequest(validationResult);
                }
            });

            app.MapGet("/aboutme", async (HttpContext context,
               [FromServices] IGetUserUseCase useCase) =>
            {
                var claimsPrincipal = context.User;
                var userId = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(userId))
                {
                    return Results.Unauthorized();
                }

                var (validationResult, userResume) = await useCase.Process(userId);
                if (!validationResult.IsValid || userResume == null)
                {
                    return Results.BadRequest(validationResult);
                }

                return Results.Ok(userResume);

            })
               .RequireAuthorization()
               .WithName("GetActualUser")
               .WithOpenApi();

            app.MapPost("/users", async (
                [FromServices] IRegisterNewUserUseCase useCase,
                [FromBody] RegisterNewUserDto request) =>
            {
                var (validationResult, userResume) = await useCase.Process(request);
                if (!validationResult.IsValid)
                {
                    return Results.BadRequest(validationResult);
                }

                return Results.Created($"/users/{userResume.IdentityUserId}", userResume);
            }).WithName("Register new user")
                .WithOpenApi();


            app.MapPut("/users/{id}", async (
                [FromRoute] string id,
                [FromBody] UpdateUserDto request,
                [FromServices] IUpdateUserUseCase useCase,
                ClaimsPrincipal claimsPrincipal) =>
            {
                var validationResult = new FinalValidationResultDto();

                var userId = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(id))
                {
                    validationResult.AddError("Id", "The id in the route and the id in the body must be the same.", "EmptyId");
                    return Results.BadRequest(validationResult);
                }

                if (userId != id)
                {
                    validationResult.AddError("Id", "The id in the route and the id in the body must be the same.", "DiferentId");
                    return Results.BadRequest(validationResult);
                }

                request.IdentityId = id;

                validationResult = await useCase.Process(request);
                if (!validationResult.IsValid)
                {
                    return Results.BadRequest(validationResult);
                }

                return Results.NoContent();

            })
                .RequireAuthorization()
                .WithName("Update user")
                .WithOpenApi();


            app.MapDelete("/users/{id}", async (
                [FromRoute] string id,
                [FromServices] IDeleteUserUseCase useCase) =>
            {
                var validationResult = await useCase.Process(id);
                if (!validationResult.IsValid)
                {
                    return Results.BadRequest(validationResult);
                }

                return Results.NoContent();
            })
                .RequireAuthorization()
                .WithName("Remove user")
                .WithOpenApi();
        }
    }
}
