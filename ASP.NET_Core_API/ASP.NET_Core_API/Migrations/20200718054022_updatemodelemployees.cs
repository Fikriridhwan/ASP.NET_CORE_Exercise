using Microsoft.EntityFrameworkCore.Migrations;

namespace ASP.NET_Core_API.Migrations
{
    public partial class updatemodelemployees : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "annualLeaveRemaining",
                table: "TB_M_Employee",
                newName: "AnnualLeaveRemaining");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AnnualLeaveRemaining",
                table: "TB_M_Employee",
                newName: "annualLeaveRemaining");
        }
    }
}
