using System;
using System.Net.Sockets;
using System.Threading;

namespace PortScanning
{
    class Program
    {
        // This is on  feature branch and Hello
        static void Main(string[] args)
        {
	    //Nmap port scan IP Address
            ScanPorts("45.33.32.156", 1, 100);
            //ScanPorts("127.0.0.1", 1, 100);
        }

        private static void ScanPorts(string ipAddress, int startPort, int endPort)
        {
            startPort = Math.Min(startPort, endPort);
            int currentPort = startPort;
            while(currentPort <= endPort)
            {
                int curPort = currentPort;
                Thread scanThread = new Thread(() =>
                {
                    CreateClientAndConnect(ipAddress, curPort);
                });
                scanThread.Start();
                currentPort++;
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

        private static void CreateClientAndConnect(string ipAddress, int currentPort)
        {
            using (TcpClient scanClient = new TcpClient())
            {
                try
                {
                    scanClient.Connect(ipAddress, currentPort);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Scan Succeeded, Port {currentPort} is open for {ipAddress}");
                }
                catch(Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Scan Succeeded, Port {currentPort} is NOT open for {ipAddress} and \n Exceptions {ex.Message}");
                }
            }
        }
    }
}
