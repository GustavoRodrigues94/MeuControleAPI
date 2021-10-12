using Microsoft.EntityFrameworkCore.Migrations;

namespace MeuControle.Infra.Migrations
{
    public partial class AgoravaiJuro6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LancamentosContas_PlanosContas_CodigoUsuario",
                table: "LancamentosContas");

            migrationBuilder.DropIndex(
                name: "IX_PlanosContas_CodigoUsuario",
                table: "PlanosContas");

            migrationBuilder.CreateIndex(
                name: "IX_LancamentosContas_CodigoPlanoConta",
                table: "LancamentosContas",
                column: "CodigoPlanoConta");

            migrationBuilder.AddForeignKey(
                name: "FK_LancamentosContas_PlanosContas_CodigoPlanoConta",
                table: "LancamentosContas",
                column: "CodigoPlanoConta",
                principalTable: "PlanosContas",
                principalColumn: "Codigo",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LancamentosContas_PlanosContas_CodigoPlanoConta",
                table: "LancamentosContas");

            migrationBuilder.DropIndex(
                name: "IX_LancamentosContas_CodigoPlanoConta",
                table: "LancamentosContas");

            migrationBuilder.CreateIndex(
                name: "IX_PlanosContas_CodigoUsuario",
                table: "PlanosContas",
                column: "CodigoUsuario");

            migrationBuilder.AddForeignKey(
                name: "FK_LancamentosContas_PlanosContas_CodigoUsuario",
                table: "LancamentosContas",
                column: "CodigoUsuario",
                principalTable: "PlanosContas",
                principalColumn: "Codigo",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
