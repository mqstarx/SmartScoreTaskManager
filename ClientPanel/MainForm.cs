using CoreLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ClientPanel
{
    public partial class MainForm : Form
    {
        private TcpModule m_TcpClient;
        public MainForm()
        {
            InitializeComponent();
            m_TcpClient = new TcpModule();
            m_TcpClient.Connected += M_TcpClient_Connected;
            m_TcpClient.Disconnected += M_TcpClient_Disconnected;
            m_TcpClient.Receive += M_TcpClient_Receive;
           
        }

        private void M_TcpClient_Receive(object sender, ReceiveEventArgs e)
        {
            
        }

        private void M_TcpClient_Disconnected(object sender, string result)
        {
           
        }

        private void M_TcpClient_Connected(object sender, string result)
        {
            Invoke(new Action(() => this.Text = result));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            m_TcpClient.ConnectClient("127.0.0.1");
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            if(op.ShowDialog()== DialogResult.OK)
            {
                UserFile uf = new UserFile(op.FileName);
                if(uf.FileLength>0)
                {
                  //  m_TcpClient.SendData(uf, "File",ProtocolOfExchange.NewMessageToUserOk);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            UserFile uf = new UserFile(int.Parse(textBox1.Text));
            //m_TcpClient.SendData(uf, "File", ProtocolOfExchange.AskNewTasks);
        }
    }
}
