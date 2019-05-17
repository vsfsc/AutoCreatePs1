using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoCreatePPT
{
    public partial class frmNewCommon : Form
    {
        internal Button btnNewPpt;
        private GroupBox groupBox2;
        internal Label label8;
        internal TextBox txtUnitID;

        public frmNewCommon()
        {
            InitializeComponent();
        }
        //2018-07-05
        private void btnNewPpt_Click(object sender, EventArgs e)
        {
            new clsPpt().CreateNewCommonEnglish(this.txtUnitID.Text.Trim());
           
        }

      

        private void InitializeComponent()
        {
            this.groupBox2 = new GroupBox();
            this.txtUnitID = new TextBox();
            this.label8 = new Label();
            this.btnNewPpt = new Button();
            this.groupBox2.SuspendLayout();
            base.SuspendLayout();
            this.groupBox2.Controls.Add(this.txtUnitID);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.btnNewPpt);
            this.groupBox2.Location = new Point(0x10, 8);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0xd0, 80);
            this.groupBox2.TabIndex = 0x33;
            this.groupBox2.TabStop = false;
            this.txtUnitID.Location = new Point(40, 0x18);
            this.txtUnitID.Name = "txtUnitID";
            this.txtUnitID.Size = new Size(0x20, 0x15);
            this.txtUnitID.TabIndex = 40;
            this.txtUnitID.Text = "01";
            this.label8.Location = new Point(8, 0x1b);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x20, 0x10);
            this.label8.TabIndex = 0x27;
            this.label8.Text = "单元";
            this.btnNewPpt.FlatStyle = FlatStyle.Popup;
            this.btnNewPpt.Location = new Point(80, 0x18);
            this.btnNewPpt.Name = "btnNewPpt";
            this.btnNewPpt.Size = new Size(0x68, 0x18);
            this.btnNewPpt.TabIndex = 0x29;
            this.btnNewPpt.Text = "新通用PPT";
            this.btnNewPpt.Click += new EventHandler(this.btnNewPpt_Click);
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(0xf2, 0x6a);
            base.Controls.Add(this.groupBox2);
            base.Name = "FrmNewCommon";
            this.Text = "新通用大学英语电子教案";
            this.groupBox2.ResumeLayout(false);
            base.ResumeLayout(false);
        }

    }
}
