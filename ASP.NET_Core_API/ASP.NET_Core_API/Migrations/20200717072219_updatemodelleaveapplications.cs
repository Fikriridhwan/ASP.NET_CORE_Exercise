using Microsoft.EntityFrameworkCore.Migrations;

namespace ASP.NET_Core_API.Migrations
{
    public partial class updatemodelleaveapplications : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_R_LeaveApplication_TB_R_Manager_ManagerId",
                table: "TB_R_LeaveApplication");

            migrationBuilder.DropIndex(
                name: "IX_TB_R_LeaveApplication_ManagerId",
                table: "TB_R_LeaveApplication");

            migrationBuilder.DropColumn(
                name: "Action",
                table: "TB_R_LeaveApplication");

            migrationBuilder.DropColumn(
                name: "ManagerId",
                table: "TB_R_LeaveApplication");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Action",
                table: "TB_R_LeaveApplication",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ManagerId",
                table: "TB_R_LeaveApplication",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TB_R_LeaveApplication_ManagerId",
                table: "TB_R_LeaveApplication",
                column: "ManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_R_LeaveApplication_TB_R_Manager_ManagerId",
                table: "TB_R_LeaveApplication",
                column: "ManagerId",
                principalTable: "TB_R_Manager",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
