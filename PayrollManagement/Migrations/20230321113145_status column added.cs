using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PayrollManagement.Migrations
{
    public partial class statuscolumnadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "PaymentManagement",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "PaymentManagement");
        }
    }
}
