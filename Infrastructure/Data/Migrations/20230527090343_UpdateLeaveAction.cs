using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    public partial class UpdateLeaveAction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "T_LEAVE",
                newName: "Action");

            migrationBuilder.CreateTable(
                name: "T_LEAVE_ACTIONS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LeaveId = table.Column<int>(type: "int", nullable: true),
                    Action = table.Column<int>(type: "int", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActionDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    ActionBy = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_LEAVE_ACTIONS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_T_LEAVE_ACTIONS_T_LEAVE_LeaveId",
                        column: x => x.LeaveId,
                        principalTable: "T_LEAVE",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_T_LEAVE_ACTIONS_LeaveId",
                table: "T_LEAVE_ACTIONS",
                column: "LeaveId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T_LEAVE_ACTIONS");

            migrationBuilder.RenameColumn(
                name: "Action",
                table: "T_LEAVE",
                newName: "Status");
        }
    }
}
