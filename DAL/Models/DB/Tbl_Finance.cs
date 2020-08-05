using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MvcMovic.Models.DB {

    //create Table finance
    public class Tbl_Finance {

        [Key]
        [Column(TypeName ="int(20)")]
        public int FinanceID { get; set; }

        [Column(TypeName ="varchar(255)")]
        public string FinanceName { get; set; }

        [Column(TypeName ="varchar(255)")]
        public string FinanceIdcard { get; set; }

        [Column(TypeName ="varchar(255)")]
        public string FinanceHN { get; set; }

        [Column(TypeName ="varchar(255)")]
        public string CreateBy { get; set; }

        public DateTime CreateDate { get; set; }

        [Column(TypeName ="varchar(255)")]
        public string Updateby { get; set; }

        public DateTime UpdateDate { get; set; }



    }
}
