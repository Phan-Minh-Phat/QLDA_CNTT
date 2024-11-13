using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeTai4.Reponsitories.Migrations
{
    public partial class AddReportAndRemoveUserIdFromProject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Xóa chỉ mục phụ thuộc vào cột UserId
            migrationBuilder.DropIndex(
                name: "IX_Project_UserId",
                table: "Project");

            // Xóa khóa ngoại phụ thuộc vào cột UserId
            migrationBuilder.DropForeignKey(
                name: "FK_Project_User_UserId",
                table: "Project");

            // Xóa cột UserId từ bảng Project
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Project");

            // Tạo bảng Reports
            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    ReportId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReportDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalProjects = table.Column<int>(type: "int", nullable: false),
                    CompletedProjects = table.Column<int>(type: "int", nullable: false),
                    TotalQuotationAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ActivePromotions = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.ReportId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Xóa bảng Reports
            migrationBuilder.DropTable(
                name: "Reports");

            // Thêm lại cột UserId vào bảng Project khi rollback
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Project",
                type: "int",
                nullable: true);

            // Thêm lại khóa ngoại và chỉ mục nếu cần rollback
            migrationBuilder.CreateIndex(
                name: "IX_Project_UserId",
                table: "Project",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Project_User_UserId",
                table: "Project",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
