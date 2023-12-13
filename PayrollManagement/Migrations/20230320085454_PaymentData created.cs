using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PayrollManagement.Migrations
{
    public partial class PaymentDatacreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PaymentManagement",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeID = table.Column<int>(type: "int", nullable: false),
                    PaymentID = table.Column<int>(type: "int", nullable: false),
                    SalaryPackage = table.Column<double>(type: "float", nullable: false),
                    Hometaken = table.Column<double>(type: "float", nullable: false),
                    TotalEarnings = table.Column<double>(type: "float", nullable: false),
                    CreditAmount = table.Column<double>(type: "float", nullable: false),
                    Bonus = table.Column<double>(type: "float", nullable: false),
                    Dues = table.Column<double>(type: "float", nullable: false),
                    CreditDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentManagement", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentManagement");
        }
    }
}
