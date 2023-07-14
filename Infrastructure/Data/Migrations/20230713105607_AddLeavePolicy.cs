using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    public partial class AddLeavePolicy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EntitleName",
                table: "T_LEAVE_TYPE");

            migrationBuilder.AddColumn<string>(
                name: "LeaveName",
                table: "T_LEAVE_TYPE",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ShortName",
                table: "T_LEAVE_TYPE",
                type: "nvarchar(5)",
                maxLength: 5,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "T_LEAVE_POLICY",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PolicyName = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    ShortName = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    IsActive = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true, defaultValue: "Y"),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_LEAVE_POLICY", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_LEAVE_POLICY_DETAIL",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LeavePolicyId = table.Column<int>(type: "int", nullable: false),
                    LeaveTypeId = table.Column<int>(type: "int", nullable: false),
                    Day = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_LEAVE_POLICY_DETAIL", x => x.Id);
                    table.ForeignKey(
                        name: "FK_T_LEAVE_POLICY_DETAIL_T_LEAVE_POLICY_LeavePolicyId",
                        column: x => x.LeavePolicyId,
                        principalTable: "T_LEAVE_POLICY",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_T_LEAVE_POLICY_DETAIL_T_LEAVE_TYPE_LeaveTypeId",
                        column: x => x.LeaveTypeId,
                        principalTable: "T_LEAVE_TYPE",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_T_LEAVE_POLICY_DETAIL_LeavePolicyId",
                table: "T_LEAVE_POLICY_DETAIL",
                column: "LeavePolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_T_LEAVE_POLICY_DETAIL_LeaveTypeId",
                table: "T_LEAVE_POLICY_DETAIL",
                column: "LeaveTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T_LEAVE_POLICY_DETAIL");

            migrationBuilder.DropTable(
                name: "T_LEAVE_POLICY");

            migrationBuilder.DropColumn(
                name: "LeaveName",
                table: "T_LEAVE_TYPE");

            migrationBuilder.DropColumn(
                name: "ShortName",
                table: "T_LEAVE_TYPE");

            migrationBuilder.AddColumn<string>(
                name: "EntitleName",
                table: "T_LEAVE_TYPE",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
