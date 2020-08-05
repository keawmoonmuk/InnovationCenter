using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MvcMovic.Models.DB {

    //Create Table Status
    public class Tbl_Status {

        [Key]
        [Column(TypeName ="int(5)")]
        public int StatusID { get; set; }
        
        [Column(TypeName ="varchar(15)")]
        public string StatusName { get; set; }

    }
}
