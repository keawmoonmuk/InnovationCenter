using Microsoft.EntityFrameworkCore.Migrations;

namespace CmsApp.Migrations
{
    public partial class UpdateEntityModeldatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TreatmentName",
                table: "Tbl_TreatMent",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100) CHARACTER SET utf8mb4",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "StatusName",
                table: "Tbl_Status",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100) CHARACTER SET utf8mb4",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "StatusImages",
                table: "Tbl_Status",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UpdateBy",
                table: "Tbl_Patients",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50) CHARACTER SET utf8mb4",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Religion",
                table: "Tbl_Patients",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100) CHARACTER SET utf8mb4",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Race",
                table: "Tbl_Patients",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100) CHARACTER SET utf8mb4",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "PatientTel",
                table: "Tbl_Patients",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100) CHARACTER SET utf8mb4",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "PatientPrefix",
                table: "Tbl_Patients",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(10) CHARACTER SET utf8mb4",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "PatientLastName",
                table: "Tbl_Patients",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100) CHARACTER SET utf8mb4",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "PatientHN",
                table: "Tbl_Patients",
                maxLength: 15,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(15) CHARACTER SET utf8mb4",
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<string>(
                name: "PatientFirstName",
                table: "Tbl_Patients",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100) CHARACTER SET utf8mb4",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "PatientEmail",
                table: "Tbl_Patients",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100) CHARACTER SET utf8mb4",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "PatientAddress",
                table: "Tbl_Patients",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100) CHARACTER SET utf8mb4",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Nationality",
                table: "Tbl_Patients",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100) CHARACTER SET utf8mb4",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Createby",
                table: "Tbl_Patients",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50) CHARACTER SET utf8mb4",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "PatientIDcard",
                table: "Tbl_Patients",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255) CHARACTER SET utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Updateby",
                table: "Tbl_Genders",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50) CHARACTER SET utf8mb4",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "GenderName",
                table: "Tbl_Genders",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100) CHARACTER SET utf8mb4",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "CreateBy",
                table: "Tbl_Genders",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50) CHARACTER SET utf8mb4",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Updateby",
                table: "Tbl_Finances",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50) CHARACTER SET utf8mb4",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "FinanceName",
                table: "Tbl_Finances",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(150) CHARACTER SET utf8mb4",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "FinanceHN",
                table: "Tbl_Finances",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50) CHARACTER SET utf8mb4",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "CreateBy",
                table: "Tbl_Finances",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50) CHARACTER SET utf8mb4",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "DrugAllergyDetail",
                table: "Tbl_DrugAllergys",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100) CHARACTER SET utf8mb4",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "ContactemergencyLastname",
                table: "Tbl_ContextEmergencys",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100) CHARACTER SET utf8mb4",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "ContactemergencyFirstname",
                table: "Tbl_ContextEmergencys",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100) CHARACTER SET utf8mb4",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "AdminUserName",
                table: "Tbl_Admins",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255) CHARACTER SET utf8mb4",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "AdminPassword",
                table: "Tbl_Admins",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255) CHARACTER SET utf8mb4",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "AdminName",
                table: "Tbl_Admins",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255) CHARACTER SET utf8mb4",
                oldMaxLength: 255);

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_TreatMent_TreatmentID",
                table: "Tbl_TreatMent",
                column: "TreatmentID");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Status_StatusID",
                table: "Tbl_Status",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Status_StatusImages",
                table: "Tbl_Status",
                column: "StatusImages");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Patients_Age",
                table: "Tbl_Patients",
                column: "Age");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Patients_PatientIDcard",
                table: "Tbl_Patients",
                column: "PatientIDcard");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Genders_CreateDate",
                table: "Tbl_Genders",
                column: "CreateDate");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Genders_GenderID",
                table: "Tbl_Genders",
                column: "GenderID");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Genders_UpdateDate",
                table: "Tbl_Genders",
                column: "UpdateDate");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Finances_CreateDate",
                table: "Tbl_Finances",
                column: "CreateDate");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Finances_FinanceID",
                table: "Tbl_Finances",
                column: "FinanceID");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Finances_UpdateDate",
                table: "Tbl_Finances",
                column: "UpdateDate");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_DrugAllergys_DrugAllergyID",
                table: "Tbl_DrugAllergys",
                column: "DrugAllergyID");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_ContextEmergencys_ContactemergencyAddress",
                table: "Tbl_ContextEmergencys",
                column: "ContactemergencyAddress");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_ContextEmergencys_ContactemergencyConcerned",
                table: "Tbl_ContextEmergencys",
                column: "ContactemergencyConcerned");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_ContextEmergencys_ContactemergencyID",
                table: "Tbl_ContextEmergencys",
                column: "ContactemergencyID");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_ContextEmergencys_ContatctemergencyTel",
                table: "Tbl_ContextEmergencys",
                column: "ContatctemergencyTel");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_ContextEmergencys_PatientIDcard",
                table: "Tbl_ContextEmergencys",
                column: "PatientIDcard");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_BloodTypes_BloodTypeID",
                table: "Tbl_BloodTypes",
                column: "BloodTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_BloodTypes_BloodTypeName",
                table: "Tbl_BloodTypes",
                column: "BloodTypeName");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_BloodTypes_BloodtypeDetail",
                table: "Tbl_BloodTypes",
                column: "BloodtypeDetail");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Admins_AdminID",
                table: "Tbl_Admins",
                column: "AdminID");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Admins_CreateBy",
                table: "Tbl_Admins",
                column: "CreateBy");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Admins_CreateDate",
                table: "Tbl_Admins",
                column: "CreateDate");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Admins_Premission",
                table: "Tbl_Admins",
                column: "Premission");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Admins_UpdateBy",
                table: "Tbl_Admins",
                column: "UpdateBy");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Admins_UpdateDate",
                table: "Tbl_Admins",
                column: "UpdateDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tbl_TreatMent_TreatmentID",
                table: "Tbl_TreatMent");

            migrationBuilder.DropIndex(
                name: "IX_Tbl_Status_StatusID",
                table: "Tbl_Status");

            migrationBuilder.DropIndex(
                name: "IX_Tbl_Status_StatusImages",
                table: "Tbl_Status");

            migrationBuilder.DropIndex(
                name: "IX_Tbl_Patients_Age",
                table: "Tbl_Patients");

            migrationBuilder.DropIndex(
                name: "IX_Tbl_Patients_PatientIDcard",
                table: "Tbl_Patients");

            migrationBuilder.DropIndex(
                name: "IX_Tbl_Genders_CreateDate",
                table: "Tbl_Genders");

            migrationBuilder.DropIndex(
                name: "IX_Tbl_Genders_GenderID",
                table: "Tbl_Genders");

            migrationBuilder.DropIndex(
                name: "IX_Tbl_Genders_UpdateDate",
                table: "Tbl_Genders");

            migrationBuilder.DropIndex(
                name: "IX_Tbl_Finances_CreateDate",
                table: "Tbl_Finances");

            migrationBuilder.DropIndex(
                name: "IX_Tbl_Finances_FinanceID",
                table: "Tbl_Finances");

            migrationBuilder.DropIndex(
                name: "IX_Tbl_Finances_UpdateDate",
                table: "Tbl_Finances");

            migrationBuilder.DropIndex(
                name: "IX_Tbl_DrugAllergys_DrugAllergyID",
                table: "Tbl_DrugAllergys");

            migrationBuilder.DropIndex(
                name: "IX_Tbl_ContextEmergencys_ContactemergencyAddress",
                table: "Tbl_ContextEmergencys");

            migrationBuilder.DropIndex(
                name: "IX_Tbl_ContextEmergencys_ContactemergencyConcerned",
                table: "Tbl_ContextEmergencys");

            migrationBuilder.DropIndex(
                name: "IX_Tbl_ContextEmergencys_ContactemergencyID",
                table: "Tbl_ContextEmergencys");

            migrationBuilder.DropIndex(
                name: "IX_Tbl_ContextEmergencys_ContatctemergencyTel",
                table: "Tbl_ContextEmergencys");

            migrationBuilder.DropIndex(
                name: "IX_Tbl_ContextEmergencys_PatientIDcard",
                table: "Tbl_ContextEmergencys");

            migrationBuilder.DropIndex(
                name: "IX_Tbl_BloodTypes_BloodTypeID",
                table: "Tbl_BloodTypes");

            migrationBuilder.DropIndex(
                name: "IX_Tbl_BloodTypes_BloodTypeName",
                table: "Tbl_BloodTypes");

            migrationBuilder.DropIndex(
                name: "IX_Tbl_BloodTypes_BloodtypeDetail",
                table: "Tbl_BloodTypes");

            migrationBuilder.DropIndex(
                name: "IX_Tbl_Admins_AdminID",
                table: "Tbl_Admins");

            migrationBuilder.DropIndex(
                name: "IX_Tbl_Admins_CreateBy",
                table: "Tbl_Admins");

            migrationBuilder.DropIndex(
                name: "IX_Tbl_Admins_CreateDate",
                table: "Tbl_Admins");

            migrationBuilder.DropIndex(
                name: "IX_Tbl_Admins_Premission",
                table: "Tbl_Admins");

            migrationBuilder.DropIndex(
                name: "IX_Tbl_Admins_UpdateBy",
                table: "Tbl_Admins");

            migrationBuilder.DropIndex(
                name: "IX_Tbl_Admins_UpdateDate",
                table: "Tbl_Admins");

            migrationBuilder.DropColumn(
                name: "StatusImages",
                table: "Tbl_Status");

            migrationBuilder.AlterColumn<string>(
                name: "TreatmentName",
                table: "Tbl_TreatMent",
                type: "varchar(100) CHARACTER SET utf8mb4",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "StatusName",
                table: "Tbl_Status",
                type: "varchar(100) CHARACTER SET utf8mb4",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UpdateBy",
                table: "Tbl_Patients",
                type: "varchar(50) CHARACTER SET utf8mb4",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Religion",
                table: "Tbl_Patients",
                type: "varchar(100) CHARACTER SET utf8mb4",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Race",
                table: "Tbl_Patients",
                type: "varchar(100) CHARACTER SET utf8mb4",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PatientTel",
                table: "Tbl_Patients",
                type: "varchar(100) CHARACTER SET utf8mb4",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PatientPrefix",
                table: "Tbl_Patients",
                type: "varchar(10) CHARACTER SET utf8mb4",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PatientLastName",
                table: "Tbl_Patients",
                type: "varchar(100) CHARACTER SET utf8mb4",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PatientHN",
                table: "Tbl_Patients",
                type: "varchar(15) CHARACTER SET utf8mb4",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 15,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PatientFirstName",
                table: "Tbl_Patients",
                type: "varchar(100) CHARACTER SET utf8mb4",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PatientEmail",
                table: "Tbl_Patients",
                type: "varchar(100) CHARACTER SET utf8mb4",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PatientAddress",
                table: "Tbl_Patients",
                type: "varchar(100) CHARACTER SET utf8mb4",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nationality",
                table: "Tbl_Patients",
                type: "varchar(100) CHARACTER SET utf8mb4",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Createby",
                table: "Tbl_Patients",
                type: "varchar(50) CHARACTER SET utf8mb4",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PatientIDcard",
                table: "Tbl_Patients",
                type: "varchar(255) CHARACTER SET utf8mb4",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<string>(
                name: "Updateby",
                table: "Tbl_Genders",
                type: "varchar(50) CHARACTER SET utf8mb4",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "GenderName",
                table: "Tbl_Genders",
                type: "varchar(100) CHARACTER SET utf8mb4",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreateBy",
                table: "Tbl_Genders",
                type: "varchar(50) CHARACTER SET utf8mb4",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Updateby",
                table: "Tbl_Finances",
                type: "varchar(50) CHARACTER SET utf8mb4",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FinanceName",
                table: "Tbl_Finances",
                type: "varchar(150) CHARACTER SET utf8mb4",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FinanceHN",
                table: "Tbl_Finances",
                type: "varchar(50) CHARACTER SET utf8mb4",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreateBy",
                table: "Tbl_Finances",
                type: "varchar(50) CHARACTER SET utf8mb4",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DrugAllergyDetail",
                table: "Tbl_DrugAllergys",
                type: "varchar(100) CHARACTER SET utf8mb4",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ContactemergencyLastname",
                table: "Tbl_ContextEmergencys",
                type: "varchar(100) CHARACTER SET utf8mb4",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ContactemergencyFirstname",
                table: "Tbl_ContextEmergencys",
                type: "varchar(100) CHARACTER SET utf8mb4",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AdminUserName",
                table: "Tbl_Admins",
                type: "varchar(255) CHARACTER SET utf8mb4",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AdminPassword",
                table: "Tbl_Admins",
                type: "varchar(255) CHARACTER SET utf8mb4",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AdminName",
                table: "Tbl_Admins",
                type: "varchar(255) CHARACTER SET utf8mb4",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 255,
                oldNullable: true);
        }
    }
}
