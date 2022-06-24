using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Publisher.DTOs;
using Publisher.Events;
using Publisher.Services.Interfaces;

namespace Publisher.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly ILogger<PersonController> _logger;
        private readonly IPersonService _personService;
        private readonly IPublishEndpoint _publishEndpoint;

        public PersonController(
            ILogger<PersonController> logger,
            IPersonService personService,
            IPublishEndpoint publishEndpoint)
        {
            _logger = logger;
            _personService = personService;
            _publishEndpoint = publishEndpoint;
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreatePersonDTO person)
        {
            var result = _personService.Create(person);

            _publishEndpoint.Publish<ICreatedPersonEvent>(result);

            return Ok(result);
        }
    }
}
