using Microsoft.EntityFrameworkCore.Migrations;

namespace _072_HammadArshad_OrderSystem.Migrations
{
    public partial class addtotal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Total",
                table: "OrderModels",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Total",
                table: "OrderModels");
        }
    }
}
