using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Tarefas.Migrations
{
    /// <inheritdoc />
    public partial class AddPessoaInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Pessoas",
                columns: new[] { "Id", "Nome" },
                values: new object[,]
                {
                    { 1, "Pedro Rodrigo da Silva" },
                    { 2, "Matheus Henrique da Silva" },
                    { 3, "Mariana Aparecida de Jesus" },
                    { 4, "Jessica Peixoto de Souza" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Pessoas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Pessoas",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Pessoas",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Pessoas",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
