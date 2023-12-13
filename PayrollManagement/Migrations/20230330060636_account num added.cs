using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PayrollManagement.Migrations
{
    public partial class accountnumadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "accountNumber",
                table: "PaymentManagement",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "accountNumber",
                table: "PaymentManagement");
        }
    }
}
