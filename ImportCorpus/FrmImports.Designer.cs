namespace ImportCorpus
{
    partial class FrmImports
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.txtPath = new System.Windows.Forms.TextBox();
            this.btnImport = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnOpen = new System.Windows.Forms.Button();
            this.lbMsg = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbAC = new System.Windows.Forms.RadioButton();
            this.rbLC = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rbTagged = new System.Windows.Forms.RadioButton();
            this.rbUntagged = new System.Windows.Forms.RadioButton();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtPath
            // 
            this.txtPath.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPath.Location = new System.Drawing.Point(121, 211);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(500, 29);
            this.txtPath.TabIndex = 2;
            // 
            // btnImport
            // 
            this.btnImport.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnImport.Location = new System.Drawing.Point(366, 281);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(110, 31);
            this.btnImport.TabIndex = 3;
            this.btnImport.Text = "导入";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(118, 181);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(389, 19);
            this.label1.TabIndex = 4;
            this.label1.Text = "填写或通过按钮打开语料文件所在的文件夹：";
            // 
            // btnOpen
            // 
            this.btnOpen.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOpen.Location = new System.Drawing.Point(628, 212);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(108, 30);
            this.btnOpen.TabIndex = 5;
            this.btnOpen.Text = "打开文件夹";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // lbMsg
            // 
            this.lbMsg.AutoSize = true;
            this.lbMsg.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbMsg.ForeColor = System.Drawing.Color.Maroon;
            this.lbMsg.Location = new System.Drawing.Point(162, 260);
            this.lbMsg.Name = "lbMsg";
            this.lbMsg.Size = new System.Drawing.Size(57, 12);
            this.lbMsg.TabIndex = 6;
            this.lbMsg.Text = "信息提醒";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(129, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(123, 19);
            this.label2.TabIndex = 7;
            this.label2.Text = "语料库类型：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(129, 135);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 19);
            this.label3.TabIndex = 8;
            this.label3.Text = "是否赋码：";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.rbAC);
            this.panel1.Controls.Add(this.rbLC);
            this.panel1.Location = new System.Drawing.Point(275, 58);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(242, 55);
            this.panel1.TabIndex = 11;
            // 
            // rbAC
            // 
            this.rbAC.AutoSize = true;
            this.rbAC.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rbAC.Location = new System.Drawing.Point(162, 22);
            this.rbAC.Name = "rbAC";
            this.rbAC.Size = new System.Drawing.Size(77, 23);
            this.rbAC.TabIndex = 3;
            this.rbAC.Text = "NEUAC";
            this.rbAC.UseVisualStyleBackColor = true;
            // 
            // rbLC
            // 
            this.rbLC.AutoSize = true;
            this.rbLC.Checked = true;
            this.rbLC.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rbLC.Location = new System.Drawing.Point(4, 22);
            this.rbLC.Name = "rbLC";
            this.rbLC.Size = new System.Drawing.Size(77, 23);
            this.rbLC.TabIndex = 2;
            this.rbLC.TabStop = true;
            this.rbLC.Text = "NEULC";
            this.rbLC.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.AutoSize = true;
            this.panel2.Controls.Add(this.rbTagged);
            this.panel2.Controls.Add(this.rbUntagged);
            this.panel2.Location = new System.Drawing.Point(275, 127);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(254, 51);
            this.panel2.TabIndex = 12;
            // 
            // rbTagged
            // 
            this.rbTagged.AutoSize = true;
            this.rbTagged.Checked = true;
            this.rbTagged.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rbTagged.Location = new System.Drawing.Point(161, 10);
            this.rbTagged.Name = "rbTagged";
            this.rbTagged.Size = new System.Drawing.Size(87, 23);
            this.rbTagged.TabIndex = 12;
            this.rbTagged.TabStop = true;
            this.rbTagged.Text = "Tagged";
            this.rbTagged.UseVisualStyleBackColor = true;
            // 
            // rbUntagged
            // 
            this.rbUntagged.AutoSize = true;
            this.rbUntagged.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rbUntagged.Location = new System.Drawing.Point(3, 10);
            this.rbUntagged.Name = "rbUntagged";
            this.rbUntagged.Size = new System.Drawing.Size(107, 23);
            this.rbUntagged.TabIndex = 11;
            this.rbUntagged.Text = "UnTagged";
            this.rbUntagged.UseVisualStyleBackColor = true;
            // 
            // FrmImports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(874, 441);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbMsg);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.txtPath);
            this.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "FrmImports";
            this.Text = "批量导入语料库";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Label lbMsg;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rbAC;
        private System.Windows.Forms.RadioButton rbLC;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton rbTagged;
        private System.Windows.Forms.RadioButton rbUntagged;
    }
}

