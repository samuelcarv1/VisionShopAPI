using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VisionShopAPI.Migrations
{
    public partial class AdicionandoColunaEstoque : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Estoque",
                table: "Oculos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estoque",
                table: "Oculos");
        }
    }
}
