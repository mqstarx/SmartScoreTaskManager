namespace ClientPanel
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.connectionStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.TimeOutTimer = new System.Windows.Forms.Timer(this.components);
            this.tabControl = new System.Windows.Forms.TabControl();
            this.MessagesTab = new System.Windows.Forms.TabPage();
            this.messagesListView = new System.Windows.Forms.ListView();
            this.To = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.frommsg = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.message = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.date = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.have_attachments = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.outBoxRadioBtn = new System.Windows.Forms.RadioButton();
            this.inboxRadioBtn = new System.Windows.Forms.RadioButton();
            this.newMessageBtn = new System.Windows.Forms.Button();
            this.TasksPage = new System.Windows.Forms.TabPage();
            this.SyncMessageBtn = new System.Windows.Forms.Button();
            this.statusStrip.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.MessagesTab.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectionStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 551);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1118, 22);
            this.statusStrip.TabIndex = 4;
            this.statusStrip.Text = "statusStrip1";
            // 
            // connectionStatusLabel
            // 
            this.connectionStatusLabel.BackColor = System.Drawing.Color.Red;
            this.connectionStatusLabel.Name = "connectionStatusLabel";
            this.connectionStatusLabel.Size = new System.Drawing.Size(137, 17);
            this.connectionStatusLabel.Text = "Соединение с серверов";
            // 
            // TimeOutTimer
            // 
            this.TimeOutTimer.Tick += new System.EventHandler(this.TimeOutTimer_Tick);
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.MessagesTab);
            this.tabControl.Controls.Add(this.TasksPage);
            this.tabControl.Location = new System.Drawing.Point(0, 1);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1118, 547);
            this.tabControl.TabIndex = 5;
            // 
            // MessagesTab
            // 
            this.MessagesTab.Controls.Add(this.SyncMessageBtn);
            this.MessagesTab.Controls.Add(this.messagesListView);
            this.MessagesTab.Controls.Add(this.groupBox1);
            this.MessagesTab.Location = new System.Drawing.Point(4, 22);
            this.MessagesTab.Name = "MessagesTab";
            this.MessagesTab.Padding = new System.Windows.Forms.Padding(3);
            this.MessagesTab.Size = new System.Drawing.Size(1110, 521);
            this.MessagesTab.TabIndex = 0;
            this.MessagesTab.Text = "Сообщения";
            this.MessagesTab.UseVisualStyleBackColor = true;
            // 
            // messagesListView
            // 
            this.messagesListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.messagesListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.To,
            this.frommsg,
            this.message,
            this.date,
            this.have_attachments});
            this.messagesListView.FullRowSelect = true;
            this.messagesListView.GridLines = true;
            this.messagesListView.Location = new System.Drawing.Point(189, 6);
            this.messagesListView.Name = "messagesListView";
            this.messagesListView.Size = new System.Drawing.Size(913, 486);
            this.messagesListView.SmallImageList = this.imageList1;
            this.messagesListView.TabIndex = 1;
            this.messagesListView.UseCompatibleStateImageBehavior = false;
            this.messagesListView.View = System.Windows.Forms.View.Details;
            // 
            // To
            // 
            this.To.Text = "Кому:";
            this.To.Width = 244;
            // 
            // frommsg
            // 
            this.frommsg.Text = "От кого: ";
            this.frommsg.Width = 238;
            // 
            // message
            // 
            this.message.Text = "Сообщение";
            this.message.Width = 201;
            // 
            // date
            // 
            this.date.Text = "Дата";
            this.date.Width = 158;
            // 
            // have_attachments
            // 
            this.have_attachments.Text = "Файлы:";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "skrepka2.gif");
            this.imageList1.Images.SetKeyName(1, "skrepka.png");
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.outBoxRadioBtn);
            this.groupBox1.Controls.Add(this.inboxRadioBtn);
            this.groupBox1.Controls.Add(this.newMessageBtn);
            this.groupBox1.Location = new System.Drawing.Point(8, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(175, 129);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Сообщения";
            // 
            // outBoxRadioBtn
            // 
            this.outBoxRadioBtn.AutoSize = true;
            this.outBoxRadioBtn.Location = new System.Drawing.Point(14, 94);
            this.outBoxRadioBtn.Name = "outBoxRadioBtn";
            this.outBoxRadioBtn.Size = new System.Drawing.Size(83, 17);
            this.outBoxRadioBtn.TabIndex = 2;
            this.outBoxRadioBtn.Text = "Исходящие";
            this.outBoxRadioBtn.UseVisualStyleBackColor = true;
            // 
            // inboxRadioBtn
            // 
            this.inboxRadioBtn.AutoSize = true;
            this.inboxRadioBtn.Checked = true;
            this.inboxRadioBtn.Location = new System.Drawing.Point(14, 60);
            this.inboxRadioBtn.Name = "inboxRadioBtn";
            this.inboxRadioBtn.Size = new System.Drawing.Size(76, 17);
            this.inboxRadioBtn.TabIndex = 1;
            this.inboxRadioBtn.TabStop = true;
            this.inboxRadioBtn.Text = "Входящие";
            this.inboxRadioBtn.UseVisualStyleBackColor = true;
            // 
            // newMessageBtn
            // 
            this.newMessageBtn.Location = new System.Drawing.Point(6, 19);
            this.newMessageBtn.Name = "newMessageBtn";
            this.newMessageBtn.Size = new System.Drawing.Size(163, 23);
            this.newMessageBtn.TabIndex = 0;
            this.newMessageBtn.Text = "Новое сообщение";
            this.newMessageBtn.UseVisualStyleBackColor = true;
            this.newMessageBtn.Click += new System.EventHandler(this.newMessageBtn_Click);
            // 
            // TasksPage
            // 
            this.TasksPage.Location = new System.Drawing.Point(4, 22);
            this.TasksPage.Name = "TasksPage";
            this.TasksPage.Padding = new System.Windows.Forms.Padding(3);
            this.TasksPage.Size = new System.Drawing.Size(1110, 521);
            this.TasksPage.TabIndex = 1;
            this.TasksPage.Text = "Задачи";
            this.TasksPage.UseVisualStyleBackColor = true;
            // 
            // SyncMessageBtn
            // 
            this.SyncMessageBtn.Location = new System.Drawing.Point(8, 141);
            this.SyncMessageBtn.Name = "SyncMessageBtn";
            this.SyncMessageBtn.Size = new System.Drawing.Size(175, 23);
            this.SyncMessageBtn.TabIndex = 2;
            this.SyncMessageBtn.Text = "Синхронизировать";
            this.SyncMessageBtn.UseVisualStyleBackColor = true;
            this.SyncMessageBtn.Click += new System.EventHandler(this.SyncMessageBtn_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1118, 573);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.statusStrip);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.MessagesTab.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel connectionStatusLabel;
        private System.Windows.Forms.Timer TimeOutTimer;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage MessagesTab;
        private System.Windows.Forms.TabPage TasksPage;
        private System.Windows.Forms.ListView messagesListView;
        private System.Windows.Forms.ColumnHeader To;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton outBoxRadioBtn;
        private System.Windows.Forms.RadioButton inboxRadioBtn;
        private System.Windows.Forms.Button newMessageBtn;
        private System.Windows.Forms.ColumnHeader frommsg;
        private System.Windows.Forms.ColumnHeader message;
        private System.Windows.Forms.ColumnHeader date;
        private System.Windows.Forms.ColumnHeader have_attachments;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button SyncMessageBtn;
    }
}