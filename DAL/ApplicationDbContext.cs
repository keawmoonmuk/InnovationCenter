// =============================
// Email: info@ebenmonney.com
// www.ebenmonney.com/templates
// =============================

using DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using DAL.Models.Interfaces;
using DAL.Models.DB;

namespace DAL {

    //import Identity for =>  ApplicationUser , ApplicationRole
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string> {

        //entities
        public string CurrentUserId { get; set; }
        //public DbSet<Customer> Customers { get; set; }
        //public DbSet<ProductCategory> ProductCategories { get; set; }
        //public DbSet<Product> Products { get; set; }
       // public DbSet<Order> Orders { get; set; }
       // public DbSet<OrderDetail> OrderDetails { get; set; }  
       
       
        //--------create DbSet------------------------
        public DbSet<Tbl_Admins> Admins { get; set; }
        public DbSet<Tbl_BloodType> BloodTypes { get; set; }
        public DbSet<Tbl_ContactEmergency> ContextEmergencys { get; set; }
        public DbSet<Tbl_DrugAllergy> DrugAllergys { get; set; }
        public DbSet<Tbl_Finance> Finances { get; set; }
        public DbSet<Tbl_Patients> Patients { get; set; }
        public DbSet<Tbl_Gender> Genders { get; set; }
        public DbSet<Tbl_Status> Status { get; set; }
        public DbSet<Tbl_TreatMent> TreatMent { get; set; }

        //base option
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

        // write Fluent API configurations here
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //const string priceDecimalType = "decimal(18,2)";

            builder.Entity<ApplicationUser>().HasMany(u => u.Claims).WithOne().HasForeignKey(c => c.UserId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            builder.Entity<ApplicationUser>().HasMany(u => u.Roles).WithOne().HasForeignKey(r => r.UserId).IsRequired().OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ApplicationRole>().HasMany(r => r.Claims).WithOne().HasForeignKey(c => c.RoleId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            builder.Entity<ApplicationRole>().HasMany(r => r.Users).WithOne().HasForeignKey(r => r.RoleId).IsRequired().OnDelete(DeleteBehavior.Cascade);

            //***************************************************************************    
            //Admin
            builder.Entity<Tbl_Admins>().Property(a => a.AdminID).IsRequired().HasMaxLength(255);       //ต้องระบุ, Max Length 255
            builder.Entity<Tbl_Admins>().HasIndex(a => a.AdminID);

            builder.Entity<Tbl_Admins>().Property(a => a.AdminName).HasMaxLength(255);       // Max Length 255
            builder.Entity<Tbl_Admins>().HasIndex(a => a.AdminName);

            builder.Entity<Tbl_Admins>().Property(a => a.AdminUserName).HasMaxLength(255);  //  Max Length 255 
            builder.Entity<Tbl_Admins>().HasIndex(a => a.AdminUserName);

            builder.Entity<Tbl_Admins>().Property(a => a.AdminPassword).HasMaxLength(255);  //  Max Length 255 
            builder.Entity<Tbl_Admins>().HasIndex(a => a.AdminPassword);

            builder.Entity<Tbl_Admins>().Property(a => a.Premission).HasMaxLength(100);  // Max Length 255 
            builder.Entity<Tbl_Admins>().HasIndex(a => a.Premission);

            builder.Entity<Tbl_Admins>().Property(a => a.CreateBy).HasMaxLength(100);  // Max Length 255 
            builder.Entity<Tbl_Admins>().HasIndex(a => a.CreateBy);

            builder.Entity<Tbl_Admins>().Property(a => a.CreateDate).HasMaxLength(100);  // Max Length 255 
            builder.Entity<Tbl_Admins>().HasIndex(a => a.CreateDate);

            builder.Entity<Tbl_Admins>().Property(a => a.UpdateBy).HasMaxLength(100);  //  Max Length 255 
            builder.Entity<Tbl_Admins>().HasIndex(a => a.UpdateBy);

            builder.Entity<Tbl_Admins>().Property(a => a.UpdateDate).HasMaxLength(100);  //  Max Length 255 
            builder.Entity<Tbl_Admins>().HasIndex(a => a.UpdateDate);

            builder.Entity<Tbl_Admins>().ToTable($"Tbl_{nameof(this.Admins)}");  //Create table name ex... Tbl_Admins

            //BloodType
            builder.Entity<Tbl_BloodType>().Property(b => b.BloodTypeID).IsRequired().HasMaxLength(100);
            builder.Entity<Tbl_BloodType>().HasIndex(b => b.BloodTypeID);

            builder.Entity<Tbl_BloodType>().Property(b => b.BloodTypeName).HasMaxLength(255);
            builder.Entity<Tbl_BloodType>().HasIndex(b => b.BloodTypeName);

            builder.Entity<Tbl_BloodType>().Property(b=> b.BloodtypeDetail).HasMaxLength(255);
            builder.Entity<Tbl_BloodType>().HasIndex(b => b.BloodtypeDetail);

            builder.Entity<Tbl_BloodType>().ToTable($"Tbl_{nameof(this.BloodTypes)}");    //Create Table Name

            //Contactemergency
            builder.Entity<Tbl_ContactEmergency>().Property(c => c.ContactemergencyID).IsRequired().HasMaxLength(100);
            builder.Entity<Tbl_ContactEmergency>().HasIndex(c => c.ContactemergencyID);

            builder.Entity<Tbl_ContactEmergency>().Property(c => c.ContactemergencyFirstname).HasMaxLength(100);
            builder.Entity<Tbl_ContactEmergency>().HasIndex(c => c.ContactemergencyFirstname);

            builder.Entity<Tbl_ContactEmergency>().Property(c => c.ContactemergencyLastname).HasMaxLength(100);
            builder.Entity<Tbl_ContactEmergency>().HasIndex(c => c.ContactemergencyLastname);

            builder.Entity<Tbl_ContactEmergency>().Property(c => c.PatientIDcard).HasMaxLength(15);
            builder.Entity<Tbl_ContactEmergency>().HasIndex(c => c.PatientIDcard);

            builder.Entity<Tbl_ContactEmergency>().Property(c => c.ContactemergencyAddress).HasMaxLength(255);
            builder.Entity<Tbl_ContactEmergency>().HasIndex(c => c.ContactemergencyAddress);

            builder.Entity<Tbl_ContactEmergency>().Property(c => c.ContatctemergencyTel).IsUnicode(false).HasMaxLength(30);
            builder.Entity<Tbl_ContactEmergency>().HasIndex(c => c.ContatctemergencyTel);

            builder.Entity<Tbl_ContactEmergency>().Property(c => c.ContactemergencyConcerned).HasMaxLength(255);
            builder.Entity<Tbl_ContactEmergency>().HasIndex(c => c.ContactemergencyConcerned);

            builder.Entity<Tbl_ContactEmergency>().ToTable($"Tbl_{nameof(this.ContextEmergencys)}");  //  //Create Table Name

            //****************DrugAllergy***************
            builder.Entity<Tbl_DrugAllergy>().Property(d => d.DrugAllergyID).IsRequired().HasMaxLength(100);
            builder.Entity<Tbl_DrugAllergy>().HasIndex(d => d.DrugAllergyID);

            builder.Entity<Tbl_DrugAllergy>().Property(d => d.DrugAllergyName).IsRequired().HasMaxLength(100);
            builder.Entity<Tbl_DrugAllergy>().HasIndex(d => d.DrugAllergyName);

            builder.Entity<Tbl_DrugAllergy>().Property(d => d.DrugAllergyDetail).HasMaxLength(100);
            builder.Entity<Tbl_DrugAllergy>().HasIndex(d => d.DrugAllergyDetail);

            builder.Entity<Tbl_DrugAllergy>().ToTable($"Tbl_{nameof(this.DrugAllergys)}");  // //Create Table Name

            //***********Finance****************
            builder.Entity<Tbl_Finance>().Property(f => f.FinanceID).IsRequired().HasMaxLength(100);
            builder.Entity<Tbl_Finance>().HasIndex(f => f.FinanceID);

            builder.Entity<Tbl_Finance>().Property(f => f.FinanceName).HasMaxLength(150);
            builder.Entity<Tbl_Finance>().HasIndex(f => f.FinanceName);

            builder.Entity<Tbl_Finance>().Property(f => f.FinanceIdcard).IsRequired().HasMaxLength(100);
            builder.Entity<Tbl_Finance>().HasIndex(f => f.FinanceIdcard);

            builder.Entity<Tbl_Finance>().Property(f => f.FinanceHN).HasMaxLength(50);
            builder.Entity<Tbl_Finance>().HasIndex(f => f.FinanceHN);

            builder.Entity<Tbl_Finance>().Property(f => f.CreateBy).HasMaxLength(50);
            builder.Entity<Tbl_Finance>().HasIndex(f => f.CreateBy);

            builder.Entity<Tbl_Finance>().Property(f => f.CreateDate).HasMaxLength(50);
            builder.Entity<Tbl_Finance>().HasIndex(f => f.CreateDate);

            builder.Entity<Tbl_Finance>().Property(f => f.Updateby).HasMaxLength(50);
            builder.Entity<Tbl_Finance>().HasIndex(f => f.Updateby);

            builder.Entity<Tbl_Finance>().Property(f => f.UpdateDate).HasMaxLength(50);
            builder.Entity<Tbl_Finance>().HasIndex(f => f.UpdateDate);

            builder.Entity<Tbl_Finance>().ToTable($"Tbl_{nameof(this.Finances)}");  //  //Create Table Name

            //*********Patient***************** 
            builder.Entity<Tbl_Patients>().Property(p => p.PatientIDcard).IsRequired().HasMaxLength(15);
            builder.Entity<Tbl_Patients>().HasIndex(p => p.PatientIDcard);

            builder.Entity<Tbl_Patients>().Property(p => p.PatientHN).HasMaxLength(15);
            builder.Entity<Tbl_Patients>().HasIndex(p => p.PatientHN);

            builder.Entity<Tbl_Patients>().Property(p => p.PatientPrefix).HasMaxLength(10);
            builder.Entity<Tbl_Patients>().HasIndex(p => p.PatientPrefix);

            builder.Entity<Tbl_Patients>().Property(p => p.PatientFirstName).HasMaxLength(100);
            builder.Entity<Tbl_Patients>().HasIndex(p => p.PatientFirstName);

            builder.Entity<Tbl_Patients>().Property(p => p.PatientLastName).HasMaxLength(100);
            builder.Entity<Tbl_Patients>().HasIndex(p => p.PatientLastName);

            builder.Entity<Tbl_Patients>().Property(p => p.Age).HasMaxLength(3);
            builder.Entity<Tbl_Patients>().HasIndex(p => p.Age);
            //เชื้อชาติ
            builder.Entity<Tbl_Patients>().Property(p => p.Race).HasMaxLength(100);
            builder.Entity<Tbl_Patients>().HasIndex(p => p.Race);
            //สัญชาติ
            builder.Entity<Tbl_Patients>().Property(p => p.Nationality).HasMaxLength(100);
            builder.Entity<Tbl_Patients>().HasIndex(p => p.Nationality);
            //ศาสนา
            builder.Entity<Tbl_Patients>().Property(p => p.Religion).HasMaxLength(100);
            builder.Entity<Tbl_Patients>().HasIndex(p => p.Religion);

            builder.Entity<Tbl_Patients>().Property(p => p.PatientAddress).HasMaxLength(100);
            builder.Entity<Tbl_Patients>().HasIndex(p => p.PatientAddress);

            builder.Entity<Tbl_Patients>().Property(p => p.PatientTel).HasMaxLength(100);
            builder.Entity<Tbl_Patients>().HasIndex(p => p.PatientTel);

            builder.Entity<Tbl_Patients>().Property(p => p.PatientEmail).HasMaxLength(100);
            builder.Entity<Tbl_Patients>().HasIndex(p => p.PatientEmail);

            builder.Entity<Tbl_Patients>().Property(p => p.Createby).HasMaxLength(50);
            builder.Entity<Tbl_Patients>().HasIndex(p => p.Createby);

            builder.Entity<Tbl_Patients>().Property(p => p.UpdateBy).HasMaxLength(50);
            builder.Entity<Tbl_Patients>().HasIndex(p => p.UpdateBy);
            builder.Entity<Tbl_Patients>().ToTable($"Tbl_{nameof(this.Patients)}");  //  //Create Table Name

            //Gender     
            builder.Entity<Tbl_Gender>().Property(g => g.GenderID).HasMaxLength(100);
            builder.Entity<Tbl_Gender>().HasIndex(g => g.GenderID);

            builder.Entity<Tbl_Gender>().Property(g => g.GenderName).HasMaxLength(100);
            builder.Entity<Tbl_Gender>().HasIndex(g => g.GenderName);

            builder.Entity<Tbl_Gender>().Property(g => g.CreateBy).HasMaxLength(50);
            builder.Entity<Tbl_Gender>().HasIndex(g => g.CreateBy);

            builder.Entity<Tbl_Gender>().Property(g => g.CreateDate).HasMaxLength(50);
            builder.Entity<Tbl_Gender>().HasIndex(g => g.CreateDate);

            builder.Entity<Tbl_Gender>().Property(g => g.Updateby).HasMaxLength(50);
            builder.Entity<Tbl_Gender>().HasIndex(g => g.Updateby);

            builder.Entity<Tbl_Gender>().Property(g => g.UpdateDate).HasMaxLength(50);
            builder.Entity<Tbl_Gender>().HasIndex(g => g.UpdateDate);

            builder.Entity<Tbl_Gender>().ToTable($"Tbl_{nameof(this.Genders)}");  //  //Create Table Name

            //Status       
            builder.Entity<Tbl_Status>().Property(s => s.StatusID).HasMaxLength(100);
            builder.Entity<Tbl_Status>().HasIndex(s => s.StatusID);

            builder.Entity<Tbl_Status>().Property(s => s.StatusName).HasMaxLength(100);
            builder.Entity<Tbl_Status>().HasIndex(s => s.StatusName);

            builder.Entity<Tbl_Status>().HasIndex(s => s.StatusImages);

            builder.Entity<Tbl_Status>().ToTable($"Tbl_{nameof(this.Status)}");  //  //Create Table Name

            //TreatMent         
            builder.Entity<Tbl_TreatMent>().Property(t => t.TreatmentID).HasMaxLength(100);
            builder.Entity<Tbl_TreatMent>().HasIndex(t => t.TreatmentID);

            builder.Entity<Tbl_TreatMent>().Property(t => t.TreatmentName).HasMaxLength(100);
            builder.Entity<Tbl_TreatMent>().HasIndex(t => t.TreatmentName);

            builder.Entity<Tbl_TreatMent>().ToTable($"Tbl_{nameof(this.TreatMent)}");  //  //Create Table Name
        }

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
                .Where(x => x.Entity is IAuditableEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));


            foreach (var entry in modifiedEntries) {
                var entity = (IAuditableEntity)entry.Entity;
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
