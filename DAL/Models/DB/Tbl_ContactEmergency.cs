using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Models.DB {
    //create Table Context EmerGency
    public class Tbl_ContactEmergency {

        [Key]
        public int ContactemergencyID { get; set; }
        public string ContactemergencyFirstname { get; set; }
        public string ContactemergencyLastname { get; set; }
        public int PatientIDcard { get; set; }
        public string ContactemergencyAddress { get; set; }
        public string ContatctemergencyTel { get; set; }
        public string ContactemergencyConcerned { get; set; }

    }
}
