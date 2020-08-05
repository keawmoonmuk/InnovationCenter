using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MvcMovic.Models.DB {

    //create Table bloodType
    public class Tbl_BloodType {

        [Key]
        [Column(TypeName ="int(2)")]
        public int BloodTypeID { get; set; }

        [Column(TypeName ="varchar(10)")]
        public string BloodTypeName { get; set; }

        [Column(TypeName="varchar(50)")]
        public string BloodtypeDetail { get; set; }
    }
}
