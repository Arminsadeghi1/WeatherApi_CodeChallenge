using Microsoft.AspNetCore.Mvc;
using Weather_ApplicationService.Weather.Handlers;

namespace WeatherApi_CodeChallenge.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class WeatherController : Controller
    {

        private readonly ILogger<WeatherController> _logger;

        public WeatherController(ILogger<WeatherController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetLastTrades([FromServices] WeatherConditionsHandler handler, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await handler.Handle(cancellationToken));
            }
            catch (NotSupportedException ex)
            {
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
