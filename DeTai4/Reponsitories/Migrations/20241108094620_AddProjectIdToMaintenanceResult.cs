using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeTai4.Reponsitories.Migrations
{
    public partial class AddProjectIdToMaintenanceResult : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "MaintenanceResult",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceResult_ProjectId",
                table: "MaintenanceResult",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_MaintenanceResult_Project_ProjectId",
                table: "MaintenanceResult",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaintenanceResult_Project_ProjectId",
                table: "MaintenanceResult");

            migrationBuilder.DropIndex(
                name: "IX_MaintenanceResult_ProjectId",
                table: "MaintenanceResult");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "MaintenanceResult");
        }
    }
}
