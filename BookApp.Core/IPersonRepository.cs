using BookApp.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookApp.Core
{
   public interface IPersonRepository:IRepository<Person>
    {
        ResponseDTO<PhoneBookViewModel> GetAllData(PaginationFilterDTO model);
        PhoneBookViewModel FindById(int id);
        Person FindPerson(int id);
        IEnumerable<PhoneBookViewModel> SerchyBy(Expression<Func<Person, bool>> predicate);
    }
}
