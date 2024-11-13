using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeTai4.Reponsitories.Migrations
{
    /// <inheritdoc />
    public partial class AddCustomerIdToInvoice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Invoice",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_CustomerId",
                table: "Invoice",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoice_Customer_CustomerId",
                table: "Invoice",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoice_Customer_CustomerId",
                table: "Invoice");

            migrationBuilder.DropIndex(
                name: "IX_Invoice_CustomerId",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Invoice");
        }
    }
}
