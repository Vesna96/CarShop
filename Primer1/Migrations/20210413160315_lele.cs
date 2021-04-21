using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Primer1.Migrations
{
    public partial class lele : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Automobil",
                columns: table => new
                {
                    AutomobilId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Marka = table.Column<string>(maxLength: 30, nullable: false),
                    Model = table.Column<string>(maxLength: 30, nullable: false),
                    Godiste = table.Column<DateTime>(nullable: false),
                    ZapreminaMotora = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Snaga = table.Column<string>(maxLength: 30, nullable: false),
                    Gorivo = table.Column<string>(maxLength: 30, nullable: false),
                    Karoserija = table.Column<string>(maxLength: 30, nullable: false),
                    Fotografija = table.Column<string>(nullable: true),
                    Opis = table.Column<string>(maxLength: 30, nullable: false),
                    Cena = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Kontakt = table.Column<string>(maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Automobil", x => x.AutomobilId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Automobil");
        }
    }
}
