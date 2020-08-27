using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;  //import   DataAnnotions 
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Models.DB {

    //Create Table Admins
    public class Tbl_Admins {

        [Key]
        public int AdminID { get; set; }
        public string AdminName { get; set; }
        public string AdminUserName { get; set; }
        public string AdminPassword { get; set; }
        public string Premission { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime UpdateDate { get; set; }

    }
}
