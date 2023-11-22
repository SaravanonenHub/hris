using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    public partial class ActionTemplate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_T_ACTION_HISTORY_T_LEAVE_LeaveId",
                table: "T_ACTION_HISTORY");

            migrationBuilder.RenameColumn(
                name: "Reason",
                table: "T_ACTION_HISTORY",
                newName: "Comment");

            migrationBuilder.RenameColumn(
                name: "LeaveId",
                table: "T_ACTION_HISTORY",
                newName: "RequestId");

            migrationBuilder.RenameIndex(
                name: "IX_T_ACTION_HISTORY_LeaveId",
                table: "T_ACTION_HISTORY",
                newName: "IX_T_ACTION_HISTORY_RequestId");

            migrationBuilder.AddColumn<string>(
                name: "RequestId",
                table: "T_REQUEST",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_T_ACTION_HISTORY_T_REQUEST_RequestId",
                table: "T_ACTION_HISTORY",
                column: "RequestId",
                principalTable: "T_REQUEST",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_T_ACTION_HISTORY_T_REQUEST_RequestId",
                table: "T_ACTION_HISTORY");

            migrationBuilder.DropColumn(
                name: "RequestId",
                table: "T_REQUEST");

            migrationBuilder.RenameColumn(
                name: "RequestId",
                table: "T_ACTION_HISTORY",
                newName: "LeaveId");

            migrationBuilder.RenameColumn(
                name: "Comment",
                table: "T_ACTION_HISTORY",
                newName: "Reason");

            migrationBuilder.RenameIndex(
                name: "IX_T_ACTION_HISTORY_RequestId",
                table: "T_ACTION_HISTORY",
                newName: "IX_T_ACTION_HISTORY_LeaveId");

            migrationBuilder.AddForeignKey(
                name: "FK_T_ACTION_HISTORY_T_LEAVE_LeaveId",
                table: "T_ACTION_HISTORY",
                column: "LeaveId",
                principalTable: "T_LEAVE",
                principalColumn: "Id");
        }
    }
}
