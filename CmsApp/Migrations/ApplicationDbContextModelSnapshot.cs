﻿// <auto-generated />
using System;
using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CmsApp.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("DAL.Models.ApplicationRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                        .HasMaxLength(256);

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("DAL.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Configuration")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("FullName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("IsEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("JobTitle")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("UserName")
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("DAL.Models.DB.Tbl_Admins", b =>
                {
                    b.Property<int>("AdminID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasMaxLength(255);

                    b.Property<string>("AdminName")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4")
                        .HasMaxLength(255);

                    b.Property<string>("AdminPassword")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4")
                        .HasMaxLength(255);

                    b.Property<string>("AdminUserName")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4")
                        .HasMaxLength(255);

                    b.Property<string>("CreateBy")
                        .HasColumnType("varchar(100) CHARACTER SET utf8mb4")
                        .HasMaxLength(100);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime(6)")
                        .HasMaxLength(100);

                    b.Property<string>("Premission")
                        .HasColumnType("varchar(100) CHARACTER SET utf8mb4")
                        .HasMaxLength(100);

                    b.Property<string>("UpdateBy")
                        .HasColumnType("varchar(100) CHARACTER SET utf8mb4")
                        .HasMaxLength(100);

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime(6)")
                        .HasMaxLength(100);

                    b.HasKey("AdminID");

                    b.HasIndex("AdminID");

                    b.HasIndex("AdminName");

                    b.HasIndex("AdminPassword");

                    b.HasIndex("AdminUserName");

                    b.HasIndex("CreateBy");

                    b.HasIndex("CreateDate");

                    b.HasIndex("Premission");

                    b.HasIndex("UpdateBy");

                    b.HasIndex("UpdateDate");

                    b.ToTable("Tbl_Admins");
                });

            modelBuilder.Entity("DAL.Models.DB.Tbl_BloodType", b =>
                {
                    b.Property<int>("BloodTypeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasMaxLength(100);

                    b.Property<string>("BloodTypeName")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4")
                        .HasMaxLength(255);

                    b.Property<string>("BloodtypeDetail")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4")
                        .HasMaxLength(255);

                    b.HasKey("BloodTypeID");

                    b.HasIndex("BloodTypeID");

                    b.HasIndex("BloodTypeName");

                    b.HasIndex("BloodtypeDetail");

                    b.ToTable("Tbl_BloodTypes");
                });

            modelBuilder.Entity("DAL.Models.DB.Tbl_ContactEmergency", b =>
                {
                    b.Property<int>("ContactemergencyID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasMaxLength(100);

                    b.Property<string>("ContactemergencyAddress")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4")
                        .HasMaxLength(255);

                    b.Property<string>("ContactemergencyConcerned")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4")
                        .HasMaxLength(255);

                    b.Property<string>("ContactemergencyFirstname")
                        .HasColumnType("varchar(100) CHARACTER SET utf8mb4")
                        .HasMaxLength(100);

                    b.Property<string>("ContactemergencyLastname")
                        .HasColumnType("varchar(100) CHARACTER SET utf8mb4")
                        .HasMaxLength(100);

                    b.Property<string>("ContatctemergencyTel")
                        .HasColumnType("varchar(30) CHARACTER SET utf8mb4")
                        .HasMaxLength(30)
                        .IsUnicode(false);

                    b.Property<int>("PatientIDcard")
                        .HasColumnType("int")
                        .HasMaxLength(15);

                    b.HasKey("ContactemergencyID");

                    b.HasIndex("ContactemergencyAddress");

                    b.HasIndex("ContactemergencyConcerned");

                    b.HasIndex("ContactemergencyFirstname");

                    b.HasIndex("ContactemergencyID");

                    b.HasIndex("ContactemergencyLastname");

                    b.HasIndex("ContatctemergencyTel");

                    b.HasIndex("PatientIDcard");

                    b.ToTable("Tbl_ContextEmergencys");
                });

            modelBuilder.Entity("DAL.Models.DB.Tbl_DrugAllergy", b =>
                {
                    b.Property<int>("DrugAllergyID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasMaxLength(100);

                    b.Property<string>("DrugAllergyDetail")
                        .HasColumnType("varchar(100) CHARACTER SET utf8mb4")
                        .HasMaxLength(100);

                    b.Property<string>("DrugAllergyName")
                        .IsRequired()
                        .HasColumnType("varchar(100) CHARACTER SET utf8mb4")
                        .HasMaxLength(100);

                    b.HasKey("DrugAllergyID");

                    b.HasIndex("DrugAllergyDetail");

                    b.HasIndex("DrugAllergyID");

                    b.HasIndex("DrugAllergyName");

                    b.ToTable("Tbl_DrugAllergys");
                });

            modelBuilder.Entity("DAL.Models.DB.Tbl_Finance", b =>
                {
                    b.Property<int>("FinanceID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasMaxLength(100);

                    b.Property<string>("CreateBy")
                        .HasColumnType("varchar(50) CHARACTER SET utf8mb4")
                        .HasMaxLength(50);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime(6)")
                        .HasMaxLength(50);

                    b.Property<string>("FinanceHN")
                        .HasColumnType("varchar(50) CHARACTER SET utf8mb4")
                        .HasMaxLength(50);

                    b.Property<string>("FinanceIdcard")
                        .IsRequired()
                        .HasColumnType("varchar(100) CHARACTER SET utf8mb4")
                        .HasMaxLength(100);

                    b.Property<string>("FinanceName")
                        .HasColumnType("varchar(150) CHARACTER SET utf8mb4")
                        .HasMaxLength(150);

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime(6)")
                        .HasMaxLength(50);

                    b.Property<string>("Updateby")
                        .HasColumnType("varchar(50) CHARACTER SET utf8mb4")
                        .HasMaxLength(50);

                    b.HasKey("FinanceID");

                    b.HasIndex("CreateBy");

                    b.HasIndex("CreateDate");

                    b.HasIndex("FinanceHN");

                    b.HasIndex("FinanceID");

                    b.HasIndex("FinanceIdcard");

                    b.HasIndex("FinanceName");

                    b.HasIndex("UpdateDate");

                    b.HasIndex("Updateby");

                    b.ToTable("Tbl_Finances");
                });

            modelBuilder.Entity("DAL.Models.DB.Tbl_Gender", b =>
                {
                    b.Property<int>("GenderID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasMaxLength(100);

                    b.Property<string>("CreateBy")
                        .HasColumnType("varchar(50) CHARACTER SET utf8mb4")
                        .HasMaxLength(50);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime(6)")
                        .HasMaxLength(50);

                    b.Property<string>("GenderName")
                        .HasColumnType("varchar(100) CHARACTER SET utf8mb4")
                        .HasMaxLength(100);

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime(6)")
                        .HasMaxLength(50);

                    b.Property<string>("Updateby")
                        .HasColumnType("varchar(50) CHARACTER SET utf8mb4")
                        .HasMaxLength(50);

                    b.HasKey("GenderID");

                    b.HasIndex("CreateBy");

                    b.HasIndex("CreateDate");

                    b.HasIndex("GenderID");

                    b.HasIndex("GenderName");

                    b.HasIndex("UpdateDate");

                    b.HasIndex("Updateby");

                    b.ToTable("Tbl_Genders");
                });

            modelBuilder.Entity("DAL.Models.DB.Tbl_Patients", b =>
                {
                    b.Property<string>("PatientIDcard")
                        .HasColumnType("varchar(15) CHARACTER SET utf8mb4")
                        .HasMaxLength(15);

                    b.Property<int>("Age")
                        .HasColumnType("int")
                        .HasMaxLength(3);

                    b.Property<int>("BloodTypeID")
                        .HasColumnType("int");

                    b.Property<int>("ContactemergencyID")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Createby")
                        .HasColumnType("varchar(50) CHARACTER SET utf8mb4")
                        .HasMaxLength(50);

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("DrugAllergyID")
                        .HasColumnType("int");

                    b.Property<int>("GenderID")
                        .HasColumnType("int");

                    b.Property<string>("Nationality")
                        .HasColumnType("varchar(100) CHARACTER SET utf8mb4")
                        .HasMaxLength(100);

                    b.Property<string>("PatientAddress")
                        .HasColumnType("varchar(100) CHARACTER SET utf8mb4")
                        .HasMaxLength(100);

                    b.Property<string>("PatientEmail")
                        .HasColumnType("varchar(100) CHARACTER SET utf8mb4")
                        .HasMaxLength(100);

                    b.Property<string>("PatientFirstName")
                        .HasColumnType("varchar(100) CHARACTER SET utf8mb4")
                        .HasMaxLength(100);

                    b.Property<string>("PatientHN")
                        .HasColumnType("varchar(15) CHARACTER SET utf8mb4")
                        .HasMaxLength(15);

                    b.Property<string>("PatientLastName")
                        .HasColumnType("varchar(100) CHARACTER SET utf8mb4")
                        .HasMaxLength(100);

                    b.Property<string>("PatientPrefix")
                        .HasColumnType("varchar(10) CHARACTER SET utf8mb4")
                        .HasMaxLength(10);

                    b.Property<string>("PatientTel")
                        .HasColumnType("varchar(100) CHARACTER SET utf8mb4")
                        .HasMaxLength(100);

                    b.Property<string>("Race")
                        .HasColumnType("varchar(100) CHARACTER SET utf8mb4")
                        .HasMaxLength(100);

                    b.Property<string>("Religion")
                        .HasColumnType("varchar(100) CHARACTER SET utf8mb4")
                        .HasMaxLength(100);

                    b.Property<int>("StatusID")
                        .HasColumnType("int");

                    b.Property<int>("TreatmentID")
                        .HasColumnType("int");

                    b.Property<string>("UpdateBy")
                        .HasColumnType("varchar(50) CHARACTER SET utf8mb4")
                        .HasMaxLength(50);

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("PatientIDcard");

                    b.HasIndex("Age");

                    b.HasIndex("Createby");

                    b.HasIndex("Nationality");

                    b.HasIndex("PatientAddress");

                    b.HasIndex("PatientEmail");

                    b.HasIndex("PatientFirstName");

                    b.HasIndex("PatientHN");

                    b.HasIndex("PatientIDcard");

                    b.HasIndex("PatientLastName");

                    b.HasIndex("PatientPrefix");

                    b.HasIndex("PatientTel");

                    b.HasIndex("Race");

                    b.HasIndex("Religion");

                    b.HasIndex("UpdateBy");

                    b.ToTable("Tbl_Patients");
                });

            modelBuilder.Entity("DAL.Models.DB.Tbl_Status", b =>
                {
                    b.Property<int>("StatusID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasMaxLength(100);

                    b.Property<string>("StatusImages")
                        .HasColumnType("varchar(100) CHARACTER SET utf8mb4")
                        .HasMaxLength(100);

                    b.Property<string>("StatusName")
                        .HasColumnType("varchar(100) CHARACTER SET utf8mb4")
                        .HasMaxLength(100);

                    b.HasKey("StatusID");

                    b.HasIndex("StatusID");

                    b.HasIndex("StatusImages");

                    b.HasIndex("StatusName");

                    b.ToTable("Tbl_Status");
                });

            modelBuilder.Entity("DAL.Models.DB.Tbl_TreatMent", b =>
                {
                    b.Property<int>("TreatmentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasMaxLength(100);

                    b.Property<string>("TreatmentName")
                        .HasColumnType("varchar(100) CHARACTER SET utf8mb4")
                        .HasMaxLength(100);

                    b.HasKey("TreatmentID");

                    b.HasIndex("TreatmentID");

                    b.HasIndex("TreatmentName");

                    b.ToTable("Tbl_TreatMent");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("RoleId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("Value")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("DAL.Models.ApplicationRole", null)
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("DAL.Models.ApplicationUser", null)
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("DAL.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("DAL.Models.ApplicationRole", null)
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAL.Models.ApplicationUser", null)
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("DAL.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
