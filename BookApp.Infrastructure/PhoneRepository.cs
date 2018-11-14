using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookApp.Core;
using BookApp.Core.ViewModels;

namespace BookApp.Infrastructure
{
  public  class PhoneRepository:Repository<Phone>,IPhoneRepository
    {
        public PhoneRepository(BookContext context)
            :base(context)
        {
        }

        public Phone FindById(int id)
        {
            return BookContext.Phones.Find(id);
        }

        public List<PhoneViewModel> FingByPersonId(int id)
        {
            var query = BookContext.Phones.Where(x => x.PersonIdn == id).Select(x=> new PhoneViewModel {
                PhoneNumber=x.PhoneNumber,
                Flag=(PhoneType)x.Flag,
                PersonId=x.PersonIdn,
                PhoneId=x.ID
            })
            .ToList();
            return query;
        }

        public BookContext BookContext
        {
            get { return Context as BookContext; }
        }
    }
}
