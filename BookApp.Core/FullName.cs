
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookApp.Core
{
    [ComplexType]
    public class FullName:System.Object
    {
        public FullName() { }
        public FullName(string firstName, string lastName)
        {
            LastName = lastName;
            FirstName = firstName;
        }

        [Required()]
        [MaxLength(20)]
        public string FirstName { get; set; }

        [Required()]
        [MaxLength(30)]
        public string LastName { get; set; }
    }
}
