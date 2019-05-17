namespace AutoCreatePs1
{
    partial class FrmImportsData
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
            this.btn2016 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtListName = new System.Windows.Forms.TextBox();
            this.txtSubWeb = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btn2016
            // 
            this.btn2016.Location = new System.Drawing.Point(419, 146);
            this.btn2016.Name = "btn2016";
            this.btn2016.Size = new System.Drawing.Size(135, 23);
            this.btn2016.TabIndex = 58;
            this.btn2016.Text = "导入";
            this.btn2016.UseVisualStyleBackColor = true;
            this.btn2016.Click += new System.EventHandler(this.btn2016_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(127, 53);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 57;
            this.label6.Text = "子网站";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(366, 56);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 56;
            this.label5.Text = "列表名";
            // 
            // txtListName
            // 
            this.txtListName.Location = new System.Drawing.Point(429, 53);
            this.txtListName.Name = "txtListName";
            this.txtListName.Size = new System.Drawing.Size(125, 21);
            this.txtListName.TabIndex = 55;
            this.txtListName.Text = "操作";
            // 
            // txtSubWeb
            // 
            this.txtSubWeb.Location = new System.Drawing.Point(234, 53);
            this.txtSubWeb.Name = "txtSubWeb";
            this.txtSubWeb.Size = new System.Drawing.Size(116, 21);
            this.txtSubWeb.TabIndex = 54;
            this.txtSubWeb.Text = "blog";
            // 
            // FrmImportsData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 282);
            this.Controls.Add(this.btn2016);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtListName);
            this.Controls.Add(this.txtSubWeb);
            this.Name = "FrmImportsData";
            this.Text = "FrmImportsData";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn2016;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtListName;
        private System.Windows.Forms.TextBox txtSubWeb;
    }
}