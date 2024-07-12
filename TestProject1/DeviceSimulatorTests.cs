using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Azure.Devices.Client;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;

namespace IOTDeviceSimulator.Tests
{
    [TestClass]
    public class DeviceSimulatorTests
    {
        [TestMethod]
        public void DeviceSimulator_Constructor_DeviceIdIsNull_ThrowsArgumentNullException()
        {
            // Act and Assert
            Assert.ThrowsException<ArgumentNullException>(() => new DeviceSimulator(null, "tag", 20, 60, 10, 5));
        }

        [TestMethod]
        public void DeviceSimulator_Constructor_TagIsNull_ThrowsArgumentNullException()
        {
            // Act and Assert
            Assert.ThrowsException<ArgumentNullException>(() => new DeviceSimulator("deviceId", null, 20, 60, 10, 5));
        }

        [TestMethod]
        public void DeviceSimulator_GenerateJsonOutput_ValidData_Succeeds()
        {
            // Arrange
            var deviceSimulator = new DeviceSimulator("deviceId", "tag", 20, 60, 10, 5);

            // Act
            var jsonOutput = deviceSimulator.GenerateJsonOutput();

            // Assert
            Assert.IsNotNull(jsonOutput);
            Assert.IsTrue(jsonOutput.Contains("deviceId"));
            Assert.IsTrue(jsonOutput.Contains("tag"));
            Assert.IsTrue(jsonOutput.Contains("Temp"));
            Assert.IsTrue(jsonOutput.Contains("Humidity"));
        }




    }
}