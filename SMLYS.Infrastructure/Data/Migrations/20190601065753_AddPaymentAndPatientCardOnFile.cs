using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SMLYS.Infrastructure.Data.Migrations
{
    public partial class AddPaymentAndPatientCardOnFile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PatientCardOnFile",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PatientId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    CustomerCode = table.Column<string>(maxLength: 150, nullable: true),
                    CardToken = table.Column<string>(maxLength: 150, nullable: true),
                    CardF4L4 = table.Column<string>(maxLength: 8, nullable: true),
                    Description = table.Column<string>(nullable: true),
                    UpdatedDateUtc = table.Column<DateTime>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    UpdatedBy = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientCardOnFile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientCardOnFile_Patient_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatientCardOnFile_SiteUser_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "SiteUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PaymentTypeId = table.Column<int>(nullable: false),
                    PaymentMethodTypeId = table.Column<int>(nullable: false),
                    PaymentStatusTypeId = table.Column<int>(nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    AuthorizationCode = table.Column<string>(maxLength: 50, nullable: true),
                    TransactionId = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    CardToken = table.Column<string>(maxLength: 150, nullable: true),
                    CardF4L4 = table.Column<string>(nullable: true),
                    PaymentDate = table.Column<DateTime>(nullable: false),
                    UpdatedDateUtc = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<int>(nullable: false),
                    ClinicId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payment_Clinic_ClinicId",
                        column: x => x.ClinicId,
                        principalTable: "Clinic",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Payment_SiteUser_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "SiteUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InvoicePayment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    InvoiceId = table.Column<int>(nullable: false),
                    PaymentId = table.Column<int>(nullable: false),
                    AmountPaid = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    Note = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoicePayment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoicePayment_Invoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvoicePayment_Payment_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "Payment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvoicePayment_InvoiceId",
                table: "InvoicePayment",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoicePayment_PaymentId",
                table: "InvoicePayment",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientCardOnFile_PatientId",
                table: "PatientCardOnFile",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientCardOnFile_UpdatedBy",
                table: "PatientCardOnFile",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_ClinicId",
                table: "Payment",
                column: "ClinicId");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_UpdatedBy",
                table: "Payment",
                column: "UpdatedBy");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvoicePayment");

            migrationBuilder.DropTable(
                name: "PatientCardOnFile");

            migrationBuilder.DropTable(
                name: "Payment");
        }
    }
}
