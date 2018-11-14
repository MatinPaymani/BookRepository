using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApp.Core
{
    public class Address : BaseEntity
    {
        public Address()
        { }

        [Required()]
        public int PersonIdn { get; set; }

        [Required()]
        public int PostNumber { get; set; }

        [Required()]
        public string Street { get; set; }

        [StringLength(2)]
        [Column(TypeName = "char")]
        public string Floor { get; set; }

        public int Unit { get; set; }

        public virtual Person Person { get; set; }

        [StringLength(1)]
        [Column(TypeName = "char")]
        public string Status { get; set; }

        
    }
}
