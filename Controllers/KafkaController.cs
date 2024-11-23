using KafkaExample.Services;
using Microsoft.AspNetCore.Mvc;

namespace KafkaProducerConsumer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KafkaController : ControllerBase
    {
        private readonly IKafkaProducerService _producerService;

        public KafkaController(IKafkaProducerService producerService)
        {
            _producerService = producerService;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendMessage([FromQuery] string topic, [FromQuery] string message)
        {
            if (string.IsNullOrEmpty(topic) || string.IsNullOrEmpty(message))
            {
                return BadRequest("Both 'topic' and 'message' query parameters are required.");
            }

            await _producerService.SendMessageAsync(topic, message);
            return Ok($"Message '{message}' sent successfully to topic '{topic}'.");
        }
    }
}