using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    public partial class UpdateModuleLeave : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_T_LEAVE_T_LEAVE_TYPE_LeaveTypeId",
                table: "T_LEAVE");

            migrationBuilder.DropIndex(
                name: "IX_T_LEAVE_LeaveTypeId",
                table: "T_LEAVE");

            migrationBuilder.DropColumn(
                name: "LeaveTypeId",
                table: "T_LEAVE");

            migrationBuilder.AddColumn<string>(
                name: "LeaveType",
                table: "T_LEAVE",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LeaveType",
                table: "T_LEAVE");

            migrationBuilder.AddColumn<int>(
                name: "LeaveTypeId",
                table: "T_LEAVE",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_T_LEAVE_LeaveTypeId",
                table: "T_LEAVE",
                column: "LeaveTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_T_LEAVE_T_LEAVE_TYPE_LeaveTypeId",
                table: "T_LEAVE",
                column: "LeaveTypeId",
                principalTable: "T_LEAVE_TYPE",
                principalColumn: "Id");
        }
    }
}
