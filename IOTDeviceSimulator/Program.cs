using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure;
using IoTClassLibrary.IRepository;
using Microsoft.Azure.Devices.Client;

namespace IOTDeviceSimulator
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string connectionString = "HostName=ih-iothub.azure-devices.net;DeviceId=5865aab4-3a1f-4ee6-85d3-ac26da75b742;SharedAccessKey=nLFJGtq+WiUNXTmwLyVKtC6kXdVozgneuAIoTJvRIQ0=";

            // Create an instance of DeviceClientImpl (which implements IDeviceClient)
            var deviceClientImpl = new DeviceClientImpl(connectionString);

            bool exitProgram = false;

            while (!exitProgram)
            {
                Console.WriteLine("1 - Send data from Device 1");
                Console.WriteLine("2 - Send data from Device 2");
                Console.WriteLine("3 - Exit");
                Console.Write("Enter your choice: ");
                string choiceStr = Console.ReadLine();

                if (int.TryParse(choiceStr, out int choice))
                {
                    if (choice == 3)
                    {
                        exitProgram = true;
                    }
                    else if (choice == 1)
                    {
                        // Device 1 sends 12 records every 5 min
                        string deviceId = "63d0d1be-3df7-45f4-9b1a-73126e960190";
                        Console.Write("Enter the Tag: ");
                        string tag = Console.ReadLine();
                        int dataFrequency = 300; // 5 min
                        int count = 12;

                        var deviceSimulator = new DeviceSimulator(deviceClientImpl, deviceId, tag, dataFrequency, count);

                        for (int i = 0; i < count; i++)
                        {
                            await deviceSimulator.SendMessageToIotHub();
                            Console.WriteLine($"Message {i + 1} sent to IoT Hub!");
                            await Task.Delay(dataFrequency * 1000); // wait for 1 hour
                        }
                    }
                    else if (choice == 2)
                    {
                        // Device 2 sends 1 record every 1 hour
                        string deviceId = "5865aab4-3a1f-4ee6-85d3-ac26da75b742";
                        Console.Write("Enter the Tag: ");
                        string tag = Console.ReadLine();
                        int dataFrequency = 60; // 1 hour
                        int count = 1;

                        var deviceSimulator = new DeviceSimulator(deviceClientImpl, deviceId, tag, dataFrequency, count);

                        for (int i = 0; i < count; i++)
                        {
                            await deviceSimulator.SendMessageToIotHub();
                            Console.WriteLine($"Message {i + 1} sent to IoT Hub!");
                            await Task.Delay(dataFrequency * 1000); // wait for 1 hour
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid choice. Please enter a valid number.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please enter a valid number.");
                }
            }
        }
    }
}