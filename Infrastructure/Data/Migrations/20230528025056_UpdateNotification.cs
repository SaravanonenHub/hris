using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    public partial class UpdateNotification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Action",
                table: "T_LEAVE",
                newName: "Status");

            migrationBuilder.AlterColumn<int>(
                name: "LeaveId",
                table: "T_LEAVE_ACTIONS",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "T_NOTIFICATION",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TeamId = table.Column<int>(type: "int", nullable: true),
                    TeamRoleId = table.Column<int>(type: "int", nullable: true),
                    EmployeeId = table.Column<int>(type: "int", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    IsActive = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true, defaultValue: "Y"),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_NOTIFICATION", x => x.Id);
                    table.ForeignKey(
                        name: "FK_T_NOTIFICATION_T_EMPLOYEE_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "T_EMPLOYEE",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_T_NOTIFICATION_T_TEAM_ROLE_TeamRoleId",
                        column: x => x.TeamRoleId,
                        principalTable: "T_TEAM_ROLE",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_T_NOTIFICATION_T_TEAM_TeamId",
                        column: x => x.TeamId,
                        principalTable: "T_TEAM",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_T_NOTIFICATION_EmployeeId",
                table: "T_NOTIFICATION",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_T_NOTIFICATION_TeamId",
                table: "T_NOTIFICATION",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_T_NOTIFICATION_TeamRoleId",
                table: "T_NOTIFICATION",
                column: "TeamRoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T_NOTIFICATION");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "T_LEAVE",
                newName: "Action");

            migrationBuilder.AlterColumn<int>(
                name: "LeaveId",
                table: "T_LEAVE_ACTIONS",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
