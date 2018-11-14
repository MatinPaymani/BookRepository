using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BookApp.Core
{
   public class Phone:BaseEntity
    {
        public Phone()
        { }

        [Key()]
        [Required()]
        [Column("PhoneIdn")]
        public int ID { get; set; }

        [Required()]
        public int PhoneNumber { get; set; }

        [Required()]
        public int PersonIdn { get; set; }

         public Int16 Flag { get; set; }

        public virtual Person Person { get; set; }
       
    }
}
