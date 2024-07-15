using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Azure;
using IoTClassLibrary.IRepository;
using Microsoft.Azure.Devices.Client;

namespace IOTDeviceSimulator
{
    public class DeviceSimulator : IDeviceSimulator
    {
        private readonly IDeviceClient _deviceClient;
        private readonly Device _device;
        private DateTime _timestamp;
        private readonly double _temperature;
        private readonly double _humidity;
        public DeviceSimulator(IDeviceClient deviceClient, string deviceId, string tag, double temperature, double humidity, int dataFrequency, int count)
        {
            if (deviceClient == null)
            {
                throw new ArgumentNullException(nameof(deviceClient), "Device client cannot be null");
            }
            _deviceClient = deviceClient;
            _device = new Device(deviceId, tag, temperature, humidity, dataFrequency, count);
            _timestamp = DateTime.Now;
        }

        public string GenerateJsonOutput()
        {
            if (_temperature < 0 || _temperature > 100.0)
            {
                throw new ArgumentException("Temperature is out of range. It should be between 0 and 100.", nameof(_temperature));
            }

            if (_humidity < 0 || _humidity > 100.0)
            {
                throw new ArgumentException("Humidity is out of range. It should be between 0 and 100.", nameof(_humidity));
            }
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

        public async Task SendMessageToIotHub()
        {
            var messageString = GenerateJsonOutput();
            var messageBytes = Encoding.UTF8.GetBytes(messageString);
            var message = new Message(messageBytes);

            await _deviceClient.SendEventAsync(message);
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