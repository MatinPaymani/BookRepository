using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookApp.Core;
using System.Data.Entity;
using System.Linq.Expressions;

namespace BookApp.Infrastructure
{
    public class Repository<T> : IRepository<T> where T :class
    {
        private readonly T _entity;
        public Repository(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            _entity = entity;
        }
        protected readonly DbContext Context;
        public Repository(DbContext context)
        {
            Context = context;
        }
        public void Add(T _entity)
        {
            Context.Set<T>().Add(_entity);
        }
        public void Edit(T _entity)
        {
            Context.Entry(_entity).State = EntityState.Modified;
        }
        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return Context.Set<T>().Where(predicate);
        }
        public IEnumerable<T> GetAll()
        {
            return Context.Set<T>().ToList();
        }
        public void Remove(T _entity)
        {
            Context.Set<T>().Remove(_entity);
        }
    }
}
