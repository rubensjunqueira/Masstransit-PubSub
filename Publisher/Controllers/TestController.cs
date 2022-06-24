using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Publisher.Events;
using Publisher.Models;
using System.Threading.Tasks;

namespace Publisher.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _logger;
        private readonly IPublishEndpoint _publishEndpoint;

        public TestController(ILogger<TestController> logger, IPublishEndpoint publishEndpoint)
        {
            _logger = logger;
            _publishEndpoint = publishEndpoint;
        }

        [HttpPost]
        public async Task<ActionResult> Home([FromBody] Message model)
        {
            await _publishEndpoint.Publish<IMessageEvent>(new { Message = model.MSG });
            return Ok(model);
        }
    }
}
