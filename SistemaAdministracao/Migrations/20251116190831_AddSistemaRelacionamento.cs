using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SistemaAdministracao.Migrations
{
    /// <inheritdoc />
    public partial class AddSistemaRelacionamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sistemas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Descricao = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sistemas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Papeis",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Descricao = table.Column<string>(type: "text", nullable: true),
                    IdSistema = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Papeis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Papeis_Sistemas_IdSistema",
                        column: x => x.IdSistema,
                        principalTable: "Sistemas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Permissoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Descricao = table.Column<string>(type: "text", nullable: true),
                    IdSistema = table.Column<int>(type: "integer", nullable: false),
                    SistemaId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Permissoes_Sistemas_SistemaId",
                        column: x => x.SistemaId,
                        principalTable: "Sistemas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UsuariosPapeis",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(type: "integer", nullable: false),
                    IdPapel = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuariosPapeis", x => new { x.IdUsuario, x.IdPapel });
                    table.ForeignKey(
                        name: "FK_UsuariosPapeis_Papeis_IdPapel",
                        column: x => x.IdPapel,
                        principalTable: "Papeis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuariosPapeis_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PapeisPermissoes",
                columns: table => new
                {
                    IdPapel = table.Column<int>(type: "integer", nullable: false),
                    IdPermissao = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PapeisPermissoes", x => new { x.IdPapel, x.IdPermissao });
                    table.ForeignKey(
                        name: "FK_PapeisPermissoes_Papeis_IdPapel",
                        column: x => x.IdPapel,
                        principalTable: "Papeis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PapeisPermissoes_Permissoes_IdPermissao",
                        column: x => x.IdPermissao,
                        principalTable: "Permissoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Papeis_IdSistema",
                table: "Papeis",
                column: "IdSistema");

            migrationBuilder.CreateIndex(
                name: "IX_PapeisPermissoes_IdPermissao",
                table: "PapeisPermissoes",
                column: "IdPermissao");

            migrationBuilder.CreateIndex(
                name: "IX_Permissoes_SistemaId",
                table: "Permissoes",
                column: "SistemaId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosPapeis_IdPapel",
                table: "UsuariosPapeis",
                column: "IdPapel");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PapeisPermissoes");

            migrationBuilder.DropTable(
                name: "UsuariosPapeis");

            migrationBuilder.DropTable(
                name: "Permissoes");

            migrationBuilder.DropTable(
                name: "Papeis");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Sistemas");
        }
    }
}
