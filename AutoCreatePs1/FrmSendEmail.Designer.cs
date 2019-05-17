namespace AutoCreatePs1
{
    partial class FrmSendEmail
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
            this.txtBody = new System.Windows.Forms.RichTextBox();
            this.txtSubject = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTos = new System.Windows.Forms.TextBox();
            this.txtFromShowName = new System.Windows.Forms.TextBox();
            this.txtFrom = new System.Windows.Forms.TextBox();
            this.btnSendEmail = new System.Windows.Forms.Button();
            this.txtPwd = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.txtSmtpServer = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtListName = new System.Windows.Forms.TextBox();
            this.txtSubWeb = new System.Windows.Forms.TextBox();
            this.txtFldName = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lbl = new System.Windows.Forms.Label();
            this.txtFldValue = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtBody
            // 
            this.txtBody.Location = new System.Drawing.Point(159, 331);
            this.txtBody.Name = "txtBody";
            this.txtBody.Size = new System.Drawing.Size(644, 121);
            this.txtBody.TabIndex = 0;
            this.txtBody.Text = "";
            // 
            // txtSubject
            // 
            this.txtSubject.Location = new System.Drawing.Point(159, 295);
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.Size = new System.Drawing.Size(644, 21);
            this.txtSubject.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(43, 298);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "邮件主题";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(43, 334);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "邮件正文";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(43, 179);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(113, 12);
            this.label5.TabIndex = 6;
            this.label5.Text = "收件人（分号分隔）";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(43, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "发件人显示名";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(43, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "发件人";
            // 
            // txtTos
            // 
            this.txtTos.Location = new System.Drawing.Point(159, 176);
            this.txtTos.Multiline = true;
            this.txtTos.Name = "txtTos";
            this.txtTos.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtTos.Size = new System.Drawing.Size(644, 102);
            this.txtTos.TabIndex = 3;
            // 
            // txtFromShowName
            // 
            this.txtFromShowName.Location = new System.Drawing.Point(159, 57);
            this.txtFromShowName.Name = "txtFromShowName";
            this.txtFromShowName.Size = new System.Drawing.Size(380, 21);
            this.txtFromShowName.TabIndex = 4;
            // 
            // txtFrom
            // 
            this.txtFrom.Location = new System.Drawing.Point(159, 19);
            this.txtFrom.Name = "txtFrom";
            this.txtFrom.Size = new System.Drawing.Size(380, 21);
            this.txtFrom.TabIndex = 5;
            // 
            // btnSendEmail
            // 
            this.btnSendEmail.Location = new System.Drawing.Point(248, 516);
            this.btnSendEmail.Name = "btnSendEmail";
            this.btnSendEmail.Size = new System.Drawing.Size(102, 36);
            this.btnSendEmail.TabIndex = 56;
            this.btnSendEmail.Text = "发邮件";
            this.btnSendEmail.UseVisualStyleBackColor = true;
            this.btnSendEmail.Click += new System.EventHandler(this.btnSendEmail_Click);
            // 
            // txtPwd
            // 
            this.txtPwd.Location = new System.Drawing.Point(159, 95);
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.PasswordChar = '*';
            this.txtPwd.Size = new System.Drawing.Size(380, 21);
            this.txtPwd.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(43, 98);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 7;
            this.label6.Text = "发件人密码";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(371, 516);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(102, 36);
            this.btnClose.TabIndex = 56;
            this.btnClose.Text = "关闭";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // txtSmtpServer
            // 
            this.txtSmtpServer.Location = new System.Drawing.Point(159, 136);
            this.txtSmtpServer.Name = "txtSmtpServer";
            this.txtSmtpServer.Size = new System.Drawing.Size(380, 21);
            this.txtSmtpServer.TabIndex = 4;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(43, 139);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 7;
            this.label7.Text = "发件服务器";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(501, 516);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(126, 36);
            this.button1.TabIndex = 57;
            this.button1.Text = "发邮件（读列表）";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(555, 22);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 61;
            this.label8.Text = "子网站";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(555, 63);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 12);
            this.label9.TabIndex = 60;
            this.label9.Text = "列表名";
            // 
            // txtListName
            // 
            this.txtListName.Location = new System.Drawing.Point(609, 60);
            this.txtListName.Name = "txtListName";
            this.txtListName.Size = new System.Drawing.Size(194, 21);
            this.txtListName.TabIndex = 59;
            this.txtListName.Text = "创意";
            // 
            // txtSubWeb
            // 
            this.txtSubWeb.Location = new System.Drawing.Point(609, 22);
            this.txtSubWeb.Name = "txtSubWeb";
            this.txtSubWeb.Size = new System.Drawing.Size(194, 21);
            this.txtSubWeb.TabIndex = 58;
            this.txtSubWeb.Text = "SmartNEU";
            // 
            // txtFldName
            // 
            this.txtFldName.Location = new System.Drawing.Point(609, 98);
            this.txtFldName.Name = "txtFldName";
            this.txtFldName.Size = new System.Drawing.Size(90, 21);
            this.txtFldName.TabIndex = 59;
            this.txtFldName.Text = "Flag";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(555, 101);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 12);
            this.label10.TabIndex = 60;
            this.label10.Text = "条件列";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(609, 139);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(194, 21);
            this.txtEmail.TabIndex = 59;
            this.txtEmail.Text = "电子邮箱";
            // 
            // lbl
            // 
            this.lbl.AutoSize = true;
            this.lbl.Location = new System.Drawing.Point(555, 142);
            this.lbl.Name = "lbl";
            this.lbl.Size = new System.Drawing.Size(53, 12);
            this.lbl.TabIndex = 60;
            this.lbl.Text = "邮件列名";
            // 
            // txtFldValue
            // 
            this.txtFldValue.Location = new System.Drawing.Point(726, 98);
            this.txtFldValue.Name = "txtFldValue";
            this.txtFldValue.Size = new System.Drawing.Size(77, 21);
            this.txtFldValue.TabIndex = 59;
            this.txtFldValue.Text = "2";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(705, 101);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(17, 12);
            this.label11.TabIndex = 60;
            this.label11.Text = "值";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(667, 516);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(123, 36);
            this.button2.TabIndex = 62;
            this.button2.Text = "发邮件（获奖学生，外语除外）";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // FrmSendEmail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(872, 564);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.lbl);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.txtFldValue);
            this.Controls.Add(this.txtFldName);
            this.Controls.Add(this.txtListName);
            this.Controls.Add(this.txtSubWeb);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSendEmail);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtTos);
            this.Controls.Add(this.txtSmtpServer);
            this.Controls.Add(this.txtPwd);
            this.Controls.Add(this.txtFromShowName);
            this.Controls.Add(this.txtFrom);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtSubject);
            this.Controls.Add(this.txtBody);
            this.Name = "FrmSendEmail";
            this.Text = "群发邮件";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSendEmail;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lbl;
        private System.Windows.Forms.RichTextBox txtBody;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtFldName;
        private System.Windows.Forms.TextBox txtFldValue;
        private System.Windows.Forms.TextBox txtFrom;
        private System.Windows.Forms.TextBox txtFromShowName;
        private System.Windows.Forms.TextBox txtListName;
        private System.Windows.Forms.TextBox txtPwd;
        private System.Windows.Forms.TextBox txtSmtpServer;
        private System.Windows.Forms.TextBox txtSubject;
        private System.Windows.Forms.TextBox txtSubWeb;
        private System.Windows.Forms.TextBox txtTos;
        private System.Windows.Forms.Button button2;
    }
}