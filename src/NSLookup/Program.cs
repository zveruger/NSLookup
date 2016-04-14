using System;
using System.Net;
using System.Net.Sockets;
using NSLookup.Properties;

namespace NSLookup
{
    class Program
    {
        #region private
        private static void _dnsLookup(string hostNameOrAddress)
        {
            try
            {
                var hostEntry = Dns.GetHostEntry(hostNameOrAddress);
                _showMessage($"{Resources.MessageHostName}: {hostEntry.HostName}");

                foreach (var ipAddress in hostEntry.AddressList)
                {
                    switch (ipAddress.AddressFamily)
                    {
                        case AddressFamily.InterNetwork:
                            { _showMessage($"{Resources.MessageHostIPv4}: {ipAddress}"); }
                            break;
                        case AddressFamily.InterNetworkV6:
                            { _showMessage($"{Resources.MessageHostIPv6}: {ipAddress}"); }
                            break;
                    }
                }

            }
            catch (SocketException)
            {
                _showMessage(string.Format(Resources.MessageErrorNotDNSInformation, hostNameOrAddress));
            }
        }
        //---------------------------------------------------------------------
        private static void _clearMessages()
        {
            Console.Clear();
        }
        private static void _showMessage(string message, bool newLine = true)
        {
            if (newLine)
            { Console.WriteLine(message); }
            else
            { Console.Write(message); }
        }
        #endregion

        [STAThread]
        static void Main()
        {
            do
            {
                _clearMessages();
                _showMessage(Resources.MessageEnterIPAddress, false);

                var hostNameOrAddress = Console.ReadLine();

                _showMessage(string.Empty);
                _dnsLookup(hostNameOrAddress);

                _showMessage(string.Empty);
                _showMessage(Resources.MessageExit);
            }
            while (Console.ReadKey().Key != ConsoleKey.Escape);
        }
    }
}
