using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class RemoveTableFile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_tr_file");

            migrationBuilder.AddColumn<byte[]>(
                name: "file_data",
                table: "tb_m_reports",
                type: "varbinary(MAX)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<string>(
                name: "file_name",
                table: "tb_m_reports",
                type: "nvarchar(100)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "file_type",
                table: "tb_m_reports",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "tb_m_role",
                keyColumn: "guid",
                keyValue: new Guid("bad2010a-8d51-4eaf-eccb-08db73d114ff"),
                columns: new[] { "created_date", "modified_date" },
                values: new object[] { new DateTime(2023, 6, 24, 19, 23, 10, 927, DateTimeKind.Local).AddTicks(7574), new DateTime(2023, 6, 24, 19, 23, 10, 927, DateTimeKind.Local).AddTicks(7584) });

            migrationBuilder.UpdateData(
                table: "tb_m_role",
                keyColumn: "guid",
                keyValue: new Guid("f0ed952a-0321-4193-3653-08db73d30b74"),
                columns: new[] { "created_date", "modified_date" },
                values: new object[] { new DateTime(2023, 6, 24, 19, 23, 10, 927, DateTimeKind.Local).AddTicks(7587), new DateTime(2023, 6, 24, 19, 23, 10, 927, DateTimeKind.Local).AddTicks(7587) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "file_data",
                table: "tb_m_reports");

            migrationBuilder.DropColumn(
                name: "file_name",
                table: "tb_m_reports");

            migrationBuilder.DropColumn(
                name: "file_type",
                table: "tb_m_reports");

            migrationBuilder.CreateTable(
                name: "tb_tr_file",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    file_data = table.Column<byte[]>(type: "varbinary(MAX)", nullable: false),
                    file_name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    file_type = table.Column<int>(type: "int", nullable: false),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_tr_file", x => x.guid);
                    table.ForeignKey(
                        name: "FK_tb_tr_file_tb_m_reports_guid",
                        column: x => x.guid,
                        principalTable: "tb_m_reports",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "tb_m_role",
                keyColumn: "guid",
                keyValue: new Guid("bad2010a-8d51-4eaf-eccb-08db73d114ff"),
                columns: new[] { "created_date", "modified_date" },
                values: new object[] { new DateTime(2023, 6, 24, 14, 44, 54, 394, DateTimeKind.Local).AddTicks(4104), new DateTime(2023, 6, 24, 14, 44, 54, 394, DateTimeKind.Local).AddTicks(4113) });

            migrationBuilder.UpdateData(
                table: "tb_m_role",
                keyColumn: "guid",
                keyValue: new Guid("f0ed952a-0321-4193-3653-08db73d30b74"),
                columns: new[] { "created_date", "modified_date" },
                values: new object[] { new DateTime(2023, 6, 24, 14, 44, 54, 394, DateTimeKind.Local).AddTicks(4116), new DateTime(2023, 6, 24, 14, 44, 54, 394, DateTimeKind.Local).AddTicks(4116) });
        }
    }
}
