using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using BookApp.Core;
namespace BookApp.Infrastructure
{
    public class AddressMap:EntityTypeConfiguration<Address>
    {
        public AddressMap()
        {
          HasKey(current => current.PersonIdn);
          HasRequired(current => current.Person)
        .WithOptional(person => person.Address)
        .WillCascadeOnDelete(true);
        }

    }
}




