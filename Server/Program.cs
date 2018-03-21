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
        static MessageDataBase m_MessageDataBase;
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

            //temp
            //m_UserDataBase.Add(new User(3, 0, Hash.EncodeString("1q2w3e"), "Филимонов Анатолий Иванович", "Заместитель директора по качеству"));
            //m_UserDataBase.Add(new User(4, 2, Hash.EncodeString("1q2w3e"), "Петренко Андрей Александрович", "Заместитель главного конструктора"));
            //Func.SaveConfig(m_UserDataBase, "usersdb.bin");
            //
            m_MessageDataBase = new MessageDataBase();

            Console.Read();
        }

        private static void M_Tcp_Receive(object sender, ReceiveEventArgs e)
        {
            TcpModule tcp = (TcpModule)sender;
            if(e.sendInfo.ProtocolMsg == ProtocolOfExchange.CheckConnection)
            {
                tcp.SendData(null, "", ProtocolOfExchange.CheckConnectionOK, null);
            }
            if(e.sendInfo.ProtocolMsg== ProtocolOfExchange.AskUserInfoList)
            {
                tcp.SendData(m_UserDataBase.GetListUserInfo(), "", ProtocolOfExchange.UserInfoListOk, null);
            }
            if(e.sendInfo.ProtocolMsg== ProtocolOfExchange.AddUser)
            {
                if(m_UserDataBase.Add((User)e.Object))
                {
                    Func.SaveConfig(m_UserDataBase, "usersdb.bin");
                    tcp.SendData(m_UserDataBase.GetListUserInfo(), "", ProtocolOfExchange.AddUserOK, null);
                }
            }
            if(e.sendInfo.ProtocolMsg == ProtocolOfExchange.TryAuth)
            {
                AuthInfo authinfo = (AuthInfo)e.Object;
                if (m_UserDataBase.IsUserAuth(authinfo.UserId, authinfo.Password))
                {
                    tcp.SendData(m_UserDataBase.GetUserObject(authinfo.UserId), "", ProtocolOfExchange.AuthOk, authinfo);
                }
                else
                {
                    tcp.SendData(null, "", ProtocolOfExchange.AuthFail, null);
                }
            }
            if(e.sendInfo.ProtocolMsg== ProtocolOfExchange.NewMessages)
            {

                object[] obj = (object[])e.Object;
                List<UserInfo> toList = (List<UserInfo>)obj[0];

                foreach(UserInfo to in toList)
                {
                    m_MessageDataBase.NewMessage(new Message(obj[1].ToString(), new UserInfo((User)e.sendInfo.InfoObject), to));
                }

                //m_MessageDataBase.NewMessage(new Message())
            }
            if (e.sendInfo.ProtocolMsg == ProtocolOfExchange.SyncMessages)
            {
                List<Message> result = null;

                if (e.sendInfo.message == "from")
                    m_MessageDataBase.SyncMessages( new UserInfo((User)e.sendInfo.InfoObject), (string[])e.Object, true);
                else
                    m_MessageDataBase.SyncMessages(new UserInfo((User)e.sendInfo.InfoObject), (string[])e.Object, false);

                tcp.SendData(result, "", ProtocolOfExchange.SyncMessages, null);

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
            Console.WriteLine("Клиент подключился: " +  ((TcpModule)sender).ToString());
        }
    }
}
