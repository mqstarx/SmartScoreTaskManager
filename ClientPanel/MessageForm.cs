using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CoreLib;

namespace ClientPanel
{
    public partial class MessageForm : Form
    {
        private CoreLib.Message m_Message;
        public event EventHandler ReadMessage;
        public MessageForm()
        {
            InitializeComponent();
        }

        public CoreLib.Message Message
        {
            get
            {
                return m_Message;
            }

            set
            {
                m_Message = value;
                this.Text = "Сообщение от:" + m_Message.FromId;
                messageTxb.Text = m_Message.Msg;
            }
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            if (ReadMessage != null)
                ReadMessage(m_Message, null);
            this.Close();
        }
    }
}
