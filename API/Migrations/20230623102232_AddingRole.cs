using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class AddingRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "tb_m_role",
                columns: new[] { "guid", "created_date", "modified_date", "name" },
                values: new object[,]
                {
                    { new Guid("bad2010a-8d51-4eaf-eccb-08db73d114ff"), new DateTime(2023, 6, 23, 17, 22, 32, 816, DateTimeKind.Local).AddTicks(3197), new DateTime(2023, 6, 23, 17, 22, 32, 816, DateTimeKind.Local).AddTicks(3216), "employee" },
                    { new Guid("f0ed952a-0321-4193-3653-08db73d30b74"), new DateTime(2023, 6, 23, 17, 22, 32, 816, DateTimeKind.Local).AddTicks(3221), new DateTime(2023, 6, 23, 17, 22, 32, 816, DateTimeKind.Local).AddTicks(3221), "manager" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "tb_m_role",
                keyColumn: "guid",
                keyValue: new Guid("bad2010a-8d51-4eaf-eccb-08db73d114ff"));

            migrationBuilder.DeleteData(
                table: "tb_m_role",
                keyColumn: "guid",
                keyValue: new Guid("f0ed952a-0321-4193-3653-08db73d30b74"));
        }
    }
}
