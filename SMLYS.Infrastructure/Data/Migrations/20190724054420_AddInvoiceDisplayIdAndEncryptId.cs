using Microsoft.EntityFrameworkCore.Migrations;

namespace SMLYS.Infrastructure.Data.Migrations
{
    public partial class AddInvoiceDisplayIdAndEncryptId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DisplayId",
                table: "Invoice",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EncryptId",
                table: "Invoice",
                maxLength: 100,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayId",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "EncryptId",
                table: "Invoice");
        }
    }
}
