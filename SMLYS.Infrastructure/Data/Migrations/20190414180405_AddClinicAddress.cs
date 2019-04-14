using Microsoft.EntityFrameworkCore.Migrations;

namespace SMLYS.Infrastructure.Data.Migrations
{
    public partial class AddClinicAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "Clinic",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Clinic_AddressId",
                table: "Clinic",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clinic_Address_AddressId",
                table: "Clinic",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clinic_Address_AddressId",
                table: "Clinic");

            migrationBuilder.DropIndex(
                name: "IX_Clinic_AddressId",
                table: "Clinic");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Clinic");
        }
    }
}
