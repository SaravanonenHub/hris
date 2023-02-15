using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    public partial class initialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "T_BRANCH",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BranchName = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    IsActive = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true, defaultValue: "Y"),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_BRANCH", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_DESIGNATION",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DesignationName = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    IsActive = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true, defaultValue: "Y"),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_DESIGNATION", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_DIVISION",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DivisionName = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    ShortName = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    IsActive = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true, defaultValue: "Y"),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_DIVISION", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_SHIFT",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShiftName = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    InTime = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    OutTime = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    WorkingHours = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Break = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    HasOT = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false, defaultValue: "N"),
                    HasSatOff = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false, defaultValue: "N"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    InTimeDbl = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    OutTimeDbl = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    IsActive = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true, defaultValue: "Y"),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_SHIFT", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_TEAM_ROLE",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Role = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    HLevel = table.Column<int>(type: "int", nullable: false),
                    HasApprovalAuth = table.Column<string>(type: "char(1)", nullable: false, defaultValue: "N"),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    IsActive = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true, defaultValue: "Y"),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_TEAM_ROLE", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_USER_ROLE",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    IsActive = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true, defaultValue: "Y"),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_USER_ROLE", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_USERLEVEL",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserLevelName = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    IsActive = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true, defaultValue: "Y"),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_USERLEVEL", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_DEPARTMENT",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DivisionId = table.Column<int>(type: "int", nullable: false),
                    DepartmentName = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    ShortName = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    IsActive = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true, defaultValue: "Y"),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_DEPARTMENT", x => x.Id);
                    table.ForeignKey(
                        name: "FK_T_DEPARTMENT_T_DIVISION_DivisionId",
                        column: x => x.DivisionId,
                        principalTable: "T_DIVISION",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "T_TEAM",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeamName = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    IsActive = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true, defaultValue: "Y"),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_TEAM", x => x.Id);
                    table.ForeignKey(
                        name: "FK_T_TEAM_T_DEPARTMENT_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "T_DEPARTMENT",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "T_EMPLOYEE",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    DivisionId = table.Column<int>(type: "int", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    DesignationId = table.Column<int>(type: "int", nullable: false),
                    Qualification = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    BirthDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    JoinDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    EmailID = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    BloodGroup = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    MartialStatus = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    EmployeeNature = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OptionalSaturday = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    TeamId = table.Column<int>(type: "int", nullable: false),
                    TeamRoleId = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    IsActive = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true, defaultValue: "Y"),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_EMPLOYEE", x => x.Id);
                    table.ForeignKey(
                        name: "FK_T_EMPLOYEE_T_BRANCH_BranchId",
                        column: x => x.BranchId,
                        principalTable: "T_BRANCH",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_T_EMPLOYEE_T_DEPARTMENT_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "T_DEPARTMENT",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_T_EMPLOYEE_T_DESIGNATION_DesignationId",
                        column: x => x.DesignationId,
                        principalTable: "T_DESIGNATION",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_T_EMPLOYEE_T_DIVISION_DivisionId",
                        column: x => x.DivisionId,
                        principalTable: "T_DIVISION",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_T_EMPLOYEE_T_TEAM_ROLE_TeamRoleId",
                        column: x => x.TeamRoleId,
                        principalTable: "T_TEAM_ROLE",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_T_EMPLOYEE_T_TEAM_TeamId",
                        column: x => x.TeamId,
                        principalTable: "T_TEAM",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "T_APPUSER",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserRoleId = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    IsActive = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true, defaultValue: "Y"),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_APPUSER", x => x.Id);
                    table.ForeignKey(
                        name: "FK_T_APPUSER_T_EMPLOYEE_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "T_EMPLOYEE",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_T_APPUSER_T_USER_ROLE_UserRoleId",
                        column: x => x.UserRoleId,
                        principalTable: "T_USER_ROLE",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "T_EMPLOYEE_EXPERIANCE",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeID = table.Column<int>(type: "int", nullable: false),
                    PastExp_Year = table.Column<int>(type: "int", nullable: false),
                    PastExp_Month = table.Column<int>(type: "int", nullable: false),
                    CurrentExp_Year = table.Column<int>(type: "int", nullable: false),
                    CurrentExp_Month = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    IsActive = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true, defaultValue: "Y"),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_EMPLOYEE_EXPERIANCE", x => x.Id);
                    table.ForeignKey(
                        name: "FK_T_EMPLOYEE_EXPERIANCE_T_EMPLOYEE_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "T_EMPLOYEE",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "T_EMPLOYEE_PERSONAL",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeID = table.Column<int>(type: "int", nullable: false),
                    FatherName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    MobileNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    PersonalEmailID = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    EmergencyContactPerson = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    EmergenctContactMobile = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    IsActive = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true, defaultValue: "Y"),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_EMPLOYEE_PERSONAL", x => x.Id);
                    table.ForeignKey(
                        name: "FK_T_EMPLOYEE_PERSONAL_T_EMPLOYEE_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "T_EMPLOYEE",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "T_EMPLOYEE_SHIFTS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: true),
                    ShiftID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_EMPLOYEE_SHIFTS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_T_EMPLOYEE_SHIFTS_T_EMPLOYEE_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "T_EMPLOYEE",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_T_EMPLOYEE_SHIFTS_T_SHIFT_ShiftID",
                        column: x => x.ShiftID,
                        principalTable: "T_SHIFT",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "T_TEAM_DETAILS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeamId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_TEAM_DETAILS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_T_TEAM_DETAILS_T_EMPLOYEE_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "T_EMPLOYEE",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_T_TEAM_DETAILS_T_TEAM_ROLE_RoleId",
                        column: x => x.RoleId,
                        principalTable: "T_TEAM_ROLE",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_T_TEAM_DETAILS_T_TEAM_TeamId",
                        column: x => x.TeamId,
                        principalTable: "T_TEAM",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_T_APPUSER_EmployeeId",
                table: "T_APPUSER",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_T_APPUSER_UserRoleId",
                table: "T_APPUSER",
                column: "UserRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_T_DEPARTMENT_DivisionId",
                table: "T_DEPARTMENT",
                column: "DivisionId");

            migrationBuilder.CreateIndex(
                name: "IX_T_EMPLOYEE_BranchId",
                table: "T_EMPLOYEE",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_T_EMPLOYEE_DepartmentId",
                table: "T_EMPLOYEE",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_T_EMPLOYEE_DesignationId",
                table: "T_EMPLOYEE",
                column: "DesignationId");

            migrationBuilder.CreateIndex(
                name: "IX_T_EMPLOYEE_DivisionId",
                table: "T_EMPLOYEE",
                column: "DivisionId");

            migrationBuilder.CreateIndex(
                name: "IX_T_EMPLOYEE_TeamId",
                table: "T_EMPLOYEE",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_T_EMPLOYEE_TeamRoleId",
                table: "T_EMPLOYEE",
                column: "TeamRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_T_EMPLOYEE_EXPERIANCE_EmployeeID",
                table: "T_EMPLOYEE_EXPERIANCE",
                column: "EmployeeID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_T_EMPLOYEE_PERSONAL_EmployeeID",
                table: "T_EMPLOYEE_PERSONAL",
                column: "EmployeeID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_T_EMPLOYEE_SHIFTS_EmployeeId",
                table: "T_EMPLOYEE_SHIFTS",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_T_EMPLOYEE_SHIFTS_ShiftID",
                table: "T_EMPLOYEE_SHIFTS",
                column: "ShiftID");

            migrationBuilder.CreateIndex(
                name: "IX_T_TEAM_DepartmentId",
                table: "T_TEAM",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_T_TEAM_DETAILS_EmployeeId",
                table: "T_TEAM_DETAILS",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_T_TEAM_DETAILS_RoleId",
                table: "T_TEAM_DETAILS",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_T_TEAM_DETAILS_TeamId",
                table: "T_TEAM_DETAILS",
                column: "TeamId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T_APPUSER");

            migrationBuilder.DropTable(
                name: "T_EMPLOYEE_EXPERIANCE");

            migrationBuilder.DropTable(
                name: "T_EMPLOYEE_PERSONAL");

            migrationBuilder.DropTable(
                name: "T_EMPLOYEE_SHIFTS");

            migrationBuilder.DropTable(
                name: "T_TEAM_DETAILS");

            migrationBuilder.DropTable(
                name: "T_USERLEVEL");

            migrationBuilder.DropTable(
                name: "T_USER_ROLE");

            migrationBuilder.DropTable(
                name: "T_SHIFT");

            migrationBuilder.DropTable(
                name: "T_EMPLOYEE");

            migrationBuilder.DropTable(
                name: "T_BRANCH");

            migrationBuilder.DropTable(
                name: "T_DESIGNATION");

            migrationBuilder.DropTable(
                name: "T_TEAM_ROLE");

            migrationBuilder.DropTable(
                name: "T_TEAM");

            migrationBuilder.DropTable(
                name: "T_DEPARTMENT");

            migrationBuilder.DropTable(
                name: "T_DIVISION");
        }
    }
}
