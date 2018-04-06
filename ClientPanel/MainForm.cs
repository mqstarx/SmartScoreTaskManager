using CoreLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using NetworkLib;
using System.Net;

namespace ClientPanel
{
    public partial class MainForm : Form
    {
        private TcpModuleClient m_TcpClient;
        private string m_ServerIp="192.168.100.13";
        private bool m_IsConnectedToServer;
        private int m_TcpPort = 5454;
        private List<UserInfo> m_UserInfoArray;
        private AuthInfo m_CurrentAuth;
        private User m_CurrentUser;

        private List<CoreLib.Message> m_inbox;
        private List<CoreLib.Message> m_outbox;

       

        public MainForm()
        {
            InitializeComponent();
            m_TcpClient = new TcpModuleClient();
            m_TcpClient.Connected += M_TcpClient_Connected;
            m_TcpClient.Recieved += M_TcpClient_Receive;
           
            m_inbox = new List<CoreLib.Message>();
            m_outbox = new List<CoreLib.Message>();

          
            ///

        }






        #region Работа с сетью


        private void M_TcpClient_Connected(object sender, EventArgs e)
        {
            SendReqest(ProtocolOfExchange.CheckConnection, new NetworkTransferObjects());
            TimeOutTimer.Start();
        }
        private void TimeOutTimer_Tick(object sender, EventArgs e)
        {
          //  ConnectToServer();
        }
        private void MainForm_Shown(object sender, EventArgs e)
        {
            ConnectToServer();
           
        }
        //оброботка входящих данных из сети
        private void M_TcpClient_Receive(object sender, SocketData s)
        {
            // TcpModule tcp = (TcpModule)sender;
            NetworkTransferObjects obj = (NetworkTransferObjects)sender;
            //проверка соединения
            if (obj.ProtocolMessage == ProtocolOfExchange.CheckConnectionOK)
            {
                m_IsConnectedToServer = true;
                this.Invoke((new Action(() => connectionStatusLabel.BackColor = Color.Green)));
                TimeOutTimer.Stop();
                SendReqest(ProtocolOfExchange.AskUserInfoList, new NetworkTransferObjects());


            }
            //получение списка пользователей для выбора при авторизации
            if (obj.ProtocolMessage == ProtocolOfExchange.UserInfoListOk)
            {
                m_UserInfoArray = (List<UserInfo>)obj.ListUserInfo;
                this.Invoke((new Action(() => UserListRecieved())));


            }

            if (obj.ProtocolMessage == ProtocolOfExchange.AuthOk)
            {
                m_CurrentUser = obj.User;
                m_CurrentAuth = obj.AuthInfo;
                this.Invoke((new Action(() => MessageBox.Show("Вы авторизированы"))));
                this.Invoke((new Action(() => this.Text = m_CurrentUser.FullName)));
                this.Invoke((new Action(() => SyncTimer_Tick(null, null))));
                this.Invoke((new Action(() => SyncTimer.Start())));
            }
            if (obj.ProtocolMessage == ProtocolOfExchange.AuthFail)
            {
                this.Invoke((new Action(() => UserListRecieved())));
            }
            //временно
            /* if (e.sendInfo.ProtocolMsg== ProtocolOfExchange.AddUserOK)
                 this.Invoke((new Action(() => MessageBox.Show("UserAdded"))));*/

            if (obj.ProtocolMessage == ProtocolOfExchange.SyncMessages)
            {
                if (obj.AuthInfo.UserId == m_CurrentUser.Id)
                {
                    if (obj.Message != null)
                        m_inbox.Add(obj.Message);
                    if (obj.Message_1 != null)
                        m_outbox.Add(obj.Message_1);
                    this.Invoke((new Action(() => SyncMessageInbox())));
                }
            }
            if (obj.ProtocolMessage == ProtocolOfExchange.NewVersionClientNone)
            {
                this.Invoke((new Action(() => MessageBox.Show("Нет новых обновлений"))));

            }
            if (obj.ProtocolMessage == ProtocolOfExchange.NewVersionClientOk)
            {
                this.Invoke((new Action(() => System.Diagnostics.Process.Start("Updater.exe", m_ServerIp + " 5454"))));
                this.Invoke((new Action(() => this.Close())));
               
               

            }
        }

        //заполняет лист в соответствии с параметрами
        private void SyncMessageInbox()
        {
            messagesListView.Items.Clear();
            List<CoreLib.Message> syncList = new List<CoreLib.Message>();
            if (inboxRadioBtn.Checked)
                syncList = m_inbox;
            else
                syncList = m_outbox;
            

            
                if (syncList != null)
                {
                    foreach (CoreLib.Message msg in syncList)
                    {
                   

                    string[] strItems = new string[4];
                    if (inboxRadioBtn.Checked)
                    {
                        strItems[0] = msg.FromId.FullName;
                        from_To.Text = "От:";
                    }
                    else
                    {
                        strItems[0] = msg.ToId.FullName;
                        from_To.Text = "Кому:";
                    }
                   

                    strItems[1] = msg.Msg;

                    strItems[2] = msg.DateTimeOfMessage.ToString("dd.MM.yy:HH:mm");
                   
                   

                    if (msg.FileAttachmentsInfo != null)
                    {
                        strItems[3] = "+";
                    }
                    ListViewItem lvi;
                    if (!msg.IsReaded)
                        lvi = new ListViewItem(strItems, -1, Color.Black, Color.LightGreen, null);
                    else
                        lvi = new ListViewItem(strItems, -1, Color.Black, Color.Empty, null);

                    lvi.Tag = msg;
                    lvi.ToolTipText = msg.Msg;
                    

                    messagesListView.Items.Add (lvi);

                    messagesListView.Sorting = SortOrder.Descending;
                    messagesListView.ListViewItemSorter = new ListViewDateComparer();
                    //messagesListView.Sorting = SortOrder.Descending;
                   // messagesListView.Sort();
                    ///



                }
                    
                }
            
           
        }

