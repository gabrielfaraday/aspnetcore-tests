using System.Collections.Generic;
using System.Linq;
using MyProject.Core.People.Entities;
using MyProject.Core.People.Interfaces;

namespace MyProject.Core.People.Services
{
    public class PersonService : IPersonService
    {
        protected IPersonRepository _repository;

        public PersonService(IPersonRepository repository)
        {
            _repository = repository;
        }
        public virtual Person Add(Person entity)
        {
            if (!entity.IsValid())
                return entity;

            return _repository.Add(entity);
        }

        public Person FindById(int id)
        {
            return _repository.Find(p => p.Id == id).FirstOrDefault();
        }

        public IEnumerable<Person> GetAll()
        {
            return _repository.Find(p => true);
        }
    }
}