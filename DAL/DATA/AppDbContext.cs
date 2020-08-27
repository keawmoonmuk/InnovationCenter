using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using DAL.Models.DB;  // import model database
//using DAL.Models;
using DAL.Models.Identity;  //import Identity
using System.Threading;
using DAL.Models.Interfaces;

namespace DAL.DATA {

    //create Identiy Dbcontext 
    public class AppDbContext : IdentityDbContext<AppIdentityUser, AppIdentityRole, string> {

        //create options
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //const string SetDecimalType = "decimal(18,2)";

            builder.Entity<AppIdentityUser>().HasMany(u => u.Claims).WithOne().HasForeignKey(c => c.UserId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            builder.Entity<AppIdentityUser>().HasMany(u => u.Roles).WithOne().HasForeignKey(r => r.UserId).IsRequired().OnDelete(DeleteBehavior.Cascade);

            builder.Entity<AppIdentityRole>().HasMany(r => r.Claims).WithOne().HasForeignKey(c => c.RoleId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            builder.Entity<AppIdentityRole>().HasMany(r => r.Users).WithOne().HasForeignKey(r => r.RoleId).IsRequired().OnDelete(DeleteBehavior.Cascade);

            //***************Write fluent api*************************
            // HasKey ==> configuration primary key
            //Admin
            builder.Entity<Tbl_Admins>().Property(c => c.AdminName).IsRequired().HasMaxLength(255);       //ต้องระบุ, Max Length 255
            builder.Entity<Tbl_Admins>().HasIndex(c => c.AdminName);
            builder.Entity<Tbl_Admins>().Property(c => c.AdminUserName).IsRequired().HasMaxLength(255);  //ต้องระบุ,  Max Length 255 
            builder.Entity<Tbl_Admins>().HasIndex(c => c.AdminUserName);
            builder.Entity<Tbl_Admins>().Property(c => c.AdminPassword).IsRequired().HasMaxLength(255);  //ต้องระบุ,  Max Length 255 
            builder.Entity<Tbl_Admins>().HasIndex(c => c.AdminPassword);
            builder.Entity<Tbl_Admins>().Property(c => c.Premission).HasMaxLength(100);  //ต้องระบุ,  Max Length 255 
            builder.Entity<Tbl_Admins>().Property(c => c.CreateBy).HasMaxLength(100);  //ต้องระบุ,  Max Length 255 
            builder.Entity<Tbl_Admins>().Property(c => c.UpdateBy).HasMaxLength(100);  //ต้องระบุ,  Max Length 255 
            builder.Entity<Tbl_Admins>().ToTable($"Tbl_{nameof(this.Admins)}");  //Create table name ex... Tbl_Admins
            //BloodType
            builder.Entity<Tbl_BloodType>().Property(d => d.BloodTypeName).HasMaxLength(255);
            builder.Entity<Tbl_BloodType>().Property(d => d.BloodtypeDetail).HasMaxLength(255);
            builder.Entity<Tbl_BloodType>().ToTable($"Tbl_{nameof(this.BloodTypes)}");    //Create Table Name

            //Contactemergency
            builder.Entity<Tbl_ContactEmergency>().Property(p => p.ContactemergencyFirstname).IsRequired().HasMaxLength(100);
            builder.Entity<Tbl_ContactEmergency>().HasIndex(p => p.ContactemergencyFirstname);
            builder.Entity<Tbl_ContactEmergency>().Property(p => p.ContactemergencyLastname).IsRequired().HasMaxLength(100);
            builder.Entity<Tbl_ContactEmergency>().HasIndex(p => p.ContactemergencyLastname);
            builder.Entity<Tbl_ContactEmergency>().Property(p => p.ContactemergencyAddress).HasMaxLength(255);
            builder.Entity<Tbl_ContactEmergency>().Property(p => p.ContatctemergencyTel).IsUnicode(false).HasMaxLength(30);
            builder.Entity<Tbl_ContactEmergency>().Property(p => p.ContactemergencyConcerned).HasMaxLength(255);
            builder.Entity<Tbl_ContactEmergency>().ToTable($"Tbl_{nameof(this.ContextEmergencys)}");  //  //Create Table Name

            //DrugAllergy
            builder.Entity<Tbl_DrugAllergy>().Property(e => e.DrugAllergyName).IsRequired().HasMaxLength(100);
            builder.Entity<Tbl_DrugAllergy>().HasIndex(e => e.DrugAllergyName);
            builder.Entity<Tbl_DrugAllergy>().Property(e => e.DrugAllergyDetail).IsRequired().HasMaxLength(100);
            builder.Entity<Tbl_DrugAllergy>().HasIndex(e => e.DrugAllergyDetail);
            builder.Entity<Tbl_DrugAllergy>().ToTable($"Tbl_{nameof(this.DrugAllergys)}");  // //Create Table Name

            //Finance
            builder.Entity<Tbl_Finance>().Property(f => f.FinanceName).IsRequired().HasMaxLength(150);
            builder.Entity<Tbl_Finance>().HasIndex(f => f.FinanceName);
            builder.Entity<Tbl_Finance>().Property(f => f.FinanceIdcard).IsRequired().HasMaxLength(100);
            builder.Entity<Tbl_Finance>().HasIndex(f => f.FinanceIdcard);
            builder.Entity<Tbl_Finance>().Property(f => f.FinanceHN).IsRequired().HasMaxLength(50);
            builder.Entity<Tbl_Finance>().HasIndex(f => f.FinanceHN);
            builder.Entity<Tbl_Finance>().Property(f => f.CreateBy).IsRequired().HasMaxLength(50);
            builder.Entity<Tbl_Finance>().HasIndex(f => f.CreateBy);
            builder.Entity<Tbl_Finance>().Property(f => f.Updateby).IsRequired().HasMaxLength(50);
            builder.Entity<Tbl_Finance>().HasIndex(f => f.Updateby);
            builder.Entity<Tbl_Finance>().ToTable($"Tbl_{nameof(this.Finances)}");  //  //Create Table Name

            //patient 
            builder.Entity<Tbl_Patients>().Property(g => g.PatientHN).IsRequired().HasMaxLength(15);
            builder.Entity<Tbl_Patients>().HasIndex(g => g.PatientHN);
            builder.Entity<Tbl_Patients>().Property(g => g.PatientPrefix).IsRequired().HasMaxLength(10);
            builder.Entity<Tbl_Patients>().HasIndex(g => g.PatientPrefix);
            builder.Entity<Tbl_Patients>().Property(g => g.PatientFirstName).IsRequired().HasMaxLength(100);
            builder.Entity<Tbl_Patients>().HasIndex(g => g.PatientFirstName);
            builder.Entity<Tbl_Patients>().Property(g => g.PatientLastName).IsRequired().HasMaxLength(100);
            builder.Entity<Tbl_Patients>().HasIndex(g => g.PatientLastName);
            builder.Entity<Tbl_Patients>().Property(g => g.Race).IsRequired().HasMaxLength(100);
            builder.Entity<Tbl_Patients>().HasIndex(g => g.Race);
            builder.Entity<Tbl_Patients>().Property(g => g.Nationality).IsRequired().HasMaxLength(100);
            builder.Entity<Tbl_Patients>().HasIndex(g => g.Nationality);
            builder.Entity<Tbl_Patients>().Property(g => g.Religion).IsRequired().HasMaxLength(100);
            builder.Entity<Tbl_Patients>().HasIndex(g => g.Religion);
            builder.Entity<Tbl_Patients>().Property(g => g.PatientAddress).IsRequired().HasMaxLength(100);
            builder.Entity<Tbl_Patients>().HasIndex(g => g.PatientAddress);
            builder.Entity<Tbl_Patients>().Property(g => g.PatientTel).IsRequired().HasMaxLength(100);
            builder.Entity<Tbl_Patients>().HasIndex(g => g.PatientTel);
            builder.Entity<Tbl_Patients>().Property(g => g.PatientEmail).IsRequired().HasMaxLength(100);
            builder.Entity<Tbl_Patients>().HasIndex(g => g.PatientEmail);
            builder.Entity<Tbl_Patients>().Property(g => g.Createby).IsRequired().HasMaxLength(50);
            builder.Entity<Tbl_Patients>().HasIndex(g => g.Createby);
            builder.Entity<Tbl_Patients>().Property(g => g.UpdateBy).IsRequired().HasMaxLength(50);
            builder.Entity<Tbl_Patients>().HasIndex(g => g.UpdateBy);
            builder.Entity<Tbl_Patients>().ToTable($"Tbl_{nameof(this.Patients)}");  //  //Create Table Name

            //Gender         
            builder.Entity<Tbl_Gender>().Property(h => h.GenderName).IsRequired().HasMaxLength(100);
            builder.Entity<Tbl_Gender>().HasIndex(h => h.GenderName);
            builder.Entity<Tbl_Gender>().Property(f => f.CreateBy).IsRequired().HasMaxLength(50);
            builder.Entity<Tbl_Gender>().HasIndex(f => f.CreateBy);
            builder.Entity<Tbl_Gender>().Property(f => f.Updateby).IsRequired().HasMaxLength(50);
            builder.Entity<Tbl_Gender>().HasIndex(f => f.Updateby);
            builder.Entity<Tbl_Gender>().ToTable($"Tbl_{nameof(this.Genders)}");  //  //Create Table Name

            //Status         
            builder.Entity<Tbl_Status>().Property(j => j.StatusName).IsRequired().HasMaxLength(100);
            builder.Entity<Tbl_Status>().HasIndex(j => j.StatusName);
            builder.Entity<Tbl_Status>().ToTable($"Tbl_{nameof(this.Status)}");  //  //Create Table Name

            //TreatMent         
            builder.Entity<Tbl_TreatMent>().Property(k => k.TreatmentName).IsRequired().HasMaxLength(100);
            builder.Entity<Tbl_TreatMent>().HasIndex(k => k.TreatmentName);
            builder.Entity<Tbl_TreatMent>().ToTable($"Tbl_{nameof(this.TreatMent)}");  //  //Create Table Name
        }

        //create DbSet
        public string CurrentUserId { get; set; }
        public DbSet<Tbl_Admins> Admins { get; set; }
        public DbSet<Tbl_BloodType> BloodTypes { get; set; }
        public DbSet<Tbl_ContactEmergency> ContextEmergencys { get; set; }
        public DbSet<Tbl_DrugAllergy> DrugAllergys { get; set; }
        public DbSet<Tbl_Finance> Finances { get; set; }
        public DbSet<Tbl_Patients> Patients { get; set; }
        public DbSet<Tbl_Gender> Genders { get; set; }
        public DbSet<Tbl_Status> Status { get; set; }
        public DbSet<Tbl_TreatMent> TreatMent { get; set; }

        //บันทึกการเปลี่ยนแปลง
        public override int SaveChanges()
        {
            UpdateAuditEntities();
            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            UpdateAuditEntities();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            UpdateAuditEntities();
            return base.SaveChangesAsync(cancellationToken);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            UpdateAuditEntities();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        //Update AuditEntities ตรวจสอบ
        private void UpdateAuditEntities()
        {
            var modifiedEntries = ChangeTracker.Entries()
                .Where(x => x.Entity is AppIAuditableEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));


            foreach (var entry in modifiedEntries) {
                var entity = (AppIAuditableEntity)entry.Entity;
                DateTime now = DateTime.UtcNow;

                if (entry.State == EntityState.Added) {
                    entity.CreatedDate = now;
                    entity.CreatedBy = CurrentUserId;
                }
                else {
                    base.Entry(entity).Property(x => x.CreatedBy).IsModified = false;
                    base.Entry(entity).Property(x => x.CreatedDate).IsModified = false;
                }

                entity.UpdatedDate = now;
                entity.UpdatedBy = CurrentUserId;
            }
        }

    }

}



