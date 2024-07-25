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
            Assert.ThrowsException<ArgumentNullException>(() => new Device(deviceId, tag, dataFrequency, count));
        }

        [TestMethod]
        public void Constructor_TagIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            string deviceId = "deviceId";
            string tag = null;
            int dataFrequency = 10;
            int count = 5;

            // Act and Assert
            Assert.ThrowsException<ArgumentNullException>(() => new Device(deviceId, tag, dataFrequency, count));
        }


    }
}
