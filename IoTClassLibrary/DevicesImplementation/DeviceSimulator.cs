using IoTClassLibrary.IRepository;
using Microsoft.Azure.Devices.Client;
using System.Text;
using System.Text.Json;

public class DeviceSimulator : IDeviceSimulator
{
    private readonly IDeviceClient _deviceClient;
    private readonly string _deviceId;
    private readonly string _tag;
    private readonly int _dataFrequency;
    private readonly int _count;

    public DeviceSimulator(IDeviceClient deviceClient, string deviceId, string tag, int dataFrequency, int count)
    {
        _deviceClient = deviceClient;
        _deviceId = deviceId;
        _tag = tag;
        _dataFrequency = dataFrequency;
        _count = count;
    }

    public string GenerateJsonOutput()
    {
        if (string.IsNullOrEmpty(_tag))
        {
            throw new Exception("Tag cannot be empty");
        }

        var jsonData = new
        {
            d = _deviceId,
            t = _tag,
            trend = new[]
            {
            new TrendData(" dummy", 0, DateTime.Now)
        }
        };

        return JsonSerializer.Serialize(jsonData);
    }

    public async Task SendMessageToIotHub()
    {
        var messageString = GenerateJsonOutput();
        if (string.IsNullOrEmpty(messageString))
        {
            throw new Exception("Invalid message");
        }
        var messageBytes = Encoding.UTF8.GetBytes(messageString);
        var message = new Message(messageBytes);

        await _deviceClient.SendEventAsync(message);
    }
}

public class TrendData
{
    public string Tag { get; set; }
    public double V { get; set; }
    public long T { get; set; }

    public TrendData(string tag, double value, DateTime timestamp)
    {
        Tag = tag;
        V = value;
        T = (long)timestamp.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
    }
}