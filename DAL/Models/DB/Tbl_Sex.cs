using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MvcMovic.Models.DB {


    //Creat Table sex
    public class Tbl_Sex {

        [Key]
        [Column(TypeName ="int(8)")]
        public int SexID { get; set; }

        [Column(TypeName ="varchar(255)")]
        public string SexName { get; set; }

        [Column(TypeName ="varchar(255)")]
        public string CreateBy { get; set; }

        public DateTime CreateDate { get; set; }

        [Column(TypeName ="varchar(255)")]
        public string Updateby { get; set; }

        public DateTime UpdateDate { get; set; }

    }
}
