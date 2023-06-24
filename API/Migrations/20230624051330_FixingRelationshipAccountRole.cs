using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class FixingRelationshipAccountRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_tr_accountrole_tb_m_accounts_guid",
                table: "tb_tr_accountrole");

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

            migrationBuilder.CreateIndex(
                name: "IX_tb_tr_accountrole_account_guid",
                table: "tb_tr_accountrole",
                column: "account_guid");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_tr_accountrole_tb_m_accounts_account_guid",
                table: "tb_tr_accountrole",
                column: "account_guid",
                principalTable: "tb_m_accounts",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_tr_accountrole_tb_m_accounts_account_guid",
                table: "tb_tr_accountrole");

            migrationBuilder.DropIndex(
                name: "IX_tb_tr_accountrole_account_guid",
                table: "tb_tr_accountrole");

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
                name: "FK_tb_tr_accountrole_tb_m_accounts_guid",
                table: "tb_tr_accountrole",
                column: "guid",
                principalTable: "tb_m_accounts",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
