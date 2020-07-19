using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ASP.NET_Core_API.Migrations
{
    public partial class addmodelleavereports : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_R_LeaveReport",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ApplicationEntry = table.Column<DateTime>(nullable: false),
                    LeaveValidationId = table.Column<int>(nullable: true),
                    LeaveApplicationId = table.Column<int>(nullable: true),
                    HumanResourceId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_R_LeaveReport", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TB_R_LeaveReport_TB_M_HumanResource_HumanResourceId",
                        column: x => x.HumanResourceId,
                        principalTable: "TB_M_HumanResource",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TB_R_LeaveReport_TB_R_LeaveApplication_LeaveApplicationId",
                        column: x => x.LeaveApplicationId,
                        principalTable: "TB_R_LeaveApplication",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TB_R_LeaveReport_TB_R_LeaveValidation_LeaveValidationId",
                        column: x => x.LeaveValidationId,
                        principalTable: "TB_R_LeaveValidation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_R_LeaveReport_HumanResourceId",
                table: "TB_R_LeaveReport",
                column: "HumanResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_R_LeaveReport_LeaveApplicationId",
                table: "TB_R_LeaveReport",
                column: "LeaveApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_R_LeaveReport_LeaveValidationId",
                table: "TB_R_LeaveReport",
                column: "LeaveValidationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_R_LeaveReport");
        }
    }
}
