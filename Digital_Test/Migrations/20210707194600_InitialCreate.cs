using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Digital_Test.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id_Cliente = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id_Cliente);
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    Id_Producto = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(nullable: true),
                    Precio = table.Column<decimal>(nullable: false),
                    Costo = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.Id_Producto);
                });

            migrationBuilder.CreateTable(
                name: "Facturas",
                columns: table => new
                {
                    Id_Factura = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateTime>(nullable: false),
                    Id_Cliente = table.Column<int>(nullable: false),
                    Total = table.Column<decimal>(nullable: false),
                    ClienteId_Cliente = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facturas", x => x.Id_Factura);
                    table.ForeignKey(
                        name: "FK_Facturas_Clientes_ClienteId_Cliente",
                        column: x => x.ClienteId_Cliente,
                        principalTable: "Clientes",
                        principalColumn: "Id_Cliente",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Detalle_Facturas",
                columns: table => new
                {
                    Id_DetalleFact = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_Factura = table.Column<int>(nullable: false),
                    Cantidad = table.Column<int>(nullable: false),
                    Precio_Unitario = table.Column<decimal>(nullable: false),
                    Id_Producto = table.Column<int>(nullable: false),
                    FacturaId_Factura = table.Column<int>(nullable: true),
                    ProductoId_Producto = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Detalle_Facturas", x => x.Id_DetalleFact);
                    table.ForeignKey(
                        name: "FK_Detalle_Facturas_Facturas_FacturaId_Factura",
                        column: x => x.FacturaId_Factura,
                        principalTable: "Facturas",
                        principalColumn: "Id_Factura",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Detalle_Facturas_Productos_ProductoId_Producto",
                        column: x => x.ProductoId_Producto,
                        principalTable: "Productos",
                        principalColumn: "Id_Producto",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Detalle_Facturas_FacturaId_Factura",
                table: "Detalle_Facturas",
                column: "FacturaId_Factura");

            migrationBuilder.CreateIndex(
                name: "IX_Detalle_Facturas_ProductoId_Producto",
                table: "Detalle_Facturas",
                column: "ProductoId_Producto");

            migrationBuilder.CreateIndex(
                name: "IX_Facturas_ClienteId_Cliente",
                table: "Facturas",
                column: "ClienteId_Cliente");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Detalle_Facturas");

            migrationBuilder.DropTable(
                name: "Facturas");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "Clientes");
        }
    }
}
