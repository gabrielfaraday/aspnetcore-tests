using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MyProject.Api.Controllers;
using MyProject.Api.Dtos;
using MyProject.Core.People.Entities;
using MyProject.Core.People.Interfaces;
using Xunit;

namespace MyProject.Api.UnitTests.Controllers
{
    public class PersonControllerTest
    {
        Mock<IPersonService> _serviceMock;
        PersonController _controller;

        public PersonControllerTest()
        {
            _serviceMock = new Mock<IPersonService>();
            _controller = new PersonController(_serviceMock.Object);
        }

        [Fact]
        public void Get_ShouldCallService_AndReturn200WithDtos()
        {
            var expectedReturnFromService = new List<Person>
            {
                new Person (id: 1, name: "João", email: "joao@test.com", dateOfBirth: new DateTime(1970, 09, 16)),
                new Person (id: 2, name: "Pedro", email: "pedro@test.com", dateOfBirth: new DateTime(1970, 09, 16))
            };

            _serviceMock.Setup(_ => _.GetAll()).Returns(expectedReturnFromService);

            var result = _controller.Get();

            _serviceMock.Verify(x => x.GetAll(), Times.Once);

            var objResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, objResult.StatusCode);

            var dtos = Assert.IsType<List<PersonDto>>(objResult.Value);
            Assert.NotEmpty(dtos);
            Assert.Equal(expectedReturnFromService.Count, dtos.Count);
        }

        [Fact]
        public void GetById_ShouldCallService_AndReturn200WithDto_WhenPersonFound()
        {
            var expectedReturnFromService = new Person (id: 1, name: "João", email: "joao@test.com", dateOfBirth: new DateTime(1970, 09, 16));

            _serviceMock.Setup(_ => _.FindById(1)).Returns(expectedReturnFromService);

            var result = _controller.GetById(1);

            _serviceMock.Verify(x => x.FindById(1), Times.Once);

            var objResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, objResult.StatusCode);

            var dto = Assert.IsType<PersonDto>(objResult.Value);
            Assert.Equal(expectedReturnFromService.Name, dto.Name);
        }

        [Fact]
        public void GetById_ShouldCallService_AndReturn404_WhenPersonNotFound()
        {
            _serviceMock.Setup(_ => _.FindById(1)).Returns(value: null);

            var result = _controller.GetById(1);

            _serviceMock.Verify(x => x.FindById(1), Times.Once);

            var objResult = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(404, objResult.StatusCode);
        }

        [Fact]
        public void Post_ShouldCallService_AndReturn201_WhenEverythingGoesRight()
        {
            var dto = new NewPersonDto { Name = "João", Email = "joao@test.com", DateOfBirth = new DateTime(1970, 09, 16) };
            var personMock = new Mock<Person>();

            _serviceMock.Setup(x => x.Add(It.IsAny<Person>())).Returns(personMock.Object);
            personMock.Setup(x => x.IsValid()).Returns(true);

            var result = _controller.Post(dto);

            var objResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(201, objResult.StatusCode);
        }

        [Fact]
        public void Post_ShouldCallService_AndReturn400WithErrors_WhenValidationFails()
        {
            var dto = new NewPersonDto { Name = "João", Email = "joao@test.com", DateOfBirth = new DateTime(1970, 09, 16) };
            var personMock = new Mock<Person>();

            _serviceMock.Setup(x => x.Add(It.IsAny<Person>())).Returns(personMock.Object);
            personMock.Setup(x => x.IsValid()).Returns(false);
            
            var result = _controller.Post(dto);

            var objResult = Assert.IsType<BadRequestResult>(result);
            Assert.Equal(400, objResult.StatusCode);
        }
    }
}
