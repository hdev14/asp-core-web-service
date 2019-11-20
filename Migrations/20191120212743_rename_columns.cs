using Microsoft.EntityFrameworkCore.Migrations;

namespace web_service.Migrations
{
    public partial class rename_columns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pelada_Sport_SportId",
                table: "Pelada");

            migrationBuilder.DropForeignKey(
                name: "FK_Pelada_User_UsuarioId",
                table: "Pelada");

            migrationBuilder.DropIndex(
                name: "IX_Pelada_UsuarioId",
                table: "Pelada");

            migrationBuilder.DropColumn(
                name: "Nome",
                table: "Team");

            migrationBuilder.DropColumn(
                name: "Descricao",
                table: "Pelada");

            migrationBuilder.DropColumn(
                name: "EsporteId",
                table: "Pelada");

            migrationBuilder.DropColumn(
                name: "Local",
                table: "Pelada");

            migrationBuilder.DropColumn(
                name: "Titulo",
                table: "Pelada");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Pelada");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Team",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SportId",
                table: "Pelada",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Pelada",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Place",
                table: "Pelada",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Pelada",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Pelada",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Pelada_UserId",
                table: "Pelada",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pelada_Sport_SportId",
                table: "Pelada",
                column: "SportId",
                principalTable: "Sport",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pelada_User_UserId",
                table: "Pelada",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pelada_Sport_SportId",
                table: "Pelada");

            migrationBuilder.DropForeignKey(
                name: "FK_Pelada_User_UserId",
                table: "Pelada");

            migrationBuilder.DropIndex(
                name: "IX_Pelada_UserId",
                table: "Pelada");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Team");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Pelada");

            migrationBuilder.DropColumn(
                name: "Place",
                table: "Pelada");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Pelada");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Pelada");

            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "Team",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SportId",
                table: "Pelada",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "Descricao",
                table: "Pelada",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EsporteId",
                table: "Pelada",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Local",
                table: "Pelada",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Titulo",
                table: "Pelada",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "Pelada",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Pelada_UsuarioId",
                table: "Pelada",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pelada_Sport_SportId",
                table: "Pelada",
                column: "SportId",
                principalTable: "Sport",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pelada_User_UsuarioId",
                table: "Pelada",
                column: "UsuarioId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
