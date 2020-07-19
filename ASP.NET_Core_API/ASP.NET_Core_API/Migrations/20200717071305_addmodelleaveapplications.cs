using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ASP.NET_Core_API.Migrations
{
    public partial class addmodelleaveapplications : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_R_LeaveApplication",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Reason = table.Column<string>(nullable: true),
                    LeaveDuration = table.Column<int>(nullable: false),
                    Action = table.Column<string>(nullable: true),
                    EmployeeId = table.Column<int>(nullable: true),
                    ManagerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_R_LeaveApplication", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TB_R_LeaveApplication_TB_M_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "TB_M_Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TB_R_LeaveApplication_TB_R_Manager_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "TB_R_Manager",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_R_LeaveApplication_EmployeeId",
                table: "TB_R_LeaveApplication",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_R_LeaveApplication_ManagerId",
                table: "TB_R_LeaveApplication",
                column: "ManagerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_R_LeaveApplication");
        }
    }
}
