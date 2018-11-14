using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookApp.Core
{
    public interface IRepository<T> where T:class
    {
        void Add(T entity);
        void Edit(T entity);
        void Remove(T entity);
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Expression<Func<T,bool>> predicate);
    }
}
