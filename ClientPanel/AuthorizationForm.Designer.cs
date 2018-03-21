namespace ClientPanel
{
    partial class AuthorizationForm
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
            this.userCmb = new System.Windows.Forms.ComboBox();
            this.passwordTxb = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.rememberCxb = new System.Windows.Forms.CheckBox();
            this.showSymbols = new System.Windows.Forms.CheckBox();
            this.OkAuth = new System.Windows.Forms.Button();
            this.exitBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // userCmb
            // 
            this.userCmb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.userCmb.FormattingEnabled = true;
            this.userCmb.Location = new System.Drawing.Point(104, 23);
            this.userCmb.Name = "userCmb";
            this.userCmb.Size = new System.Drawing.Size(466, 21);
            this.userCmb.TabIndex = 0;
            // 
            // passwordTxb
            // 
            this.passwordTxb.Location = new System.Drawing.Point(104, 71);
            this.passwordTxb.Name = "passwordTxb";
            this.passwordTxb.Size = new System.Drawing.Size(335, 20);
            this.passwordTxb.TabIndex = 1;
            this.passwordTxb.UseSystemPasswordChar = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Пользователь: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Пароль:";
            // 
            // rememberCxb
            // 
            this.rememberCxb.AutoSize = true;
            this.rememberCxb.Location = new System.Drawing.Point(488, 136);
            this.rememberCxb.Name = "rememberCxb";
            this.rememberCxb.Size = new System.Drawing.Size(82, 17);
            this.rememberCxb.TabIndex = 4;
            this.rememberCxb.Text = "Запомнить";
            this.rememberCxb.UseVisualStyleBackColor = true;
            // 
            // showSymbols
            // 
            this.showSymbols.AutoSize = true;
            this.showSymbols.Location = new System.Drawing.Point(446, 74);
            this.showSymbols.Name = "showSymbols";
            this.showSymbols.Size = new System.Drawing.Size(124, 17);
            this.showSymbols.TabIndex = 5;
            this.showSymbols.Text = "Показать символы";
            this.showSymbols.UseVisualStyleBackColor = true;
            this.showSymbols.CheckedChanged += new System.EventHandler(this.showSymbols_CheckedChanged);
            // 
            // OkAuth
            // 
            this.OkAuth.Location = new System.Drawing.Point(345, 132);
            this.OkAuth.Name = "OkAuth";
            this.OkAuth.Size = new System.Drawing.Size(124, 23);
            this.OkAuth.TabIndex = 6;
            this.OkAuth.Text = "OK";
            this.OkAuth.UseVisualStyleBackColor = true;
            this.OkAuth.Click += new System.EventHandler(this.OkAuth_Click);
            // 
            // exitBtn
            // 
            this.exitBtn.Location = new System.Drawing.Point(18, 130);
            this.exitBtn.Name = "exitBtn";
            this.exitBtn.Size = new System.Drawing.Size(139, 23);
            this.exitBtn.TabIndex = 7;
            this.exitBtn.Text = "Выход из приложения";
            this.exitBtn.UseVisualStyleBackColor = true;
            this.exitBtn.Click += new System.EventHandler(this.exitBtn_Click);
            // 
            // AuthorizationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(582, 171);
            this.Controls.Add(this.exitBtn);
            this.Controls.Add(this.OkAuth);
            this.Controls.Add(this.showSymbols);
            this.Controls.Add(this.rememberCxb);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.passwordTxb);
            this.Controls.Add(this.userCmb);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AuthorizationForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Авторизация";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox rememberCxb;
        private System.Windows.Forms.CheckBox showSymbols;
        private System.Windows.Forms.Button OkAuth;
        public System.Windows.Forms.ComboBox userCmb;
        public System.Windows.Forms.TextBox passwordTxb;
        private System.Windows.Forms.Button exitBtn;
    }
}