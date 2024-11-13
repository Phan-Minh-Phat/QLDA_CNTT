using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DeTai4.Reponsitories.Migrations
{
    public partial class ReAddUserIdToProject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Thêm lại cột UserId vào bảng Project
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Project",
                type: "int",
                nullable: true);

            // Tạo lại chỉ mục cho cột UserId
            migrationBuilder.CreateIndex(
                name: "IX_Project_UserId",
                table: "Project",
                column: "UserId");

            // Thêm lại khóa ngoại từ Project tới User
            migrationBuilder.AddForeignKey(
                name: "FK_Project_User_UserId",
                table: "Project",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Xóa khóa ngoại, chỉ mục và cột UserId nếu rollback
            migrationBuilder.DropForeignKey(
                name: "FK_Project_User_UserId",
                table: "Project");

            migrationBuilder.DropIndex(
                name: "IX_Project_UserId",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Project");
        }
    }
}
