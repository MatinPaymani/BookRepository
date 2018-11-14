//using System.Web.Http;
//using System.Web.Http.Results;
using NUnit.Framework;
//using System.Net;
using BookApp.Core;
using BookApp.Infrastructure;
using System;
//using Xunit;
using AutoFixture;
using NSubstitute;
//using BookApp.Web.Controllers;
using BookApp.Core.ViewModels;

namespace UnitTest
{
    [TestFixture]
    public class PersonShould
    {
        [Test]
        public void add_person()
        {
            // Arrange
            var repo = Substitute.For<IPersonRepository>();
            var dummyPerson = new Person
            {
                FullName =new FullName() { FirstName="Chanler",LastName="Bing"}
            };

            // Act
            repo.Add(dummyPerson);
            
            // Assert
            repo.Received(1)
                .Add(Arg.Is<Person>(x =>
                                    x.FullName.FirstName == dummyPerson.FullName.FirstName
                                    && x.FullName.LastName == dummyPerson.FullName.LastName));
        }

        [Test]
        public void remove_person()
        {
            // Arrange
            var fixture = new Fixture();

            var repo = Substitute.For<IPersonRepository>();

            var dummyPerson = new Person
            {
                FullName = new FullName() { FirstName = "Chanler", LastName = "Bing" },
                ID = fixture.Create<int>()
            };

            //if you delete a person, all relataed rows in address table and phone table will be deleted authomatically
            // because of WillCascadeOnDelete feature in Fluent Api
            //so in this project we just need to test delete a person

            ////Act
            repo.Remove(dummyPerson);

            ////Assert
            repo.Received(1).Remove(Arg.Is(dummyPerson));

        }

        [Test]
        public void update_person()
        {
            //Arrange

            var fixture = new Fixture();
            var personId = fixture.Create<int>();

            var repo = Substitute.For<IPersonRepository>();

            var dummyPerson = new Person
            {
                ID = personId
            };

            //Act
            repo.Edit(dummyPerson);

            //Assert
            repo.Received(1).Edit(dummyPerson);

        }

    }
}
