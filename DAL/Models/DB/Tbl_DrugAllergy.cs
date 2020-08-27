using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Models.DB {

    //create Table DrugAllergy
    public class Tbl_DrugAllergy {
        [Key]
        public int DrugAllergyID { get; set; }
        public string DrugAllergyName { get; set; }
        public string DrugAllergyDetail { get; set; }
    }
}
