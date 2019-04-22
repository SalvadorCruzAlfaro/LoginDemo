using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LoginDemo.DAL.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "USUARIOs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Usuario = table.Column<string>(maxLength: 25, nullable: false),
                    Contrasena = table.Column<string>(maxLength: 25, nullable: true),
                    Correo = table.Column<string>(maxLength: 50, nullable: true),
                    Estatus = table.Column<bool>(nullable: false),
                    Sexo = table.Column<int>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USUARIOs", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "USUARIOs");
        }
    }
}
