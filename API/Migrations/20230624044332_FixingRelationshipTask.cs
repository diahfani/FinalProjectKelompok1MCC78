using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class FixingRelationshipTask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_tr_tasks_tb_m_ratings_guid",
                table: "tb_tr_tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_tr_tasks_tb_m_reports_guid",
                table: "tb_tr_tasks");

            migrationBuilder.UpdateData(
                table: "tb_m_role",
                keyColumn: "guid",
                keyValue: new Guid("bad2010a-8d51-4eaf-eccb-08db73d114ff"),
                columns: new[] { "created_date", "modified_date" },
                values: new object[] { new DateTime(2023, 6, 24, 11, 43, 32, 109, DateTimeKind.Local).AddTicks(1963), new DateTime(2023, 6, 24, 11, 43, 32, 109, DateTimeKind.Local).AddTicks(1974) });

            migrationBuilder.UpdateData(
                table: "tb_m_role",
                keyColumn: "guid",
                keyValue: new Guid("f0ed952a-0321-4193-3653-08db73d30b74"),
                columns: new[] { "created_date", "modified_date" },
                values: new object[] { new DateTime(2023, 6, 24, 11, 43, 32, 109, DateTimeKind.Local).AddTicks(1976), new DateTime(2023, 6, 24, 11, 43, 32, 109, DateTimeKind.Local).AddTicks(1976) });

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_ratings_tb_tr_tasks_guid",
                table: "tb_m_ratings",
                column: "guid",
                principalTable: "tb_tr_tasks",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_reports_tb_tr_tasks_guid",
                table: "tb_m_reports",
                column: "guid",
                principalTable: "tb_tr_tasks",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_m_ratings_tb_tr_tasks_guid",
                table: "tb_m_ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_m_reports_tb_tr_tasks_guid",
                table: "tb_m_reports");

            migrationBuilder.UpdateData(
                table: "tb_m_role",
                keyColumn: "guid",
                keyValue: new Guid("bad2010a-8d51-4eaf-eccb-08db73d114ff"),
                columns: new[] { "created_date", "modified_date" },
                values: new object[] { new DateTime(2023, 6, 23, 17, 22, 32, 816, DateTimeKind.Local).AddTicks(3197), new DateTime(2023, 6, 23, 17, 22, 32, 816, DateTimeKind.Local).AddTicks(3216) });

            migrationBuilder.UpdateData(
                table: "tb_m_role",
                keyColumn: "guid",
                keyValue: new Guid("f0ed952a-0321-4193-3653-08db73d30b74"),
                columns: new[] { "created_date", "modified_date" },
                values: new object[] { new DateTime(2023, 6, 23, 17, 22, 32, 816, DateTimeKind.Local).AddTicks(3221), new DateTime(2023, 6, 23, 17, 22, 32, 816, DateTimeKind.Local).AddTicks(3221) });

            migrationBuilder.AddForeignKey(
                name: "FK_tb_tr_tasks_tb_m_ratings_guid",
                table: "tb_tr_tasks",
                column: "guid",
                principalTable: "tb_m_ratings",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_tr_tasks_tb_m_reports_guid",
                table: "tb_tr_tasks",
                column: "guid",
                principalTable: "tb_m_reports",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
