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
using Microsoft.SharePoint.Administration;
using System.IO;
using Microsoft.Office.Server;
using Microsoft.Office.Server.UserProfiles;
using System.Web;
using System.Collections;

namespace AutoCreatePs1
{
    public partial class FrmStart : Form
    {
        public FrmStart()
        {
            InitializeComponent();
            InitText();
        }
       /// <summary>
       /// 初始化变量的值 
       /// </summary>
        private void InitText()
        {
            txtSourceCol.Text  = "项目名称；立项单位；项目负责人；参与人员；院内参与人员；年度；相关文档；创建者；创建时间；修改者；修改时间";
            txtDesCol.Text = "项目名称；立项单位；项目负责人；参与人员；院内参与人员；年度；相关文档；创建者；创建时间；修改者；修改时间";
            //"获奖题目；获奖负责人；项目负责人；获奖教师；院内获奖教师；年度；相关文档；创建者；创建时间；修改者；修改时间";//教学获奖
            //"竞赛名称；竞赛负责人；参与人员；院内参与人员；年度；相关文档；创建者；创建时间；修改者；修改时间";//教学竞赛
            txtSiteUrl.Text  = "Performance";//ComputerCompetitive虚拟学院
            txtSourceListName.Text  = "科研立项";
            txtDesListName.Text = "教学立项";// 获奖";// 竞赛";
        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            SPWebApplication webApp = SPWebApplication.Lookup(new Uri("http://localhost"));
            StreamWriter writeMy = new StreamWriter(txtPath.Text + "backupmySite.txt", false);
            writeMy.WriteLine("Add-PsSnapin Microsoft.SharePoint.PowerShell");
            writeMy.WriteLine("$sourcePath=\""+txtBackPath.Text +"\"");
            foreach (SPSite mySite in webApp.Sites)
            {
                string url = mySite.Url;
                if (url.IndexOf("/personal/") > 0)
                {
                    writeMy.WriteLine("Backup-SPSite " + url + " -Path $sourcePath\"" + url.Substring(url.LastIndexOf("/") + 1) + ".bak\" -force");
                }
            }

            writeMy.Close();
            writeMy.Dispose();
            MessageBox.Show("ok");
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            SPWebApplication webApp = SPWebApplication.Lookup(new Uri("http://localhost"));
            StreamWriter writeMy = new StreamWriter(txtPath.Text + "restoremySite.txt", false);
            writeMy.WriteLine("Add-PsSnapin Microsoft.SharePoint.PowerShell");
            writeMy.WriteLine("$dbName=\"WSS_Content\"");
            writeMy.WriteLine("$serverName=\"qx2012\"");
            writeMy.WriteLine("$sourcePath=\"d:\\Backup\\\"");
            string loginName;
            string url;
            foreach (SPSite mySite in webApp.Sites)
            {
                url = mySite.Url;
                if (url.IndexOf("/personal/") > 0)
                {
                    loginName = url.Substring(url.LastIndexOf("/") + 1);
                    //if (chkNew.Checked)
                    //{
                    //    //writeMy.WriteLine("remove-spsite -identity " + url + "-GradualDelete -Confirm:$False");
                    //    writeMy.WriteLine("New-SPSite -URL " + url + " -OwnerAlias ccc\\" + loginName + "  -Confirm:$False");
                    //}
                    writeMy.WriteLine("Restore-SPSite " + url + " -Path $sourcePath'" + loginName + ".bak'  -DatabaseServer $serverName -DatabaseName $dbName -force -Confirm:$False");
                }
            }

            writeMy.Close();
            writeMy.Dispose();
            MessageBox.Show("ok");

        }
        //读创建时间和创建者
        private void btnNotice_Click(object sender, EventArgs e)
        {
            StreamWriter writeMy = new StreamWriter(txtPath.Text + txtListUrl.Text + ".txt", false);
            using (SPSite mySite = new SPSite(txtSiteUrl.Text))
            {
                SPWeb myWeb = mySite.RootWeb;
                SPList myList = myWeb.Lists[txtListUrl.Text];
                foreach (SPListItem myItem in myList.Items)
                {
                    writeMy.WriteLine(myItem.ID + "&" + myItem["创建时间"].ToString() + "&" + myItem["创建者"]);
                }
            }
            writeMy.Close();
            writeMy.Dispose();
            MessageBox.Show("ok");
        }
        private SPUser CreatedBy(string displayName, SPWeb myWeb)
        {
            if (displayName == "焦明海")
            {
                Console.WriteLine("stop");
            }

            foreach (SPUser user1 in myWeb.SiteUsers)
            {
                if (user1.Name == displayName)
                    return user1;
            }
            return null;
        }
        //ImportWeb创建者和创建时间变成了当前用户和当前时间
        private void button1_Click(object sender, EventArgs e)
        {
            StreamReader readerMy = new StreamReader(txtPath.Text + txtListUrl.Text + ".txt");
            string strLine = readerMy.ReadLine();
            using (SPSite mySite = new SPSite(txtSiteUrl.Text))
            {
                SPWeb myWeb = mySite.RootWeb;

                SPList myList = myWeb.Lists[txtListUrl.Text];
                SPListItem myItem;
                while (strLine != null)
                {
                    if (strLine.Trim().Length > 0)
                    {
                        string[] listItems = strLine.Split('&');
                        try
                        {
                            myItem = myList.Items.GetItemById(int.Parse(listItems[0]));
                            myItem["创建时间"] = DateTime.Parse(listItems[1]);
                            string[] userInfo = listItems[2].Replace("#", "").Split(';');
                            SPFieldUserValue userValue = new SPFieldUserValue(myWeb, int.Parse(userInfo[0]), userInfo[1]);
                            SPUser user = CreatedBy(userInfo[1], myWeb);
                            if (user == null)
                            {
                                user = CreatedBy("系统帐户", myWeb);
                                myItem["创建者"] = user;//listItems[2];

                            }
                            else
                                myItem["创建者"] = user;
                            myItem.Update();
                        }
                        catch
                        { }
                        strLine = readerMy.ReadLine();
                    }
                }
            }
            readerMy.Close();
            readerMy.Dispose();
            MessageBox.Show("ok");
        }
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            StreamReader readerMy = new StreamReader(txtPath.Text);
            int i = 0;
            string strLine = readerMy.ReadLine();
            using (SPSite site = new SPSite("http://localhost"))
            {
                SPServiceContext serviceContext = SPServiceContext.GetContext(site);
                UserProfileManager upm = new UserProfileManager(serviceContext);
                string accountName = "";
                //UserProfile u;
                while (strLine != null)
                {
                    if (strLine.Trim().Length > 0)
                    {
                        accountName = strLine.Trim();
                        //u = upm.GetUserProfile(accountName);
                        if (upm.UserExists(accountName))
                        {
                            upm.RemoveUserProfile(accountName);
                            i = i + 1;
                        }
                    }
                    strLine = readerMy.ReadLine();
                }
            }
            readerMy.Close();
            readerMy.Dispose();
            MessageBox.Show("共删除"+ i.ToString ()+" 条");
        }
        /// <summary>
        /// 1/6更改，将列名作为参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnImport_Click(object sender, EventArgs e)
        {
            if (txtSourceListName.Text ==""||txtDesListName.Text =="")
            {
                MessageBox.Show("导入导出的列表名称不能为空");
                return;
            }
            if (txtSourceCol.Text == "" || txtDesCol.Text == "")
            {
                MessageBox.Show("导入导出的列表列不能为空");
                return;
            }
            string txtNames=txtSourceCol.Text.Trim().Replace("；",";"); 
            txtNames=txtNames.Replace(" ","").TrimEnd (';');

            string[] srcCols = txtNames.Split(';');

            txtNames = txtDesCol.Text.Trim().Replace("；", ";");
            txtNames = txtNames.Replace(" ", "").TrimEnd(';');
            string[] desCols = txtNames.Split(';');

            if (srcCols.Length !=desCols.Length )
            {
                MessageBox.Show("导入导出的列表列个数不同");
                return;
            }
            using (SPSite mySite = new SPSite("http://localhost"))
            {
                SPWeb myWeb = mySite.AllWebs[txtSiteUrl.Text];
                SPList mySourceList = myWeb.Lists[txtSourceListName.Text];
                SPList myDesList = myWeb.Lists[txtDesListName.Text];
                List<SPListItem> items = new List<SPListItem>();
                int[] ids = new int[] {70,80,72,11 };
                //{ 106,57,51};//教学获奖
                //{ 108, 105, 90, 84, 71, 63, 62, 56, 54, 49, 48, 47, 21, 16 };//教学竞赛
                //mySourceList.Items.Cast<SPListItem>().ToList();
                SPListItem myItem;
                foreach (int id in ids)
                {
                    myItem = mySourceList.GetItemById(id);
                    SPListItem newItem = myDesList.AddItem();
                    for (int i = 0; i < srcCols.Length; i++)
                    {
                        newItem[desCols[i]] = myItem[srcCols[i]];
                    }
                    newItem["类别"] = "教改";
                    newItem["级别"] = "校级立项";
                    ////newItem["获奖类别"] = "慕课";//教学获奖
                    ////newItem["成果级别"] = "慕课";
                    //----------
                    //newItem["获奖级别"] = "校级";//教学竞赛
                    //if (id >= 90)
                    //    newItem["获奖类别"] = "教学大赛";//慕课
                    //else
                    //    newItem["获奖类别"] = "课件大赛";
                    //if (id == 56)
                    //    newItem["获奖等次"] = "一等奖";
                    //else if (id == 16 || id == 90)
                    //    newItem["获奖等次"] = "二等奖";
                    //else
                    //    newItem["获奖等次"] = "三等奖";
                    ////newItem.Update();

                    //// 删除目标项上已有的附件
                    //for (int i = newItem.Attachments.Count; i > 0; i--)
                    //{
                    //    newItem.Attachments.Delete(newItem.Attachments[i - 1]);
                    //}

                    // 复制所有的附件
                    foreach (string fileName in myItem.Attachments)
                    {
                        SPFile file = myItem.ParentList.ParentWeb.GetFile(myItem.Attachments.UrlPrefix +
                                                                              fileName);
                        byte[] imageData = file.OpenBinary();
                        newItem.Attachments.Add(fileName, imageData);
                        //newItem.Attachments.Delete(fileName);
                    }
                    newItem.SystemUpdate();
                }
                MessageBox.Show("ok");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
           //     long tick = DateTime.Now.Ticks;
           //     Random ran = new Random((int)(tick & 0xffffffffL) | (int)(tick >> 32));
           //      Random rd = new Random(10);　　//无参即为使用系统时钟为种子
           //for (int i = 0; i < 10; i++)
           // {

           //     //p = ran.Next(1, m);
           //     Console.WriteLine(ran.Next(1,10));
           // }
           // return;
            ArrayList allworks = WorksEvaluation.WorksDistribution(10, 7);
            foreach (ArrayList works in allworks)
            {
                foreach (int m in works)
                    Console.Write(m.ToString() + " ");
                Console.WriteLine();
            }

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            string listUrl = "http://xqx2012/ComputerCompetitive/Lists/List1/DispForm.aspx?ID=3&Source=http%3A%2F%2Fxqx2012%2FComputerCompetitive%2FLists%2FList1%2FAllItems%2Easpx%3FGroupString%3D%253B%2523%25E7%25B3%25BB%25E7%25BB%259F%25E5%25B8%2590%25E6%2588%25B7%253B%2523%26IsGroupRender%3DTRUE&ContentTypeId=0x0104001B928EB60D9DC5439A3E8EAD2316D88F";
            if (listUrl.StartsWith("http://"))
                listUrl = listUrl.Substring(listUrl.IndexOf("/", 7) + 1);
            if (listUrl.IndexOf("&") > 0)
                listUrl = listUrl.Substring(0, listUrl.IndexOf("&"));
            string num = listUrl.Substring(listUrl.IndexOf("=") + 1);
            string listURl = listUrl.Substring(0, listUrl.LastIndexOf("/"));
            SPSite site = new SPSite("http://xqx2012");
            SPWeb my = site.OpenWeb("djw");
            string txt;
            string listName="";
            SPWeb web = site.OpenWeb("ComputerCompetitive");
            foreach (SPList myList in web.Lists)
            {
                if (myList.BaseTemplate == SPListTemplateType.Announcements)
                {
                    txt = myList.DefaultDisplayFormUrl;
                    if (txt.Contains(listURl))
                    {
                        listName = myList.Title;
                        break;
                    }
                }
            }
            SPList list = web.Lists[listName];
            SPQuery query = new SPQuery();
            query.ViewAttributes = "Scope='RecursiveAll'";
            //query.ViewFields = "<FieldRef Name='Title'/><FieldRef Name='FileDirRef'/><FieldRef Name='Modified'/><FieldRef Name='NewsUrl'/><FieldRef Name='FileRef'/><FieldRef Name='Description'/>";
            //query.Query = "<Where><Eq><FieldRef Name='ID' /><Value Type='Counter'>"+num+"</Value></Eq></Where>";
            SPListItemCollection items = list.GetItems(query);
            if (items.Count >0)
            {
                string titel = items[0]["标题"].ToString ();
                string body = items[0]["正文"].ToString();
            }
 
        }

        private void button4_Click(object sender, EventArgs e)
        {
           
            using (SPSite mySite = new SPSite("http://localhost"))
            {
                SPWeb myWeb = mySite.AllWebs[txtSiteUrl.Text];
                SPList mySourceList = myWeb.Lists[txtSourceListName.Text];
                //SPList myDesList = myWeb.Lists[txtDesListName.Text];
                ;
                int[] ids = new int[] {103,98,95,27,12,75};
                int id;
                int total = mySourceList.Items.Count;
                foreach (SPListItem myItem in mySourceList .Items )
                {
                    //myItem = mySourceList.Items[i - 1]; 
                    id = myItem.ID;
                    if (!ids.Contains(id))// && myItem["Flag"] !=null && myItem["Flag"].ToString ()!= "1")
                    {
                        myItem["Flag"] = 2;
                        myItem.SystemUpdate();
                    }

                }
            }
            MessageBox.Show("ok");

        }
    }
}
