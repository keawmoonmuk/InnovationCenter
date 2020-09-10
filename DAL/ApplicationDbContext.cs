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
            builder.Entity<Tbl_Admins>().Property(a => a.AdminName).IsRequired().HasMaxLength(255);       //ต้องระบุ, Max Length 255
            builder.Entity<Tbl_Admins>().HasIndex(a => a.AdminName);
            builder.Entity<Tbl_Admins>().Property(a => a.AdminUserName).IsRequired().HasMaxLength(255);  //ต้องระบุ,  Max Length 255 
            builder.Entity<Tbl_Admins>().HasIndex(a => a.AdminUserName);
            builder.Entity<Tbl_Admins>().Property(a => a.AdminPassword).IsRequired().HasMaxLength(255);  //ต้องระบุ,  Max Length 255 
            builder.Entity<Tbl_Admins>().HasIndex(a => a.AdminPassword);
            builder.Entity<Tbl_Admins>().Property(a => a.Premission).HasMaxLength(100);  //ต้องระบุ,  Max Length 255 
            builder.Entity<Tbl_Admins>().Property(a => a.CreateBy).HasMaxLength(100);  //ต้องระบุ,  Max Length 255 
            builder.Entity<Tbl_Admins>().Property(a => a.UpdateBy).HasMaxLength(100);  //ต้องระบุ,  Max Length 255 
            builder.Entity<Tbl_Admins>().ToTable($"Tbl_{nameof(this.Admins)}");  //Create table name ex... Tbl_Admins
          
            //BloodType
            builder.Entity<Tbl_BloodType>().Property(b => b.BloodTypeName).HasMaxLength(255);
            builder.Entity<Tbl_BloodType>().Property(b=> b.BloodtypeDetail).HasMaxLength(255);
            builder.Entity<Tbl_BloodType>().ToTable($"Tbl_{nameof(this.BloodTypes)}");    //Create Table Name

            //Contactemergency
            builder.Entity<Tbl_ContactEmergency>().Property(c => c.ContactemergencyFirstname).IsRequired().HasMaxLength(100);
            builder.Entity<Tbl_ContactEmergency>().HasIndex(c => c.ContactemergencyFirstname);
            builder.Entity<Tbl_ContactEmergency>().Property(c => c.ContactemergencyLastname).IsRequired().HasMaxLength(100);
            builder.Entity<Tbl_ContactEmergency>().HasIndex(c => c.ContactemergencyLastname);
            builder.Entity<Tbl_ContactEmergency>().Property(c => c.ContactemergencyAddress).HasMaxLength(255);
            builder.Entity<Tbl_ContactEmergency>().Property(c => c.ContatctemergencyTel).IsUnicode(false).HasMaxLength(30);
            builder.Entity<Tbl_ContactEmergency>().Property(c => c.ContactemergencyConcerned).HasMaxLength(255);
            builder.Entity<Tbl_ContactEmergency>().ToTable($"Tbl_{nameof(this.ContextEmergencys)}");  //  //Create Table Name

            //DrugAllergy
            builder.Entity<Tbl_DrugAllergy>().Property(d => d.DrugAllergyName).IsRequired().HasMaxLength(100);
            builder.Entity<Tbl_DrugAllergy>().HasIndex(d => d.DrugAllergyName);
            builder.Entity<Tbl_DrugAllergy>().Property(d => d.DrugAllergyDetail).IsRequired().HasMaxLength(100);
            builder.Entity<Tbl_DrugAllergy>().HasIndex(d => d.DrugAllergyDetail);
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
            builder.Entity<Tbl_Patients>().Property(p => p.PatientHN).IsRequired().HasMaxLength(15);
            builder.Entity<Tbl_Patients>().HasIndex(p => p.PatientHN);
            builder.Entity<Tbl_Patients>().Property(p => p.PatientPrefix).IsRequired().HasMaxLength(10);
            builder.Entity<Tbl_Patients>().HasIndex(p => p.PatientPrefix);
            builder.Entity<Tbl_Patients>().Property(p => p.PatientFirstName).IsRequired().HasMaxLength(100);
            builder.Entity<Tbl_Patients>().HasIndex(p => p.PatientFirstName);
            builder.Entity<Tbl_Patients>().Property(p => p.PatientLastName).IsRequired().HasMaxLength(100);
            builder.Entity<Tbl_Patients>().HasIndex(p => p.PatientLastName);
            builder.Entity<Tbl_Patients>().Property(p => p.Race).IsRequired().HasMaxLength(100);
            builder.Entity<Tbl_Patients>().HasIndex(p => p.Race);
            builder.Entity<Tbl_Patients>().Property(p => p.Nationality).IsRequired().HasMaxLength(100);
            builder.Entity<Tbl_Patients>().HasIndex(p => p.Nationality);
            builder.Entity<Tbl_Patients>().Property(p => p.Religion).IsRequired().HasMaxLength(100);
            builder.Entity<Tbl_Patients>().HasIndex(p => p.Religion);
            builder.Entity<Tbl_Patients>().Property(p => p.PatientAddress).IsRequired().HasMaxLength(100);
            builder.Entity<Tbl_Patients>().HasIndex(p => p.PatientAddress);
            builder.Entity<Tbl_Patients>().Property(p => p.PatientTel).IsRequired().HasMaxLength(100);
            builder.Entity<Tbl_Patients>().HasIndex(p => p.PatientTel);
            builder.Entity<Tbl_Patients>().Property(p => p.PatientEmail).IsRequired().HasMaxLength(100);
            builder.Entity<Tbl_Patients>().HasIndex(p => p.PatientEmail);
            builder.Entity<Tbl_Patients>().Property(p => p.Createby).IsRequired().HasMaxLength(50);
            builder.Entity<Tbl_Patients>().HasIndex(p => p.Createby);
            builder.Entity<Tbl_Patients>().Property(p => p.UpdateBy).IsRequired().HasMaxLength(50);
            builder.Entity<Tbl_Patients>().HasIndex(p => p.UpdateBy);
            builder.Entity<Tbl_Patients>().ToTable($"Tbl_{nameof(this.Patients)}");  //  //Create Table Name

            //Gender         
            builder.Entity<Tbl_Gender>().Property(g => g.GenderName).IsRequired().HasMaxLength(100);
            builder.Entity<Tbl_Gender>().HasIndex(g => g.GenderName);
            builder.Entity<Tbl_Gender>().Property(g => g.CreateBy).IsRequired().HasMaxLength(50);
            builder.Entity<Tbl_Gender>().HasIndex(g => g.CreateBy);
            builder.Entity<Tbl_Gender>().Property(g => g.Updateby).IsRequired().HasMaxLength(50);
            builder.Entity<Tbl_Gender>().HasIndex(g => g.Updateby);
            builder.Entity<Tbl_Gender>().ToTable($"Tbl_{nameof(this.Genders)}");  //  //Create Table Name

            //Status         
            builder.Entity<Tbl_Status>().Property(s => s.StatusName).IsRequired().HasMaxLength(100);
            builder.Entity<Tbl_Status>().HasIndex(s => s.StatusName);
            builder.Entity<Tbl_Status>().ToTable($"Tbl_{nameof(this.Status)}");  //  //Create Table Name

            //TreatMent         
            builder.Entity<Tbl_TreatMent>().Property(t => t.TreatmentName).IsRequired().HasMaxLength(100);
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
