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
    public partial class AuthorizationForm : Form
    {
        private List<UserInfo> m_ListUser;
        public AuthorizationForm()
        {
            InitializeComponent();
        }

        public List<UserInfo> ListUser
        {
            get
            {
                return m_ListUser;
            }

            set
            {
                m_ListUser = value;
                userCmb.Items.AddRange(m_ListUser.ToArray());
                string[] arr = (string[])Func.LoadConfig("auth.bin");
                if(arr!=null)
                {
                    if(arr.Length==2)
                    {
                        if (userCmb.Items.Count  > int.Parse(arr[0]) && int.Parse(arr[0])!=-1)
                        {
                            userCmb.SelectedIndex = int.Parse(arr[0]);
                            passwordTxb.Text = Hash.DecodeString(arr[1]);
                            rememberCxb.Checked = true;
                        }
                      
                    }
                }

            }
        }

        private void showSymbols_CheckedChanged(object sender, EventArgs e)
        {
            if (showSymbols.Checked)
                passwordTxb.UseSystemPasswordChar = false;
            else
                passwordTxb.UseSystemPasswordChar = true;
        }

        private void OkAuth_Click(object sender, EventArgs e)
        {
            if(userCmb.SelectedIndex!=-1 && passwordTxb.Text.Length>0)
            {
                if(rememberCxb.Checked)
                {
                    string[] arr = new string[2];

                    arr[0] = userCmb.SelectedIndex.ToString();
                    arr[1] = Hash.EncodeString(passwordTxb.Text);

                    Func.SaveConfig(arr, "auth.bin");
                }
                else
                {
                    string[] arr = new string[2];

                    arr[0] = "-1";
                    arr[1] = "";

                    Func.SaveConfig(arr, "auth.bin");
                }
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }
    }
}
