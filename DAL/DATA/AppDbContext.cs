using Microsoft.EntityFrameworkCore;
using MvcMovic.Models.DB;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.DATA {
    class AppDbContext : DbContext {

        public AppDbContext(DbContextOptions<AppDbContext> options)
        :base(options)    { }

        public DbSet<Tbl_Admins> Admins { get; set; }
        public DbSet<Tbl_BloodType> BloodType { get; set; }
        public DbSet<Tbl_ContactEmergency> ContextEmergency { get; set; }
        public DbSet<Tbl_DrugAllergy> DrugAllergy { get; set; }
        public DbSet<Tbl_Finance> Finance { get; set; }
        public DbSet<Tbl_Patients> Patient { get; set; }
        public DbSet<Tbl_Sex> Sex { get; set; }
        public DbSet<Tbl_Status> Status { get; set; }
        public DbSet<Tbl_TreatMent> TreatMent { get; set; }

    }
}
