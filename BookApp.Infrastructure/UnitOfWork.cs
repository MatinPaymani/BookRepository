using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookApp.Core;

namespace BookApp.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BookContext _context;
        public UnitOfWork(BookContext context)
        {
            _context = context;
            Person = new PersonRepository(_context);
            Address = new AddressRepository(_context);
            Phone = new PhoneRepository(_context);


        }
        public IPersonRepository Person { get; private set; }
        public IAddressRepository Address { get; private set; }
        public IPhoneRepository Phone { get; private set; }
        public int Complete()
        {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
