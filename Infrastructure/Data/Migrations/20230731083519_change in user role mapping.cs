using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    public partial class changeinuserrolemapping : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_T_ROLE_MAPPING_T_USER_ROLE_ReportingRoleId",
                table: "T_ROLE_MAPPING");

            migrationBuilder.DropForeignKey(
                name: "FK_T_ROLE_MAPPING_T_USER_ROLE_RoleId",
                table: "T_ROLE_MAPPING");

            migrationBuilder.AddForeignKey(
                name: "FK_T_ROLE_MAPPING_T_TEAM_ROLE_ReportingRoleId",
                table: "T_ROLE_MAPPING",
                column: "ReportingRoleId",
                principalTable: "T_TEAM_ROLE",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_T_ROLE_MAPPING_T_TEAM_ROLE_RoleId",
                table: "T_ROLE_MAPPING",
                column: "RoleId",
                principalTable: "T_TEAM_ROLE",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_T_ROLE_MAPPING_T_TEAM_ROLE_ReportingRoleId",
                table: "T_ROLE_MAPPING");

            migrationBuilder.DropForeignKey(
                name: "FK_T_ROLE_MAPPING_T_TEAM_ROLE_RoleId",
                table: "T_ROLE_MAPPING");

            migrationBuilder.AddForeignKey(
                name: "FK_T_ROLE_MAPPING_T_USER_ROLE_ReportingRoleId",
                table: "T_ROLE_MAPPING",
                column: "ReportingRoleId",
                principalTable: "T_USER_ROLE",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_T_ROLE_MAPPING_T_USER_ROLE_RoleId",
                table: "T_ROLE_MAPPING",
                column: "RoleId",
                principalTable: "T_USER_ROLE",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
