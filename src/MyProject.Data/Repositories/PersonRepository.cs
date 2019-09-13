using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MyProject.Core.People.Entities;
using MyProject.Core.People.Interfaces;
using MyProject.Data.Context;

namespace MyProject.Data.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        protected MainContext Db;
        protected DbSet<Person> DbSet;

        public PersonRepository(MainContext mainContext)
        {
            Db = mainContext;
            DbSet = Db.Set<Person>();
        }

        public virtual Person Add(Person entity)
        {
            entity = DbSet.Add(entity).Entity;
            Db.SaveChanges();
            return entity;
        }

        public virtual IEnumerable<Person> Find(Expression<Func<Person, bool>> predicate)
        {
            return DbSet.AsNoTracking().Where(predicate);
        }
    }
}