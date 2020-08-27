using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CmsApp.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    JobTitle = table.Column<string>(nullable: true),
                    FullName = table.Column<string>(nullable: true),
                    Configuration = table.Column<string>(nullable: true),
                    IsEnabled = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Gender = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Admins",
                columns: table => new
                {
                    AdminID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AdminName = table.Column<string>(maxLength: 255, nullable: false),
                    AdminUserName = table.Column<string>(maxLength: 255, nullable: false),
                    AdminPassword = table.Column<string>(maxLength: 255, nullable: false),
                    Premission = table.Column<string>(maxLength: 100, nullable: true),
                    CreateBy = table.Column<string>(maxLength: 100, nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    UpdateBy = table.Column<string>(maxLength: 100, nullable: true),
                    UpdateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_Admins", x => x.AdminID);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_BloodTypes",
                columns: table => new
                {
                    BloodTypeID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BloodTypeName = table.Column<string>(maxLength: 255, nullable: true),
                    BloodtypeDetail = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_BloodTypes", x => x.BloodTypeID);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_ContextEmergencys",
                columns: table => new
                {
                    ContactemergencyID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ContactemergencyFirstname = table.Column<string>(maxLength: 100, nullable: false),
                    ContactemergencyLastname = table.Column<string>(maxLength: 100, nullable: false),
                    PatientIDcard = table.Column<int>(nullable: false),
                    ContactemergencyAddress = table.Column<string>(maxLength: 255, nullable: true),
                    ContatctemergencyTel = table.Column<string>(unicode: false, maxLength: 30, nullable: true),
                    ContactemergencyConcerned = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_ContextEmergencys", x => x.ContactemergencyID);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_DrugAllergys",
                columns: table => new
                {
                    DrugAllergyID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DrugAllergyName = table.Column<string>(maxLength: 100, nullable: false),
                    DrugAllergyDetail = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_DrugAllergys", x => x.DrugAllergyID);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Finances",
                columns: table => new
                {
                    FinanceID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FinanceName = table.Column<string>(maxLength: 150, nullable: false),
                    FinanceIdcard = table.Column<string>(maxLength: 100, nullable: false),
                    FinanceHN = table.Column<string>(maxLength: 50, nullable: false),
                    CreateBy = table.Column<string>(maxLength: 50, nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    Updateby = table.Column<string>(maxLength: 50, nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_Finances", x => x.FinanceID);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Genders",
                columns: table => new
                {
                    GenderID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    GenderName = table.Column<string>(maxLength: 100, nullable: false),
                    CreateBy = table.Column<string>(maxLength: 50, nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    Updateby = table.Column<string>(maxLength: 50, nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_Genders", x => x.GenderID);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Patients",
                columns: table => new
                {
                    PatientIDcard = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PatientHN = table.Column<string>(maxLength: 15, nullable: false),
                    PatientPrefix = table.Column<string>(maxLength: 10, nullable: false),
                    PatientFirstName = table.Column<string>(maxLength: 100, nullable: false),
                    PatientLastName = table.Column<string>(maxLength: 100, nullable: false),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    Age = table.Column<int>(nullable: false),
                    GenderID = table.Column<int>(nullable: false),
                    StatusID = table.Column<int>(nullable: false),
                    Race = table.Column<string>(maxLength: 100, nullable: false),
                    Nationality = table.Column<string>(maxLength: 100, nullable: false),
                    Religion = table.Column<string>(maxLength: 100, nullable: false),
                    PatientAddress = table.Column<string>(maxLength: 100, nullable: false),
                    PatientTel = table.Column<string>(maxLength: 100, nullable: false),
                    PatientEmail = table.Column<string>(maxLength: 100, nullable: false),
                    ContactemergencyID = table.Column<int>(nullable: false),
                    BloodTypeID = table.Column<int>(nullable: false),
                    TreatmentID = table.Column<int>(nullable: false),
                    DrugAllergyID = table.Column<int>(nullable: false),
                    Createby = table.Column<string>(maxLength: 50, nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    UpdateBy = table.Column<string>(maxLength: 50, nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_Patients", x => x.PatientIDcard);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Status",
                columns: table => new
                {
                    StatusID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StatusName = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_Status", x => x.StatusID);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_TreatMent",
                columns: table => new
                {
                    TreatmentID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TreatmentName = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_TreatMent", x => x.TreatmentID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Discount = table.Column<decimal>(nullable: false),
                    Comments = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    CashierId = table.Column<string>(nullable: true),
                    CustomerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_AspNetUsers_CashierId",
                        column: x => x.CashierId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Order_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Order_CashierId",
                table: "Order",
                column: "CashierId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_CustomerId",
                table: "Order",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Admins_AdminName",
                table: "Tbl_Admins",
                column: "AdminName");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Admins_AdminPassword",
                table: "Tbl_Admins",
                column: "AdminPassword");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Admins_AdminUserName",
                table: "Tbl_Admins",
                column: "AdminUserName");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_ContextEmergencys_ContactemergencyFirstname",
                table: "Tbl_ContextEmergencys",
                column: "ContactemergencyFirstname");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_ContextEmergencys_ContactemergencyLastname",
                table: "Tbl_ContextEmergencys",
                column: "ContactemergencyLastname");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_DrugAllergys_DrugAllergyDetail",
                table: "Tbl_DrugAllergys",
                column: "DrugAllergyDetail");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_DrugAllergys_DrugAllergyName",
                table: "Tbl_DrugAllergys",
                column: "DrugAllergyName");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Finances_CreateBy",
                table: "Tbl_Finances",
                column: "CreateBy");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Finances_FinanceHN",
                table: "Tbl_Finances",
                column: "FinanceHN");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Finances_FinanceIdcard",
                table: "Tbl_Finances",
                column: "FinanceIdcard");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Finances_FinanceName",
                table: "Tbl_Finances",
                column: "FinanceName");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Finances_Updateby",
                table: "Tbl_Finances",
                column: "Updateby");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Genders_CreateBy",
                table: "Tbl_Genders",
                column: "CreateBy");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Genders_GenderName",
                table: "Tbl_Genders",
                column: "GenderName");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Genders_Updateby",
                table: "Tbl_Genders",
                column: "Updateby");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Patients_Createby",
                table: "Tbl_Patients",
                column: "Createby");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Patients_Nationality",
                table: "Tbl_Patients",
                column: "Nationality");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Patients_PatientAddress",
                table: "Tbl_Patients",
                column: "PatientAddress");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Patients_PatientEmail",
                table: "Tbl_Patients",
                column: "PatientEmail");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Patients_PatientFirstName",
                table: "Tbl_Patients",
                column: "PatientFirstName");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Patients_PatientHN",
                table: "Tbl_Patients",
                column: "PatientHN");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Patients_PatientLastName",
                table: "Tbl_Patients",
                column: "PatientLastName");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Patients_PatientPrefix",
                table: "Tbl_Patients",
                column: "PatientPrefix");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Patients_PatientTel",
                table: "Tbl_Patients",
                column: "PatientTel");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Patients_Race",
                table: "Tbl_Patients",
                column: "Race");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Patients_Religion",
                table: "Tbl_Patients",
                column: "Religion");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Patients_UpdateBy",
                table: "Tbl_Patients",
                column: "UpdateBy");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Status_StatusName",
                table: "Tbl_Status",
                column: "StatusName");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_TreatMent_TreatmentName",
                table: "Tbl_TreatMent",
                column: "TreatmentName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Tbl_Admins");

            migrationBuilder.DropTable(
                name: "Tbl_BloodTypes");

            migrationBuilder.DropTable(
                name: "Tbl_ContextEmergencys");

            migrationBuilder.DropTable(
                name: "Tbl_DrugAllergys");

            migrationBuilder.DropTable(
                name: "Tbl_Finances");

            migrationBuilder.DropTable(
                name: "Tbl_Genders");

            migrationBuilder.DropTable(
                name: "Tbl_Patients");

            migrationBuilder.DropTable(
                name: "Tbl_Status");

            migrationBuilder.DropTable(
                name: "Tbl_TreatMent");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Customer");
        }
    }
}
