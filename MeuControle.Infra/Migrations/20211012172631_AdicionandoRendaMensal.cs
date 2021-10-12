using Microsoft.EntityFrameworkCore.Migrations;

namespace MeuControle.Infra.Migrations
{
    public partial class AdicionandoRendaMensal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "RendaMensal",
                table: "Usuarios",
                type: "decimal(8,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RendaMensal",
                table: "Usuarios");
        }
    }
}
