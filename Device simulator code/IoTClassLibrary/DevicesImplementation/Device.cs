using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOTDeviceSimulator
{
    public class Device
    {
        public string DeviceId { get; set; }
        public string Tag { get; set; }
        public double Temperature { get; set; }
        public double Humidity { get; set; }
        public int DataFrequency { get; set; }
        public int Count { get; set; }


        public Device(string deviceId, string tag, double temperature, double humidity, int dataFrequency, int count)

        {
            if (string.IsNullOrEmpty(deviceId))
            {
                throw new ArgumentNullException(nameof(deviceId), "Device Id cannot be empty");
            }
            if (string.IsNullOrEmpty(tag))
            {
                throw new ArgumentNullException(nameof(tag), "Tag cannot be empty");
            }
            if (humidity < 0 || humidity > 100)
            {
                throw new ArgumentException("Humidity is out of range. It should be between 0 and 100.", nameof(humidity));
            }
            if (temperature < -50 || temperature > 50)
            {
                throw new ArgumentException("Temperature is out of range. It should be between -50 and 50.", nameof(temperature));
            }


            DeviceId = deviceId;
            Tag = tag;
            Temperature = temperature;
            Humidity = humidity;
            DataFrequency = dataFrequency;
            Count = count;
        }
    }

}
