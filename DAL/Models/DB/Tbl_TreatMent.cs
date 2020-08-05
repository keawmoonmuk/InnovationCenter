using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MvcMovic.Models.DB {
    //Create Table
    public class Tbl_TreatMent {

        [Key]
        [Column(TypeName ="int(5)")]
        public int TreatmentID { get; set; }

        [Column(TypeName ="varchar(255)")]
        public string TreatmentName { get; set; }
    }
}
