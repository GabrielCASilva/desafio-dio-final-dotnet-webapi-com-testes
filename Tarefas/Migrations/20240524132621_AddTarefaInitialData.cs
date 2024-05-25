using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Tarefas.Migrations
{
    /// <inheritdoc />
    public partial class AddTarefaInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Tarefas",
                columns: new[] { "Id", "DataConclusao", "DataCriacao", "DataInicio", "Descricao", "Status", "Titulo" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2024, 5, 24, 10, 26, 20, 198, DateTimeKind.Local).AddTicks(3879), null, "Descrição da tarefa 1", 0, "Tarefa 1" },
                    { 2, null, new DateTime(2024, 5, 24, 10, 26, 20, 198, DateTimeKind.Local).AddTicks(3894), null, "Descrição da tarefa 2", 0, "Tarefa 2" },
                    { 3, null, new DateTime(2024, 5, 24, 10, 26, 20, 198, DateTimeKind.Local).AddTicks(3896), null, "Descrição da tarefa 3", 0, "Tarefa 3" },
                    { 4, null, new DateTime(2024, 5, 24, 10, 26, 20, 198, DateTimeKind.Local).AddTicks(3897), null, "Descrição da tarefa 4", 0, "Tarefa 4" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tarefas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tarefas",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Tarefas",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Tarefas",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
