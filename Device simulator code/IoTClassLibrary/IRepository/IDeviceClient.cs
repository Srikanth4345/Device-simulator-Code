using Microsoft.Azure.Devices.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTClassLibrary.IRepository
{
    public interface IDeviceClient
    {
        Task SendEventAsync(Message message);
    }
}
