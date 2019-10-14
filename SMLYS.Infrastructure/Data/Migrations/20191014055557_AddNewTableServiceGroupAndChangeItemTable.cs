using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SMLYS.Infrastructure.Data.Migrations
{
    public partial class AddNewTableServiceGroupAndChangeItemTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IndustryCode",
                table: "Item",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ServiceGroupId",
                table: "Item",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShortCode",
                table: "Item",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Subscription",
                table: "Item",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "ServiceGroup",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 150, nullable: true),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceGroup", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Item_ServiceGroupId",
                table: "Item",
                column: "ServiceGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Item_ServiceGroup",
                table: "Item",
                column: "ServiceGroupId",
                principalTable: "ServiceGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_ServiceGroup",
                table: "Item");

            migrationBuilder.DropTable(
                name: "ServiceGroup");

            migrationBuilder.DropIndex(
                name: "IX_Item_ServiceGroupId",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "IndustryCode",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "ServiceGroupId",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "ShortCode",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "Subscription",
                table: "Item");
        }
    }
}
