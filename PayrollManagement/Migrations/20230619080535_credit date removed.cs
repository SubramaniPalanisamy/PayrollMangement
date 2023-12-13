using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PayrollManagement.Migrations
{
    public partial class creditdateremoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreditDate",
                table: "PaymentManagement");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreditDate",
                table: "PaymentManagement",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
