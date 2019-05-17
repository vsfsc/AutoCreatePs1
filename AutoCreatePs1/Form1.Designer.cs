namespace AutoCreatePs1
{
    partial class Form1
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
            this.btnEnum = new System.Windows.Forms.Button();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnFollow = new System.Windows.Forms.Button();
            this.lbMsg = new System.Windows.Forms.Label();
            this.txtMsg = new System.Windows.Forms.TextBox();
            this.btnStopFollow = new System.Windows.Forms.Button();
            this.txtOldName = new System.Windows.Forms.TextBox();
            this.txtNewName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnTest = new System.Windows.Forms.Button();
            this.btnEnumerate = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnMoveOU = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSubWeb = new System.Windows.Forms.TextBox();
            this.txtListName = new System.Windows.Forms.TextBox();
            this.txtNum = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnFlag = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.txtFields = new System.Windows.Forms.TextBox();
            this.btnXueHao = new System.Windows.Forms.Button();
            this.btnTotal = new System.Windows.Forms.Button();
            this.btnScore = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.txtApproveT = new System.Windows.Forms.TextBox();
            this.txtScoreField = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.btnCreateSubTable = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.txtSubT = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtMtable = new System.Windows.Forms.TextBox();
            this.btnTestCode = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnWrite = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.txtAction = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.btnChangeFied = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.btnSendEmail = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.button13 = new System.Windows.Forms.Button();
            this.btnQueryOrder = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnEnum
            // 
            this.btnEnum.Location = new System.Drawing.Point(610, 11);
            this.btnEnum.Name = "btnEnum";
            this.btnEnum.Size = new System.Drawing.Size(75, 23);
            this.btnEnum.TabIndex = 7;
            this.btnEnum.Text = "关注的网站";
            this.btnEnum.UseVisualStyleBackColor = true;
            this.btnEnum.Click += new System.EventHandler(this.btnEnum_Click);
            // 
            // txtResult
            // 
            this.txtResult.Location = new System.Drawing.Point(25, 43);
            this.txtResult.Multiline = true;
            this.txtResult.Name = "txtResult";
            this.txtResult.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtResult.Size = new System.Drawing.Size(365, 273);
            this.txtResult.TabIndex = 6;
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(70, 13);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(108, 21);
            this.txtUser.TabIndex = 5;
            this.txtUser.Text = "xueqingxia";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "用户名";
            // 
            // btnFollow
            // 
            this.btnFollow.Location = new System.Drawing.Point(785, 11);
            this.btnFollow.Name = "btnFollow";
            this.btnFollow.Size = new System.Drawing.Size(75, 23);
            this.btnFollow.TabIndex = 8;
            this.btnFollow.Text = "重新关注";
            this.btnFollow.UseVisualStyleBackColor = true;
            this.btnFollow.Click += new System.EventHandler(this.btnFollow_Click);
            // 
            // lbMsg
            // 
            this.lbMsg.AutoSize = true;
            this.lbMsg.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Document, ((byte)(134)));
            this.lbMsg.ForeColor = System.Drawing.Color.Red;
            this.lbMsg.Location = new System.Drawing.Point(23, 391);
            this.lbMsg.Name = "lbMsg";
            this.lbMsg.Size = new System.Drawing.Size(0, 5);
            this.lbMsg.TabIndex = 9;
            // 
            // txtMsg
            // 
            this.txtMsg.Location = new System.Drawing.Point(396, 43);
            this.txtMsg.Multiline = true;
            this.txtMsg.Name = "txtMsg";
            this.txtMsg.Size = new System.Drawing.Size(464, 273);
            this.txtMsg.TabIndex = 10;
            // 
            // btnStopFollow
            // 
            this.btnStopFollow.Location = new System.Drawing.Point(693, 11);
            this.btnStopFollow.Name = "btnStopFollow";
            this.btnStopFollow.Size = new System.Drawing.Size(75, 23);
            this.btnStopFollow.TabIndex = 11;
            this.btnStopFollow.Text = "取消关注";
            this.btnStopFollow.UseVisualStyleBackColor = true;
            this.btnStopFollow.Click += new System.EventHandler(this.btnStopFollow_Click);
            // 
            // txtOldName
            // 
            this.txtOldName.Location = new System.Drawing.Point(442, 13);
            this.txtOldName.Name = "txtOldName";
            this.txtOldName.Size = new System.Drawing.Size(132, 21);
            this.txtOldName.TabIndex = 12;
            this.txtOldName.Text = "http://ccc-va-sp";
            // 
            // txtNewName
            // 
            this.txtNewName.Location = new System.Drawing.Point(242, 13);
            this.txtNewName.Name = "txtNewName";
            this.txtNewName.Size = new System.Drawing.Size(123, 21);
            this.txtNewName.TabIndex = 13;
            this.txtNewName.Text = "http://neu-va";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(183, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 14;
            this.label2.Text = "新的网址";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(371, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 15;
            this.label3.Text = "旧的网址";
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(883, 116);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(75, 23);
            this.btnTest.TabIndex = 16;
            this.btnTest.Text = "测试查询";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // btnEnumerate
            // 
            this.btnEnumerate.Location = new System.Drawing.Point(488, 467);
            this.btnEnumerate.Name = "btnEnumerate";
            this.btnEnumerate.Size = new System.Drawing.Size(93, 23);
            this.btnEnumerate.TabIndex = 17;
            this.btnEnumerate.Text = "遍历学生名单";
            this.btnEnumerate.UseVisualStyleBackColor = true;
            this.btnEnumerate.Click += new System.EventHandler(this.btnEnumerate_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "txt|xlsx";
            this.openFileDialog1.FileName = "学生名单";
            // 
            // txtFilePath
            // 
            this.txtFilePath.Location = new System.Drawing.Point(32, 469);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(392, 21);
            this.txtFilePath.TabIndex = 18;
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(430, 469);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(52, 23);
            this.btnOpen.TabIndex = 19;
            this.btnOpen.Text = "open";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnMoveOU
            // 
            this.btnMoveOU.Location = new System.Drawing.Point(488, 507);
            this.btnMoveOU.Name = "btnMoveOU";
            this.btnMoveOU.Size = new System.Drawing.Size(93, 23);
            this.btnMoveOU.TabIndex = 20;
            this.btnMoveOU.Text = "移动OU";
            this.btnMoveOU.UseVisualStyleBackColor = true;
            this.btnMoveOU.Click += new System.EventHandler(this.btnMoveOU_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label4.Location = new System.Drawing.Point(164, 512);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(305, 12);
            this.label4.TabIndex = 21;
            this.label4.Text = "从文本文件中读取学生名单，从原来的OU移到所在的班级";
            // 
            // txtSubWeb
            // 
            this.txtSubWeb.Location = new System.Drawing.Point(95, 332);
            this.txtSubWeb.Name = "txtSubWeb";
            this.txtSubWeb.Size = new System.Drawing.Size(116, 21);
            this.txtSubWeb.TabIndex = 22;
            this.txtSubWeb.Text = "blog";
            // 
            // txtListName
            // 
            this.txtListName.Location = new System.Drawing.Point(290, 332);
            this.txtListName.Name = "txtListName";
            this.txtListName.Size = new System.Drawing.Size(125, 21);
            this.txtListName.TabIndex = 23;
            this.txtListName.Text = "教材与专著";
            // 
            // txtNum
            // 
            this.txtNum.Location = new System.Drawing.Point(524, 332);
            this.txtNum.Name = "txtNum";
            this.txtNum.Size = new System.Drawing.Size(57, 21);
            this.txtNum.TabIndex = 24;
            this.txtNum.Text = "200";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(227, 335);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 25;
            this.label5.Text = "列表名";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(27, 332);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 26;
            this.label6.Text = "子网站";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(434, 335);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 12);
            this.label7.TabIndex = 27;
            this.label7.Text = "创意内容字数";
            // 
            // btnFlag
            // 
            this.btnFlag.Location = new System.Drawing.Point(599, 373);
            this.btnFlag.Name = "btnFlag";
            this.btnFlag.Size = new System.Drawing.Size(137, 23);
            this.btnFlag.TabIndex = 28;
            this.btnFlag.Text = "导出列表（设置标记）";
            this.btnFlag.UseVisualStyleBackColor = true;
            this.btnFlag.Click += new System.EventHandler(this.btnFlag_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(27, 362);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 30;
            this.label8.Text = "导出内容";
            // 
            // txtFields
            // 
            this.txtFields.Location = new System.Drawing.Point(95, 359);
            this.txtFields.Multiline = true;
            this.txtFields.Name = "txtFields";
            this.txtFields.Size = new System.Drawing.Size(486, 37);
            this.txtFields.TabIndex = 29;
            this.txtFields.Text = "创意名称；学号/工号；单位名称；电子邮箱；联系电话；涉及领域";
            // 
            // btnXueHao
            // 
            this.btnXueHao.Location = new System.Drawing.Point(92, 408);
            this.btnXueHao.Name = "btnXueHao";
            this.btnXueHao.Size = new System.Drawing.Size(119, 23);
            this.btnXueHao.TabIndex = 31;
            this.btnXueHao.Text = "过滤重复学号";
            this.btnXueHao.UseVisualStyleBackColor = true;
            this.btnXueHao.Click += new System.EventHandler(this.btnXueHao_Click);
            // 
            // btnTotal
            // 
            this.btnTotal.Location = new System.Drawing.Point(604, 332);
            this.btnTotal.Name = "btnTotal";
            this.btnTotal.Size = new System.Drawing.Size(86, 23);
            this.btnTotal.TabIndex = 32;
            this.btnTotal.Text = "统计字数";
            this.btnTotal.UseVisualStyleBackColor = true;
            this.btnTotal.Click += new System.EventHandler(this.btnTotal_Click);
            // 
            // btnScore
            // 
            this.btnScore.Location = new System.Drawing.Point(739, 443);
            this.btnScore.Name = "btnScore";
            this.btnScore.Size = new System.Drawing.Size(119, 23);
            this.btnScore.TabIndex = 33;
            this.btnScore.Text = "统计分数";
            this.btnScore.UseVisualStyleBackColor = true;
            this.btnScore.Click += new System.EventHandler(this.btnScore_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(501, 411);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 12);
            this.label9.TabIndex = 35;
            this.label9.Text = "评分表";
            // 
            // txtApproveT
            // 
            this.txtApproveT.Location = new System.Drawing.Point(548, 408);
            this.txtApproveT.Name = "txtApproveT";
            this.txtApproveT.Size = new System.Drawing.Size(61, 21);
            this.txtApproveT.TabIndex = 34;
            this.txtApproveT.Text = "创意评审";
            // 
            // txtScoreField
            // 
            this.txtScoreField.Location = new System.Drawing.Point(671, 408);
            this.txtScoreField.Name = "txtScoreField";
            this.txtScoreField.Size = new System.Drawing.Size(61, 21);
            this.txtScoreField.TabIndex = 36;
            this.txtScoreField.Text = "评分";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(615, 413);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 12);
            this.label10.TabIndex = 37;
            this.label10.Text = "评分字段";
            // 
            // btnCreateSubTable
            // 
            this.btnCreateSubTable.Location = new System.Drawing.Point(883, 73);
            this.btnCreateSubTable.Name = "btnCreateSubTable";
            this.btnCreateSubTable.Size = new System.Drawing.Size(84, 23);
            this.btnCreateSubTable.TabIndex = 38;
            this.btnCreateSubTable.Text = "生成子表";
            this.btnCreateSubTable.UseVisualStyleBackColor = true;
            this.btnCreateSubTable.Click += new System.EventHandler(this.btnCreateSubTable_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(996, 48);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(29, 12);
            this.label11.TabIndex = 42;
            this.label11.Text = "子表";
            // 
            // txtSubT
            // 
            this.txtSubT.Location = new System.Drawing.Point(1052, 43);
            this.txtSubT.Name = "txtSubT";
            this.txtSubT.Size = new System.Drawing.Size(61, 21);
            this.txtSubT.TabIndex = 41;
            this.txtSubT.Text = "论文业绩";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(882, 46);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(29, 12);
            this.label12.TabIndex = 40;
            this.label12.Text = "主表";
            // 
            // txtMtable
            // 
            this.txtMtable.Location = new System.Drawing.Point(929, 43);
            this.txtMtable.Name = "txtMtable";
            this.txtMtable.Size = new System.Drawing.Size(61, 21);
            this.txtMtable.TabIndex = 39;
            this.txtMtable.Text = "论文";
            // 
            // btnTestCode
            // 
            this.btnTestCode.Location = new System.Drawing.Point(973, 116);
            this.btnTestCode.Name = "btnTestCode";
            this.btnTestCode.Size = new System.Drawing.Size(80, 23);
            this.btnTestCode.TabIndex = 43;
            this.btnTestCode.Text = "测试";
            this.btnTestCode.UseVisualStyleBackColor = true;
            this.btnTestCode.Click += new System.EventHandler(this.btnTestCode_Click_1);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(219, 408);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(104, 23);
            this.button1.TabIndex = 44;
            this.button1.Text = "分组";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnWrite
            // 
            this.btnWrite.Location = new System.Drawing.Point(339, 406);
            this.btnWrite.Name = "btnWrite";
            this.btnWrite.Size = new System.Drawing.Size(119, 23);
            this.btnWrite.TabIndex = 45;
            this.btnWrite.Text = "分组写入评分表";
            this.btnWrite.UseVisualStyleBackColor = true;
            this.btnWrite.Click += new System.EventHandler(this.btnWrite_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(738, 413);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(53, 12);
            this.label13.TabIndex = 47;
            this.label13.Text = "评分阶段";
            // 
            // txtAction
            // 
            this.txtAction.Location = new System.Drawing.Point(797, 410);
            this.txtAction.Name = "txtAction";
            this.txtAction.Size = new System.Drawing.Size(61, 21);
            this.txtAction.TabIndex = 46;
            this.txtAction.Text = "初审";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1068, 116);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(80, 23);
            this.button2.TabIndex = 43;
            this.button2.Text = "测试随机数";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(739, 332);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(119, 23);
            this.button3.TabIndex = 48;
            this.button3.Text = "更新创意评审时间";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnChangeFied
            // 
            this.btnChangeFied.Location = new System.Drawing.Point(973, 73);
            this.btnChangeFied.Name = "btnChangeFied";
            this.btnChangeFied.Size = new System.Drawing.Size(119, 23);
            this.btnChangeFied.TabIndex = 38;
            this.btnChangeFied.Text = "更改作者字段";
            this.btnChangeFied.UseVisualStyleBackColor = true;
            this.btnChangeFied.Click += new System.EventHandler(this.btnChangeFied_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(1098, 73);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(98, 23);
            this.button4.TabIndex = 49;
            this.button4.Text = "更改论文创建者";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(682, 575);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(119, 23);
            this.button5.TabIndex = 50;
            this.button5.Text = "论文对导";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(739, 486);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(119, 23);
            this.button6.TabIndex = 51;
            this.button6.Text = "更新分数";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(373, 575);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(183, 23);
            this.button7.TabIndex = 52;
            this.button7.Text = "筛选入围者";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(70, 559);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(80, 23);
            this.button8.TabIndex = 53;
            this.button8.Text = "遍历字段";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(599, 508);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(119, 23);
            this.button9.TabIndex = 51;
            this.button9.Text = "徽信投票数";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(815, 559);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(75, 23);
            this.button10.TabIndex = 54;
            this.button10.Text = "dcoProp";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(599, 479);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(119, 23);
            this.button11.TabIndex = 55;
            this.button11.Text = "二等奖";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // btnSendEmail
            // 
            this.btnSendEmail.Location = new System.Drawing.Point(929, 221);
            this.btnSendEmail.Name = "btnSendEmail";
            this.btnSendEmail.Size = new System.Drawing.Size(119, 23);
            this.btnSendEmail.TabIndex = 55;
            this.btnSendEmail.Text = "发邮件";
            this.btnSendEmail.UseVisualStyleBackColor = true;
            this.btnSendEmail.Click += new System.EventHandler(this.btnSendEmail_Click);
            // 
            // button12
            // 
            this.button12.Location = new System.Drawing.Point(934, 486);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(119, 23);
            this.button12.TabIndex = 55;
            this.button12.Text = "一等奖";
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new System.EventHandler(this.button12_Click);
            // 
            // button13
            // 
            this.button13.Location = new System.Drawing.Point(929, 173);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(134, 23);
            this.button13.TabIndex = 43;
            this.button13.Text = "字段加到文档库";
            this.button13.UseVisualStyleBackColor = true;
            this.button13.Click += new System.EventHandler(this.btnTestCode_Click);
            // 
            // btnQueryOrder
            // 
            this.btnQueryOrder.Location = new System.Drawing.Point(929, 263);
            this.btnQueryOrder.Name = "btnQueryOrder";
            this.btnQueryOrder.Size = new System.Drawing.Size(108, 29);
            this.btnQueryOrder.TabIndex = 56;
            this.btnQueryOrder.Text = "btnQueryOrder";
            this.btnQueryOrder.UseVisualStyleBackColor = true;
            this.btnQueryOrder.Click += new System.EventHandler(this.btnQueryOrder_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1201, 610);
            this.Controls.Add(this.btnQueryOrder);
            this.Controls.Add(this.btnSendEmail);
            this.Controls.Add(this.button12);
            this.Controls.Add(this.button11);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.txtAction);
            this.Controls.Add(this.btnWrite);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button13);
            this.Controls.Add(this.btnTestCode);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtSubT);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtMtable);
            this.Controls.Add(this.btnChangeFied);
            this.Controls.Add(this.btnCreateSubTable);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtScoreField);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtApproveT);
            this.Controls.Add(this.btnScore);
            this.Controls.Add(this.btnTotal);
            this.Controls.Add(this.btnXueHao);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtFields);
            this.Controls.Add(this.btnFlag);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtNum);
            this.Controls.Add(this.txtListName);
            this.Controls.Add(this.txtSubWeb);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnMoveOU);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.txtFilePath);
            this.Controls.Add(this.btnEnumerate);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtNewName);
            this.Controls.Add(this.txtOldName);
            this.Controls.Add(this.btnStopFollow);
            this.Controls.Add(this.txtMsg);
            this.Controls.Add(this.lbMsg);
            this.Controls.Add(this.btnFollow);
            this.Controls.Add(this.btnEnum);
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.txtUser);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "关注网站";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        // Fields
        private System.Windows.Forms.Button btnChangeFied;
        private System.Windows.Forms.Button btnCreateSubTable;
        private System.Windows.Forms.Button btnEnum;
        private System.Windows.Forms.Button btnEnumerate;
        private System.Windows.Forms.Button btnFlag;
        private System.Windows.Forms.Button btnFollow;
        private System.Windows.Forms.Button btnMoveOU;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button btnScore;
        private System.Windows.Forms.Button btnSendEmail;
        private System.Windows.Forms.Button btnStopFollow;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Button btnTestCode;
        private System.Windows.Forms.Button btnTotal;
        private System.Windows.Forms.Button btnWrite;
        private System.Windows.Forms.Button btnXueHao;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lbMsg;
        private System.Windows.Forms.TextBox txtAction;
        private System.Windows.Forms.TextBox txtApproveT;
        private System.Windows.Forms.TextBox txtFields;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.TextBox txtListName;
        private System.Windows.Forms.TextBox txtMsg;
        private System.Windows.Forms.TextBox txtMtable;
        private System.Windows.Forms.TextBox txtNewName;
        private System.Windows.Forms.TextBox txtNum;
        private System.Windows.Forms.TextBox txtOldName;
        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.TextBox txtScoreField;
        private System.Windows.Forms.TextBox txtSubT;
        private System.Windows.Forms.TextBox txtSubWeb;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.Button button13;
        private System.Windows.Forms.Button btnQueryOrder;
    }
}

