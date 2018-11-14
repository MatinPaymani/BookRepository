using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApp.Core.ViewModels
{
  public  class PhoneViewModel
    {
       
        public int PhoneNumber { get; set; }
        public PhoneType Flag { get; set; }
        public int PhoneId { get; set; }
        public int PersonId { get; set; }
    }
    public enum PhoneType
    {
        None,
        Home = 1,
        Work = 2,
        Mobile=3
    }
}
