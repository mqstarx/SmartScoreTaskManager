using CoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NetworkLib;
namespace Server
{
    class Program
    {
        static TcpModuleServer m_Tcp;
        static UsersDataBase m_UserDataBase;
        static MessageDataBase m_MessageDataBase;
        static void Main(string[] args)
        {
            m_Tcp = new TcpModuleServer();
           
           
          
            m_Tcp.Recieved += M_Tcp_Receive;
            m_Tcp.Error += M_Tcp_Error1;
            m_Tcp.StartServer(5454);


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

        private static void M_Tcp_Error1(object sender, EventArgs e)
        {
            Console.WriteLine(sender.ToString());
        }

       

        private static void M_Tcp_Error(object sender, EventArgs e)
        {
            Console.WriteLine(sender.ToString());
         //   throw new NotImplementedException();
        }

        private static void M_Tcp_Receive(object sender, SocketData m)
        {
            NetworkTransferObjects e = (NetworkTransferObjects)sender;
            if(e.ProtocolMessage == ProtocolOfExchange.CheckConnection)
            {
                NetworkTransferObjects obj = new NetworkTransferObjects();
                obj.ProtocolMessage = ProtocolOfExchange.CheckConnectionOK;
                m_Tcp.Send(m.Socket,obj);
            }
            if(e.ProtocolMessage == ProtocolOfExchange.AskUserInfoList)
            {
                NetworkTransferObjects obj = new NetworkTransferObjects();
                obj.ListUserInfo = m_UserDataBase.GetListUserInfo();
                obj.ProtocolMessage = ProtocolOfExchange.UserInfoListOk;
                m_Tcp.Send(m.Socket, obj);
            }
            if(e.ProtocolMessage == ProtocolOfExchange.AddUser)
            {
                if(m_UserDataBase.Add(e.User))
                {
                    Func.SaveConfig(m_UserDataBase, "usersdb.bin");
                    NetworkTransferObjects obj = new NetworkTransferObjects();
                        obj.ListUserInfo = m_UserDataBase.GetListUserInfo();
                    obj.ProtocolMessage = ProtocolOfExchange.AddUserOK;
                    m_Tcp.Send(m.Socket, obj);
                }
            }
            if(e.ProtocolMessage == ProtocolOfExchange.TryAuth)
            {
                AuthInfo authinfo = e.AuthInfo;
                if (m_UserDataBase.IsUserAuth(authinfo.UserId, authinfo.Password))
                {
                    NetworkTransferObjects obj = new NetworkTransferObjects();
                    obj.User = (m_UserDataBase.GetUserObject(authinfo.UserId));
                    obj.AuthInfo = authinfo;
                    obj.ProtocolMessage = ProtocolOfExchange.AuthOk;
                    m_Tcp.Send(m.Socket, obj);
                }
                else
                {
                    NetworkTransferObjects obj = new NetworkTransferObjects();
                    obj.ProtocolMessage = ProtocolOfExchange.AuthFail;
                    m_Tcp.Send(m.Socket, obj);
                }
            }
            if(e.ProtocolMessage == ProtocolOfExchange.NewMessages)
            {

                //object[] obj = (object[])e.NetDataObj
                List<CoreLib.Message> toList = e.ListMessages;
                
                foreach(CoreLib.Message to in toList)
                {
                    //UserInfo userFrom = new UserInfo(((User)e.sendInfo.InfoObject).Id, ((User)e.sendInfo.InfoObject).IdParent, ((User)e.sendInfo.InfoObject).FullName, ((User)e.sendInfo.InfoObject).PostName);
                    m_MessageDataBase.NewMessage(to);
                    Console.WriteLine("Получено сообщение от " + to.FromId + "для" + to.ToId);
                }

                //m_MessageDataBase.NewMessage(new Message())
            }
            if (e.ProtocolMessage == ProtocolOfExchange.SyncMessages)
            {
                if (m_UserDataBase.IsUserAuth(e.AuthInfo))
                {
                    NetworkTransferObjects obj = new NetworkTransferObjects();
                    List<CoreLib.Message> inbox = m_MessageDataBase.SyncMessages(e.UserInfo, e.MessageUids, false);
                   
                   
                    obj.AuthInfo = e.AuthInfo;
                    obj.ProtocolMessage = ProtocolOfExchange.SyncMessages;
                    foreach (CoreLib.Message msg in inbox)
                    {
                        obj.Message = msg;
                        m_Tcp.Send(m.Socket, obj);
                        Thread.Sleep(20);
                    }
                    List<CoreLib.Message> outbox = m_MessageDataBase.SyncMessages(e.UserInfo, e.MessageUids_1, true);
                    obj.Message = null;
                    foreach (CoreLib.Message msg in outbox)
                    {
                        obj.Message_1 = msg;
                        m_Tcp.Send(m.Socket, obj);
                        Thread.Sleep(20);
                    }

                }
            }
            if(e.ProtocolMessage == ProtocolOfExchange.MessageRead)
            {
                m_MessageDataBase.MessageReaded(e.UserInfo, e.MessageUids[0]);
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
           // Console.WriteLine("Клиент подключился: " +  ((TcpModule)sender).ToString());
        }
    }
}
