using Microsoft.EntityFrameworkCore.Migrations;

namespace SMLYS.Infrastructure.Data.Migrations
{
    public partial class AddPostalCodeToTableAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "Address",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "Address");
        }
    }
}
