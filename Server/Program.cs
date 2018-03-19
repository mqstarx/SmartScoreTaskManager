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
        static TcpModule m_Tcp;
        static UsersDataBase m_UserDataBase;
        static void Main(string[] args)
        {
            m_Tcp = new TcpModule();
            m_Tcp.Accept += M_Tcp_Accept;
            m_Tcp.Connected += M_Tcp_Connected;
            m_Tcp.Disconnected += M_Tcp_Disconnected;
            m_Tcp.Receive += M_Tcp_Receive;
            m_Tcp.StartServer(2727);


            m_UserDataBase = (UsersDataBase)Func.LoadConfig("usersdb.bin");
            if (m_UserDataBase == null)
                m_UserDataBase = new UsersDataBase();

            Console.Read();
        }

        private static void M_Tcp_Receive(object sender, ReceiveEventArgs e)
        {
            
            if(e.sendInfo.ProtocolMsg == ProtocolOfExchange.CheckConnection)
            {
                m_Tcp.SendData(null, "", ProtocolOfExchange.CheckConnectionOK, null);
            }
            if(e.sendInfo.ProtocolMsg== ProtocolOfExchange.AskUserInfoList)
            {
                m_Tcp.SendData(m_UserDataBase.GetListUserInfo(), "", ProtocolOfExchange.UserInfoListOk, null);
            }
            if(e.sendInfo.ProtocolMsg== ProtocolOfExchange.AddUser)
            {
                if(m_UserDataBase.Add((User)e.Object))
                {
                    Func.SaveConfig(m_UserDataBase, "usersdb.bin");
                    m_Tcp.SendData(m_UserDataBase.GetListUserInfo(), "", ProtocolOfExchange.AddUserOK, null);
                }
            }
            if(e.sendInfo.ProtocolMsg == ProtocolOfExchange.TryAuth)
            {
                AuthInfo authinfo = (AuthInfo)e.Object;
                if (m_UserDataBase.IsUserAuth(authinfo.UserId, authinfo.Password))
                {
                    m_Tcp.SendData(m_UserDataBase.GetUserObject(authinfo.UserId), "", ProtocolOfExchange.AuthOk, authinfo);
                }
                else
                {
                    m_Tcp.SendData(null, "", ProtocolOfExchange.AuthFail, null);
                }
            }

            /*if (e.sendInfo.message == "File")
            {
                UserFile uf = (UserFile)e.Object;
                if(System.IO.Directory.Exists(System.Windows.Forms.Application.StartupPath + @"\" + "FileDataBase"))
                uf.SaveToLocal(System.Windows.Forms.Application.StartupPath + @"\" + "FileDataBase" + @"\" + uf.FileName);
                else
                {
                    System.IO.Directory.CreateDirectory(System.Windows.Forms.Application.StartupPath + @"\" + "FileDataBase");
                    uf.SaveToLocal(System.Windows.Forms.Application.StartupPath + @"\" + "FileDataBase" + @"\" + uf.FileName);
                }
            }*/
        }

        private static void M_Tcp_Disconnected(object sender, string result)
        {
            
        }

        private static void M_Tcp_Connected(object sender, string result)
        {
           
        }

        private static void M_Tcp_Accept(object sender)
        {
            // TcpModule tcp = (TcpModule)sender;
            Console.WriteLine("Клиент подключился: " + sender.ToString());
        }
    }
}
