using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Moq;
using IoTClassLibrary.IRepository;
using System.Text;
using Microsoft.Azure.Devices.Client;

namespace IOTDeviceSimulator.Tests
{
    [TestClass]
    public class DeviceSimulatorTests
    {
        private Mock<IDeviceClient> _deviceClientMock;
        private DeviceSimulator _deviceSimulator;

        [TestInitialize]
        public void Initialize()
        {
            _deviceClientMock = new Mock<IDeviceClient>();
            _deviceSimulator = new DeviceSimulator(_deviceClientMock.Object, "deviceId", "tag", 60, 12);
        }

        [TestMethod]
        public async Task SendMessageToIotHub_ValidMessage_MessageSent()
        {
            // Arrange
            var message = new Message(Encoding.UTF8.GetBytes("Test message"));
            _deviceClientMock.Setup(d => d.SendEventAsync(message)).Returns(Task.CompletedTask);

            // Act
            await _deviceSimulator.SendMessageToIotHub();

            // Assert
            _deviceClientMock.Verify(d => d.SendEventAsync(It.IsAny<Message>()), Times.Once);
        }

        [TestMethod]
        public async Task SendMessageToIotHub_InvalidMessage_MessageNotSent()
        {
            // Arrange
            _deviceSimulator = new DeviceSimulator(_deviceClientMock.Object, "deviceId", "", 60, 12);

            // Act and Assert
            await Assert.ThrowsExceptionAsync<Exception>(() => _deviceSimulator.SendMessageToIotHub());
        }

        [TestMethod]
        public async Task SendMessageToIotHub_DeviceClientNull_ThrowsException()
        {
            // Arrange
            _deviceSimulator = new DeviceSimulator(null, "deviceId", "tag", 60, 12);

            // Act and Assert
            await Assert.ThrowsExceptionAsync<NullReferenceException>(() => _deviceSimulator.SendMessageToIotHub());
        }
    }
}