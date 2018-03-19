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
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.connectionStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.TimeOutTimer = new System.Windows.Forms.Timer(this.components);
            this.tabControl = new System.Windows.Forms.TabControl();
            this.MessagesTab = new System.Windows.Forms.TabPage();
            this.TasksPage = new System.Windows.Forms.TabPage();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.statusStrip.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.MessagesTab.SuspendLayout();
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
            this.MessagesTab.Controls.Add(this.listBox1);
            this.MessagesTab.Location = new System.Drawing.Point(4, 22);
            this.MessagesTab.Name = "MessagesTab";
            this.MessagesTab.Padding = new System.Windows.Forms.Padding(3);
            this.MessagesTab.Size = new System.Drawing.Size(1110, 521);
            this.MessagesTab.TabIndex = 0;
            this.MessagesTab.Text = "Сообщения";
            this.MessagesTab.UseVisualStyleBackColor = true;
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
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Items.AddRange(new object[] {
            "1",
            "2",
            "1",
            "23"});
            this.listBox1.Location = new System.Drawing.Point(8, 26);
            this.listBox1.Name = "listBox1";
            this.listBox1.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBox1.Size = new System.Drawing.Size(503, 485);
            this.listBox1.TabIndex = 0;
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
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.MessagesTab.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel connectionStatusLabel;
        private System.Windows.Forms.Timer TimeOutTimer;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage MessagesTab;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.TabPage TasksPage;
    }
}