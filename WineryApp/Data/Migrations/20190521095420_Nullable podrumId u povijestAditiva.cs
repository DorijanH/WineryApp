using Microsoft.EntityFrameworkCore.Migrations;

namespace WineryApp.Migrations
{
    public partial class NullablepodrumIdupovijestAditiva : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "PreostalaKoličina",
                table: "PovijestAditiva",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PodrumId",
                table: "PovijestAditiva",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<decimal>(
                name: "IskorištenaKoličina",
                table: "PovijestAditiva",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PreostalaKoličina",
                table: "PovijestAditiva",
                nullable: true,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PodrumId",
                table: "PovijestAditiva",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IskorištenaKoličina",
                table: "PovijestAditiva",
                nullable: true,
                oldClrType: typeof(decimal),
                oldNullable: true);
        }
    }
}
