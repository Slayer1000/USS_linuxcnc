using System;

namespace Uss
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Uss Test!");
            {
                var ussModule = new UssModule();
                ussModule.ReceiveDataHandler += ReceiveDataFromUss;
                // ########################################################################################
                // next step Port Connection with variabel
                Console.WriteLine("USS_Connection");
                Console.WriteLine("Set your Connection Settings");
                Console.WriteLine("baud rates? (Example: 9600, 14400, 19200");
                string rate = Console.ReadLine();
                Console.WriteLine("serial port? (Example: dev/ttyS0");
                string port = Console.ReadLine();
                Console.WriteLine("VFD Adress ? (Example: 0, 1, 2, ...");
                int vfdAdress = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Your Connection Settings are Port: {0} Rate {1} VFD Adress {2}", port, rate,
                    vfdAdress);
                // ########################################################################################

                // Open Port with port and baud rates from the Class
                ussModule.PortOpen();
                string ussFunction;
                Console.WriteLine("Select your funktion");
                Console.WriteLine("1: RunMMS");
                Console.WriteLine("2: StopRunning");
                Console.WriteLine("3: ReverseRun");
                Console.WriteLine("4: RunMMSOnJOGMode");
                Console.WriteLine("5: ReverseJOG");
                Console.WriteLine("6: StopJOG");
                Console.WriteLine("7: ReqParam");
                Console.WriteLine("8: Exit");
                Console.WriteLine();
                do
                {
                    Console.WriteLine("Select the USS funktion?");
                    Console.WriteLine();
                    ussFunction = Console.ReadLine();
                    switch (ussFunction ?? "")
                    {
                        case "1":
                        {
                            Console.WriteLine("You choose RunMMS");
                            Console.WriteLine("Your Connection Settings are Port: {0} Rate {1} VFD Adress {2}", port,
                                rate, vfdAdress);
                            Console.WriteLine();
                            Console.WriteLine("enter the frequency");
                            int vfdFreq = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("VFD with start with {0} Hz", vfdFreq);
                            ussModule.RunMms(vfdFreq, vfdAdress);
                            break;
                        }

                        case "2":
                        {
                            Console.WriteLine("You choose StopRunning");
                            Console.WriteLine("Your Connection Settings are Port: {0} Rate {1} VFD Adress {2}", port,
                                rate, vfdAdress);
                            ussModule.StopRunning(vfdAdress);
                            break;
                        }

                        case "3":
                        {
                            Console.WriteLine("You choose ReverseRun");
                            Console.WriteLine("Your Connection Settings are Port: {0} Rate {1} VFD Adress {2}", port,
                                rate, vfdAdress);
                            Console.WriteLine();
                            Console.WriteLine("enter the frequency");
                            int vfdFreq = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("VFD with start reverse with {0} Hz", vfdFreq);
                            ussModule.ReverseRun(vfdFreq, vfdAdress);
                            break;
                        }

                        case "4":
                        {
                            Console.WriteLine("You choose RunMMSOnJOGMode");
                            Console.WriteLine("Your Connection Settings are Port: {0} Rate {1} VFD Adress {2}", port,
                                rate, vfdAdress);
                            ussModule.RunMMSOnJOGMode(vfdAdress);
                            break;
                        }

                        case "5":
                        {
                            Console.WriteLine("You choose ReverseJOG");
                            Console.WriteLine("Your Connection Settings are Port: {0} Rate {1} VFD Adress {2}", port,
                                rate, vfdAdress);
                            ussModule.ReverseJog(vfdAdress);
                            break;
                        }

                        case "6":
                        {
                            Console.WriteLine("You choose StopJOG");
                            Console.WriteLine("Your Connection Settings are Port: {0} Rate {1} VFD Adress {2}", port,
                                rate, vfdAdress);
                            ussModule.StopJog(vfdAdress);
                            break;
                        }

                        case "7":
                        {
                            Console.WriteLine("You choose ReqParam");
                            Console.WriteLine("Your Connection Settings are Port: {0} Rate {1} VFD Adress {2}", port,
                                rate, vfdAdress);
                            ussModule.ReqParam(vfdAdress);
                            break;
                        }

                        default:
                        {
                            Console.WriteLine("Exit");
                            break;
                        }
                    }
                } while (true);
            }
        }

        private static void ReceiveDataFromUss(object sender, string e)
        {
            Console.WriteLine($"I received new Message: {e}");
        }
    }
}
