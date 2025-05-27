using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TODO.Api.Application.DTOs;
using TODO.Api.Application.UseCases.Users;

namespace TODO.Api
{
    public static class Routes
    {
        public static void MapRoutes(this WebApplication app)
        {
            app.MapPost("/login", async (
                [FromServices] ILoginUserUseCase useCase, 
                [FromBody] LoginRequestDto request) =>
            {
                 var (validationResult , tokenResult) = await useCase.Process(request.UserName, request.Password);
            
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

                return Results.Created($"/users/{userResume.IdentityUserId}", userResume );
            }).WithName("Register new user")
                .WithOpenApi();


            app.MapPut("/users/{id}", async (
                [FromRoute] string id, 
                [FromBody] UpdateUserDto request ,
                [FromServices] IUpdateUserUseCase useCase,
                ClaimsPrincipal claimsPrincipal) =>
            {
                var validationResult = new FinalValidationResultDto();

                var userId = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(id))
                {
                    validationResult.AddError("Id", "The id in the route and the id in the body must be the same.","EmptyId");
                    return Results.BadRequest(validationResult);
                }
                
                if(userId != id)
                {
                    validationResult.AddError("Id", "The id in the route and the id in the body must be the same.","DiferentId");
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
