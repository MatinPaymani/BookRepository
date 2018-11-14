using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BookApp.Core;
using System.Linq.Dynamic;
using System.Reflection;
using System.Data.Entity;
using BookApp.Core.ViewModels;

namespace BookApp.Infrastructure
{
    public class PersonRepository:Repository<Person>,IPersonRepository
    {
        public PersonRepository(BookContext context )
            :base (context)
        {
        }
        public ResponseDTO<PhoneBookViewModel> GetAllData(PaginationFilterDTO model)
        {
            var query = BookContext.People.Join(BookContext.Addresses,
                        p => p.ID,
                        a => a.PersonIdn,
                        (person, addr) => new PhoneBookViewModel {
                             FirstName=person.FullName.FirstName,
                             LastName=person.FullName.LastName,
                             PersonId=person.ID,
                             PostNumber=addr.PostNumber.ToString(),
                             Street=addr.Street
                        });

            if (!string.IsNullOrEmpty(model.Search))
            {
                query = query.Where(x => x.FirstName.Contains(model.Search));
            }

            switch (model.SortDirection.ToLower())
            {
                case "asc":
                    query = query.OrderBy(BuildPredicate(model.SortBy)).AsQueryable();
                    break;
                default:
                    query = query.OrderByDescending(BuildPredicate(model.SortBy)).AsQueryable();
                    break;
            }

            var final = query.Skip((model.Current - 1) * model.RowCount)
                             .Take(model.RowCount == -1 ? query.Count() : model.RowCount)
                            .ToList();

            var response = new ResponseDTO<PhoneBookViewModel>()
            {
                current = model.Current,
                rowCount = model.RowCount,
                total=query.Count(),
                rows = final
            };
            return response;
        }
        private static Func<PhoneBookViewModel, object> BuildPredicate(string propertyName)
        {
            Type sortType = typeof(PhoneBookViewModel);
            ParameterExpression sortParam = Expression.Parameter(sortType, "x");
            MemberInfo property = sortType.GetProperty(propertyName);
            MemberExpression valueProperty = Expression.MakeMemberAccess(sortParam, property);
            UnaryExpression expression = Expression.Convert(valueProperty, typeof(object));
            Expression<Func<PhoneBookViewModel, object>> orderByExpression = Expression.Lambda<Func<PhoneBookViewModel, object>>(expression, sortParam);
            return orderByExpression.Compile();
        }
        public PhoneBookViewModel FindById(int id)
        {
            var query = BookContext.People.Where(x => x.ID == id).Join(BookContext.Addresses,
                       p => p.ID,
                       a => a.PersonIdn,
                       (person, addr) => new { person, addr });


            var final = query.GroupJoin(BookContext.Phones,
                        q => q.person.ID,
                        p => p.PersonIdn,
                        (q, p) => new {
                            Key = q,
                            values = p
                        }
                      ).Select(x => new PhoneBookViewModel()
                      {
                          FirstName = x.Key.person.FullName.FirstName,
                          LastName=x.Key.person.FullName.LastName,
                          PersonId = x.Key.person.ID,
                          PostNumber = x.Key.addr.PostNumber.ToString(),
                          Street = x.Key.addr.Street,
                          phoneNumber = x.values.Select(y => new PhoneViewModel
                          {
                              PhoneNumber = y.PhoneNumber,
                              Flag = (PhoneType)y.Flag,
                              PhoneId = y.ID,
                              PersonId = y.PersonIdn
                          }).ToList()

                      }).SingleOrDefault();

            return final;
        }
        public Person FindPerson(int id)
        {
          return  BookContext.People.Find(id);
        }
        public IEnumerable<PhoneBookViewModel> SerchyBy(Expression<Func<Person, bool>> predicate)
        {
            var query = BookContext.People.Where(predicate).Join(BookContext.Addresses,
                        p => p.ID,
                        a => a.PersonIdn,
                        (person, addr) => new { person, addr });


            var final = query.GroupJoin(BookContext.Phones,
                        q => q.person.ID,
                        p => p.PersonIdn,
                        (q, p) => new {
                            Key = q,
                            values = p
                        }
                      ).Select(x => new PhoneBookViewModel()
                      {
                          FirstName = x.Key.person.FullName.FirstName,
                          LastName=x.Key.person.FullName.LastName,
                          PersonId = x.Key.person.ID,
                          PostNumber = x.Key.addr.PostNumber.ToString(),
                          Street = x.Key.addr.Street,
                          phoneNumber = x.values.Select(y => new PhoneViewModel
                          {
                              PhoneNumber = y.PhoneNumber,
                              Flag = (PhoneType)y.Flag,
                              PhoneId = y.ID,
                              PersonId = y.PersonIdn
                          }).ToList()

                      }).ToList();
            return final;
        }
        public BookContext BookContext
        {
            get {return Context as BookContext;  }
        }
    }
}
