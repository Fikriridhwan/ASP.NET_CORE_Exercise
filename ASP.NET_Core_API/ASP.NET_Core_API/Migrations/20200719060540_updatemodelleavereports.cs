using Microsoft.EntityFrameworkCore.Migrations;

namespace ASP.NET_Core_API.Migrations
{
    public partial class updatemodelleavereports : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_R_LeaveReport_TB_R_LeaveApplication_LeaveApplicationId",
                table: "TB_R_LeaveReport");

            migrationBuilder.DropIndex(
                name: "IX_TB_R_LeaveReport_LeaveApplicationId",
                table: "TB_R_LeaveReport");

            migrationBuilder.DropColumn(
                name: "LeaveApplicationId",
                table: "TB_R_LeaveReport");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LeaveApplicationId",
                table: "TB_R_LeaveReport",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TB_R_LeaveReport_LeaveApplicationId",
                table: "TB_R_LeaveReport",
                column: "LeaveApplicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_R_LeaveReport_TB_R_LeaveApplication_LeaveApplicationId",
                table: "TB_R_LeaveReport",
                column: "LeaveApplicationId",
                principalTable: "TB_R_LeaveApplication",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
