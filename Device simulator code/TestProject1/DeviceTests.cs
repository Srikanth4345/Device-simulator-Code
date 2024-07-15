using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOTDeviceSimulator.Tests
{
    [TestClass]
    public class DeviceTests
    {
        [TestMethod]
        public void Constructor_DeviceIdIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            string deviceId = null;
            string tag = "tag";
            double temperature = 20.0;
            double humidity = 60.0;
            int dataFrequency = 10;
            int count = 5;

            // Act and Assert
            Assert.ThrowsException<ArgumentNullException>(() => new Device(deviceId, tag, temperature, humidity, dataFrequency, count));
        }

        [TestMethod]
        public void Constructor_TagIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            string deviceId = "deviceId";
            string tag = null;
            double temperature = 20.0;
            double humidity = 60.0;
            int dataFrequency = 10;
            int count = 5;

            // Act and Assert
            Assert.ThrowsException<ArgumentNullException>(() => new Device(deviceId, tag, temperature, humidity, dataFrequency, count));
        }

        [TestMethod]
        public void Constructor_TemperatureIsOutOfRange_ThrowsArgumentException()
        {
            // Arrange
            string deviceId = "deviceId";
            string tag = "tag";
            double temperature = 150.0;
            double humidity = 60.0;
            int dataFrequency = 10;
            int count = 5;

            // Act and Assert
            Assert.ThrowsException<ArgumentException>(() => new Device(deviceId, tag, temperature, humidity, dataFrequency, count));
        }

        [TestMethod]
        public void Constructor_HumidityIsOutOfRange_ThrowsArgumentException()
        {
            // Arrange
            string deviceId = "deviceId";
            string tag = "tag";
            double temperature = 20.0;
            double humidity = 150.0;
            int dataFrequency = 10;
            int count = 5;

            // Act and Assert
            Assert.ThrowsException<ArgumentException>(() => new Device(deviceId, tag, temperature, humidity, dataFrequency, count));
        }
    }
}
