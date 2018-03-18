using CoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpModule m_Tcp = new TcpModule();
            m_Tcp.Accept += M_Tcp_Accept;
            m_Tcp.Connected += M_Tcp_Connected;
            m_Tcp.Disconnected += M_Tcp_Disconnected;
            m_Tcp.Receive += M_Tcp_Receive;
            m_Tcp.StartServer();
            Console.Read();
        }

        private static void M_Tcp_Receive(object sender, ReceiveEventArgs e)
        {


            if (e.sendInfo.message == "File")
            {
                UserFile uf = (UserFile)e.Object;
                if(System.IO.Directory.Exists(System.Windows.Forms.Application.StartupPath + @"\" + "FileDataBase"))
                uf.SaveToLocal(System.Windows.Forms.Application.StartupPath + @"\" + "FileDataBase" + @"\" + uf.FileName);
                else
                {
                    System.IO.Directory.CreateDirectory(System.Windows.Forms.Application.StartupPath + @"\" + "FileDataBase");
                    uf.SaveToLocal(System.Windows.Forms.Application.StartupPath + @"\" + "FileDataBase" + @"\" + uf.FileName);
                }
            }
        }

        private static void M_Tcp_Disconnected(object sender, string result)
        {
            
        }

        private static void M_Tcp_Connected(object sender, string result)
        {
           
        }

        private static void M_Tcp_Accept(object sender)
        {
           
        }
    }
}
