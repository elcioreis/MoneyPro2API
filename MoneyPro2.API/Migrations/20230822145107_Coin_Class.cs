using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoneyPro2.API.Migrations
{
    /// <inheritdoc />
    public partial class Coin_Class : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Instituicao_UserId_Apelido",
                table: "Instituicao");

            migrationBuilder.CreateSequence<int>(
                name: "Seq_MoedaID");

            migrationBuilder.CreateTable(
                name: "Moeda",
                columns: table => new
                {
                    MoedaId = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [Seq_MoedaID]"),
                    Apelido = table.Column<string>(type: "VARCHAR(40)", maxLength: 40, nullable: false),
                    Simbolo = table.Column<string>(type: "VARCHAR(10)", maxLength: 10, nullable: false),
                    Padrao = table.Column<bool>(type: "BIT", nullable: false),
                    MoedaVirtual = table.Column<bool>(type: "BIT", nullable: false),
                    BancoCentral = table.Column<int>(type: "INT", nullable: true),
                    Eletronica = table.Column<string>(type: "VARCHAR(10)", maxLength: 10, nullable: true),
                    Observacao = table.Column<string>(type: "NVARCHAR(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Moeda", x => x.MoedaId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Coin_Apelido",
                table: "Moeda",
                column: "Apelido",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Coin_Simbolo",
                table: "Moeda",
                column: "Simbolo",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Moeda");

            migrationBuilder.DropSequence(
                name: "Seq_MoedaID");

            migrationBuilder.CreateIndex(
                name: "IX_Instituicao_UserId_Apelido",
                table: "Instituicao",
                columns: new[] { "UserId", "Apelido" },
                unique: true)
                .Annotation("SqlServer:Clustered", false);
        }
    }
}
