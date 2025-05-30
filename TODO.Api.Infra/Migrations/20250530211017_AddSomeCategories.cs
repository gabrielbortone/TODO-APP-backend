using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TODO.Api.Infra.Migrations
{
    public partial class AddSomeCategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@$"INSERT INTO public.""Categories"" (""Id"", ""Name"", ""Description"", ""Tags"", ""CreatedAt"", ""IsDeleted"") VALUES
                ('{Guid.NewGuid().ToString()}','Work', 'Tasks related to work', 'work,office,trabalho,escritório,escritorio,trampo', now() at time zone 'utc', false),
                ('{Guid.NewGuid().ToString()}','Personal', 'Personal tasks and errands', 'personal,home,pessoal,casa', now() at time zone 'utc', false),
                ('{Guid.NewGuid().ToString()}','Shopping', 'Items to buy', 'shopping,groceries,compras,comprar', now() at time zone 'utc', false),
                ('{Guid.NewGuid().ToString()}','Health', 'Health and fitness related tasks', 'health,fitness,saúde,saude', now() at time zone 'utc', false),
                ('{Guid.NewGuid().ToString()}','Study', 'Tasks related to study and learning', 'study,learning,estudar,estudando,aprendendo', now() at time zone 'utc', false);");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DELETE FROM public.""Categories"" WHERE ""Name"" IN ('Work', 'Personal', 'Shopping', 'Health', 'Study');");
        }
    }
}
