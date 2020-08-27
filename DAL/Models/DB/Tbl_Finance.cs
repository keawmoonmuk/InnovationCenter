using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Models.DB {

    //create Table finance
    public class Tbl_Finance {

        [Key]
        public int FinanceID { get; set; }
        public string FinanceName { get; set; }
        public string FinanceIdcard { get; set; }
        public string FinanceHN { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public string Updateby { get; set; }
        public DateTime UpdateDate { get; set; }

    }
}
