using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    public partial class TeamDetailsDateColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "IsActive",
                table: "T_TEAM_DETAILS",
                type: "nvarchar(5)",
                maxLength: 5,
                nullable: true,
                defaultValue: "Y",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "T_TEAM_DETAILS",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getdate()");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "T_TEAM_DETAILS",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "T_TEAM_DETAILS",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "T_TEAM_DETAILS",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getdate()");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "T_TEAM_DETAILS");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "T_TEAM_DETAILS");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "T_TEAM_DETAILS");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "T_TEAM_DETAILS");

            migrationBuilder.AlterColumn<string>(
                name: "IsActive",
                table: "T_TEAM_DETAILS",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(5)",
                oldMaxLength: 5,
                oldNullable: true,
                oldDefaultValue: "Y");
        }
    }
}
