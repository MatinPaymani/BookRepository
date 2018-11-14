using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace BookApp.Core
{
    public class Person: BaseEntity
    {
        public Person()
        { }

        [Key()]
        [Required()]
        [Column("PersonIdn")]
        public int ID { get; set; }
        public FullName FullName { get; set; }
        public virtual IList<Phone> Phone { get; set; }
        public virtual Address Address { get; set; }
        public int? AddressIdn { get; set; }

    }
}
