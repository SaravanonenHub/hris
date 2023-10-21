using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    public partial class UserMapping : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "T_ROLE_MAPPING",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ReportingRoleId = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    IsActive = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true, defaultValue: "Y"),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_ROLE_MAPPING", x => x.Id);
                    table.ForeignKey(
                        name: "FK_T_ROLE_MAPPING_T_USER_ROLE_ReportingRoleId",
                        column: x => x.ReportingRoleId,
                        principalTable: "T_USER_ROLE",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_T_ROLE_MAPPING_T_USER_ROLE_RoleId",
                        column: x => x.RoleId,
                        principalTable: "T_USER_ROLE",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_T_ROLE_MAPPING_ReportingRoleId",
                table: "T_ROLE_MAPPING",
                column: "ReportingRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_T_ROLE_MAPPING_RoleId",
                table: "T_ROLE_MAPPING",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T_ROLE_MAPPING");
        }
    }
}
