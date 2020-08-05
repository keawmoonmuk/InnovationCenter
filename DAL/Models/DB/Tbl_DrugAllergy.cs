using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MvcMovic.Models.DB {

    //create Table DrugAllergy
    public class Tbl_DrugAllergy {

        [Key]
        [Column(TypeName ="int(2)")]
        public int DrugAllergyID { get; set; }

        [Column(TypeName ="varchar(15)")]
        public string DrugAllergyName { get; set; }

        [Column(TypeName ="varchar(255)")]
        public string DrugAllergyDetail { get; set; }
    }
}
