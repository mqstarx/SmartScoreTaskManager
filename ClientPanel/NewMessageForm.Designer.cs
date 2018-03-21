namespace ClientPanel
{
    partial class NewMessageForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewMessageForm));
            this.userListTree = new System.Windows.Forms.TreeView();
            this.searchTxb = new System.Windows.Forms.TextBox();
            this.messageTxb = new System.Windows.Forms.TextBox();
            this.SendMessageBtn = new System.Windows.Forms.Button();
            this.attachmentsListBox = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // userListTree
            // 
            this.userListTree.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.userListTree.CheckBoxes = true;
            this.userListTree.Location = new System.Drawing.Point(12, 51);
            this.userListTree.Name = "userListTree";
            this.userListTree.Size = new System.Drawing.Size(277, 434);
            this.userListTree.TabIndex = 0;
            // 
            // searchTxb
            // 
            this.searchTxb.Location = new System.Drawing.Point(12, 25);
            this.searchTxb.Name = "searchTxb";
            this.searchTxb.Size = new System.Drawing.Size(277, 20);
            this.searchTxb.TabIndex = 1;
            this.searchTxb.TextChanged += new System.EventHandler(this.searchTxb_TextChanged);
            // 
            // messageTxb
            // 
            this.messageTxb.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.messageTxb.Location = new System.Drawing.Point(295, 51);
            this.messageTxb.Multiline = true;
            this.messageTxb.Name = "messageTxb";
            this.messageTxb.Size = new System.Drawing.Size(285, 434);
            this.messageTxb.TabIndex = 2;
            // 
            // SendMessageBtn
            // 
            this.SendMessageBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SendMessageBtn.Location = new System.Drawing.Point(586, 464);
            this.SendMessageBtn.Name = "SendMessageBtn";
            this.SendMessageBtn.Size = new System.Drawing.Size(140, 23);
            this.SendMessageBtn.TabIndex = 3;
            this.SendMessageBtn.Text = "Отправить";
            this.SendMessageBtn.UseVisualStyleBackColor = true;
            this.SendMessageBtn.Click += new System.EventHandler(this.SendMessageBtn_Click);
            // 
            // attachmentsListBox
            // 
            this.attachmentsListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.attachmentsListBox.FormattingEnabled = true;
            this.attachmentsListBox.Location = new System.Drawing.Point(586, 50);
            this.attachmentsListBox.Name = "attachmentsListBox";
            this.attachmentsListBox.Size = new System.Drawing.Size(140, 407);
            this.attachmentsListBox.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(583, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Вложения:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(295, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Сообщение:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Получатели:";
            // 
            // NewMessageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(738, 497);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.attachmentsListBox);
            this.Controls.Add(this.SendMessageBtn);
            this.Controls.Add(this.messageTxb);
            this.Controls.Add(this.searchTxb);
            this.Controls.Add(this.userListTree);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "NewMessageForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Новое сообщение";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView userListTree;
        private System.Windows.Forms.TextBox searchTxb;
        private System.Windows.Forms.TextBox messageTxb;
        private System.Windows.Forms.Button SendMessageBtn;
        private System.Windows.Forms.ListBox attachmentsListBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}