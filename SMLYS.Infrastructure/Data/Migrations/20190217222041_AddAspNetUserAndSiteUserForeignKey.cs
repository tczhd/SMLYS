using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SMLYS.Infrastructure.Data.Migrations
{
    public partial class AddAspNetUserAndSiteUserForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "SiteUser",
                type: "nvarchar(450)",
                fixedLength: true,
                maxLength: 450,
                nullable: false,
                oldClrType: typeof(string),
                oldFixedLength: true,
                oldMaxLength: 128);

            //migrationBuilder.CreateTable(
            //    name: "AspNetUsers",
            //    columns: table => new
            //    {
            //        Id = table.Column<string>(nullable: false),
            //        UserName = table.Column<string>(nullable: true),
            //        NormalizedUserName = table.Column<string>(nullable: true),
            //        Email = table.Column<string>(nullable: true),
            //        NormalizedEmail = table.Column<string>(nullable: true),
            //        EmailConfirmed = table.Column<bool>(nullable: false),
            //        PasswordHash = table.Column<string>(nullable: true),
            //        SecurityStamp = table.Column<string>(nullable: true),
            //        ConcurrencyStamp = table.Column<string>(nullable: true),
            //        PhoneNumber = table.Column<string>(nullable: true),
            //        PhoneNumberConfirmed = table.Column<bool>(nullable: false),
            //        TwoFactorEnabled = table.Column<bool>(nullable: false),
            //        LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
            //        LockoutEnabled = table.Column<bool>(nullable: false),
            //        AccessFailedCount = table.Column<int>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_AspNetUsers", x => x.Id);
            //    });

            migrationBuilder.CreateIndex(
                name: "IX_SiteUser_UserId",
                table: "SiteUser",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SiteUser_AspNetUsers_UserId",
                table: "SiteUser",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SiteUser_AspNetUsers_UserId",
                table: "SiteUser");

            //migrationBuilder.DropTable(
            //    name: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_SiteUser_UserId",
                table: "SiteUser");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "SiteUser",
                fixedLength: true,
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldFixedLength: true,
                oldMaxLength: 450);
        }
    }
}
