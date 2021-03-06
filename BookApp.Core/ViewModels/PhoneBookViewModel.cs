﻿using BookApp.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApp.Core.ViewModels
{
   public class PhoneBookViewModel
    {
        public int PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PostNumber { get; set; }
        public string Street { get; set; }
        public List<PhoneViewModel> phoneNumber { get; set; }

    }
}
