namespace AutoCreatePs1
{
    partial class FrmCreative
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
            this.label9 = new System.Windows.Forms.Label();
            this.txtDesCol = new System.Windows.Forms.TextBox();
            this.txtSourceCol = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtWebSourceUrl = new System.Windows.Forms.TextBox();
            this.btnImport = new System.Windows.Forms.Button();
            this.txtDesListName = new System.Windows.Forms.TextBox();
            this.txtSourceListName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtWebDesUrl = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.txtFldEqual = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.Red;
            this.label9.Location = new System.Drawing.Point(63, 36);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(89, 12);
            this.label9.TabIndex = 35;
            this.label9.Text = "此处写相对地址";
            // 
            // txtDesCol
            // 
            this.txtDesCol.Location = new System.Drawing.Point(178, 222);
            this.txtDesCol.Multiline = true;
            this.txtDesCol.Name = "txtDesCol";
            this.txtDesCol.Size = new System.Drawing.Size(456, 49);
            this.txtDesCol.TabIndex = 33;
            this.txtDesCol.Text = "所属部门";
            // 
            // txtSourceCol
            // 
            this.txtSourceCol.Location = new System.Drawing.Point(178, 175);
            this.txtSourceCol.Multiline = true;
            this.txtSourceCol.Name = "txtSourceCol";
            this.txtSourceCol.Size = new System.Drawing.Size(456, 41);
            this.txtSourceCol.TabIndex = 34;
            this.txtSourceCol.Text = "所属部门";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(58, 225);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 31;
            this.label7.Text = "目标列名";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(60, 178);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 32;
            this.label8.Text = "源列名";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(63, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 30;
            this.label2.Text = "源网站url";
            // 
            // txtWebSourceUrl
            // 
            this.txtWebSourceUrl.Location = new System.Drawing.Point(178, 12);
            this.txtWebSourceUrl.Name = "txtWebSourceUrl";
            this.txtWebSourceUrl.Size = new System.Drawing.Size(195, 21);
            this.txtWebSourceUrl.TabIndex = 29;
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(275, 293);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(111, 32);
            this.btnImport.TabIndex = 28;
            this.btnImport.Text = "导入导出";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // txtDesListName
            // 
            this.txtDesListName.Location = new System.Drawing.Point(442, 69);
            this.txtDesListName.Name = "txtDesListName";
            this.txtDesListName.Size = new System.Drawing.Size(187, 21);
            this.txtDesListName.TabIndex = 26;
            this.txtDesListName.Text = "教师花名册";
            // 
            // txtSourceListName
            // 
            this.txtSourceListName.Location = new System.Drawing.Point(442, 15);
            this.txtSourceListName.Name = "txtSourceListName";
            this.txtSourceListName.Size = new System.Drawing.Size(192, 21);
            this.txtSourceListName.TabIndex = 27;
            this.txtSourceListName.Text = "教师花名册";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(371, 75);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 24;
            this.label6.Text = "目标列表名";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(379, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 25;
            this.label5.Text = "源列表名";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(58, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 37;
            this.label1.Text = "目标网站url";
            // 
            // txtWebDesUrl
            // 
            this.txtWebDesUrl.Location = new System.Drawing.Point(178, 69);
            this.txtWebDesUrl.Name = "txtWebDesUrl";
            this.txtWebDesUrl.Size = new System.Drawing.Size(187, 21);
            this.txtWebDesUrl.TabIndex = 36;
            this.txtWebDesUrl.Text = "Performance";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(58, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 12);
            this.label3.TabIndex = 38;
            this.label3.Text = "此处写相对地址";
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(455, 293);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(111, 32);
            this.btnUpdate.TabIndex = 39;
            this.btnUpdate.Text = "更新";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // txtFldEqual
            // 
            this.txtFldEqual.Location = new System.Drawing.Point(178, 128);
            this.txtFldEqual.Name = "txtFldEqual";
            this.txtFldEqual.Size = new System.Drawing.Size(187, 21);
            this.txtFldEqual.TabIndex = 41;
            this.txtFldEqual.Text = "工号";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(64, 131);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 12);
            this.label4.TabIndex = 40;
            this.label4.Text = "值相等的条件列";
            // 
            // FrmCreative
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(815, 449);
            this.Controls.Add(this.txtFldEqual);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtWebDesUrl);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtDesCol);
            this.Controls.Add(this.txtSourceCol);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtWebSourceUrl);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.txtDesListName);
            this.Controls.Add(this.txtSourceListName);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Name = "FrmCreative";
            this.Text = "FrmCreative";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtDesCol;
        private System.Windows.Forms.TextBox txtSourceCol;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtWebSourceUrl;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.TextBox txtDesListName;
        private System.Windows.Forms.TextBox txtSourceListName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtWebDesUrl;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.TextBox txtFldEqual;
        private System.Windows.Forms.Label label4;
    }
}