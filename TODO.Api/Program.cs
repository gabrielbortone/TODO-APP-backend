using Microsoft.EntityFrameworkCore;
using TODO.Api;
using TODO.Api.Configuration;
using TODO.Api.Infra.Context;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

builder.Services.AddAppSettings(configuration);
builder.Services.AddInfraData(configuration);
builder.Services.AddApplicationServices();
builder.Services.AddAuthConfiguration(configuration);

var app = builder.Build();

app.UseRouting();
app.AddAuthAppConfiguration(configuration);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<TodoItemDbContext>();
    db.Database.Migrate();
}


app.UseHttpsRedirection();

app.MapRoutes();

app.Run();

