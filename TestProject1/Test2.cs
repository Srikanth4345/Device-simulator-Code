using IoTClassLibrary.IRepository;
using IOTDeviceSimulator;
using Microsoft.Azure.Devices.Client;
using Moq;
using System.Reflection;

namespace Test2
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestDeviceConstructor_ValidInput()
        {
            string deviceId = "deviceId";
            string tag = "tag";
            int dataFrequency = 10;
            int count = 5;
            Device device = new Device(deviceId, tag, dataFrequency, count);
            Assert.AreEqual(deviceId, device.DeviceId);
            Assert.AreEqual(tag, device.Tag);
            Assert.AreEqual(dataFrequency, device.DataFrequency);
            Assert.AreEqual(count, device.Count);
        }
        [TestMethod]
        public void TestDeviceConstructor_ValidInput_Moq()
        {

            string deviceId = "deviceId";
            string tag = "tag";
            double temperature = 20.0;
            double humidity = 60.0;
            int dataFrequency = 10;
            int count = 5;
            var deviceClientMock = new Mock<IDeviceClient>();
            deviceClientMock.Setup(dc => dc.SendEventAsync(It.IsAny<Message>()));
            Device device = new Device(deviceId, tag, dataFrequency, count);
            Assert.AreEqual(deviceId, device.DeviceId);
            Assert.AreEqual(tag, device.Tag);
            Assert.AreEqual(dataFrequency, device.DataFrequency);
            Assert.AreEqual(count, device.Count);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestDeviceConstructor_InvalidDeviceId()
        {

            string deviceId = null;
            string tag = "tag";
            int dataFrequency = 10;
            int count = 5;
            var deviceClientMock = new Mock<IDeviceClient>();
            Device device = new Device(deviceId, tag, dataFrequency, count);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestDeviceConstructor_InvalidTag()
        {

            string deviceId = "deviceId";
            string tag = null;
            int dataFrequency = 10;
            int count = 5;

            var deviceClientMock = new Mock<IDeviceClient>();
            Device device = new Device(deviceId, tag, dataFrequency, count);
        }
        [TestMethod]
        public async Task TestSendMessageToIotHub_ValidInput()
        {
            var deviceClientMock = new Mock<IDeviceClient>();
            deviceClientMock.Setup(dc => dc.SendEventAsync(It.IsAny<Message>()));
            string deviceId = "deviceId";
            string tag = "tag";
            int dataFrequency = 10;
            int count = 1;

            DeviceSimulator deviceSimulator = new DeviceSimulator(deviceClientMock.Object, deviceId, tag, dataFrequency, count);

            await deviceSimulator.SendMessageToIotHub();
            await Task.Delay(1); // wait for 1 millisecond to allow the loop to complete
            deviceClientMock.Verify(dc => dc.SendEventAsync(It.IsAny<Message>()), Times.Exactly(count));
        }

        [TestMethod]
        [ExpectedException(typeof(TargetInvocationException))]
        public void TestCreateDevice_NullDeviceId_Mock()
        {
            // Arrange
            string deviceId = null;
            string tag = "tag";
            int dataFrequency = 5;
            int count = 10;

            // Act
            try
            {
                var mockDevice = new Mock<Device>(deviceId, tag, dataFrequency, count);
                Device device = mockDevice.Object;
            }
            catch (TargetInvocationException ex)
            {
                Assert.IsNotNull(ex.InnerException);
                Assert.IsInstanceOfType(ex.InnerException, typeof(ArgumentNullException));
                throw;
            }
        }
        [TestMethod]
        public void TestCreateDevice_ValidDeviceId_Tag_DataFrequency_Count()
        {
            // Arrange
            string deviceId = "deviceId";
            string tag = "tag";
            int dataFrequency = 5;
            int count = 10;

            // Act
            var device = new Device(deviceId, tag, dataFrequency, count);

            // Assert
            Assert.IsNotNull(device);
            Assert.AreEqual(deviceId, device.DeviceId);
            Assert.AreEqual(tag, device.Tag);
            Assert.AreEqual(dataFrequency, device.DataFrequency);
            Assert.AreEqual(count, device.Count);
        }

    }
}