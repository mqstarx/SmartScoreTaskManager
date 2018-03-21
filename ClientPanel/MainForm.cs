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
        private string m_ServerIp="192.168.100.13";
        private bool m_IsConnectedToServer;
        private int m_TcpPort = 2727;
        private List<UserInfo> m_UserInfoArray;
        private AuthInfo m_CurrentAuth;
        private User m_CurrentUser;

        private List<CoreLib.Message> m_inbox;
        private List<CoreLib.Message> m_outbox;

        public MainForm()
        {
            InitializeComponent();
            m_TcpClient = new TcpModule();
            m_TcpClient.Connected += M_TcpClient_Connected;
            m_TcpClient.Disconnected += M_TcpClient_Disconnected;
            m_TcpClient.Receive += M_TcpClient_Receive;
            m_inbox = new List<CoreLib.Message>();
           
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
            TcpModule tcp = (TcpModule)sender;
            //проверка соединения
            if (e.sendInfo.ProtocolMsg ==  ProtocolOfExchange.CheckConnectionOK)
            {
                m_IsConnectedToServer = true;
                this.Invoke((new Action(() => connectionStatusLabel.BackColor = Color.Green)));
                TimeOutTimer.Stop();
                SendReqest(ProtocolOfExchange.AskUserInfoList, null, null, "",tcp);
                
                              
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
                this.Invoke((new Action(() => MessageBox.Show("Вы авторизированы"))));
                
            }
            if (e.sendInfo.ProtocolMsg == ProtocolOfExchange.AuthFail)
            {
                this.Invoke((new Action(() => UserListRecieved())));
            }
            //временно
           /* if (e.sendInfo.ProtocolMsg== ProtocolOfExchange.AddUserOK)
                this.Invoke((new Action(() => MessageBox.Show("UserAdded"))));*/

            if(e.sendInfo.ProtocolMsg== ProtocolOfExchange.SyncMessages)
            {
                m_inbox = (List<CoreLib.Message>)e.Object;
                this.Invoke((new Action(() =>SyncMessageInbox())));
            }


        }

        private void SyncMessageInbox()
        {
            foreach(CoreLib.Message msg in m_inbox)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = msg.FromId.FullName;
                if (!msg.IsReaded)
                    lvi.BackColor = Color.Green;
                ListViewItem.ListViewSubItem lvi1 = new ListViewItem.ListViewSubItem();
                lvi1.Text = msg.ToId.FullName;
                lvi.SubItems.Add(lvi1);

                lvi1 = new ListViewItem.ListViewSubItem();
                lvi1.Text = msg.Msg;

                lvi.SubItems.Add(lvi1);

                lvi1 = new ListViewItem.ListViewSubItem();
                lvi1.Text = msg.DateTimeOfMessage.ToString("dd.MM.yy:HH:mm");

                lvi.SubItems.Add(lvi1);
                lvi1 = new ListViewItem.ListViewSubItem();
                if (msg.FileAttachments!=null)
                {
                    lvi1.Text = "+";
                }

                lvi.SubItems.Add(lvi1);
                messagesListView.Items.Add(lvi);
            }
        }
        private void UserListRecieved()
        {
            AuthorizationForm authForm = new AuthorizationForm();
            authForm.ListUser = m_UserInfoArray;
            DialogResult dr = authForm.ShowDialog();
            if (dr == DialogResult.OK)
            {
                SendReqest(ProtocolOfExchange.TryAuth, new AuthInfo(((UserInfo)authForm.userCmb.SelectedItem).Id, authForm.passwordTxb.Text), null, "", m_TcpClient);
            }
            else if (dr == DialogResult.No)
                this.Close();
            else
            {
                UserListRecieved();
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

                SendReqest(ProtocolOfExchange.CheckConnection, null, null, "", (TcpModule)sender);
                TimeOutTimer.Start();

            }
            if (result == "ERR")
            {
                ConnectToServer();
            }
        }

        private void SendReqest(ProtocolOfExchange req, object p,object info,string msg,TcpModule tcp)
        {
            if (m_IsConnectedToServer || req == ProtocolOfExchange.CheckConnection)
            {
                tcp.SendData(p,msg, req,info);
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

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            m_TcpClient.CloseSocket();
        }
        #endregion




        private void button1_Click(object sender, EventArgs e)
        {
           /* SendReqest(ProtocolOfExchange.AddUser, new User(0, 0, Hash.EncodeString("1q2w3e"), "Старовойтов Андрей Юрьевич", "Генеральный директор"),null,"");
            Thread.Sleep(20);
            SendReqest(ProtocolOfExchange.AddUser, new User(1, 0, Hash.EncodeString("1q2w3e"), "Губарев Валентин Юрьевич", "Главный инженер"), null, "");
            Thread.Sleep(20);
            SendReqest(ProtocolOfExchange.AddUser, new User(2, 1, Hash.EncodeString("1q2w3e"), "Герасимова Елизаветта Сергеевна", "Главный конструктор"), null, "");
            Thread.Sleep(20);*/
        }


        #region работа с вкладкой сообщения

        private void newMessageBtn_Click(object sender, EventArgs e)
        {
            NewMessageForm newMsg = new NewMessageForm();
            newMsg.UserList = m_UserInfoArray;
            newMsg.NeedToSendMessage += NewMsg_NeedToSendMessage;
            newMsg.Show(this);
        }

        private void NewMsg_NeedToSendMessage(object sender, EventArgs e)
        {
            object[] obj = (object[])sender;
            SendReqest(ProtocolOfExchange.NewMessages, obj, m_CurrentUser, "", m_TcpClient);

        }

        private void SyncMessageBtn_Click(object sender, EventArgs e)
        {
            SendReqest(ProtocolOfExchange.NewMessages, m_inbox, m_CurrentUser, "", m_TcpClient);
        }
        #endregion


    }
}
