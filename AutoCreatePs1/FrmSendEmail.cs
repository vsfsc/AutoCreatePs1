using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.SharePoint;
using System.Net;
using System.Net.Mail;

namespace AutoCreatePs1
{
    public partial class FrmSendEmail : Form
    {
        public FrmSendEmail()
        {
            InitializeComponent();
            this.txtFrom.Text = "smartneu@mail.neu.edu.cn";
            this.txtFromShowName.Text = "智慧东大竞赛委员会";
            this.txtPwd.Text = "ccc.neu.edu.cn";
            this.txtSubject.Text = "智慧东大创意大赛领奖通知";// "智慧东大创意大赛决赛入围通知";
            //this.txtBody.Text = " 亲爱的/同学，恭喜你入围智慧东大决赛，请你准备好现场答辩的PPT，于12月18日前发到 邮箱wudx@mail.neu.edu.cn、hwz@mail.neu.edu.cn . 过期未发者，自动降为三等奖。答辩时间暂定在下周一（12月25日）。对参加答辩有困难请说明";
            //this.txtBody.Text = "  亲爱的同学，恭喜你在智慧东大创意大赛中获奖，请尚未领奖的同学在两日内（上午：8:30到12:00；下午：14:00到17:00）尽快到南湖校区计算中心305领奖";
            this.txtBody.Text = "  各位老师，恭喜您在智慧东大创意大赛中获奖，请尚未领奖的老师尽快到南湖校区计算中心305领奖";
            this.txtSmtpServer.Text = "smtp.neu.edu.cn";

        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnSendEmail_Click(object sender, EventArgs e)
        {
            string fromEmail = this.txtFrom.Text;
            string fromDisplayName = this.txtFromShowName.Text;
            string pwd = this.txtPwd.Text;
            string toSubject = this.txtSubject.Text;
            string toBody = this.txtBody.Text;
            string emails = this.txtTos.Text.Replace("；", ";").Trim().TrimEnd(new char[] { ';' });
            emails = emails.Replace(" ", "");
            emails = emails.Replace("\r\n","");
            emails = emails.Replace("\n", "");
            string host = this.txtSmtpServer.Text;
            if (emails == "")
            {
                MessageBox.Show("收件人不能为空!");
            }
            else
            {
                StringBuilder txt = new StringBuilder();

                string[] toEmails = emails.Split(new char[] { ';' });
                foreach (string email in toEmails)
                {
                    //this.SendMail(fromEmail, fromDisplayName, pwd, new string[] { email }, toSubject, toBody, host);
                    bool result = this.SendMail(fromEmail, fromDisplayName, pwd, new string[] { email }, toSubject, toBody, host);
                    txt.AppendLine(email + " " + (result ? "成功" : "失败"));

                }
                this.txtTos.Text = txt.ToString();
                MessageBox.Show("共发送邮件 " + toEmails.Count ());

                MessageBox.Show("ok!");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SPSite site = new SPSite("http://localhost");
            SPWeb myWeb = site.AllWebs[this.txtSubWeb.Text.Trim()];
            SPList myList = myWeb.Lists[this.txtListName.Text.Trim()];
            string fldEmail = this.txtEmail.Text;
            string fldName = this.txtFldName.Text;
            string fldValue = this.txtFldValue.Text;
            SPQuery oQuery = new SPQuery();
            string fldInterlName = "";
            foreach (SPField fld in myList.Fields)
            {
                if (fld.Title == fldName)
                {
                    fldInterlName = fld.InternalName;
                    break;
                }
            }
            oQuery.Query = "<Where><Eq><FieldRef Name='" + fldInterlName + "'/><Value Type='Number'>" + fldValue + "</Value></Eq></Where>";
            string fromEmail = this.txtFrom.Text;
            string fromDisplayName = this.txtFromShowName.Text;
            string pwd = this.txtPwd.Text;
            string toSubject = this.txtSubject.Text;
            string toBody = this.txtBody.Text;
            string emails = this.txtTos.Text.Replace("；", ";").Trim().TrimEnd(new char[] { ';' });
            string host = this.txtSmtpServer.Text;
            SPListItemCollection collListItems = myList.GetItems(oQuery);
            StringBuilder txt = new StringBuilder();
            foreach (SPListItem item in collListItems)
            {
                if (item[fldEmail] != null)
                {
                    string email = item[fldEmail].ToString();
                    bool result = this.SendMail(fromEmail, fromDisplayName, pwd, new string[] { email }, toSubject, toBody, host);
                    txt.AppendLine(email + " " + (result ? "成功" : "失败"));
                }
            }
            this.txtTos.Text = txt.ToString();
            MessageBox.Show("共发送邮件 " + collListItems.Count);
        }
        public bool SendMail(string fromEmail, string fromDisplayName, string pwd, string[] toMail, string toSubject, string toBody, string host)
        {
            MailAddress from = new MailAddress(fromEmail, fromDisplayName);
            MailMessage oMail = new MailMessage
            {
                From = from
            };
            for (int i = 0; i < toMail.Length; i++)
            {
                oMail.To.Add(toMail[i].ToString());
            }
            oMail.Subject = toSubject;
            oMail.Body = toBody;
            oMail.IsBodyHtml = true;
            oMail.BodyEncoding = Encoding.GetEncoding("GB2312");
            SmtpClient client = new SmtpClient
            {
                Host = host,
                Credentials = new NetworkCredential(fromEmail, pwd)
            };
            try
            {
                client.Send(oMail);
                oMail.Dispose();
                return true;
            }
            catch
            {
                oMail.Dispose();
                return false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SPSite site = new SPSite("http://localhost");
            SPWeb myWeb = site.AllWebs[this.txtSubWeb.Text.Trim()];
            SPList myList = myWeb.Lists[this.txtListName.Text.Trim()];
            string fldEmail = this.txtEmail.Text;
            string fldName = this.txtFldName.Text;
            string fldValue = this.txtFldValue.Text;
            SPQuery oQuery = new SPQuery();
         
            oQuery.Query = @"<Where>
      <And>
         <And>
            <Eq>
               <FieldRef Name='Roles' />
               <Value Type='Calculated'>学生</Value>
            </Eq>
            <Neq>
               <FieldRef Name='OrgName' />
               <Value Type='Choice'>外国语学院</Value>
            </Neq>
         </And>
         <Eq>
            <FieldRef Name='_ModerationStatus' />
            <Value Type='ModStat'>已批准</Value>
         </Eq>
      </And>
   </Where>";
            string fromEmail = this.txtFrom.Text;
            string fromDisplayName = this.txtFromShowName.Text;
            string pwd = this.txtPwd.Text;
            string toSubject = this.txtSubject.Text;
            string toBody = this.txtBody.Text;
            string emails = this.txtTos.Text.Replace("；", ";").Trim().TrimEnd(new char[] { ';' });
            string host = this.txtSmtpServer.Text;
            SPListItemCollection collListItems = myList.GetItems(oQuery);
            StringBuilder txt = new StringBuilder();
            foreach (SPListItem item in collListItems)
            {
                if (item[fldEmail] != null)
                {
                    string email = item[fldEmail].ToString();
                    bool result = this.SendMail(fromEmail, fromDisplayName, pwd, new string[] { email }, toSubject, toBody, host);
                    txt.AppendLine(email + " " + (result ? "成功" : "失败"));
                }
            }
            this.txtTos.Text = txt.ToString();
            MessageBox.Show("共发送邮件 " + collListItems.Count);
        }
    }
}
