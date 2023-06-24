using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateNameTableAccountRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountRoles_tb_m_accounts_guid",
                table: "AccountRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountRoles_tb_m_role_role_guid",
                table: "AccountRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AccountRoles",
                table: "AccountRoles");

            migrationBuilder.RenameTable(
                name: "AccountRoles",
                newName: "tb_tr_accountrole");

            migrationBuilder.RenameIndex(
                name: "IX_AccountRoles_role_guid",
                table: "tb_tr_accountrole",
                newName: "IX_tb_tr_accountrole_role_guid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_tr_accountrole",
                table: "tb_tr_accountrole",
                column: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_tr_accountrole_tb_m_accounts_guid",
                table: "tb_tr_accountrole",
                column: "guid",
                principalTable: "tb_m_accounts",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_tr_accountrole_tb_m_role_role_guid",
                table: "tb_tr_accountrole",
                column: "role_guid",
                principalTable: "tb_m_role",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_tr_accountrole_tb_m_accounts_guid",
                table: "tb_tr_accountrole");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_tr_accountrole_tb_m_role_role_guid",
                table: "tb_tr_accountrole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_tr_accountrole",
                table: "tb_tr_accountrole");

            migrationBuilder.RenameTable(
                name: "tb_tr_accountrole",
                newName: "AccountRoles");

            migrationBuilder.RenameIndex(
                name: "IX_tb_tr_accountrole_role_guid",
                table: "AccountRoles",
                newName: "IX_AccountRoles_role_guid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccountRoles",
                table: "AccountRoles",
                column: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountRoles_tb_m_accounts_guid",
                table: "AccountRoles",
                column: "guid",
                principalTable: "tb_m_accounts",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountRoles_tb_m_role_role_guid",
                table: "AccountRoles",
                column: "role_guid",
                principalTable: "tb_m_role",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
