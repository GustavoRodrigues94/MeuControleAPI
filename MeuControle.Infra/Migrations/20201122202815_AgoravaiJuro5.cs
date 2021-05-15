using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MeuControle.Infra.Migrations
{
    public partial class AgoravaiJuro5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Codigo = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    Email = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: true),
                    Senha = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Codigo);
                });

            migrationBuilder.CreateTable(
                name: "PlanosContas",
                columns: table => new
                {
                    Codigo = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CodigoUsuario = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsuarioCodigo = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Nome = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    Operacao = table.Column<string>(type: "varchar(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanosContas", x => x.Codigo);
                    table.ForeignKey(
                        name: "FK_PlanosContas_Usuarios_UsuarioCodigo",
                        column: x => x.UsuarioCodigo,
                        principalTable: "Usuarios",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LancamentosContas",
                columns: table => new
                {
                    Codigo = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CodigoUsuario = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CodigoPlanoConta = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Operacao = table.Column<string>(type: "varchar(1)", nullable: false),
                    DataLancamento = table.Column<DateTime>(type: "date", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(8,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LancamentosContas", x => x.Codigo);
                    table.ForeignKey(
                        name: "FK_LancamentosContas_PlanosContas_CodigoUsuario",
                        column: x => x.CodigoUsuario,
                        principalTable: "PlanosContas",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LancamentosContas_Usuarios_CodigoUsuario",
                        column: x => x.CodigoUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LancamentosContas_CodigoUsuario",
                table: "LancamentosContas",
                column: "CodigoUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_PlanosContas_CodigoUsuario",
                table: "PlanosContas",
                column: "CodigoUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_PlanosContas_UsuarioCodigo",
                table: "PlanosContas",
                column: "UsuarioCodigo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LancamentosContas");

            migrationBuilder.DropTable(
                name: "PlanosContas");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
