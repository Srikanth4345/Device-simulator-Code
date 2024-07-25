using IoTClassLibrary.IRepository;
using Microsoft.Azure.Devices.Client;

namespace IOTDeviceSimulator
{
    public class DeviceClientImpl : IDeviceClient
    {
        private readonly DeviceClient _deviceClient;
        public DeviceClientImpl(string connectionString)
        {
            _deviceClient = DeviceClient.CreateFromConnectionString(connectionString);
        }

        public async Task SendEventAsync(Message message)
        {
            await _deviceClient.SendEventAsync(message);
        }

    }
}