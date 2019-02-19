using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SMLYS.Infrastructure.Data.Migrations
{
    public partial class InitTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AddressType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    AddressType = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Iso2 = table.Column<string>(unicode: false, maxLength: 5, nullable: false),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Family",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Family", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceReOccouringType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    ReOccouringName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceReOccouringType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PatientStatus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Status = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Region",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Region", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SiteUserLevel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteUserLevel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Specality",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specality", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tax",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    TaxName = table.Column<string>(maxLength: 30, nullable: false),
                    TaxRate = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    RegionId = table.Column<int>(nullable: true),
                    CountryId = table.Column<int>(nullable: false),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tax", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Patient",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
                    Title = table.Column<string>(maxLength: 50, nullable: false),
                    Gender = table.Column<int>(nullable: false),
                    Age = table.Column<int>(nullable: false),
                    Phone = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Email = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Status = table.Column<int>(nullable: false),
                    CreatedDateUTC = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedDateUTC = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<int>(nullable: false),
                    UpdatedBy = table.Column<int>(nullable: true),
                    Note = table.Column<string>(unicode: false, maxLength: 500, nullable: true),
                    FamilyId = table.Column<int>(nullable: false),
                    AddressId = table.Column<int>(nullable: false),
                    ClinicId = table.Column<int>(nullable: false),
                    PrimaryMember = table.Column<bool>(nullable: false),
                    Minor = table.Column<bool>(nullable: false),
                    DoctorId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patient", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Patient_Family",
                        column: x => x.FamilyId,
                        principalTable: "Family",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Patient_PatientStatus",
                        column: x => x.Status,
                        principalTable: "PatientStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    AddressTypeId = table.Column<int>(nullable: false),
                    Address1 = table.Column<string>(maxLength: 150, nullable: false),
                    Address2 = table.Column<string>(maxLength: 50, nullable: true),
                    City = table.Column<string>(maxLength: 150, nullable: false),
                    RegionId = table.Column<int>(nullable: true),
                    Region = table.Column<string>(maxLength: 50, nullable: true),
                    CountryId = table.Column<int>(nullable: false),
                    AttentionTo = table.Column<string>(maxLength: 150, nullable: true),
                    CreatedDateUTC = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedDateUTC = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<int>(nullable: false),
                    UpdatedBy = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Address_AddressType",
                        column: x => x.AddressTypeId,
                        principalTable: "AddressType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Address_Country",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Address_Region",
                        column: x => x.RegionId,
                        principalTable: "Region",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Doctor",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
                    Title = table.Column<string>(maxLength: 50, nullable: false),
                    Gender = table.Column<int>(nullable: false),
                    Age = table.Column<int>(nullable: false),
                    Phone = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Email = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Active = table.Column<bool>(nullable: false),
                    CreatedDateUTC = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedDateUTC = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<int>(nullable: false),
                    UpdatedBy = table.Column<int>(nullable: true),
                    Note = table.Column<string>(unicode: false, maxLength: 500, nullable: true),
                    ClinicId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DoctorSpecality",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    DoctorId = table.Column<int>(nullable: false),
                    SpecalityId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorSpecality", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoctorSpecality_Doctor",
                        column: x => x.DoctorId,
                        principalTable: "Doctor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DoctorSpecality_Specality",
                        column: x => x.SpecalityId,
                        principalTable: "Specality",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Invoice",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    PatientId = table.Column<int>(nullable: false),
                    DoctorId = table.Column<int>(nullable: false),
                    InvoiceDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Subtotal = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    DiscountTotal = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    TaxTotal = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    AmountPaid = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    InvoiceStatus = table.Column<int>(nullable: false),
                    PaymentStatus = table.Column<int>(nullable: false),
                    Note = table.Column<string>(maxLength: 500, nullable: true),
                    CreatedBy = table.Column<int>(nullable: false),
                    UpdatedDateUTC = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedBy = table.Column<int>(nullable: true),
                    ReOccouring = table.Column<bool>(nullable: false),
                    ClinicId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoice_Doctor",
                        column: x => x.DoctorId,
                        principalTable: "Doctor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Invoice_Patient",
                        column: x => x.PatientId,
                        principalTable: "Patient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceReOccouring",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    InvoiceId = table.Column<int>(nullable: false),
                    InvoiceReOccouringTypeId = table.Column<int>(nullable: false),
                    StartDateUTC = table.Column<DateTime>(type: "datetime", nullable: false),
                    EndDateUTC = table.Column<DateTime>(type: "datetime", nullable: true),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceReOccouring", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceReOccouring_Invoice",
                        column: x => x.InvoiceId,
                        principalTable: "Invoice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InvoiceReOccouring_InvoiceReOccouringType",
                        column: x => x.InvoiceReOccouringTypeId,
                        principalTable: "InvoiceReOccouringType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Item",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 150, nullable: false),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    Cost = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    UpdatedDateUTC = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedBy = table.Column<int>(nullable: false),
                    ClinicId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceItem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    InvoiceId = table.Column<int>(nullable: false),
                    ItemId = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    Subtotal = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    TaxTotal = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    Note = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceItem_Invoice",
                        column: x => x.InvoiceId,
                        principalTable: "Invoice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InvoiceItem_Item",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SiteUser",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
                    Email = table.Column<string>(maxLength: 250, nullable: true),
                    UserId = table.Column<string>(fixedLength: true, maxLength: 128, nullable: false),
                    SiteUserLevelId = table.Column<int>(nullable: false),
                    Active = table.Column<string>(fixedLength: true, maxLength: 10, nullable: false),
                    Note = table.Column<string>(maxLength: 300, nullable: true),
                    ClinicId = table.Column<int>(nullable: false),
                    DoctorId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SiteUser_Doctor",
                        column: x => x.DoctorId,
                        principalTable: "Doctor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SiteUser_SiteUserLevel",
                        column: x => x.SiteUserLevelId,
                        principalTable: "SiteUserLevel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Clinic",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Phone = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Email = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Active = table.Column<bool>(nullable: false),
                    CreatedDateUTC = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedDateUTC = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<int>(nullable: false),
                    UpdatedBy = table.Column<int>(nullable: true),
                    Note = table.Column<string>(unicode: false, maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clinic", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clinic_SiteUser_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "SiteUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Clinic_SiteUser_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "SiteUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Address_AddressTypeId",
                table: "Address",
                column: "AddressTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Address_CountryId",
                table: "Address",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Address_CreatedBy",
                table: "Address",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Address_RegionId",
                table: "Address",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Address_UpdatedBy",
                table: "Address",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Clinic_CreatedBy",
                table: "Clinic",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Clinic_UpdatedBy",
                table: "Clinic",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Doctor_ClinicId",
                table: "Doctor",
                column: "ClinicId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctor_CreatedBy",
                table: "Doctor",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Doctor_UpdatedBy",
                table: "Doctor",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorSpecality_DoctorId",
                table: "DoctorSpecality",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorSpecality_SpecalityId",
                table: "DoctorSpecality",
                column: "SpecalityId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_ClinicId",
                table: "Invoice",
                column: "ClinicId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_CreatedBy",
                table: "Invoice",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_DoctorId",
                table: "Invoice",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_PatientId",
                table: "Invoice",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_UpdatedBy",
                table: "Invoice",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItem_InvoiceId",
                table: "InvoiceItem",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItem_ItemId",
                table: "InvoiceItem",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceReOccouring_InvoiceId",
                table: "InvoiceReOccouring",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceReOccouring_InvoiceReOccouringTypeId",
                table: "InvoiceReOccouring",
                column: "InvoiceReOccouringTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Item_ClinicId",
                table: "Item",
                column: "ClinicId");

            migrationBuilder.CreateIndex(
                name: "IX_Item_UpdatedBy",
                table: "Item",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Patient_AddressId",
                table: "Patient",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Patient_ClinicId",
                table: "Patient",
                column: "ClinicId");

            migrationBuilder.CreateIndex(
                name: "IX_Patient_CreatedBy",
                table: "Patient",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Patient_DoctorId",
                table: "Patient",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Patient_FamilyId",
                table: "Patient",
                column: "FamilyId");

            migrationBuilder.CreateIndex(
                name: "IX_Patient_Status",
                table: "Patient",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Patient_UpdatedBy",
                table: "Patient",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_SiteUser_ClinicId",
                table: "SiteUser",
                column: "ClinicId");

            migrationBuilder.CreateIndex(
                name: "IX_SiteUser_DoctorId",
                table: "SiteUser",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_SiteUser_SiteUserLevelId",
                table: "SiteUser",
                column: "SiteUserLevelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Patient_SiteUser_CreatedBy",
                table: "Patient",
                column: "CreatedBy",
                principalTable: "SiteUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Patient_SiteUser_UpdatedBy",
                table: "Patient",
                column: "UpdatedBy",
                principalTable: "SiteUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Patient_Clinic",
                table: "Patient",
                column: "ClinicId",
                principalTable: "Clinic",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Patient_Doctor",
                table: "Patient",
                column: "DoctorId",
                principalTable: "Doctor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Patient_Address",
                table: "Patient",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Address_SiteUser_CreatedBy",
                table: "Address",
                column: "CreatedBy",
                principalTable: "SiteUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Address_SiteUser_UpdatedBy",
                table: "Address",
                column: "UpdatedBy",
                principalTable: "SiteUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Doctor_SiteUser_CreatedBy",
                table: "Doctor",
                column: "CreatedBy",
                principalTable: "SiteUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Doctor_SiteUser_UpdatedBy",
                table: "Doctor",
                column: "UpdatedBy",
                principalTable: "SiteUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Doctor_Clinic",
                table: "Doctor",
                column: "ClinicId",
                principalTable: "Clinic",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoice_SiteUser_CreatedBy",
                table: "Invoice",
                column: "CreatedBy",
                principalTable: "SiteUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoice_SiteUser_UpdatedBy",
                table: "Invoice",
                column: "UpdatedBy",
                principalTable: "SiteUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoice_Clinic",
                table: "Invoice",
                column: "ClinicId",
                principalTable: "Clinic",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Item_SiteUser_UpdatedBy",
                table: "Item",
                column: "UpdatedBy",
                principalTable: "SiteUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Item_Clinic",
                table: "Item",
                column: "ClinicId",
                principalTable: "Clinic",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SiteUser_Clinic",
                table: "SiteUser",
                column: "ClinicId",
                principalTable: "Clinic",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clinic_SiteUser_CreatedBy",
                table: "Clinic");

            migrationBuilder.DropForeignKey(
                name: "FK_Clinic_SiteUser_UpdatedBy",
                table: "Clinic");

            migrationBuilder.DropForeignKey(
                name: "FK_Doctor_SiteUser_CreatedBy",
                table: "Doctor");

            migrationBuilder.DropForeignKey(
                name: "FK_Doctor_SiteUser_UpdatedBy",
                table: "Doctor");

            migrationBuilder.DropTable(
                name: "DoctorSpecality");

            migrationBuilder.DropTable(
                name: "InvoiceItem");

            migrationBuilder.DropTable(
                name: "InvoiceReOccouring");

            migrationBuilder.DropTable(
                name: "Tax");

            migrationBuilder.DropTable(
                name: "Specality");

            migrationBuilder.DropTable(
                name: "Item");

            migrationBuilder.DropTable(
                name: "Invoice");

            migrationBuilder.DropTable(
                name: "InvoiceReOccouringType");

            migrationBuilder.DropTable(
                name: "Patient");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "Family");

            migrationBuilder.DropTable(
                name: "PatientStatus");

            migrationBuilder.DropTable(
                name: "AddressType");

            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.DropTable(
                name: "Region");

            migrationBuilder.DropTable(
                name: "SiteUser");

            migrationBuilder.DropTable(
                name: "Doctor");

            migrationBuilder.DropTable(
                name: "SiteUserLevel");

            migrationBuilder.DropTable(
                name: "Clinic");
        }
    }
}
