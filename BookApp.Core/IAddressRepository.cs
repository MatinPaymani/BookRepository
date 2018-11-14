using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApp.Core
{
   public  interface IAddressRepository:IRepository<Address>
    {
        Boolean FindByZipCode(int zip);
    }
}
