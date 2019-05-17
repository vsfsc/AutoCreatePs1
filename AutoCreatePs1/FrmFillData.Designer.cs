namespace AutoCreatePs1
{
    partial class FrmFillData
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
            this.btnFillData = new System.Windows.Forms.Button();
            this.lblLst = new System.Windows.Forms.Label();
            this.txtListName = new System.Windows.Forms.TextBox();
            this.lblSubWeb = new System.Windows.Forms.Label();
            this.txtSubWeb = new System.Windows.Forms.TextBox();
            this.lblMsg = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.txtStart = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnFillData
            // 
            this.btnFillData.Location = new System.Drawing.Point(387, 127);
            this.btnFillData.Name = "btnFillData";
            this.btnFillData.Size = new System.Drawing.Size(164, 23);
            this.btnFillData.TabIndex = 0;
            this.btnFillData.Text = "填充活动类型和活动操作";
            this.btnFillData.UseVisualStyleBackColor = true;
            this.btnFillData.Click += new System.EventHandler(this.btnFillData_Click);
            // 
            // lblLst
            // 
            this.lblLst.AutoSize = true;
            this.lblLst.Location = new System.Drawing.Point(83, 93);
            this.lblLst.Name = "lblLst";
            this.lblLst.Size = new System.Drawing.Size(53, 12);
            this.lblLst.TabIndex = 1;
            this.lblLst.Text = "列表名称";
            // 
            // txtListName
            // 
            this.txtListName.Location = new System.Drawing.Point(197, 93);
            this.txtListName.Name = "txtListName";
            this.txtListName.Size = new System.Drawing.Size(180, 21);
            this.txtListName.TabIndex = 2;
            this.txtListName.Text = "个人学习助手";
            // 
            // lblSubWeb
            // 
            this.lblSubWeb.AutoSize = true;
            this.lblSubWeb.Location = new System.Drawing.Point(83, 57);
            this.lblSubWeb.Name = "lblSubWeb";
            this.lblSubWeb.Size = new System.Drawing.Size(41, 12);
            this.lblSubWeb.TabIndex = 28;
            this.lblSubWeb.Text = "子网站";
            // 
            // txtSubWeb
            // 
            this.txtSubWeb.Location = new System.Drawing.Point(197, 57);
            this.txtSubWeb.Name = "txtSubWeb";
            this.txtSubWeb.Size = new System.Drawing.Size(180, 21);
            this.txtSubWeb.TabIndex = 27;
            this.txtSubWeb.Text = "Projects/VAExtension";
            // 
            // lblMsg
            // 
            this.lblMsg.AutoSize = true;
            this.lblMsg.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblMsg.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblMsg.Location = new System.Drawing.Point(487, 268);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(44, 12);
            this.lblMsg.TabIndex = 47;
            this.lblMsg.Text = "子网站";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(113, 268);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(368, 23);
            this.progressBar1.TabIndex = 48;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(83, 132);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 12);
            this.label1.TabIndex = 52;
            this.label1.Text = "遍历开始（0开始）";
            // 
            // txtStart
            // 
            this.txtStart.Location = new System.Drawing.Point(196, 129);
            this.txtStart.Name = "txtStart";
            this.txtStart.Size = new System.Drawing.Size(181, 21);
            this.txtStart.TabIndex = 51;
            this.txtStart.Text = "2165";
            // 
            // FrmFillData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(679, 350);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtStart);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.lblSubWeb);
            this.Controls.Add(this.txtSubWeb);
            this.Controls.Add(this.txtListName);
            this.Controls.Add(this.lblLst);
            this.Controls.Add(this.btnFillData);
            this.Name = "FrmFillData";
            this.Text = "个人学习助手填充数据";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnFillData;
        private System.Windows.Forms.Label lblLst;
        private System.Windows.Forms.TextBox txtListName;
        private System.Windows.Forms.Label lblSubWeb;
        private System.Windows.Forms.TextBox txtSubWeb;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtStart;
    }
}