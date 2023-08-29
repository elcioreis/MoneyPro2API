using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoneyPro2.API.Migrations
{
    /// <inheritdoc />
    public partial class AccountGroup_Class : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_User_CPF",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_Email",
                table: "User");

            migrationBuilder.CreateSequence<int>(
                name: "Seq_GrupoContaID");

            migrationBuilder.AlterColumn<bool>(
                name: "EmailVerificado",
                table: "User",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "User",
                type: "VARCHAR(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "CPF",
                table: "User",
                type: "CHAR(11)",
                maxLength: 11,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "CHAR(11)",
                oldMaxLength: 11);

            migrationBuilder.CreateTable(
                name: "GrupoConta",
                columns: table => new
                {
                    GrupoContaId = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [Seq_GrupoContaID]"),
                    UsuarioId = table.Column<int>(type: "INT", nullable: false),
                    Apelido = table.Column<string>(type: "NVARCHAR(40)", maxLength: 40, nullable: false),
                    Descricao = table.Column<string>(type: "NVARCHAR(100)", maxLength: 100, nullable: false),
                    Ordem = table.Column<int>(type: "INT", nullable: false, defaultValueSql: "0"),
                    Ativo = table.Column<bool>(type: "BIT", nullable: false, defaultValueSql: "1"),
                    Painel = table.Column<bool>(type: "BIT", nullable: false, defaultValueSql: "0"),
                    FluxoDisponivel = table.Column<bool>(type: "BIT", nullable: false, defaultValueSql: "0"),
                    FluxoCredito = table.Column<bool>(type: "BIT", nullable: false, defaultValueSql: "0")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrupoConta", x => x.GrupoContaId);
                    table.ForeignKey(
                        name: "FK_AccountGroup_User_UsuarioID",
                        column: x => x.UsuarioId,
                        principalTable: "User",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_CPF",
                table: "User",
                column: "CPF",
                unique: true,
                filter: "[CPF] IS NOT NULL")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_User_Email",
                table: "User",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_AccountGroup_UsuarioID_Apelido",
                table: "GrupoConta",
                columns: new[] { "UsuarioId", "Apelido" },
                unique: true)
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_AccountGroup_UsuarioID_Ordem",
                table: "GrupoConta",
                columns: new[] { "UsuarioId", "Ordem" })
                .Annotation("SqlServer:Clustered", false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GrupoConta");

            migrationBuilder.DropIndex(
                name: "IX_User_CPF",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_Email",
                table: "User");

            migrationBuilder.DropSequence(
                name: "Seq_GrupoContaID");

            migrationBuilder.AlterColumn<bool>(
                name: "EmailVerificado",
                table: "User",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "User",
                type: "VARCHAR(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "VARCHAR(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CPF",
                table: "User",
                type: "CHAR(11)",
                maxLength: 11,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "CHAR(11)",
                oldMaxLength: 11,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_CPF",
                table: "User",
                column: "CPF",
                unique: true)
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_User_Email",
                table: "User",
                column: "Email",
                unique: true)
                .Annotation("SqlServer:Clustered", false);
        }
    }
}
