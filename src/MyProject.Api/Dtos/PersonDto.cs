using System;
using MyProject.Core.People.Entities;

namespace MyProject.Api.Dtos
{
    public class PersonDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
    }

    public static class PersonDtoExtensions
    {
        public static PersonDto ToDto(this Person person)
        {
            return new PersonDto
            {
                Id = person.Id,
                Name = person.Name,
                Email = person.Email,
                DateOfBirth = person.DateOfBirth
            };
        }
    }
}