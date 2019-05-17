namespace AutoCreatePs1
{
    partial class FrmChangeAuthor
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
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNewUser = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtFields = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtListName = new System.Windows.Forms.TextBox();
            this.txtSubWeb = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(415, 204);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(154, 32);
            this.button1.TabIndex = 51;
            this.button1.Text = "更新创建者";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 104);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 50;
            this.label1.Text = "替换成";
            // 
            // txtNewUser
            // 
            this.txtNewUser.Location = new System.Drawing.Point(97, 101);
            this.txtNewUser.Multiline = true;
            this.txtNewUser.Name = "txtNewUser";
            this.txtNewUser.Size = new System.Drawing.Size(486, 37);
            this.txtNewUser.TabIndex = 49;
            this.txtNewUser.Text = "31059";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(29, 48);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 48;
            this.label8.Text = "原创建者";
            // 
            // txtFields
            // 
            this.txtFields.Location = new System.Drawing.Point(97, 45);
            this.txtFields.Multiline = true;
            this.txtFields.Name = "txtFields";
            this.txtFields.Size = new System.Drawing.Size(486, 37);
            this.txtFields.TabIndex = 47;
            this.txtFields.Text = "黄卫华；黄人杰；穆友胜；薛清侠;系统帐户";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(29, 18);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 46;
            this.label6.Text = "子网站";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(229, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 45;
            this.label5.Text = "列表名";
            // 
            // txtListName
            // 
            this.txtListName.Location = new System.Drawing.Point(292, 18);
            this.txtListName.Name = "txtListName";
            this.txtListName.Size = new System.Drawing.Size(125, 21);
            this.txtListName.TabIndex = 44;
            this.txtListName.Text = "个人状态指标 ";
            // 
            // txtSubWeb
            // 
            this.txtSubWeb.Location = new System.Drawing.Point(97, 18);
            this.txtSubWeb.Name = "txtSubWeb";
            this.txtSubWeb.Size = new System.Drawing.Size(116, 21);
            this.txtSubWeb.TabIndex = 43;
            this.txtSubWeb.Text = "blog";
            // 
            // FrmChangeAuthor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(694, 333);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtNewUser);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtFields);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtListName);
            this.Controls.Add(this.txtSubWeb);
            this.Name = "FrmChangeAuthor";
            this.Text = "FrmChangeAuthor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNewUser;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtFields;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtListName;
        private System.Windows.Forms.TextBox txtSubWeb;
    }
}