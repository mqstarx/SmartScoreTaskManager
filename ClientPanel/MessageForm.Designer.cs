namespace ClientPanel
{
    partial class MessageForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MessageForm));
            this.messageTxb = new System.Windows.Forms.TextBox();
            this.replyBtn = new System.Windows.Forms.Button();
            this.closeBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // messageTxb
            // 
            this.messageTxb.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.messageTxb.BackColor = System.Drawing.Color.White;
            this.messageTxb.Location = new System.Drawing.Point(2, 2);
            this.messageTxb.Multiline = true;
            this.messageTxb.Name = "messageTxb";
            this.messageTxb.ReadOnly = true;
            this.messageTxb.Size = new System.Drawing.Size(700, 417);
            this.messageTxb.TabIndex = 0;
            // 
            // replyBtn
            // 
            this.replyBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.replyBtn.Location = new System.Drawing.Point(2, 425);
            this.replyBtn.Name = "replyBtn";
            this.replyBtn.Size = new System.Drawing.Size(75, 23);
            this.replyBtn.TabIndex = 1;
            this.replyBtn.Text = "Ответить";
            this.replyBtn.UseVisualStyleBackColor = true;
            // 
            // closeBtn
            // 
            this.closeBtn.Location = new System.Drawing.Point(624, 425);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(75, 23);
            this.closeBtn.TabIndex = 2;
            this.closeBtn.Text = "Закрыть";
            this.closeBtn.UseVisualStyleBackColor = true;
            this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
            // 
            // MessageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(702, 455);
            this.Controls.Add(this.closeBtn);
            this.Controls.Add(this.replyBtn);
            this.Controls.Add(this.messageTxb);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MessageForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox messageTxb;
        private System.Windows.Forms.Button replyBtn;
        private System.Windows.Forms.Button closeBtn;
    }
}