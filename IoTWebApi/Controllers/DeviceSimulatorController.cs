using IOTDeviceSimulator;
using Microsoft.AspNetCore.Mvc;

namespace IoTWebApi.Controllers
{
    public class DeviceSimulatorController : Controller
    {
        private readonly string _connectionString = "HostName=ih-iotproject.azure-devices.net;DeviceId=50f7481b-2a67-4f32-ac65-78e261326031;SharedAccessKey=XAex3q+Pih3ySRTXEDZKjoPpEAYWHDdZQAIoTE/TzMc=";

        [HttpPost]
        public async Task<IActionResult> SendMessageToIotHub(DeviceSimulatorRequest request)
        {
            var deviceClientImpl = new DeviceClientImpl(_connectionString);

            var deviceId = GetDeviceId(request.DeviceIdChoice);
            if (string.IsNullOrEmpty(deviceId))
            {
                return BadRequest("Invalid device ID choice. Please enter a valid number.");
            }

            var deviceSimulator = new DeviceSimulator(deviceClientImpl, deviceId, request.Tag, request.Temperature, request.Humidity, request.DataFrequency, request.Count);

            for (int i = 0; i < request.Count; i++)
            {
                await deviceSimulator.SendMessageToIotHub();
                Console.WriteLine($"Message {i + 1}  sent to IoT Hub!");
                await Task.Delay(request.DataFrequency * 1000);
            }

            return Ok($"Messages sent to IoT Hub successfully!");
        }

        private string GetDeviceId(int deviceIdChoice)
        {
            switch (deviceIdChoice)
            {
                case 1:
                    return "63d0d1be-3df7-45f4-9b1a-73126e960190";
                case 2:
                    return "5865aab4-3a1f-4ee6-85d3-ac26da75b742";
                case 3:
                    return "50f7481b-2a67-4f32-ac65-78e261326031";
                default:
                    return null;
            }
        }

        public class DeviceSimulatorRequest
        {
            public int DeviceIdChoice { get; set; }
            public string Tag { get; set; }
            public double Temperature { get; set; }
            public double Humidity { get; set; }
            public int DataFrequency { get; set; }
            public int Count { get; set; }
        }

    }
}
