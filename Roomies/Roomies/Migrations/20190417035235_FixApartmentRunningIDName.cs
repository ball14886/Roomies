using Microsoft.EntityFrameworkCore.Migrations;

namespace Roomies.Migrations
{
    public partial class FixApartmentRunningIDName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Apartments",
                newName: "ApartmentID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ApartmentID",
                table: "Apartments",
                newName: "ID");
        }
    }
}
