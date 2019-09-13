using System.Collections.Generic;
using MyProject.Core.People.Entities;

namespace MyProject.Core.People.Interfaces
{
    public interface IPersonService
    {
        Person Add(Person entity);
        Person FindById(int id);
        IEnumerable<Person> GetAll();
    }
}