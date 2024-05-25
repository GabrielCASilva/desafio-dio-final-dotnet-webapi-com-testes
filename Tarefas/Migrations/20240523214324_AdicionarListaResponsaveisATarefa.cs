using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tarefas.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarListaResponsaveisATarefa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Responsaveis",
                table: "Tarefas");

            migrationBuilder.CreateTable(
                name: "TarefaPessoa",
                columns: table => new
                {
                    PessoaId = table.Column<int>(type: "int", nullable: false),
                    TarefaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TarefaPessoa", x => new { x.PessoaId, x.TarefaId });
                    table.ForeignKey(
                        name: "FK_TarefaPessoa_Pessoas_PessoaId",
                        column: x => x.PessoaId,
                        principalTable: "Pessoas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TarefaPessoa_Tarefas_TarefaId",
                        column: x => x.TarefaId,
                        principalTable: "Tarefas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TarefaPessoa_TarefaId",
                table: "TarefaPessoa",
                column: "TarefaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TarefaPessoa");

            migrationBuilder.AddColumn<string>(
                name: "Responsaveis",
                table: "Tarefas",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
