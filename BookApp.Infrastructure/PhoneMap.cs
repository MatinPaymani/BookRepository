using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookApp.Core;
using System.Data.Entity.ModelConfiguration;

namespace BookApp.Infrastructure
{
    public class PhoneMap:EntityTypeConfiguration<Phone>
    {
        public PhoneMap()
        {
            HasRequired(current => current.Person)
           .WithMany(person => person.Phone)
           .HasForeignKey(current => current.PersonIdn)
           .WillCascadeOnDelete(true)
           ;
        }
       
    }
}

