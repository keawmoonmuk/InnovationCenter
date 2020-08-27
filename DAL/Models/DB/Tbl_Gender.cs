using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Models.DB {


    //Creat Table sex
    public class Tbl_Gender {

        [Key]
        public int GenderID { get; set; }
        public string GenderName { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public string Updateby { get; set; }
        public DateTime UpdateDate { get; set; }

    }
}
