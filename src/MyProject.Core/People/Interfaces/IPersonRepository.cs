using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MyProject.Core.People.Entities;

namespace MyProject.Core.People.Interfaces
{
    public interface IPersonRepository
    {
        Person Add(Person entity);
        IEnumerable<Person> Find(Expression<Func<Person, bool>> predicate);
    }
}