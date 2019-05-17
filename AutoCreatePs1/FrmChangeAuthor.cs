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

namespace AutoCreatePs1
{
    public partial class FrmChangeAuthor : Form
    {
        public FrmChangeAuthor()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SPSite mySite = new SPSite("http://localhost"))
            {
                SPWeb myWeb = mySite.AllWebs[txtSubWeb.Text];
                if (!myWeb.Exists)
                {
                    MessageBox.Show("子网站不存在");
                    return;
                }
                string[] listItems = txtFields.Text.Replace(";", "；").Split('；');
                SPList myList = myWeb.Lists[txtListName.Text.Trim ()];
                SPUser newUser = myWeb.EnsureUser("ccc\\" + txtNewUser.Text );
                foreach (SPListItem myItem in myList.Items)
                {
                    try
                    {
                        string author = myItem["创建者"].ToString();
                        foreach (string dispName in listItems)
                        {
                            if (author.Contains(dispName))
                            {
                                myItem["创建者"] = newUser;
                                //如果未开启审批，则会报错
                                try
                                {
                                    myItem.ModerationInformation.Status = SPModerationStatusType.Approved;
                                    myItem.ModerationInformation.Comment = "更改创建者";
                                }
                                catch
                                { }
                                myItem.SystemUpdate ();//修改时间不发生变化
                                break;
                            }
                        }
                    }
                    catch  
                    { }

                }

            }
            MessageBox.Show("ok");
        }
        private SPUser CreatedBy(string displayName, SPWeb myWeb)
        {
            foreach (SPUser user1 in myWeb.SiteUsers)
            {
                if (user1.Name == displayName)
                    return user1;
            }
            return null;
        }
    }
}
