using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApp.Core
{
   public class PhoneBookViewModel
    {
        public int PersonId { get; set; }
        public FullName FullName { get; set; }
        public string PostNumber { get; set; }
        public string Street { get; set; }
        public List<PhoneViewModel> phoneNumber { get; set; }

    }
}
