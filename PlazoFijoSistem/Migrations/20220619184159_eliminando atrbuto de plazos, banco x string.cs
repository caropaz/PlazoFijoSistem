using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlazoFijoSistem.Migrations
{
    public partial class eliminandoatrbutodeplazosbancoxstring : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plazos_Bancos_Bancosid",
                table: "Plazos");

            migrationBuilder.DropIndex(
                name: "IX_Plazos_Bancosid",
                table: "Plazos");

            migrationBuilder.DropColumn(
                name: "Bancosid",
                table: "Plazos");

            migrationBuilder.AddColumn<string>(
                name: "Bancos",
                table: "Plazos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bancos",
                table: "Plazos");

            migrationBuilder.AddColumn<int>(
                name: "Bancosid",
                table: "Plazos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Plazos_Bancosid",
                table: "Plazos",
                column: "Bancosid");

            migrationBuilder.AddForeignKey(
                name: "FK_Plazos_Bancos_Bancosid",
                table: "Plazos",
                column: "Bancosid",
                principalTable: "Bancos",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
