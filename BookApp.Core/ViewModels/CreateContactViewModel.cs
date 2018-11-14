using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApp.Core.ViewModels
{
   public class CreateContactViewModel
    {
        public int PersonId { get; set; }
        public FullName FullName { get; set; }

        [Required()]
        public int PostNumber { get; set; }

        [Required()]
        public string Street { get; set; }

        [Required()]
        public int Unit { get; set; }

        [Required()]
        public int PhoneNumber { get; set; }

        public Int16 Flag { get; set; }
        public int PhoneId { get; set; }
    }
}
