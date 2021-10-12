using Microsoft.EntityFrameworkCore.Migrations;

namespace MeuControle.Infra.Migrations
{
    public partial class AgoravaiJuro7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_PlanosContas_CodigoUsuario",
                table: "PlanosContas",
                column: "CodigoUsuario");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PlanosContas_CodigoUsuario",
                table: "PlanosContas");
        }
    }
}
