using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MyProject.Api.Dtos;
using MyProject.Core.People.Interfaces;

namespace MyProject.Api.Controllers
{
    [ApiController]
    [Route("api/person")]
    public class PersonController : ControllerBase
    {
        IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var people = _personService.GetAll();

            var dtos = new List<PersonDto>();
            people.ToList().ForEach(person => dtos.Add(person.ToDto()));

            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var person = _personService.FindById(id);

            if (person == null)
                return NotFound();

            return Ok(person.ToDto());
        }

        [HttpPost]
        public IActionResult Post([FromBody] NewPersonDto dto)
        {
            var person = _personService.Add(dto.ToEntity());

            if (!person.IsValid())
                return BadRequest();

            return StatusCode(201, person.ToDto());
        }
    }
}
