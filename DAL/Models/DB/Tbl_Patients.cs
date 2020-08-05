using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;  //import component DataAnnotations
using System.ComponentModel.DataAnnotations.Schema;  //import component DataAnnotions.schema
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace MvcMovic.Models.DB {

    //Create table Patient
    public class Tbl_Patients {

        [Key]
        [Column(TypeName ="int(13)")]
        public int PatientIDcard { get; set; }

        [Column(TypeName = "varchar(25)")]
        public string PatientHN { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string PatientPrefix { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string PatientFirstName { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string PatientLastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        //อายุ
        [Column(TypeName = "int(100)")]
        public int Age { get; set; }
        //เพศ
        [Column(TypeName = "int(8)")]
        public int SexID { get; set; }

        //สถานภาพ
        [Column(TypeName = "int(5)")]
        public int StatusID { get; set; }

        //เชื้อชาติ
        [Column(TypeName ="varchar(255)")]
        public string Race { get; set; }

        //สัญชาติ
        [Column(TypeName ="varchar(255)")]
        public string Nationality { get; set; }

        //ศาสนา
        [Column(TypeName = "varchar(255)")]
        public  string Religion { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string PatientAddress { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string PatientTel { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string PatientEmail { get; set; }

     

        //ติดต่อกรณีฉุเฉิน
        [Column(TypeName = "int(3)")]
        public int ContactemergencyID { get; set; }

        //กรุ๊ปเลือด
        [Column(TypeName = "int(2)")]
        public int BloodTypeID { get; set; }

        //สิทธิ์การักษา
        [Column(TypeName = "int(5)")]
        public int TreatmentID { get; set; }

        //การแพ้ยา
        [Column(TypeName = "int(2)")]
        public int DrugAllergyID { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string PatientCreateby { get; set; }


        public DateTime PatientCreateDate { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string UpdateBy { get; set; }

        public DateTime UpdateDate { get; set; }


    }
}
