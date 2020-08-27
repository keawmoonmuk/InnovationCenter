using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Models.DB {

    //create Table bloodType
    public class Tbl_BloodType {

        [Key]
        public int BloodTypeID { get; set; }
        public string BloodTypeName { get; set; }
        public string BloodtypeDetail { get; set; }
    }
}
