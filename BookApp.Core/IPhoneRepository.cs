using BookApp.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApp.Core
{
  public interface IPhoneRepository:IRepository<Phone>
    {
        Phone FindById(int id);
        List<PhoneViewModel> FingByPersonId(int id);
    }
}
