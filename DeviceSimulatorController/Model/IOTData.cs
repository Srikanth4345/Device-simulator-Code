using Microsoft.Azure.Cosmos.Table;
using System;
namespace DeviceSimulatorController.Model
{
    public class IoTData : TableEntity
    {
        public string DeviceId { get; set; }
        public double Temperature { get; set; }
        public double Humidity { get; set; }
        public long Timestamp { get; set; } // Epoch time
    }
}

