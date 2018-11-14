using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookApp.Core;

namespace BookApp.Infrastructure
{
   public class AddressRepository:Repository<Address>,IAddressRepository
    {
        public AddressRepository(BookContext context)
            : base(context)
        {
        }

        public BookContext BookContext
        {
            get { return Context as BookContext; }
        }

        public bool FindByZipCode(int zip)
        {
           return BookContext.Addresses.Any(x => x.PostNumber == zip);
        }
    }
}
