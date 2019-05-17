using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace AutoCreatePPT
{
    public partial class frmPPT : Form
    {
      
        internal Button button1;
        internal Button button10;
        internal Button button11;
        private Button button12;
        private Button button13;
        private Button button14;
        private Button button2;
        internal Button button3;
        internal Button button4;
        internal Button button5;
        internal Button button6;
        private Button button7;
        internal Button button8;
        internal Button Button9;
        internal Button cmd;
        private ComboBox comboBox1;
        //private Container components = null;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        internal Label label1;
        internal Label label2;
        internal Label label3;
        internal Label label4;
        internal Label label5;
        internal Label Label6;
        internal Label label7;
        internal Label label8;
        internal TextBox txtDesname;
        internal TextBox txtPath;
        internal TextBox txtPptName;
        internal TextBox txtPptPath;
        internal TextBox txtPubName;
        internal TextBox txtPubPath;
        internal TextBox txtSlidePage;
        internal TextBox txtSrcname;
        internal TextBox txtTo;
        internal TextBox txtUnit;
        internal TextBox txtUnitID;

        // Methods
        public frmPPT()
        {
            this.InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //new clsPublisher().CreateWordByPublisher(this.txtPubName.Text, this.txtPubPath.Text);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            new clsPpt().CreateComputerEnglish("", this.txtUnit.Text.Trim());
        }

        private void button11_Click(object sender, EventArgs e)
        {
            new clsPpt().CreateCommerceEnglish(this.txtUnitID.Text.Trim());
        }

        private void button12_Click(object sender, EventArgs e)
        {
            new clsPpt().AddMediaButton(this.txtUnitID.Text.Trim());
        }

        private void button13_Click(object sender, EventArgs e)
        {
        }

        private void button14_Click(object sender, EventArgs e)
        {
            new clsPpt().CreateNewCommonEnglish(this.txtUnitID.Text.Trim());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            clsPpt ppt = new clsPpt();
            string strPpt = this.txtPptPath.Text + @"\ppt\unit";
            string commonImagePath = this.txtPptPath.Text + @"\commonImages\";
            ppt.ChangeBackPicture(strPpt, commonImagePath, int.Parse(this.txtTo.Text));
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            XmlTextWriter writer = null;
            StringWriter w = new StringWriter();
            writer = new XmlTextWriter(w)
            {
                Formatting = Formatting.Indented,
                Indentation = 4
            };
            writer.WriteComment("sample XML fragment");
            writer.WriteStartElement("book");
            writer.WriteAttributeString("xmlns", "bk", null, "urn:samples");
            writer.WriteAttributeString("genre", "novel");
            writer.WriteStartElement("title");
            writer.WriteString("The Handmaid's Tale");
            writer.WriteEndElement();
            writer.WriteElementString("price", "19.95");
            string prefix = writer.LookupPrefix("urn:samples");
            writer.WriteStartElement(prefix, "ISBN", "urn:samples");
            writer.WriteString("1-861003-78");
            writer.WriteEndElement();
            writer.WriteElementString("style", "urn:samples", "hardcover");
            writer.WriteEndElement();
            MessageBox.Show(w.ToString());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new common().changeName(this.txtPath.Text, this.txtSrcname.Text, this.txtDesname.Text);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new clsPpt().AddAnimate(this.txtPptName.Text, this.txtSlidePage.Text.Split(new char[] { ' ' }));
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //new clsPublisher().CreateWordByPub(this.txtPubName.Text, this.txtPubName.Text.Replace("pub", "doc"));
        }

        private void button6_Click(object sender, EventArgs e)
        {
            new clsPpt().deleControl(this.txtPptName.Text);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //MainClass.Main();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //new clsPublisher().ChangeFontOfPub(this.txtPubName.Text.Trim());
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            new clsPpt().StartUpAutoCreatePowerPointJCH("", this.txtUnit.Text.Trim());
        }

      

        private void frm_keyPress(string s)
        {
            MessageBox.Show(s);
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(frmPPT));
            this.Button9 = new Button();
            this.txtUnit = new TextBox();
            this.Label6 = new Label();
            this.groupBox1 = new GroupBox();
            this.button8 = new Button();
            this.button5 = new Button();
            this.txtPubName = new TextBox();
            this.label2 = new Label();
            this.button1 = new Button();
            this.txtPubPath = new TextBox();
            this.label1 = new Label();
            this.cmd = new Button();
            this.button3 = new Button();
            this.txtPath = new TextBox();
            this.txtSrcname = new TextBox();
            this.txtDesname = new TextBox();
            this.groupBox2 = new GroupBox();
            this.button14 = new Button();
            this.button12 = new Button();
            this.txtUnitID = new TextBox();
            this.label8 = new Label();
            this.button11 = new Button();
            this.txtTo = new TextBox();
            this.label3 = new Label();
            this.txtPptPath = new TextBox();
            this.label4 = new Label();
            this.groupBox3 = new GroupBox();
            this.button6 = new Button();
            this.label5 = new Label();
            this.txtSlidePage = new TextBox();
            this.button4 = new Button();
            this.txtPptName = new TextBox();
            this.label7 = new Label();
            this.button2 = new Button();
            this.button7 = new Button();
            this.button10 = new Button();
            this.button13 = new Button();
            this.comboBox1 = new ComboBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            base.SuspendLayout();
            this.Button9.FlatStyle = FlatStyle.Popup;
            this.Button9.Location = new Point(0x58, 0x22);
            this.Button9.Name = "Button9";
            this.Button9.Size = new Size(0x68, 0x18);
            this.Button9.TabIndex = 0x26;
            this.Button9.Text = "中职英语基础版";
            this.Button9.Click += new EventHandler(this.Button9_Click);
            this.txtUnit.Location = new Point(0x30, 0x24);
            this.txtUnit.Name = "txtUnit";
            this.txtUnit.Size = new Size(0x20, 0x15);
            this.txtUnit.TabIndex = 0x25;
            this.txtUnit.Text = "01";
            this.Label6.Location = new Point(0x10, 0x26);
            this.Label6.Name = "Label6";
            this.Label6.Size = new Size(0x20, 0x10);
            this.Label6.TabIndex = 0x24;
            this.Label6.Text = "单元";
            this.groupBox1.Controls.Add(this.button8);
            this.groupBox1.Controls.Add(this.button5);
            this.groupBox1.Controls.Add(this.txtPubName);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.txtPubPath);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new Point(0, 0x38);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x1d8, 80);
            this.groupBox1.TabIndex = 0x2c;
            this.groupBox1.TabStop = false;
            this.button8.FlatStyle = FlatStyle.Popup;
            this.button8.Location = new Point(0xf8, 0x10);
            this.button8.Name = "button8";
            this.button8.Size = new Size(80, 0x18);
            this.button8.TabIndex = 50;
            this.button8.Text = "ChangeFont";
            this.button8.Click += new EventHandler(this.button8_Click);
            this.button5.FlatStyle = FlatStyle.Popup;
            this.button5.Location = new Point(360, 0x10);
            this.button5.Name = "button5";
            this.button5.Size = new Size(0x68, 0x18);
            this.button5.TabIndex = 0x31;
            this.button5.Text = "WordByPub";
            this.button5.Click += new EventHandler(this.button5_Click);
            this.txtPubName.Location = new Point(80, 0x10);
            this.txtPubName.Name = "txtPubName";
            this.txtPubName.Size = new Size(0x90, 0x15);
            this.txtPubName.TabIndex = 0x30;
            this.txtPubName.Text = @"D:\我的共享\chb.pub";
            this.label2.Location = new Point(8, 0x10);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x48, 0x10);
            this.label2.TabIndex = 0x2f;
            this.label2.Text = "出版物名称";
            this.button1.FlatStyle = FlatStyle.Popup;
            this.button1.Location = new Point(360, 0x30);
            this.button1.Name = "button1";
            this.button1.Size = new Size(0x68, 0x18);
            this.button1.TabIndex = 0x2e;
            this.button1.Text = "出版物生成文档";
            this.button1.Click += new EventHandler(this.button1_Click);
            this.txtPubPath.Location = new Point(80, 0x30);
            this.txtPubPath.Name = "txtPubPath";
            this.txtPubPath.Size = new Size(0x108, 0x15);
            this.txtPubPath.TabIndex = 0x2d;
            this.txtPubPath.Text = @"D:\我的共享\中职英语教材";
            this.label1.Location = new Point(8, 0x30);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x48, 0x10);
            this.label1.TabIndex = 0x2c;
            this.label1.Text = "出版物路径";
            this.cmd.FlatStyle = FlatStyle.Popup;
            this.cmd.Location = new Point(0x178, 0x20);
            this.cmd.Name = "cmd";
            this.cmd.Size = new Size(0x58, 0x18);
            this.cmd.TabIndex = 0x2d;
            this.cmd.Text = "更改背景图";
            this.cmd.Click += new EventHandler(this.button2_Click);
            this.button3.FlatStyle = FlatStyle.Popup;
            this.button3.Location = new Point(0x18b, 140);
            this.button3.Name = "button3";
            this.button3.Size = new Size(0x51, 0x18);
            this.button3.TabIndex = 0x2f;
            this.button3.Text = "更改文件名";
            this.button3.Click += new EventHandler(this.button3_Click);
            this.txtPath.Location = new Point(2, 140);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new Size(0xa8, 0x15);
            this.txtPath.TabIndex = 0x2e;
            this.txtPath.Text = @"D:\我的共享\计算机网络PPT";
            this.txtSrcname.Location = new Point(0xae, 140);
            this.txtSrcname.Name = "txtSrcname";
            this.txtSrcname.Size = new Size(0x70, 0x15);
            this.txtSrcname.TabIndex = 0x30;
            this.txtSrcname.Text = "计算机网络教案";
            this.txtDesname.Location = new Point(290, 140);
            this.txtDesname.Name = "txtDesname";
            this.txtDesname.Size = new Size(0x68, 0x15);
            this.txtDesname.TabIndex = 0x31;
            this.txtDesname.Text = "网络教案";
            this.groupBox2.Controls.Add(this.button14);
            this.groupBox2.Controls.Add(this.button12);
            this.groupBox2.Controls.Add(this.txtUnitID);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.button11);
            this.groupBox2.Location = new Point(480, 0x10);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0xe0, 0x60);
            this.groupBox2.TabIndex = 50;
            this.groupBox2.TabStop = false;
            this.button14.Location = new Point(0x98, 0x38);
            this.button14.Name = "button14";
            this.button14.Size = new Size(0x40, 0x18);
            this.button14.TabIndex = 0x3b;
            this.button14.Text = "新通用";
            this.button14.Click += new EventHandler(this.button14_Click);
            this.button12.FlatStyle = FlatStyle.Flat;
            this.button12.Location = new Point(8, 0x38);
            this.button12.Name = "button12";
            this.button12.Size = new Size(0x88, 0x18);
            this.button12.TabIndex = 0x2a;
            this.button12.Text = "中职/商务加播放控件";
            this.button12.Click += new EventHandler(this.button12_Click);
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
            this.button11.FlatStyle = FlatStyle.Popup;
            this.button11.Location = new Point(80, 0x18);
            this.button11.Name = "button11";
            this.button11.Size = new Size(0x68, 0x18);
            this.button11.TabIndex = 0x29;
            this.button11.Text = "商务英语";
            this.button11.Click += new EventHandler(this.button11_Click);
            this.txtTo.Location = new Point(0x158, 0x23);
            this.txtTo.Name = "txtTo";
            this.txtTo.Size = new Size(0x18, 0x15);
            this.txtTo.TabIndex = 0x33;
            this.txtTo.Text = "14";
            this.label3.Location = new Point(0x120, 40);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x30, 0x10);
            this.label3.TabIndex = 0x34;
            this.label3.Text = "To Unit";
            this.txtPptPath.Location = new Point(0x30, 8);
            this.txtPptPath.Name = "txtPptPath";
            this.txtPptPath.Size = new Size(0x188, 0x15);
            this.txtPptPath.TabIndex = 0x35;
            this.txtPptPath.Text = @"D:\我的共享\以前的PPT\宾馆英语电子教案";
            this.label4.Location = new Point(0x10, 8);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x20, 0x10);
            this.label4.TabIndex = 0x36;
            this.label4.Text = "路径";
            this.groupBox3.Controls.Add(this.button6);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.txtSlidePage);
            this.groupBox3.Controls.Add(this.button4);
            this.groupBox3.Controls.Add(this.txtPptName);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Location = new Point(8, 0xa8);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new Size(0x1d8, 80);
            this.groupBox3.TabIndex = 0x37;
            this.groupBox3.TabStop = false;
            this.button6.FlatStyle = FlatStyle.Popup;
            this.button6.Location = new Point(0x170, 0x30);
            this.button6.Name = "button6";
            this.button6.Size = new Size(0x60, 0x18);
            this.button6.TabIndex = 0x31;
            this.button6.Text = "deleteControl";
            this.button6.Click += new EventHandler(this.button6_Click);
            this.label5.Location = new Point(8, 0x30);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x30, 0x10);
            this.label5.TabIndex = 0x30;
            this.label5.Text = "索引页";
            this.txtSlidePage.Location = new Point(0x3e, 0x30);
            this.txtSlidePage.Name = "txtSlidePage";
            this.txtSlidePage.Size = new Size(0x128, 0x15);
            this.txtSlidePage.TabIndex = 0x2f;
            this.txtSlidePage.Text = "5 6";
            this.button4.FlatStyle = FlatStyle.Popup;
            this.button4.Location = new Point(0x170, 0x10);
            this.button4.Name = "button4";
            this.button4.Size = new Size(0x60, 0x18);
            this.button4.TabIndex = 0x2e;
            this.button4.Text = "effect";
            this.button4.Click += new EventHandler(this.button4_Click);
            this.txtPptName.Location = new Point(0x3e, 0x10);
            this.txtPptName.Name = "txtPptName";
            this.txtPptName.Size = new Size(0x128, 0x15);
            this.txtPptName.TabIndex = 0x2d;
            this.txtPptName.Text = @"D:\我的共享\unit04.ppt";
            this.label7.Location = new Point(8, 0x10);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x38, 0x10);
            this.label7.TabIndex = 0x2c;
            this.label7.Text = "电子教案";
            this.button2.BackColor = SystemColors.Control;
            this.button2.FlatStyle = FlatStyle.Flat;
            this.button2.Location = new Point(0x1e8, 160);
            this.button2.Name = "button2";
            this.button2.Size = new Size(80, 0x18);
            this.button2.TabIndex = 0x38;
            this.button2.Text = "XmlTextWritertest";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new EventHandler(this.button2_Click_1);
            this.button7.BackColor = SystemColors.Control;
            this.button7.FlatStyle = FlatStyle.Flat;
            this.button7.Location = new Point(0x240, 160);
            this.button7.Name = "button7";
            this.button7.Size = new Size(80, 0x18);
            this.button7.TabIndex = 0x39;
            this.button7.Text = "delegate";
            this.button7.UseVisualStyleBackColor = false;
            this.button7.Click += new EventHandler(this.button7_Click);
            this.button10.FlatStyle = FlatStyle.Popup;
            this.button10.Location = new Point(200, 0x20);
            this.button10.Name = "button10";
            this.button10.Size = new Size(80, 0x17);
            this.button10.TabIndex = 0x26;
            this.button10.Text = "ComputerEng";
            this.button10.Click += new EventHandler(this.button10_Click);
            this.button13.Location = new Point(0x1f0, 200);
            this.button13.Name = "button13";
            this.button13.Size = new Size(0x40, 0x18);
            this.button13.TabIndex = 0x3a;
            this.button13.Text = "enum";
            this.button13.Click += new EventHandler(this.button13_Click);
            this.comboBox1.Location = new Point(0x128, 0x110);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new Size(0xb0, 20);
            this.comboBox1.TabIndex = 0x3b;
            this.comboBox1.Text = "comboBox1";
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(0x2c0, 0x15d);
            base.Controls.Add(this.comboBox1);
            base.Controls.Add(this.button13);
            base.Controls.Add(this.button7);
            base.Controls.Add(this.button2);
            base.Controls.Add(this.groupBox3);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.txtPptPath);
            base.Controls.Add(this.txtTo);
            base.Controls.Add(this.txtDesname);
            base.Controls.Add(this.txtSrcname);
            base.Controls.Add(this.txtPath);
            base.Controls.Add(this.txtUnit);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.button3);
            base.Controls.Add(this.cmd);
            base.Controls.Add(this.Label6);
            base.Controls.Add(this.Button9);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.button10);
            //base.Icon = (Icon)manager.GetObject("$this.Icon");
            base.Name = "frmPPT";
            this.Text = "自动生成演示文稿";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        // Nested Types
        [Flags]
        private enum Days
        {
            Mon = 4,
            Sat = 1,
            Sun = 2
        }

    }
}
