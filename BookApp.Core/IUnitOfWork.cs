using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApp.Core
{
    public interface IUnitOfWork:IDisposable
    {
        IPersonRepository Person { get; }
        IPhoneRepository Phone { get; }
        IAddressRepository Address { get; }
        int Complete();
    }
}
