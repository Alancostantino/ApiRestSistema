using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiRestSistema.Migrations
{
    /// <inheritdoc />
    public partial class MigracionInicia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NumeroDocumento",
                columns: table => new
                {
                    idNumeroDocumento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ultimo_Numero = table.Column<int>(type: "int", nullable: false),
                    fechaRegistro = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__NumeroDo__471E421A2AE15B63", x => x.idNumeroDocumento);
                });

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
                    numeroDocumento = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: true),
                    tipoPago = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    fechaRegistro = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    total = table.Column<decimal>(type: "decimal(10,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Venta__077D5614B7613BCA", x => x.idVenta);
                });

            migrationBuilder.CreateTable(
                name: "DetalleVenta",
                columns: table => new
                {
                    idDetalleVenta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idVenta = table.Column<int>(type: "int", nullable: true),
                    idProducto = table.Column<int>(type: "int", nullable: true),
                    Cantidad = table.Column<int>(type: "int", nullable: true),
                    precio = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    total = table.Column<decimal>(type: "decimal(10,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__DetalleV__BFE2843F4E8F137D", x => x.idDetalleVenta);
                    table.ForeignKey(
                        name: "FK__DetalleVe__idPro__239E4DCF",
                        column: x => x.idProducto,
                        principalTable: "Vehiculos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__DetalleVe__idVen__22AA2996",
                        column: x => x.idVenta,
                        principalTable: "Ventas",
                        principalColumn: "idVenta");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DetalleVenta_idProducto",
                table: "DetalleVenta",
                column: "idProducto");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleVenta_idVenta",
                table: "DetalleVenta",
                column: "idVenta");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetalleVenta");

            migrationBuilder.DropTable(
                name: "NumeroDocumento");

            migrationBuilder.DropTable(
                name: "Vehiculos");

            migrationBuilder.DropTable(
                name: "Ventas");
        }
    }
}
