using Microsoft.EntityFrameworkCore.Migrations;

namespace Roomies.Migrations
{
    public partial class AddMaxTenantCountInRoomType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaxTenantCount",
                table: "RoomTypes",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxTenantCount",
                table: "RoomTypes");
        }
    }
}
