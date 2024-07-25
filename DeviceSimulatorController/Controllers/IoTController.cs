using DeviceSimulatorController.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace IOTDeviceSimulator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IoTController : ControllerBase
    {
        private readonly TableStorageService _tableStorageService;
        private readonly ILogger<IoTController> _logger;

        public IoTController(TableStorageService tableStorageService, ILogger<IoTController> logger)
        {
            _tableStorageService = tableStorageService;
            _logger = logger;
        }

        [HttpGet("data")]
        public async Task<IActionResult> GetDataBetweenTimes([FromQuery] string startDateTime, [FromQuery] string endDateTime)
        {
            try
            {
                DateTime startDate = DateTime.ParseExact(startDateTime, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                DateTime endDate = DateTime.ParseExact(endDateTime, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);

                long startEpoch = new DateTimeOffset(startDate).ToUnixTimeSeconds();
                long endEpoch = new DateTimeOffset(endDate).ToUnixTimeSeconds();

                _logger.LogInformation("Fetching data between {startEpoch} and {endEpoch}", startEpoch, endEpoch);

                var data = await _tableStorageService.GetDataBetweenTimesAsync(startEpoch, endEpoch);
                if (!data.Any())
                {
                    _logger.LogInformation("No devices found between the specified times.");
                    return Ok("No devices found");
                }
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching data.");
                return StatusCode(500, ex.Message);
            }
        }
    }
}