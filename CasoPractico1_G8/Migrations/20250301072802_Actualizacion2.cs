using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CasoPractico1_G8.Migrations
{
    /// <inheritdoc />
    public partial class Actualizacion2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Horario_Ruta_RutaId",
                table: "Horario");

            migrationBuilder.DropForeignKey(
                name: "FK_Parada_Ruta_RutaId",
                table: "Parada");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehiculo_Usuario_UsuarioRegistroId",
                table: "Vehiculo");

            migrationBuilder.DropIndex(
                name: "IX_Parada_RutaId",
                table: "Parada");

            migrationBuilder.DropIndex(
                name: "IX_Horario_RutaId",
                table: "Horario");

            migrationBuilder.DropColumn(
                name: "VehiculoId",
                table: "Ruta");

            migrationBuilder.DropColumn(
                name: "RutaId",
                table: "Parada");

            migrationBuilder.DropColumn(
                name: "RutaId",
                table: "Horario");

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioRegistroId",
                table: "Vehiculo",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Descripcion",
                table: "Parada",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "HoraLlegada",
                table: "Horario",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.CreateTable(
                name: "RutaHorario",
                columns: table => new
                {
                    RutaId = table.Column<int>(type: "int", nullable: false),
                    HorarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RutaHorario", x => new { x.RutaId, x.HorarioId });
                    table.ForeignKey(
                        name: "FK_RutaHorario_Horario_HorarioId",
                        column: x => x.HorarioId,
                        principalTable: "Horario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RutaHorario_Ruta_RutaId",
                        column: x => x.RutaId,
                        principalTable: "Ruta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RutaParada",
                columns: table => new
                {
                    RutaId = table.Column<int>(type: "int", nullable: false),
                    ParadaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RutaParada", x => new { x.RutaId, x.ParadaId });
                    table.ForeignKey(
                        name: "FK_RutaParada_Parada_ParadaId",
                        column: x => x.ParadaId,
                        principalTable: "Parada",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RutaParada_Ruta_RutaId",
                        column: x => x.RutaId,
                        principalTable: "Ruta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RutaHorario_HorarioId",
                table: "RutaHorario",
                column: "HorarioId");

            migrationBuilder.CreateIndex(
                name: "IX_RutaParada_ParadaId",
                table: "RutaParada",
                column: "ParadaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehiculo_Usuario_UsuarioRegistroId",
                table: "Vehiculo",
                column: "UsuarioRegistroId",
                principalTable: "Usuario",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehiculo_Usuario_UsuarioRegistroId",
                table: "Vehiculo");

            migrationBuilder.DropTable(
                name: "RutaHorario");

            migrationBuilder.DropTable(
                name: "RutaParada");

            migrationBuilder.DropColumn(
                name: "Descripcion",
                table: "Parada");

            migrationBuilder.DropColumn(
                name: "HoraLlegada",
                table: "Horario");

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioRegistroId",
                table: "Vehiculo",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VehiculoId",
                table: "Ruta",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RutaId",
                table: "Parada",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RutaId",
                table: "Horario",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Parada_RutaId",
                table: "Parada",
                column: "RutaId");

            migrationBuilder.CreateIndex(
                name: "IX_Horario_RutaId",
                table: "Horario",
                column: "RutaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Horario_Ruta_RutaId",
                table: "Horario",
                column: "RutaId",
                principalTable: "Ruta",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Parada_Ruta_RutaId",
                table: "Parada",
                column: "RutaId",
                principalTable: "Ruta",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehiculo_Usuario_UsuarioRegistroId",
                table: "Vehiculo",
                column: "UsuarioRegistroId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
