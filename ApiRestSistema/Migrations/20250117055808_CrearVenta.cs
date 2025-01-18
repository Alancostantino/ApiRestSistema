using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiRestSistema.Migrations
{
    /// <inheritdoc />
    public partial class CrearVenta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vehiculos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoVehiculo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Marca = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Modelo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Anio = table.Column<int>(type: "int", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Kilometraje = table.Column<double>(type: "float", nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    FechaIngreso = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehiculos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ventas",
                columns: table => new
                {
                    idVenta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    numeroVenta = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    fechaRegistro = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    idVehiculo = table.Column<int>(type: "int", nullable: false),
                    descripcionVehiculo = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    precioVenta = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    tipoPago = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    cliente = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ventas", x => x.idVenta);
                    table.ForeignKey(
                        name: "FK_Venta_Vehiculo",
                        column: x => x.idVehiculo,
                        principalTable: "Vehiculos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ventas_idVehiculo",
                table: "Ventas",
                column: "idVehiculo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ventas");

            migrationBuilder.DropTable(
                name: "Vehiculos");
        }
    }
}
