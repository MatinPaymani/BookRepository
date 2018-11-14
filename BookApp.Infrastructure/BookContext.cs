using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApp.Infrastructure
{
   public  class BookContext: DbContext
    {
        static BookContext()
        {
            System.Data.Entity.Database.SetInitializer
                           (new System.Data.Entity.MigrateDatabaseToLatestVersion<BookContext, BookApp.Infrastructure.Migrations.Configuration>());
        }
        public BookContext() : base("BookAppConnectionString")
        {
            Configuration.LazyLoadingEnabled = true;
        }

        

        public DbSet<BookApp.Core.Person> People { get; set; }
        public DbSet<BookApp.Core.Phone> Phones { get; set; }
        public DbSet<BookApp.Core.Address> Addresses { get; set; }

        protected override void OnModelCreating
            (DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new PhoneMap());
            modelBuilder.Configurations.Add(new PersonMap());
            modelBuilder.Configurations.Add(new AddressMap());
        }
    }
}
