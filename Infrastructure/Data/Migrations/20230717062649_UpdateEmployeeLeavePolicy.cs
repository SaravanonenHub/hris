using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    public partial class UpdateEmployeeLeavePolicy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LeavePolicyId",
                table: "T_EMPLOYEE",
                type: "int",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_T_EMPLOYEE_LeavePolicyId",
                table: "T_EMPLOYEE",
                column: "LeavePolicyId");

            migrationBuilder.AddForeignKey(
                name: "FK_T_EMPLOYEE_T_LEAVE_POLICY_LeavePolicyId",
                table: "T_EMPLOYEE",
                column: "LeavePolicyId",
                principalTable: "T_LEAVE_POLICY",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_T_EMPLOYEE_T_LEAVE_POLICY_LeavePolicyId",
                table: "T_EMPLOYEE");

            migrationBuilder.DropIndex(
                name: "IX_T_EMPLOYEE_LeavePolicyId",
                table: "T_EMPLOYEE");

            migrationBuilder.DropColumn(
                name: "LeavePolicyId",
                table: "T_EMPLOYEE");
        }
    }
}
