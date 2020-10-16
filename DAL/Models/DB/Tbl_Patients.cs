using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;  //import component DataAnnotations
using System.ComponentModel.DataAnnotations.Schema;  //import component DataAnnotions.schema
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace DAL.Models.DB {

    //Create table Patient
    public class Tbl_Patients {

        [Key]
        public string PatientIDcard { get; set; }
        public string PatientHN { get; set; }
        public string PatientPrefix { get; set; }
        public string PatientFirstName { get; set; }
        public string PatientLastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Age { get; set; }
        public int GenderID { get; set; }                   //สถานภาพ ID
        public int StatusID { get; set; }                   //สถานภาพ
        public string Race { get; set; }                    //เชื้อชาติ
        public string Nationality { get; set; }             //สัญชาติ
        public string Religion { get; set; }                //ศาสนา
        public string PatientAddress { get; set; }
        public string PatientTel { get; set; }
        public string PatientEmail { get; set; }
        public int ContactemergencyID { get; set; }         //ติดต่อกรณีฉุเฉิน 
        public int BloodTypeID { get; set; }                //กรุ๊ปเลือด
        public int TreatmentID { get; set; }                //สิทธิ์การักษา  
        public int DrugAllergyID { get; set; }              //การแพ้ยา
        public string Createby { get; set; }
        public DateTime CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime UpdateDate { get; set; }

    }
}
