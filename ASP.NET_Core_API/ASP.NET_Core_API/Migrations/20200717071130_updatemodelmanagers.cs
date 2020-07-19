using Microsoft.EntityFrameworkCore.Migrations;

namespace ASP.NET_Core_API.Migrations
{
    public partial class updatemodelmanagers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_R_Manager_TB_M_Employee_employeeId",
                table: "TB_R_Manager");

            migrationBuilder.RenameColumn(
                name: "employeeId",
                table: "TB_R_Manager",
                newName: "EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_TB_R_Manager_employeeId",
                table: "TB_R_Manager",
                newName: "IX_TB_R_Manager_EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_R_Manager_TB_M_Employee_EmployeeId",
                table: "TB_R_Manager",
                column: "EmployeeId",
                principalTable: "TB_M_Employee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_R_Manager_TB_M_Employee_EmployeeId",
                table: "TB_R_Manager");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "TB_R_Manager",
                newName: "employeeId");

            migrationBuilder.RenameIndex(
                name: "IX_TB_R_Manager_EmployeeId",
                table: "TB_R_Manager",
                newName: "IX_TB_R_Manager_employeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_R_Manager_TB_M_Employee_employeeId",
                table: "TB_R_Manager",
                column: "employeeId",
                principalTable: "TB_M_Employee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
