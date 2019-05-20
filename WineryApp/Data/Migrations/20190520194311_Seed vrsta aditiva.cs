using Microsoft.EntityFrameworkCore.Migrations;

namespace WineryApp.Migrations
{
    public partial class Seedvrstaaditiva : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Spremnik__Podrum__46E78A0C",
                table: "Spremnik");

            migrationBuilder.AlterColumn<int>(
                name: "PodrumId",
                table: "Spremnik",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "KategorijaZadatka",
                columns: new[] { "KategorijaZadatkaId", "ImeKategorije" },
                values: new object[] { 13, "Uzorak za analizu" });

            migrationBuilder.InsertData(
                table: "VrstaAditiva",
                columns: new[] { "VrstaAditivaId", "NazivVrste", "Opis" },
                values: new object[,]
                {
                    { 14, "Pojačivač okusa/arome", null },
                    { 13, "Tvar za sprečavanje zgrudnjavanja", null },
                    { 12, "Učvršćivač", null },
                    { 11, "Tvar za želiranje", null },
                    { 10, "Kvasac", null },
                    { 9, "Tvar za zaslađivanje", null },
                    { 15, "Sladilo", null },
                    { 8, "Zgušnjivač", null },
                    { 6, "Emulgator", null },
                    { 5, "Antioksidans", null },
                    { 4, "Potisni plin", null },
                    { 3, "Regulator kiselosti", null },
                    { 2, "Konzervans", null },
                    { 1, "Bojilo", null },
                    { 7, "Stabilizator", null },
                    { 16, "Modificirani škrob", null }
                });

            migrationBuilder.AddForeignKey(
                name: "FK__Spremnik__Podrum__46E78A0C",
                table: "Spremnik",
                column: "PodrumId",
                principalTable: "Podrum",
                principalColumn: "PodrumId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Spremnik__Podrum__46E78A0C",
                table: "Spremnik");

            migrationBuilder.DeleteData(
                table: "KategorijaZadatka",
                keyColumn: "KategorijaZadatkaId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "VrstaAditiva",
                keyColumn: "VrstaAditivaId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "VrstaAditiva",
                keyColumn: "VrstaAditivaId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "VrstaAditiva",
                keyColumn: "VrstaAditivaId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "VrstaAditiva",
                keyColumn: "VrstaAditivaId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "VrstaAditiva",
                keyColumn: "VrstaAditivaId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "VrstaAditiva",
                keyColumn: "VrstaAditivaId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "VrstaAditiva",
                keyColumn: "VrstaAditivaId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "VrstaAditiva",
                keyColumn: "VrstaAditivaId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "VrstaAditiva",
                keyColumn: "VrstaAditivaId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "VrstaAditiva",
                keyColumn: "VrstaAditivaId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "VrstaAditiva",
                keyColumn: "VrstaAditivaId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "VrstaAditiva",
                keyColumn: "VrstaAditivaId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "VrstaAditiva",
                keyColumn: "VrstaAditivaId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "VrstaAditiva",
                keyColumn: "VrstaAditivaId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "VrstaAditiva",
                keyColumn: "VrstaAditivaId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "VrstaAditiva",
                keyColumn: "VrstaAditivaId",
                keyValue: 16);

            migrationBuilder.AlterColumn<int>(
                name: "PodrumId",
                table: "Spremnik",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK__Spremnik__Podrum__46E78A0C",
                table: "Spremnik",
                column: "PodrumId",
                principalTable: "Podrum",
                principalColumn: "PodrumId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
