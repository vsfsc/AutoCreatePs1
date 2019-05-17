namespace AutoCreatePs1
{
    partial class FrmStart
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label4 = new System.Windows.Forms.Label();
            this.txtBackPath = new System.Windows.Forms.TextBox();
            this.txtSourceListName = new System.Windows.Forms.TextBox();
            this.txtDesListName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.btnRestore = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnNotice = new System.Windows.Forms.Button();
            this.txtListUrl = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnWorks = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSiteUrl = new System.Windows.Forms.TextBox();
            this.txtDesCol = new System.Windows.Forms.TextBox();
            this.txtSourceCol = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(57, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "备份的文件夹";
            // 
            // txtBackPath
            // 
            this.txtBackPath.Location = new System.Drawing.Point(155, 28);
            this.txtBackPath.Name = "txtBackPath";
            this.txtBackPath.Size = new System.Drawing.Size(222, 21);
            this.txtBackPath.TabIndex = 11;
            this.txtBackPath.Text = "d:\\backup";
            // 
            // txtSourceListName
            // 
            this.txtSourceListName.Location = new System.Drawing.Point(143, 56);
            this.txtSourceListName.Name = "txtSourceListName";
            this.txtSourceListName.Size = new System.Drawing.Size(249, 21);
            this.txtSourceListName.TabIndex = 13;
            this.txtSourceListName.Text = "日程";
            // 
            // txtDesListName
            // 
            this.txtDesListName.Location = new System.Drawing.Point(469, 56);
            this.txtDesListName.Name = "txtDesListName";
            this.txtDesListName.Size = new System.Drawing.Size(187, 21);
            this.txtDesListName.TabIndex = 13;
            this.txtDesListName.Text = "新日程";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(80, 62);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 10;
            this.label5.Text = "源列表名";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(398, 62);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "目的列表名";
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(281, 210);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(111, 32);
            this.btnImport.TabIndex = 14;
            this.btnImport.Text = "导入导出";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(57, 71);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(103, 32);
            this.btnBack.TabIndex = 0;
            this.btnBack.Text = "Backup Script";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(173, 134);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(204, 21);
            this.txtPath.TabIndex = 1;
            this.txtPath.Text = "d:\\DelResult.txt";
            // 
            // btnRestore
            // 
            this.btnRestore.Location = new System.Drawing.Point(189, 71);
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.Size = new System.Drawing.Size(110, 32);
            this.btnRestore.TabIndex = 2;
            this.btnRestore.Text = "Restore script";
            this.btnRestore.UseVisualStyleBackColor = true;
            this.btnRestore.Click += new System.EventHandler(this.btnRestore_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(55, 137);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "生成文件的文件夹";
            // 
            // btnNotice
            // 
            this.btnNotice.Location = new System.Drawing.Point(57, 178);
            this.btnNotice.Name = "btnNotice";
            this.btnNotice.Size = new System.Drawing.Size(110, 32);
            this.btnNotice.TabIndex = 4;
            this.btnNotice.Text = "NoticeAuthorTime";
            this.btnNotice.UseVisualStyleBackColor = true;
            this.btnNotice.Click += new System.EventHandler(this.btnNotice_Click);
            // 
            // txtListUrl
            // 
            this.txtListUrl.Location = new System.Drawing.Point(173, 227);
            this.txtListUrl.Name = "txtListUrl";
            this.txtListUrl.Size = new System.Drawing.Size(204, 21);
            this.txtListUrl.TabIndex = 7;
            this.txtListUrl.Text = "新闻公告";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(55, 230);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "列表Name";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(189, 178);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(154, 32);
            this.button1.TabIndex = 9;
            this.button1.Text = "UpdateNoticeAuthorTime";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(331, 71);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(97, 32);
            this.button2.TabIndex = 12;
            this.button2.Text = "DelUserProfile";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnWorks);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtListUrl);
            this.panel1.Controls.Add(this.btnNotice);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtBackPath);
            this.panel1.Controls.Add(this.btnRestore);
            this.panel1.Controls.Add(this.txtPath);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.btnBack);
            this.panel1.Location = new System.Drawing.Point(57, 249);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(626, 258);
            this.panel1.TabIndex = 15;
            // 
            // btnWorks
            // 
            this.btnWorks.Location = new System.Drawing.Point(480, 40);
            this.btnWorks.Name = "btnWorks";
            this.btnWorks.Size = new System.Drawing.Size(84, 31);
            this.btnWorks.TabIndex = 13;
            this.btnWorks.Text = "WorksEvaluation";
            this.btnWorks.UseVisualStyleBackColor = true;
            this.btnWorks.Click += new System.EventHandler(this.button3_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(84, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 17;
            this.label2.Text = "网站url";
            // 
            // txtSiteUrl
            // 
            this.txtSiteUrl.Location = new System.Drawing.Point(145, 16);
            this.txtSiteUrl.Name = "txtSiteUrl";
            this.txtSiteUrl.Size = new System.Drawing.Size(378, 21);
            this.txtSiteUrl.TabIndex = 16;
            this.txtSiteUrl.Text = "ComputerCompetitive";
            // 
            // txtDesCol
            // 
            this.txtDesCol.Location = new System.Drawing.Point(139, 147);
            this.txtDesCol.Multiline = true;
            this.txtDesCol.Name = "txtDesCol";
            this.txtDesCol.Size = new System.Drawing.Size(517, 49);
            this.txtDesCol.TabIndex = 20;
            this.txtDesCol.Text = "标题;任务名称;分配对象;计划时长;创建时间;执行者;活动时长;修改时间";
            // 
            // txtSourceCol
            // 
            this.txtSourceCol.Location = new System.Drawing.Point(139, 100);
            this.txtSourceCol.Multiline = true;
            this.txtSourceCol.Name = "txtSourceCol";
            this.txtSourceCol.Size = new System.Drawing.Size(517, 41);
            this.txtSourceCol.TabIndex = 21;
            this.txtSourceCol.Text = "标题;任务名称;分配对象;计划时长;创建时间;执行者;活动时长;修改时间";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(80, 150);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 18;
            this.label7.Text = "目的列名";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(82, 103);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 19;
            this.label8.Text = "源列名";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.Red;
            this.label9.Location = new System.Drawing.Point(84, 40);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(89, 12);
            this.label9.TabIndex = 22;
            this.label9.Text = "此处写相对地址";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(564, 210);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(91, 39);
            this.button3.TabIndex = 23;
            this.button3.Text = "test";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(427, 210);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(111, 32);
            this.button4.TabIndex = 24;
            this.button4.Text = "删除数据 ";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // FrmStart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(715, 519);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtDesCol);
            this.Controls.Add(this.txtSourceCol);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtSiteUrl);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.txtDesListName);
            this.Controls.Add(this.txtSourceListName);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Name = "FrmStart";
            this.Text = "列表数据导入导出";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtBackPath;
        private System.Windows.Forms.TextBox txtSourceListName;
        private System.Windows.Forms.TextBox txtDesListName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Button btnRestore;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnNotice;
        private System.Windows.Forms.TextBox txtListUrl;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSiteUrl;
        private System.Windows.Forms.TextBox txtDesCol;
        private System.Windows.Forms.TextBox txtSourceCol;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnWorks;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
    }
}

