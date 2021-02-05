using Microsoft.EntityFrameworkCore.Migrations;

namespace ClientsManager.WebApi.Migrations
{
    public partial class InitialMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    CPF = table.Column<string>(type: "TEXT", nullable: false),
                    DataNascimento = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Enderecos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Logradouro = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Bairro = table.Column<string>(type: "TEXT", maxLength: 40, nullable: false),
                    Cidade = table.Column<string>(type: "TEXT", maxLength: 40, nullable: false),
                    Estado = table.Column<string>(type: "TEXT", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enderecos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClientesEnderecos",
                columns: table => new
                {
                    ClienteId = table.Column<int>(type: "INTEGER", nullable: false),
                    EnderecoId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientesEnderecos", x => new { x.ClienteId, x.EnderecoId });
                    table.ForeignKey(
                        name: "FK_ClientesEnderecos_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientesEnderecos_Enderecos_EnderecoId",
                        column: x => x.EnderecoId,
                        principalTable: "Enderecos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientesEnderecos_EnderecoId",
                table: "ClientesEnderecos",
                column: "EnderecoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientesEnderecos");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Enderecos");
        }
    }
}
