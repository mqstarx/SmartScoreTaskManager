using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetworkLib;
using CoreLib;
namespace Updater
{
    class Program
    {
        private static TcpModuleClient _client;
        static bool exit = false;
        static void Main(string[] args)
        {

            _client = new TcpModuleClient();
            _client.Connect(args[0], int.Parse(args[1]));
            _client.Connected += _client_Connected;
            _client.Recieved += _client_Recieved;
            Console.WriteLine(args[0] + " " + args[1]);
            while(!exit)
            {

            }
        }

        private static void _client_Recieved(object obj, SocketData data)
        {
            Func.SaveFile("ClientPanel.exe", ((NetworkTransferObjects)obj).Data);
            System.Diagnostics.Process.Start("ClientPanel.exe");
            exit = true;
        }

        private static void _client_Connected(object sender, EventArgs e)
        {
            NetworkTransferObjects obj = new NetworkTransferObjects();
            obj.ProtocolMessage = ProtocolOfExchange.GetNewVersionClient;
            _client.Send(obj);
        }
    }
}
