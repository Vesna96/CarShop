using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Primer1.Migrations
{
    public partial class qq : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fotografija",
                table: "Automobil");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Godiste",
                table: "Automobil",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<byte[]>(
                name: "FajlSlike",
                table: "Automobil",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TipFajla",
                table: "Automobil",
                maxLength: 20,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FajlSlike",
                table: "Automobil");

            migrationBuilder.DropColumn(
                name: "TipFajla",
                table: "Automobil");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Godiste",
                table: "Automobil",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AddColumn<string>(
                name: "Fotografija",
                table: "Automobil",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
