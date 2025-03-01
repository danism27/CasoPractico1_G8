using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CasoPractico1_G8.Migrations
{
    /// <inheritdoc />
    public partial class actualizando : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VehiculoId",
                table: "Ruta",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VehiculoId",
                table: "Ruta");
        }
    }
}
