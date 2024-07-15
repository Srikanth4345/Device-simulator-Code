using IoTClassLibrary.IRepository;
using Microsoft.Azure.Devices.Client;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace IOTDeviceSimulator.Tests
{
    [TestClass]
    public class DeviceSimulatorTests
    {
        [TestMethod]
        public async Task SendMessageToIotHub_SendMessageSuccessfully()
        {
            // Arrange
            var deviceClientMock = new Mock<IDeviceClient>();
            deviceClientMock.Setup(d => d.SendEventAsync(It.IsAny<Message>()))
               .Returns(Task.CompletedTask);

            var deviceSimulator = new DeviceSimulator(deviceClientMock.Object, "deviceId", "tag", 20.0, 60.0, 10, 5);

            // Act
            await deviceSimulator.SendMessageToIotHub();

            // Assert
            deviceClientMock.Verify(d => d.SendEventAsync(It.IsAny<Message>()), Times.Once);
        }

        [TestMethod]
        public async Task SendMessageToIotHub_ThrowExceptionWhenSendEventAsyncFails()
        {
            // Arrange
            var deviceClientMock = new Mock<IDeviceClient>();
            deviceClientMock.Setup(d => d.SendEventAsync(It.IsAny<Message>()))
               .ThrowsAsync(new Exception("Error sending event"));

            var deviceSimulator = new DeviceSimulator(deviceClientMock.Object, "deviceId", "tag", 20.0, 60.0, 10, 5);

            // Act and Assert
            await Assert.ThrowsExceptionAsync<Exception>(() => deviceSimulator.SendMessageToIotHub());
        }

        [TestMethod]
        public void GenerateJsonOutput_GenerateJsonSuccessfully()
        {
            // Arrange
            var deviceSimulator = new DeviceSimulator(new Mock<IDeviceClient>().Object, "deviceId", "tag", 20.0, 60.0, 10, 5);

            // Act
            var jsonOutput = deviceSimulator.GenerateJsonOutput();

            // Assert
            Assert.IsNotNull(jsonOutput);
            Assert.IsTrue(jsonOutput.Contains("deviceId"));
            Assert.IsTrue(jsonOutput.Contains("tag"));
            Assert.IsTrue(jsonOutput.Contains("trend"));
        }
        [TestMethod]
        public void Constructor_ThrowExceptionWhenDeviceIdIsNull()
        {
            // Arrange
            var deviceClientMock = new Mock<IDeviceClient>();

            // Act and Assert
            Assert.ThrowsException<ArgumentNullException>(() => new DeviceSimulator(deviceClientMock.Object, null, "tag", 20.0, 60.0, 10, 5));
        }
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
            Assert.ThrowsException<ArgumentNullException>(() => new DeviceSimulator(new Mock<IDeviceClient>().Object, deviceId, tag, temperature, humidity, dataFrequency, count));
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
            Assert.ThrowsException<ArgumentNullException>(() => new DeviceSimulator(new Mock<IDeviceClient>().Object, deviceId, tag, temperature, humidity, dataFrequency, count));
        }
        [TestMethod]
        public void Constructor_DeviceClientIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            IDeviceClient deviceClient = null;
            string deviceId = "deviceId";
            string tag = "tag";
            double temperature = 20.0;
            double humidity = 60.0;
            int dataFrequency = 10;
            int count = 5;

            // Act and Assert
            Assert.ThrowsException<ArgumentNullException>(() => new DeviceSimulator(deviceClient, deviceId, tag, temperature, humidity, dataFrequency, count));
        }

        [TestMethod]
        public void Constructor_DeviceIdIsEmpty_ThrowsArgumentNullException()
        {
            // Arrange
            string deviceId = string.Empty;
            string tag = "tag";
            double temperature = 20.0;
            double humidity = 60.0;
            int dataFrequency = 10;
            int count = 5;

            // Act and Assert
            Assert.ThrowsException<ArgumentNullException>(() => new DeviceSimulator(new Mock<IDeviceClient>().Object, deviceId, tag, temperature, humidity, dataFrequency, count));
        }

        [TestMethod]
        public void Constructor_TagIsEmpty_ThrowsArgumentNullException()
        {
            // Arrange
            string deviceId = "deviceId";
            string tag = string.Empty;
            double temperature = 20.0;
            double humidity = 60.0;
            int dataFrequency = 10;
            int count = 5;

            // Act and Assert
            Assert.ThrowsException<ArgumentNullException>(() => new DeviceSimulator(new Mock<IDeviceClient>().Object, deviceId, tag, temperature, humidity, dataFrequency, count));
        }


    }
}