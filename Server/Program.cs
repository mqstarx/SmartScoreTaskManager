using CoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
            m_Tcp = new TcpModule(true);
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
            if(e.SendInfo.ProtocolMsg == ProtocolOfExchange.CheckConnection)
            {
                tcp.SendData(null, ProtocolOfExchange.CheckConnectionOK);
            }
            if(e.SendInfo.ProtocolMsg== ProtocolOfExchange.AskUserInfoList)
            {
                NetworkTransferObjects obj = new NetworkTransferObjects();
                obj.ListUserInfo = m_UserDataBase.GetListUserInfo();
                tcp.SendData(obj, ProtocolOfExchange.UserInfoListOk);
            }
            if(e.SendInfo.ProtocolMsg== ProtocolOfExchange.AddUser)
            {
                if(m_UserDataBase.Add(e.NetDataObj.User))
                {
                    Func.SaveConfig(m_UserDataBase, "usersdb.bin");
                    NetworkTransferObjects obj = new NetworkTransferObjects();
                        obj.ListUserInfo = m_UserDataBase.GetListUserInfo();
                    tcp.SendData(obj, ProtocolOfExchange.AddUserOK);
                }
            }
            if(e.SendInfo.ProtocolMsg == ProtocolOfExchange.TryAuth)
            {
                AuthInfo authinfo = e.NetDataObj.AuthInfo;
                if (m_UserDataBase.IsUserAuth(authinfo.UserId, authinfo.Password))
                {
                    NetworkTransferObjects obj = new NetworkTransferObjects();
                    obj.User = (m_UserDataBase.GetUserObject(authinfo.UserId));
                    obj.AuthInfo = authinfo;
                    tcp.SendData(obj, ProtocolOfExchange.AuthOk);
                }
                else
                {
                    tcp.SendData(null, ProtocolOfExchange.AuthFail);
                }
            }
            if(e.SendInfo.ProtocolMsg== ProtocolOfExchange.NewMessages)
            {

                //object[] obj = (object[])e.NetDataObj
                List<Message> toList = e.NetDataObj.ListMessages;
                
                foreach(Message to in toList)
                {
                    //UserInfo userFrom = new UserInfo(((User)e.sendInfo.InfoObject).Id, ((User)e.sendInfo.InfoObject).IdParent, ((User)e.sendInfo.InfoObject).FullName, ((User)e.sendInfo.InfoObject).PostName);
                    m_MessageDataBase.NewMessage(to);
                    Console.WriteLine("Получено сообщение от " + to.FromId + "для" + to.ToId);
                }

                //m_MessageDataBase.NewMessage(new Message())
            }
            if (e.SendInfo.ProtocolMsg == ProtocolOfExchange.SyncMessages)
            {
                if (m_UserDataBase.IsUserAuth(e.NetDataObj.AuthInfo))
                {
                    NetworkTransferObjects obj = new NetworkTransferObjects();
                    List<Message> inbox = m_MessageDataBase.SyncMessages(e.NetDataObj.UserInfo, e.NetDataObj.MessageUids, false);
                   
                   
                    obj.AuthInfo = e.NetDataObj.AuthInfo;
                    foreach (Message m in inbox)
                    {
                        obj.Message = m;
                        tcp.SendData(obj, ProtocolOfExchange.SyncMessages);
                        Thread.Sleep(20);
                    }
                    List<Message> outbox = m_MessageDataBase.SyncMessages(e.NetDataObj.UserInfo, e.NetDataObj.MessageUids_1, true);
                    obj.Message = null;
                    foreach (Message m in outbox)
                    {
                        obj.Message_1 = m;
                        tcp.SendData(obj, ProtocolOfExchange.SyncMessages);
                        Thread.Sleep(20);
                    }

                }
            }
            if(e.SendInfo.ProtocolMsg == ProtocolOfExchange.MessageRead)
            {
                m_MessageDataBase.MessageReaded(e.NetDataObj.UserInfo, e.NetDataObj.MessageUids[0]);
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
