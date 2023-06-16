using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    public partial class UpdateTeamDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_T_EMPLOYEE_T_TEAM_ROLE_TeamRoleId",
                table: "T_EMPLOYEE");

            migrationBuilder.DropForeignKey(
                name: "FK_T_EMPLOYEE_T_TEAM_TeamId",
                table: "T_EMPLOYEE");

            migrationBuilder.DropIndex(
                name: "IX_T_EMPLOYEE_TeamId",
                table: "T_EMPLOYEE");

            migrationBuilder.DropIndex(
                name: "IX_T_EMPLOYEE_TeamRoleId",
                table: "T_EMPLOYEE");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "T_EMPLOYEE");

            migrationBuilder.DropColumn(
                name: "TeamRoleId",
                table: "T_EMPLOYEE");

            migrationBuilder.AddColumn<string>(
                name: "IsActive",
                table: "T_TEAM_DETAILS",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "T_TEAM_DETAILS");

            migrationBuilder.AddColumn<int>(
                name: "TeamId",
                table: "T_EMPLOYEE",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TeamRoleId",
                table: "T_EMPLOYEE",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_T_EMPLOYEE_TeamId",
                table: "T_EMPLOYEE",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_T_EMPLOYEE_TeamRoleId",
                table: "T_EMPLOYEE",
                column: "TeamRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_T_EMPLOYEE_T_TEAM_ROLE_TeamRoleId",
                table: "T_EMPLOYEE",
                column: "TeamRoleId",
                principalTable: "T_TEAM_ROLE",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_T_EMPLOYEE_T_TEAM_TeamId",
                table: "T_EMPLOYEE",
                column: "TeamId",
                principalTable: "T_TEAM",
                principalColumn: "Id");
        }
    }
}