        //вызывает ворму авторизации пользователя, после получения списка всех пользователей
        private void UserListRecieved()
        {
            AuthorizationForm authForm = new AuthorizationForm();
            authForm.ListUser = m_UserInfoArray;
            DialogResult dr = authForm.ShowDialog();
            if (dr == DialogResult.OK)
            {
                NetworkTransferObjects obj = new NetworkTransferObjects();
                obj.AuthInfo = new AuthInfo(((UserInfo)authForm.userCmb.SelectedItem).Id, authForm.passwordTxb.Text);
                SendReqest(ProtocolOfExchange.TryAuth,obj);
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

       

        private void SendReqest(ProtocolOfExchange req, NetworkTransferObjects obj )
        {
            if (m_IsConnectedToServer || req == ProtocolOfExchange.CheckConnection)
            {
                obj.ProtocolMessage = req;
                m_TcpClient.Send(obj);
            }

        }

        private void ConnectToServer()
        {
            if (!m_IsConnectedToServer )
            {
                m_TcpClient.Connect(m_ServerIp, m_TcpPort);
                
            }
           
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
           
            
        }
        #endregion




       


        #region работа с вкладкой сообщения

        private void newMessageBtn_Click(object sender, EventArgs e)
        {
            NewMessageForm newMsg = new NewMessageForm();
            newMsg.UserList = m_UserInfoArray;
            newMsg.CurentUser = m_CurrentUser;
            newMsg.Text = "Сообщение от: " + m_CurrentUser.FullName;
            newMsg.NeedToSendMessage += NewMsg_NeedToSendMessage;
            newMsg.Show(this);
        }

        //событие отправки сообщения на сервер
        private void NewMsg_NeedToSendMessage(object sender, EventArgs e)
        {
            NetworkTransferObjects obj = new NetworkTransferObjects();
            obj.ListMessages = (List<CoreLib.Message>)sender;
            SendReqest(ProtocolOfExchange.NewMessages, obj);

        }

        //синхронизация сообщений с сервером
        private void SyncMessageBtn_Click(object sender, EventArgs e)
        {
            if (m_inbox == null)
                m_inbox = new List<CoreLib.Message>();
            if (m_outbox == null)
                m_outbox = new List<CoreLib.Message>();

            NetworkTransferObjects obj = new NetworkTransferObjects();
            List<string> uids = new List<string>();
            List<string> uids_1 = new List<string>();
            foreach (CoreLib.Message to in m_inbox)
            {
                uids.Add(to.UidMsg);
            }
            foreach (CoreLib.Message to in m_outbox)
            {
                uids_1.Add(to.UidMsg);
            }
            obj.MessageUids = uids.ToArray();
            obj.MessageUids_1 = uids_1.ToArray();
            obj.UserInfo = m_CurrentUser.GetInfo();
            obj.AuthInfo = m_CurrentAuth;
            SendReqest(ProtocolOfExchange.SyncMessages, obj);
        }
        private void inboxRadioBtn_CheckedChanged(object sender, EventArgs e)
        {
            SyncMessageInbox();
        }

        private void messagesListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if(messagesListView.SelectedIndices.Count>0 )
            {
                MessageForm msgform = new MessageForm();
                msgform.Message = (CoreLib.Message)messagesListView.SelectedItems[0].Tag;
                if(inboxRadioBtn.Checked)
                msgform.ReadMessage += Msgform_ReadMessage;
                msgform.Show();  
            }
        }

        private void Msgform_ReadMessage(object sender, EventArgs e)
        {
            NetworkTransferObjects obj = new NetworkTransferObjects();
            obj.MessageUids = new string[] { ((CoreLib.Message)sender).UidMsg };
            obj.UserInfo = ((CoreLib.Message)sender).ToId; 

            foreach (CoreLib.Message msg in m_inbox)
            {
                if(msg.UidMsg == ((CoreLib.Message)sender).UidMsg)
                {
                    msg.IsReaded = true;
                    SyncMessageInbox();
                    break;
                }
            }

            foreach (CoreLib.Message msg in m_outbox)
            {
                if (msg.UidMsg == ((CoreLib.Message)sender).UidMsg)
                {
                    msg.IsReaded = true;
                    SyncMessageInbox();
                    break;
                }
            }
            SendReqest(ProtocolOfExchange.MessageRead, obj);

        }

        private void SyncTimer_Tick(object sender, EventArgs e)
        {
            SyncMessageBtn_Click(null, null);
        }


        #endregion


        #region Настройки
        private void check_newVersion_Click(object sender, EventArgs e)
        {
            SendReqest(ProtocolOfExchange.AskNewVersionClient, new NetworkTransferObjects());
        }
        #endregion
    }
    // Implements the manual sorting of items by columns.
    class ListViewDateComparer : IComparer
    {
       
        public ListViewDateComparer()
        {
            
        }
       
        public int Compare(object x, object y)
        {
            return DateTime.Compare(((CoreLib.Message)((ListViewItem)y).Tag).DateTimeOfMessage, ((CoreLib.Message)((ListViewItem)x).Tag).DateTimeOfMessage);
        }
    }
}
