using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnFileNameInReport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "file_name",
                table: "tb_m_reports",
                type: "nvarchar(max)",
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
                values: new object[] { new DateTime(2023, 6, 24, 14, 2, 15, 171, DateTimeKind.Local).AddTicks(757), new DateTime(2023, 6, 24, 14, 2, 15, 171, DateTimeKind.Local).AddTicks(769) });

            migrationBuilder.UpdateData(
                table: "tb_m_role",
                keyColumn: "guid",
                keyValue: new Guid("f0ed952a-0321-4193-3653-08db73d30b74"),
                columns: new[] { "created_date", "modified_date" },
                values: new object[] { new DateTime(2023, 6, 24, 14, 2, 15, 171, DateTimeKind.Local).AddTicks(771), new DateTime(2023, 6, 24, 14, 2, 15, 171, DateTimeKind.Local).AddTicks(772) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "file_name",
                table: "tb_m_reports");

            migrationBuilder.DropColumn(
                name: "file_type",
                table: "tb_m_reports");

            migrationBuilder.UpdateData(
                table: "tb_m_role",
                keyColumn: "guid",
                keyValue: new Guid("bad2010a-8d51-4eaf-eccb-08db73d114ff"),
                columns: new[] { "created_date", "modified_date" },
                values: new object[] { new DateTime(2023, 6, 24, 12, 13, 30, 504, DateTimeKind.Local).AddTicks(4788), new DateTime(2023, 6, 24, 12, 13, 30, 504, DateTimeKind.Local).AddTicks(4796) });

            migrationBuilder.UpdateData(
                table: "tb_m_role",
                keyColumn: "guid",
                keyValue: new Guid("f0ed952a-0321-4193-3653-08db73d30b74"),
                columns: new[] { "created_date", "modified_date" },
                values: new object[] { new DateTime(2023, 6, 24, 12, 13, 30, 504, DateTimeKind.Local).AddTicks(4798), new DateTime(2023, 6, 24, 12, 13, 30, 504, DateTimeKind.Local).AddTicks(4799) });
        }
    }
}
