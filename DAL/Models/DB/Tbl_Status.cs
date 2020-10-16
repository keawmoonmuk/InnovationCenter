using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Models.DB {

    //Create Table Status
    public class Tbl_Status {
        [Key]
        public int StatusID { get; set; }
        public string StatusName { get; set; }
        public string StatusImages { get; set; }

    }
}
