using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlazoFijoSistem.Migrations
{
    public partial class plazos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bancos",
                table: "Plazos");

            migrationBuilder.AddColumn<int>(
                name: "BancoId",
                table: "Plazos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "Plazos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Plazos_BancoId",
                table: "Plazos",
                column: "BancoId");

            migrationBuilder.CreateIndex(
                name: "IX_Plazos_UsuarioId",
                table: "Plazos",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Plazos_Bancos_BancoId",
                table: "Plazos",
                column: "BancoId",
                principalTable: "Bancos",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Plazos_Usuarios_UsuarioId",
                table: "Plazos",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plazos_Bancos_BancoId",
                table: "Plazos");

            migrationBuilder.DropForeignKey(
                name: "FK_Plazos_Usuarios_UsuarioId",
                table: "Plazos");

            migrationBuilder.DropIndex(
                name: "IX_Plazos_BancoId",
                table: "Plazos");

            migrationBuilder.DropIndex(
                name: "IX_Plazos_UsuarioId",
                table: "Plazos");

            migrationBuilder.DropColumn(
                name: "BancoId",
                table: "Plazos");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Plazos");

            migrationBuilder.AddColumn<string>(
                name: "Bancos",
                table: "Plazos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
