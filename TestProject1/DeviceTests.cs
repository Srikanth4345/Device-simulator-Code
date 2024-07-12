using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IOTDeviceSimulator.Tests
{
    [TestClass]
    public class DeviceTests
    {
        [TestMethod]
        public void Device_Constructor_DeviceIdIsNull_ThrowsArgumentNullException()
        {
            // Act and Assert
            Assert.ThrowsException<ArgumentNullException>(() => new Device(null, "tag", 20, 60, 10, 5));
        }

        [TestMethod]
        public void Device_Constructor_TagIsNull_ThrowsArgumentNullException()
        {
            // Act and Assert
            Assert.ThrowsException<ArgumentNullException>(() => new Device("deviceId", null, 20, 60, 10, 5));
        }

        [TestMethod]
        public void Device_Constructor_ValidData_Succeeds()
        {
            // Act
            var device = new Device("deviceId", "tag", 20, 60, 10, 5);

            // Assert
            Assert.IsNotNull(device);
            Assert.AreEqual("deviceId", device.DeviceId);
            Assert.AreEqual("tag", device.Tag);
            Assert.AreEqual(20, device.Temperature);
            Assert.AreEqual(60, device.Humidity);
            Assert.AreEqual(10, device.DataFrequency);
            Assert.AreEqual(5, device.Count);
        }



    }
}