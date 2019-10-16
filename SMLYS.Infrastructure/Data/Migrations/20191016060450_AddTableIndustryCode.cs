using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SMLYS.Infrastructure.Data.Migrations
{
    public partial class AddTableIndustryCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IndustryCode",
                table: "Item");

            migrationBuilder.AddColumn<int>(
                name: "IndustryCodeId",
                table: "Item",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "IndustryCode",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 150, nullable: true),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndustryCode", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Item_IndustryCodeId",
                table: "Item",
                column: "IndustryCodeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Item_IndustryCode",
                table: "Item",
                column: "IndustryCodeId",
                principalTable: "IndustryCode",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_IndustryCode",
                table: "Item");

            migrationBuilder.DropTable(
                name: "IndustryCode");

            migrationBuilder.DropIndex(
                name: "IX_Item_IndustryCodeId",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "IndustryCodeId",
                table: "Item");

            migrationBuilder.AddColumn<string>(
                name: "IndustryCode",
                table: "Item",
                maxLength: 150,
                nullable: true);
        }
    }
}
