using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoneyPro2.API.Migrations
{
    /// <inheritdoc />
    public partial class InstitutionType_Class : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TipoInstituicao",
                columns: table => new
                {
                    TipoInstituicaoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "INT", nullable: false),
                    Apelido = table.Column<string>(type: "NVARCHAR(40)", maxLength: 40, nullable: false),
                    Descricao = table.Column<string>(type: "NVARCHAR(100)", maxLength: 100, nullable: false),
                    Ativo = table.Column<bool>(type: "BIT", nullable: false, defaultValueSql: "1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoInstituicao", x => x.TipoInstituicaoId);
                    table.ForeignKey(
                        name: "FK_TipoInstituicao_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TipoInstituicao_UserId_Apelido",
                table: "TipoInstituicao",
                columns: new[] { "UserId", "Apelido" },
                unique: true)
                .Annotation("SqlServer:Clustered", false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TipoInstituicao");
        }
    }
}
