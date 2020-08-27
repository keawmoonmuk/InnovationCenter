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
        public int PatientIDcard { get; set; }
        public string PatientHN { get; set; }
        public string PatientPrefix { get; set; }
        public string PatientFirstName { get; set; }
        public string PatientLastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        //อายุ
        public int Age { get; set; }
        //เพศ
        public int GenderID { get; set; }
        //สถานภาพ
        public int StatusID { get; set; }
        //เชื้อชาติ
        public string Race { get; set; }
        //สัญชาติ
        public string Nationality { get; set; }
        //ศาสนา
        public string Religion { get; set; }
        //address
        public string PatientAddress { get; set; }
        //tel
        public string PatientTel { get; set; }
        public string PatientEmail { get; set; }
        //ติดต่อกรณีฉุเฉิน
        public int ContactemergencyID { get; set; }
        //กรุ๊ปเลือด
        public int BloodTypeID { get; set; }
        //สิทธิ์การักษา
        public int TreatmentID { get; set; }
        //การแพ้ยา
        public int DrugAllergyID { get; set; }
        public string Createby { get; set; }
        public DateTime CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime UpdateDate { get; set; }

    }
}
