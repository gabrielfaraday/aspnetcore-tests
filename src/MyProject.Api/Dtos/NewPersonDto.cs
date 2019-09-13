using System;
using MyProject.Core.People.Entities;

namespace MyProject.Api.Dtos
{
    public class NewPersonDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
    }

    public static class NewPersonDtoExtensions
    {
        public static Person ToEntity(this NewPersonDto dto)
        {
            return new Person(dto.Name, dto.Email, dto.DateOfBirth);
        }
    }
}