using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MvcMovic.Models.DB {
    //create Table Context EmerGency
    public class Tbl_ContactEmergency {

        [Key]
        [Column(TypeName ="int(3)")]
        public int ContactemergencyID { get; set; }

        [Column(TypeName ="varchar(255)")]
        public string ContactemergencyFirstname { get; set; }

        [Column(TypeName ="varchar(255)")]
        public string ContactemergencyLastname { get; set; }

        [Column(TypeName ="int(13)")]
        public int PatientIDcard { get; set; }

        [Column(TypeName ="varchar(255)")]
        public string ContactemergencyAddress { get; set; }

        [Column(TypeName ="varchar(50)")]
        public string ContatctemergencyTel { get; set; }

        [Column(TypeName ="varchar(50)")]
        public string ContactemergencyConcerned { get; set; }

    }
}
