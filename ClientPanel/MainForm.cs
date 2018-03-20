using CoreLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ClientPanel
{
    public partial class MainForm : Form
    {
        private TcpModule m_TcpClient;
        private string m_ServerIp="127.0.0.1";
        private bool m_IsConnectedToServer;
        private int m_TcpPort = 2727;
        private List<UserInfo> m_UserInfoArray;
        private AuthInfo m_CurrentAuth;
        private User m_CurrentUser;

        public MainForm()
        {
            InitializeComponent();
            m_TcpClient = new TcpModule();
            m_TcpClient.Connected += M_TcpClient_Connected;
            m_TcpClient.Disconnected += M_TcpClient_Disconnected;
            m_TcpClient.Receive += M_TcpClient_Receive;
           
        }
        #region Работа с сетью
        private void TimeOutTimer_Tick(object sender, EventArgs e)
        {
            ConnectToServer();
        }
        private void MainForm_Shown(object sender, EventArgs e)
        {
            ConnectToServer();
        }
        private void M_TcpClient_Receive(object sender, ReceiveEventArgs e)
        {
            //проверка соединения
            if (e.sendInfo.ProtocolMsg ==  ProtocolOfExchange.CheckConnectionOK)
            {
                m_IsConnectedToServer = true;
                this.Invoke((new Action(() => connectionStatusLabel.BackColor = Color.Green)));
                TimeOutTimer.Stop();
                SendReqest(ProtocolOfExchange.AskUserInfoList, null, null, "");
                
                              
            }
            //получение списка пользователей для выбора при авторизации
            if(e.sendInfo.ProtocolMsg== ProtocolOfExchange.UserInfoListOk)
            {
                m_UserInfoArray = (List<UserInfo>)e.Object;
                this.Invoke((new Action(() => UserListRecieved())));
                

            }

            if(e.sendInfo.ProtocolMsg == ProtocolOfExchange.AuthOk)
            {
                m_CurrentUser = (User)e.Object;
                m_CurrentAuth = (AuthInfo)e.sendInfo.InfoObject;
                MessageBox.Show("Вы авторизированы");
            }
            if (e.sendInfo.ProtocolMsg == ProtocolOfExchange.AuthFail)
            {
                this.Invoke((new Action(() => UserListRecieved())));
            }
            //временно
            if (e.sendInfo.ProtocolMsg== ProtocolOfExchange.AddUserOK)
                this.Invoke((new Action(() => MessageBox.Show("UserAdded"))));

        }

        private void UserListRecieved()
        {
            AuthorizationForm authForm = new AuthorizationForm();
            authForm.ListUser = m_UserInfoArray;
            if(authForm.ShowDialog()== DialogResult.OK)
            {
                SendReqest(ProtocolOfExchange.TryAuth, new AuthInfo(((UserInfo)authForm.userCmb.SelectedItem).Id, authForm.passwordTxb.Text), null, "");
            }
        }

        private void M_TcpClient_Disconnected(object sender, string result)
        {
            this.Invoke((new Action(() => connectionStatusLabel.BackColor = Color.Red)));
            m_IsConnectedToServer = false;
            ConnectToServer();
        }

        private void M_TcpClient_Connected(object sender, string result)
        {
            if (result == "OK")
            {

                SendReqest(ProtocolOfExchange.CheckConnection, null, null, "");
                TimeOutTimer.Start();

            }
            if (result == "ERR")
            {
                ConnectToServer();
            }
        }

        private void SendReqest(ProtocolOfExchange req, object p,object info,string msg)
        {
            if (m_IsConnectedToServer || req == ProtocolOfExchange.CheckConnection)
            {
                m_TcpClient.SendData(p,msg, req,info);
            }

        }

        private void ConnectToServer()
        {
            if (!m_IsConnectedToServer)
            {
                m_TcpClient = new TcpModule();
                m_TcpClient.Connected += M_TcpClient_Connected;
                m_TcpClient.Receive += M_TcpClient_Receive;
                m_TcpClient.Disconnected += M_TcpClient_Disconnected;               
                m_TcpClient.ConnectClient(m_ServerIp,m_TcpPort);
            }
        }
        #endregion
        

       

        private void button1_Click(object sender, EventArgs e)
        {
            SendReqest(ProtocolOfExchange.AddUser, new User(0, 0, Hash.EncodeString("1q2w3e"), "Старовойтов Андрей Юрьевич", "Генеральный директор"),null,"");
            Thread.Sleep(20);
            SendReqest(ProtocolOfExchange.AddUser, new User(1, 0, Hash.EncodeString("1q2w3e"), "Губарев Валентин Юрьевич", "Главный инженер"), null, "");
            Thread.Sleep(20);
            SendReqest(ProtocolOfExchange.AddUser, new User(2, 1, Hash.EncodeString("1q2w3e"), "Герасимова Елизаветта Сергеевна", "Главный конструктор"), null, "");
            Thread.Sleep(20);
        }


        #region работа с вкладкой сообщения

        private void newMessageBtn_Click(object sender, EventArgs e)
        {
            NewMessageForm newMsg = new NewMessageForm();
            newMsg.UserList = m_UserInfoArray;
            newMsg.Show(this);
        }

        #endregion
    }
}
