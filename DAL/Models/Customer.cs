// =============================
// Email: info@ebenmonney.com
// www.ebenmonney.com/templates
// =============================

using DAL.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    //Inheritance AdditableEntity
    public class Customer : AuditableEntity
    {
        public int Id { get; set; }      // primary key 
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public Gender Gender { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

       // public ICollection<Order> Orders { get; set; }     // โดย class Customer บรรจุ Orders ได้หลายรายการ
    }
}
