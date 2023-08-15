using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoneyPro2.API.Migrations
{
    /// <inheritdoc />
    public partial class Institution_Class : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Instituicao",
                columns: table => new
                {
                    InstituicaoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "INT", nullable: false),
                    TipoInstituicaoId = table.Column<int>(type: "INT", nullable: false),
                    Apelido = table.Column<string>(type: "NVARCHAR(40)", maxLength: 40, nullable: false),
                    Descricao = table.Column<string>(type: "NVARCHAR(100)", maxLength: 100, nullable: false),
                    Ativo = table.Column<bool>(type: "BIT", nullable: false, defaultValueSql: "1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instituicao", x => x.InstituicaoId);
                    table.ForeignKey(
                        name: "FK_Instituicao_TipoInstituicao_TipoInstituicaoId",
                        column: x => x.TipoInstituicaoId,
                        principalTable: "TipoInstituicao",
                        principalColumn: "TipoInstituicaoId");
                    table.ForeignKey(
                        name: "FK_Instituicao_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Instituicao_TipoInstituicaoId",
                table: "Instituicao",
                column: "TipoInstituicaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Instituicao_UserId_Apelido",
                table: "Instituicao",
                columns: new[] { "UserId", "Apelido" },
                unique: true)
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_Instituicao_UserId_TipoInstituicaoId_Apelido",
                table: "Instituicao",
                columns: new[] { "UserId", "TipoInstituicaoId", "Apelido" },
                unique: true)
                .Annotation("SqlServer:Clustered", false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Instituicao");
        }
    }
}
