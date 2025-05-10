namespace TODO.Api
{
    public static class Routes
    {
        public static void MapRoutes(this WebApplication app)
        {
            app.MapGet("/todoitems", async () =>
            {
                //var items = await repository.GetAllAsync();
                //return Results.Ok(items);
            }).WithName("GetToDoItems")
                .WithOpenApi();

            app.MapPost("/todoitems", async () =>
            {
//                return Results.Created($"/todoitems/{item.Id}", item);
            }).WithName("PostToDoItems")
                .WithOpenApi();
            ;
            app.MapPut("/todoitems/{id}", async (int id) =>
            {

            }).WithName("PutToDoItems")
                .WithOpenApi();
            app.MapDelete("/todoitems/{id}", async (int id) =>
            {

            }).WithName("DeleteToDoItems")
                .WithOpenApi();

        }
    }
}
