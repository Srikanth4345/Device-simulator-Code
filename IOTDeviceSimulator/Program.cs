using Microsoft.Azure.Devices.Client;
using System;
using System.Threading.Tasks;

namespace IOTDeviceSimulator
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string connectionString = "HostName=ih-iotproject.azure-devices.net;DeviceId=50f7481b-2a67-4f32-ac65-78e261326031;SharedAccessKey=XAex3q+Pih3ySRTXEDZKjoPpEAYWHDdZQAIoTE/TzMc=";
            var deviceClient = DeviceClient.CreateFromConnectionString(connectionString);

            bool exitProgram = false;

            while (!exitProgram)
            {
                Console.WriteLine("1 - Send the trend data");
                Console.WriteLine("2 - Exit");
                Console.Write("Enter your choice: ");
                string choiceStr = Console.ReadLine();

                if (int.TryParse(choiceStr, out int choice))
                {
                    if (choice == 2)
                    {
                        exitProgram = true; // exit the program
                    }
                    else if (choice == 1)
                    {
                        int deviceIdChoice = 0;
                        do
                        {
                            Console.Write("Enter the Device Id: ");
                            string deviceId = Console.ReadLine();

                            Console.Write("Enter the Tag: ");
                            string tag = Console.ReadLine();

                            double temperature;
                            do
                            {
                                Console.Write("Enter the Temperature: ");
                                string tempStr = Console.ReadLine();

                                if (double.TryParse(tempStr, out temperature))
                                {
                                    if (temperature < -50 || temperature > 50)
                                    {
                                        Console.WriteLine("Temperature should be between -50 and 50. Please enter a valid temperature.");
                                        temperature = 0;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Invalid temperature. Please enter a valid number.");
                                }
                            } while (temperature == 0);

                            double humidity;
                            do
                            {
                                Console.Write("Enter the Humidity: ");
                                string humidityStr = Console.ReadLine();

                                if (double.TryParse(humidityStr, out humidity))
                                {
                                    if (humidity < 0 || humidity > 100)
                                    {
                                        Console.WriteLine("Humidity should be between 0 and 100. Please enter a valid humidity.");
                                        humidity = 0;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Invalid humidity. Please enter a valid number.");
                                }
                            } while (humidity == 0);

                            Console.Write("Enter the Frequency (in seconds): ");
                            string freqStr = Console.ReadLine();

                            if (int.TryParse(freqStr, out int freq))
                            {
                                Console.Write("Enter the Count: ");
                                string countStr = Console.ReadLine();

                                if (int.TryParse(countStr, out int count))
                                {
                                    var deviceSimulator = new DeviceSimulator(deviceId, tag, temperature, humidity, freq, count);

                                    for (int i = 0; i < count; i++)
                                    {
                                        await deviceSimulator.SendMessageToIotHub(deviceClient);
                                        Console.WriteLine($"Message {i + 1} sent to IoT Hub!");
                                        await Task.Delay(freq * 1000); // wait for freq seconds
                                    }

                                    Console.WriteLine("0. Continue");
                                    Console.WriteLine("1. Exit");
                                    Console.Write("Enter your choice: ");
                                    string continueChoiceStr = Console.ReadLine();

                                    if (int.TryParse(continueChoiceStr, out int continueChoice))
                                    {
                                        if (continueChoice == 0)
                                        {
                                            deviceIdChoice = 0;
                                        }
                                        else if (continueChoice == 1)
                                        {
                                            deviceIdChoice = 1;
                                            exitProgram = true;
                                        }
                                        else
                                        {
                                            Console.WriteLine("Invalid choice. Please choose a valid option.");
                                            deviceIdChoice = 0;
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid choice. Please enter a valid number.");
                                        deviceIdChoice = 0;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Invalid count. Please enter a valid number.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid frequency. Please enter a valid number.");
                            }
                        } while (deviceIdChoice == 0 && !exitProgram);
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
