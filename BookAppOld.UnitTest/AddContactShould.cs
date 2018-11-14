using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using BookApp.Core;
using BookApp.Infrastructure;
using Xunit;
using AutoFixture;

namespace BookApp.UnitTest
{
    [TestClass]
    public  class AddContactShould
    {
       [Fact]
        public void throw_when_person_is_null()
        {
            // Arrange
            UnitOfWork Repo = new UnitOfWork(new BookContext());
            Person person = null;

            //   Act
            var ex = Xunit.Assert.Throws<ArgumentNullException>(
               () => Repo.Person.Add(person));
            // Assert
            Xunit.Assert.Equal("entity", ex.ParamName);
        }

        [Fact]
        public void throw_when_ZipCode_is_duplicate()
        {
            // Arrange
            var fixture = new Fixture();
            UnitOfWork Repo = new UnitOfWork(new BookContext());

            var Addr = 2500;
            // Act
            var ex = Xunit.Assert.Throws<ArgumentException>(
                () => Repo.Address.FindByZipCode(Addr));
            // Assert
            Xunit.Assert.Equal("This address has already existed.", ex.ParamName);
        }
    }
}
