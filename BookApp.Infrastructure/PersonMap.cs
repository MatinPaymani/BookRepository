using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using BookApp.Core;

namespace BookApp.Infrastructure
{
    public class PersonMap:EntityTypeConfiguration<Person>
    {
        public PersonMap()
        {

        }
    }
}

