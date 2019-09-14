using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Moq;
using MyProject.Core.People.Entities;
using MyProject.Core.People.Interfaces;
using MyProject.Core.People.Services;
using Xunit;

namespace MyProject.Core.UnitTests.People.Services
{
    public class PersonServiceTest
    {
        Mock<IPersonRepository> _repositoryMock;
        Mock<Person> _personMock;
        PersonService _service;

        public PersonServiceTest()
        {
            _repositoryMock = new Mock<IPersonRepository>();
            _personMock = new Mock<Person>();
            _service = new PersonService(_repositoryMock.Object);
        }

        [Fact]
        public void Add_WhenPersonIsValid_ShouldCallRepository()
        {
            _personMock.Setup(x => x.IsValid()).Returns(true);

            _service.Add(_personMock.Object);

            _repositoryMock.Verify(x => x.Add(_personMock.Object), Times.Once);
        }

        [Fact]
        public void Add_WhenPersonIsNotValid_ShouldNotCallRepository()
        {
            _personMock.Setup(x => x.IsValid()).Returns(false);

            _service.Add(_personMock.Object);

            _repositoryMock.Verify(x => x.Add(_personMock.Object), Times.Never);
        }

        [Fact]
        public void FindById_ShouldCallRepository_AndReturnPersonFromRepository()
        {
            var expected = new Person("abc", "abc@test.com", new DateTime(1990,01,02));
            _repositoryMock.Setup(x => x.Find(It.IsAny<Expression<Func<Person, bool>>>())).Returns(new List<Person>{expected});

            var result = _service.FindById(It.IsAny<int>());

            _repositoryMock.Verify(x => x.Find(It.IsAny<Expression<Func<Person, bool>>>()), Times.Once);
            Assert.Same(expected, result);
        }

        [Fact]
        public void GetAll_ShouldCallRepository_AndReturnPeopleFromRepository()
        {
            var expected = new List<Person>
            {
                new Person("abc", "abc@test.com", new DateTime(1990,01,02)),
                new Person("def", "def@test.com", new DateTime(1990,01,02))
            };

            _repositoryMock.Setup(x => x.Find(It.IsAny<Expression<Func<Person, bool>>>())).Returns(expected);

            var result = _service.GetAll();

            _repositoryMock.Verify(x => x.Find(It.IsAny<Expression<Func<Person, bool>>>()), Times.Once);
            Assert.Same(expected, result);
        }
        
    }
}