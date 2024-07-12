using System.Text;
using System.Text.Json;
using IoTClassLibrary.IRepository;
using Microsoft.Azure.Devices.Client;

namespace IOTDeviceSimulator
{
    public class DeviceSimulator : IDeviceSimulator
    {
        private readonly Device _device;
        private DateTime _timestamp;

        public DeviceSimulator(string deviceId, string tag, double temperature, double humidity, int dataFrequency, int count)
        {
            _device = new Device(deviceId, tag, temperature, humidity, dataFrequency, count);
            _timestamp = DateTime.Now;
        }

        public string GenerateJsonOutput()
        {
            var trendData = new[]
            {
                new TrendData("Temp", _device.Temperature, _timestamp),
                new TrendData("Humidity", _device.Humidity, _timestamp)
            };

            var jsonData = new
            {
                d = _device.DeviceId,
                t = _device.Tag,
                trend = trendData
            };

            return JsonSerializer.Serialize(jsonData);
        }

        public async Task SendMessageToIotHub(DeviceClient deviceClient)
        {
            if (deviceClient == null)
            {
                throw new ArgumentNullException(nameof(deviceClient));
            }
            var messageString = GenerateJsonOutput();
            var messageBytes = Encoding.UTF8.GetBytes(messageString);
            var message = new Message(messageBytes);

            await deviceClient.SendEventAsync(message);
        }
    }

    public class TrendData
    {
        public string Tag { get; set; }
        public double V { get; set; }
        public long T { get; set; }

        public TrendData(string tag, double value, DateTime timestamp)
        {
            Tag = tag;
            V = value;
            T = (long)timestamp.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        }
    }
}