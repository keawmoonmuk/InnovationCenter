using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models.Interfaces {

    //audit interface entity 
    interface AppIAuditableEntity {

        string CreatedBy { get; set; }
        string UpdatedBy { get; set; }
        DateTime CreatedDate { get; set; }
        DateTime UpdatedDate { get; set; }

    }
}
