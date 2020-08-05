using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;  //import   DataAnnotions 
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace MvcMovic.Models.DB {

    //Create Table Admins

    public class Tbl_Admins {

        [Key]
        [Column(TypeName ="int(13)")]
        public int AdminID { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string AdminName { get; set; }

        [Column(TypeName = "Varchar(255)")]
        public string AdminUserName { get; set; }

        [Column(TypeName ="varchar(255)")]
        public string AdminPassword { get; set; }

        [Column(TypeName ="varchar(255)")]
        public string Premission { get; set; }

        [Column(TypeName ="varchar(255)")]
        public string CreateBy { get; set; }

        public DateTime CreateDate { get; set; }

        [Column(TypeName ="varchar(255)")]
        public string UpdateBy { get; set; }

        public DateTime UpdateDate { get; set; }

    }
}
