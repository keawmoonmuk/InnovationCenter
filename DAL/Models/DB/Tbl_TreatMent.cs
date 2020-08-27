using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Models.DB {
    //Create Table
    public class Tbl_TreatMent {
        [Key]
        public int TreatmentID { get; set; }
        public string TreatmentName { get; set; }
    }
}
