using System;
using System.Threading.Tasks;

namespace IOTDeviceSimulator
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string connectionString = "HostName=ih-iotproject.azure-devices.net;DeviceId=50f7481b-2a67-4f32-ac65-78e261326031;SharedAccessKey=XAex3q+Pih3ySRTXEDZKjoPpEAYWHDdZQAIoTE/TzMc=";

            // Create an instance of DeviceClientImpl (which implements IDeviceClient)
            var deviceClientImpl = new DeviceClientImpl(connectionString);

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
                        Console.WriteLine("Select a device ID:");
                        Console.WriteLine("1. 63d0d1be-3df7-45f4-9b1a-73126e960190");
                        Console.WriteLine("2. 5865aab4-3a1f-4ee6-85d3-ac26da75b742");
                        Console.WriteLine("3. 50f7481b-2a67-4f32-ac65-78e261326031");
                        Console.Write("Enter your choice: ");
                        string deviceIdChoiceStr = Console.ReadLine();

                        if (int.TryParse(deviceIdChoiceStr, out int deviceIdChoice))
                        {
                            string deviceId;
                            if (deviceIdChoice == 1)
                            {
                                deviceId = "63d0d1be-3df7-45f4-9b1a-73126e960190";
                            }
                            else if (deviceIdChoice == 2)
                            {
                                deviceId = "5865aab4-3a1f-4ee6-85d3-ac26da75b742";
                            }
                            else if (deviceIdChoice == 3)
                            {
                                deviceId = "50f7481b-2a67-4f32-ac65-78e261326031";
                            }
                            else
                            {
                                Console.WriteLine("Invalid device ID choice. Please enter a valid number.");
                                continue;
                            }

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
                                    // Create an instance of DeviceSimulator with the DeviceClientImpl instance
                                    var deviceSimulator = new DeviceSimulator(deviceClientImpl, deviceId, tag, temperature, humidity, freq, count);

                                    for (int i = 0; i < count; i++)
                                    {
                                        await deviceSimulator.SendMessageToIotHub();
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
                                            // continue
                                        }
                                        else if (continueChoice == 1)
                                        {
                                            exitProgram = true; // exit the program
                                        }
                                        else
                                        {
                                            Console.WriteLine("Invalid choice. Please choose a valid option.");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid choice. Please enter a valid number.");
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
                        }
                        else
                        {
                            Console.WriteLine("Invalid device ID choice. Please enter a valid number.");
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