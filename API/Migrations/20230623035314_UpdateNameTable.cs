using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateNameTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Reports_guid",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_tb_m_employee_employee_id",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_tb_m_ratings_guid",
                table: "Tasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tasks",
                table: "Tasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reports",
                table: "Reports");

            migrationBuilder.RenameTable(
                name: "Tasks",
                newName: "tb_tr_tasks");

            migrationBuilder.RenameTable(
                name: "Reports",
                newName: "tb_m_reports");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_employee_id",
                table: "tb_tr_tasks",
                newName: "IX_tb_tr_tasks_employee_id");

            migrationBuilder.AlterColumn<string>(
                name: "subject",
                table: "tb_tr_tasks",
                type: "nvarchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "subject(255)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_tr_tasks",
                table: "tb_tr_tasks",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_m_reports",
                table: "tb_m_reports",
                column: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_tr_tasks_tb_m_employee_employee_id",
                table: "tb_tr_tasks",
                column: "employee_id",
                principalTable: "tb_m_employee",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_tr_tasks_tb_m_employee_employee_id",
                table: "tb_tr_tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_tr_tasks_tb_m_ratings_guid",
                table: "tb_tr_tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_tr_tasks_tb_m_reports_guid",
                table: "tb_tr_tasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_tr_tasks",
                table: "tb_tr_tasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_m_reports",
                table: "tb_m_reports");

            migrationBuilder.RenameTable(
                name: "tb_tr_tasks",
                newName: "Tasks");

            migrationBuilder.RenameTable(
                name: "tb_m_reports",
                newName: "Reports");

            migrationBuilder.RenameIndex(
                name: "IX_tb_tr_tasks_employee_id",
                table: "Tasks",
                newName: "IX_Tasks_employee_id");

            migrationBuilder.AlterColumn<string>(
                name: "subject",
                table: "Tasks",
                type: "subject(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tasks",
                table: "Tasks",
                column: "guid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reports",
                table: "Reports",
                column: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Reports_guid",
                table: "Tasks",
                column: "guid",
                principalTable: "Reports",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_tb_m_employee_employee_id",
                table: "Tasks",
                column: "employee_id",
                principalTable: "tb_m_employee",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_tb_m_ratings_guid",
                table: "Tasks",
                column: "guid",
                principalTable: "tb_m_ratings",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
