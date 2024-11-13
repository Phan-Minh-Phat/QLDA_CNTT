using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace DeTai4.Reponsitories.Migrations
{
    public partial class AddQuotationTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Thêm các cột báo giá vào bảng Project
            migrationBuilder.AddColumn<decimal>(
                name: "QuotationAmount",
                table: "Project",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QuotationDetails",
                table: "Project",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "QuotationDate",
                table: "Project",
                nullable: true);

            // Tạo bảng Quotations
            migrationBuilder.CreateTable(
                name: "Quotations",
                columns: table => new
                {
                    QuotationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Details = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quotations", x => x.QuotationId);
                    table.ForeignKey(
                        name: "FK_Quotations_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project", // Tham chiếu đúng tên bảng Project
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Quotations_ProjectId",
                table: "Quotations",
                column: "ProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Xóa các cột báo giá khỏi bảng Project
            migrationBuilder.DropColumn(
                name: "QuotationAmount",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "QuotationDetails",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "QuotationDate",
                table: "Project");

            // Xóa bảng Quotations
            migrationBuilder.DropTable(
                name: "Quotations");
        }
    }
}
