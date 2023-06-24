using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class AddingTableRatingAndAddCardinality : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    subject = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    file = table.Column<byte[]>(type: "varbinary(MAX)", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.guid);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_employee",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    nik = table.Column<string>(type: "char(6)", nullable: false),
                    fullname = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    gender = table.Column<int>(type: "int", nullable: false),
                    email = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    phone_number = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    hiring_date = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    manager_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_employee", x => x.guid);
                    table.ForeignKey(
                        name: "FK_tb_m_employee_tb_m_employee_manager_id",
                        column: x => x.manager_id,
                        principalTable: "tb_m_employee",
                        principalColumn: "guid");
                });

            migrationBuilder.CreateTable(
                name: "tb_m_ratings",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    rating_value = table.Column<double>(type: "float", nullable: false),
                    comment = table.Column<string>(type: "text", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_ratings", x => x.guid);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_role",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_role", x => x.guid);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_accounts",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_accounts", x => x.guid);
                    table.ForeignKey(
                        name: "FK_tb_m_accounts_tb_m_employee_guid",
                        column: x => x.guid,
                        principalTable: "tb_m_employee",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    subject = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    deadline = table.Column<DateTime>(type: "datetime2", nullable: false),
                    employee_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.guid);
                    table.ForeignKey(
                        name: "FK_Tasks_Reports_guid",
                        column: x => x.guid,
                        principalTable: "Reports",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tasks_tb_m_employee_employee_id",
                        column: x => x.employee_id,
                        principalTable: "tb_m_employee",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tasks_tb_m_ratings_guid",
                        column: x => x.guid,
                        principalTable: "tb_m_ratings",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountRoles",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    account_guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    role_guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountRoles", x => x.guid);
                    table.ForeignKey(
                        name: "FK_AccountRoles_tb_m_accounts_guid",
                        column: x => x.guid,
                        principalTable: "tb_m_accounts",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountRoles_tb_m_role_role_guid",
                        column: x => x.role_guid,
                        principalTable: "tb_m_role",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountRoles_role_guid",
                table: "AccountRoles",
                column: "role_guid");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_employee_id",
                table: "Tasks",
                column: "employee_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_employee_manager_id",
                table: "tb_m_employee",
                column: "manager_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_employee_nik_email_phone_number",
                table: "tb_m_employee",
                columns: new[] { "nik", "email", "phone_number" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountRoles");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "tb_m_accounts");

            migrationBuilder.DropTable(
                name: "tb_m_role");

            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.DropTable(
                name: "tb_m_ratings");

            migrationBuilder.DropTable(
                name: "tb_m_employee");
        }
    }
}
