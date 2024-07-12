﻿using System;
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

            DeviceId = deviceId;
            Tag = tag;
            Temperature = temperature;
            Humidity = humidity;
            DataFrequency = dataFrequency;
            Count = count;
        }
    }

}
