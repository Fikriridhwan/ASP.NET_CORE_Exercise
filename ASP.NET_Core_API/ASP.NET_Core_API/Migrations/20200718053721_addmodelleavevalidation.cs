using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ASP.NET_Core_API.Migrations
{
    public partial class addmodelleavevalidation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_R_LeaveValidation",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Action = table.Column<string>(nullable: true),
                    ValidDuration = table.Column<int>(nullable: false),
                    ManagerId = table.Column<int>(nullable: true),
                    LeaveApplicationId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_R_LeaveValidation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TB_R_LeaveValidation_TB_R_LeaveApplication_LeaveApplicationId",
                        column: x => x.LeaveApplicationId,
                        principalTable: "TB_R_LeaveApplication",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TB_R_LeaveValidation_TB_R_Manager_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "TB_R_Manager",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_R_LeaveValidation_LeaveApplicationId",
                table: "TB_R_LeaveValidation",
                column: "LeaveApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_R_LeaveValidation_ManagerId",
                table: "TB_R_LeaveValidation",
                column: "ManagerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_R_LeaveValidation");
        }
    }
}
