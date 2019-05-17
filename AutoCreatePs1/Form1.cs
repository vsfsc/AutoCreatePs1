using System;
using System.Web;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.SharePoint;
using System.Text.RegularExpressions;
using System.DirectoryServices;
using System.Net;
using System.Net.Mail;
using Microsoft.Office.Server;
using Microsoft.Office.Server.UserProfiles ;
using Microsoft.Office.Server.Social;
using Microsoft.Office.Server.Search;
using Microsoft.Office.Server.Search.Query;
using Microsoft.Office.Server.Search.Administration;
using Microsoft.SharePoint.Client;
using System.Collections;

namespace AutoCreatePs1
{
    public partial class Form1 : System.Windows.Forms.Form
    {
        private OpenFileDialog openFileDialog1;
        private int totalCount = 75;


        // Methods
        public Form1()
        {
            this.InitializeComponent();
        }

        private void btnChangeFied_Click(object sender, EventArgs e)
        {
            SPSite site = new SPSite("http://localhost");
            SPWeb myWeb = site.AllWebs[this.txtSubWeb.Text.Trim()];
            SPList myList = myWeb.Lists[this.txtMtable.Text.Trim()];
            SPList subList = myWeb.Lists[this.txtSubT.Text.Trim()];
            foreach (SPListItem item in myList.Items)
            {
                string[] authors = Regex.Split(item["作者"].ToString().TrimEnd(new char[] { ';' }), ";");
                SPFieldUserValueCollection author1 = new SPFieldUserValueCollection();
                for (int i = 0; i < authors.Length; i++)
                {
                    DirectoryEntry adUser = GetDirectoryEntryByName(authors[i].Trim());
                    if (adUser != null)
                    {
                        SPUser user = myWeb.EnsureUser(@"ccc\" + adUser.Properties["sAMAccountName"].Value.ToString());
                        SPFieldUserValue fUser = new SPFieldUserValue(myWeb, user.ID, user.LoginName);
                        author1.Add(fUser);
                    }
                }
                item["作者1"] = author1;
                item.Update();
                try
                {
                    item.ModerationInformation.Status = SPModerationStatusType.Approved;
                    item.SystemUpdate();
                }
                catch
                {
                }
            }
            MessageBox.Show("ok!");
        }

        private void btnCreateSubTable_Click(object sender, EventArgs e)
        {
            SPSite site = new SPSite("http://localhost");
            SPWeb myWeb = site.AllWebs[this.txtSubWeb.Text.Trim()];
            SPList myList = myWeb.Lists[this.txtMtable.Text.Trim()];
            SPList subList = myWeb.Lists[this.txtSubT.Text.Trim()];
            foreach (SPListItem item in myList.Items)
            {
                string txtTitle = item["标题"].ToString();
                string[] authors = Regex.Split(item["作者"].ToString().TrimEnd(new char[] { ';' }), ";");
                SPFieldUserValue creator = null;
                for (int i = 0; i < authors.Length; i++)
                {
                    DirectoryEntry adUser = GetDirectoryEntryByName(authors[i].Trim());
                    if (adUser != null)
                    {
                        SPUser user = myWeb.EnsureUser(@"ccc\" + adUser.Properties["sAMAccountName"].Value.ToString());
                        SPFieldUserValue fUser = new SPFieldUserValue(myWeb, user.ID, user.LoginName);
                        if (i == 0)
                        {
                            creator = fUser;
                        }
                        SPListItem subItem = this.getAtraivementItem(this.txtSubT.Text.Trim(), txtTitle, authors[i].Trim());
                        if (subItem == null)
                        {
                            string authorOder;
                            subItem = subList.AddItem();
                            subItem["论文标题"] = item.ID + ";#" + txtTitle;
                            subItem["姓名"] = fUser;
                            switch (i)
                            {
                                case 0:
                                    authorOder = "第一作者";
                                    break;

                                case 1:
                                    authorOder = "第二作者";
                                    break;

                                case 2:
                                    authorOder = "第三作者";
                                    break;

                                default:
                                    authorOder = "其他";
                                    break;
                            }
                            subItem["作者排序"] = authorOder;
                            if (i == 0)
                            {
                                subItem["系数"] = 1;
                            }
                        }
                        else
                        {
                            subItem["创建者"] = creator;
                        }
                        subItem.Update();
                        try
                        {
                            subItem.ModerationInformation.Status = SPModerationStatusType.Approved;
                            subItem.SystemUpdate();
                        }
                        catch
                        {
                        }
                    }
                }
            }
            MessageBox.Show("ok!");
        }

        private void btnEnum_Click(object sender, EventArgs e)
        {
            SPSite site = new SPSite("http://localhost");
            try
            {
                SPServiceContext serviceContext = SPServiceContext.GetContext(site);
                UserProfileManager upm = new UserProfileManager(serviceContext);
                string accountName = @"ccc\" + this.txtUser.Text.ToString();
                if (upm.UserExists(accountName))
                {
                    SPSocialFollowingManager follow = new SPSocialFollowingManager(upm.GetUserProfile(accountName), serviceContext);
                    SPSocialActorInfo socialInfo = new SPSocialActorInfo();
                    StringBuilder txt = new StringBuilder();
                    foreach (SPSocialActor spFollow in follow.GetFollowed(SPSocialActorTypes.Sites))
                    {
                        socialInfo.ContentUri = spFollow.Uri;
                        socialInfo.ActorType = SPSocialActorType.Site;
                        txt.AppendLine(spFollow.Uri.ToString());
                    }
                    this.txtResult.Text = txt.ToString();
                }
            }
            catch (Exception ex)
            {
                this.txtResult.Text = ex.ToString();
            }
        }

        private void btnEnumerate_Click(object sender, EventArgs e)
        {
            if (this.txtFilePath.Text.Length == 0)
            {
                MessageBox.Show("文件名不能为空");
            }
            else
            {
                this.ReadTxt(this.txtFilePath.Text);
            }
        }

        private void btnFlag_Click(object sender, EventArgs e)
        {
            SPSite site = new SPSite("http://localhost");
            SPWeb myWeb = site.AllWebs[this.txtSubWeb.Text.Trim()];
            SPList myList = myWeb.Lists[this.txtListName.Text.Trim()];
            StreamWriter w = new StreamWriter(this.txtListName.Text + this.txtNum.Text + ".txt", false, Encoding.Default);
            int tNum = int.Parse(this.txtNum.Text);
            int tCount = 0;
            string[] showFields = Regex.Split(this.txtFields.Text.Trim().Replace("；", ";"), ";");
            string showContents = "";
            foreach (string fName in showFields)
            {
                showContents = showContents + fName + "\t";
            }
            showContents = showContents + "角色";
            w.WriteLine(showContents);
            foreach (SPListItem item in myList.Items)
            {
                if ((item["正文"] != null) && (GetHanNumFromString(item["正文"].ToString()) >= tNum))
                {
                    showContents = "";
                    foreach (string fName in showFields)
                    {
                        showContents = showContents + item[fName] + "\t";
                    }
                    if (item["学号/工号"].ToString().Length > 6)
                    {
                        showContents = showContents + "学生";
                    }
                    else
                    {
                        showContents = showContents + "教师";
                    }
                    w.WriteLine(showContents);
                    tCount++;
                }
            }
            w.Close();
            MessageBox.Show("ok!共导出" + tCount.ToString() + "人");
        }

        private void btnFollow_Click(object sender, EventArgs e)
        {
            SPSite site = new SPSite("http://localhost");
            try
            {
                SPServiceContext serviceContext = SPServiceContext.GetContext(site);
                UserProfileManager upm = new UserProfileManager(serviceContext);
                string accountName = @"ccc\" + this.txtUser.Text.ToString();
                if (upm.UserExists(accountName))
                {
                    UserProfile u = upm.GetUserProfile(accountName);
                    SPSocialFollowingManager follow = new SPSocialFollowingManager(u, serviceContext);
                    SPSocialActorInfo socialInfo = new SPSocialActorInfo();
                    StringBuilder txt = new StringBuilder();
                    foreach (SPSocialActor spFollow in follow.GetFollowed(SPSocialActorTypes.Sites))
                    {
                        socialInfo.ContentUri = spFollow.Uri;
                        socialInfo.ActorType = SPSocialActorType.Site;
                        string url = spFollow.Uri.ToString();
                        if (url.StartsWith(this.txtOldName.Text.Trim()))
                        {
                            follow.StopFollowing(socialInfo);
                            txt.AppendLine(spFollow.Uri.ToString() + "    取消关注");
                            socialInfo.ContentUri = new Uri(url.Replace(this.txtOldName.Text.Trim(), this.txtNewName.Text.Trim()));
                            socialInfo.AccountName = u.AccountName;
                            follow.Follow(socialInfo);
                            txt.AppendLine(socialInfo.ContentUri.ToString() + "    重新关注");
                        }
                    }
                    txt.AppendLine("ok");
                    this.txtMsg.Text = txt.ToString();
                }
            }
            catch (Exception ex)
            {
                this.txtMsg.Text = ex.ToString();
            }
        }

        private void btnMoveOU_Click(object sender, EventArgs e)
        {
            if (this.txtFilePath.Text.Length == 0)
            {
                MessageBox.Show("文件名不能为空");
            }
            else
            {
                this.ReadStudent(this.txtFilePath.Text);
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            this.openFileDialog1.ShowDialog();
            this.txtFilePath.Text = this.openFileDialog1.FileName;
        }

        private void btnScore_Click(object sender, EventArgs e)
        {
            SPSite site = new SPSite("http://localhost");
            SPWeb myWeb = site.AllWebs[this.txtSubWeb.Text.Trim()];
            SPList myList = myWeb.Lists[this.txtListName.Text.Trim()];
            string strAction = this.txtAction.Text.Trim();
            foreach (SPListItem item in myList.Items)
            {
                int creaID = item.ID;
                float score = this.GetScore(creaID, strAction);
                item[this.txtScoreField.Text.Trim()] = score;
                SPModerationStatusType appraiseState = item.ModerationInformation.Status;
                item.SystemUpdate();
                item.ModerationInformation.Status = appraiseState;
                item.SystemUpdate();
            }
            MessageBox.Show("ok!");
        }

        private void btnSendEmail_Click(object sender, EventArgs e)
        {
            SPSite site = new SPSite("http://localhost");
            SPWeb myWeb = site.AllWebs[this.txtSubWeb.Text.Trim()];
            SPList myList = myWeb.Lists[this.txtListName.Text.Trim()];
            SPList mySubList = myWeb.Lists[this.txtApproveT.Text.Trim()];
            SPQuery oQuery = new SPQuery
            {
                Query = "<Where><Eq><FieldRef Name='Flag0'/><Value Type='Number'>2</Value></Eq></Where>"
            };
            string fromEmail = "smartneu@mail.neu.edu.cn";
            string fromDisplayName = "智慧东大竞赛委员会";
            string pwd = "ccc.neu.edu.cn";
            string toSubject = "智慧东大创意大赛决赛入围通知";
            string toBody = " 亲爱的老师/同学，恭喜你入围智慧东大决赛，请你准备好现场答辩的PPT，于12月18日前发到 邮箱wudx@mail.neu.edu.cn、hwz@mail.neu.edu.cn . 过期未发者，自动降为三等奖。答辩时间暂定定在下周（12月21日）。对参加答辩有困难请说明";
            SPListItemCollection collListItems = myList.GetItems(oQuery);
            foreach (SPListItem item in collListItems)
            {
                string email = item["电子邮箱"].ToString();
                this.SendMail(fromEmail, fromDisplayName, pwd, new string[] { email }, toSubject, toBody);
            }
            MessageBox.Show("ok!");
        }

        private void btnStopFollow_Click(object sender, EventArgs e)
        {
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            int queryState = 2;
            StringBuilder txt = new StringBuilder();
            SPSite site = new SPSite("http://localhost");
            DataTable queryDataTable = null;
            SearchServiceApplicationProxy ssap = (SearchServiceApplicationProxy)SearchServiceApplicationProxy.GetProxy(SPServiceContext.GetContext(site));
            using (KeywordQuery qry = new KeywordQuery(ssap))
            {
                qry.EnableStemming = true;
                qry.TrimDuplicates = true;
                if (queryState == 1)
                {
                    qry.RowLimit = this.totalCount;
                }
                else
                {
                    qry.RowLimit = 0x1388;
                }
                qry.QueryText = "-author:系统帐户 -author:administrator";
                qry.SortList.Add("Created", Microsoft.Office.Server.Search.Query.SortDirection.Descending);
                SearchExecutor searchExecutor = new SearchExecutor();
                IEnumerator<ResultTable> iResult = searchExecutor.ExecuteQuery(qry).Filter("TableType", KnownTableTypes.RelevantResults).GetEnumerator();
                iResult.MoveNext();
                ResultTable resultTable = iResult.Current;
                queryDataTable = resultTable.Table;
                DataTable disTable = queryDataTable.DefaultView.ToTable(true, new string[] { "contentclass" });
                foreach (string fName in qry.SelectProperties)
                {
                    txt.AppendLine(fName + "  dataType:" + queryDataTable.Columns[fName].DataType.ToString());
                }
                txt.AppendLine("ok");
            }
            this.txtResult.Text = txt.ToString();
        }

        private void btnTestCode_Click(object sender, EventArgs e)
        {
            string txtPath = @"G:\VS2013项目\Projects\AutoCreatePs1\AutoCreatePs1\Thumbnail.xml";
            using (SPSite osite = new SPSite("http://localhost"))
            {
                using (SPWeb oweb = osite.OpenWeb(txtSubWeb.Text))
                {
                    //document library name
                    SPList olist = oweb.Lists[txtSubT.Text];
                    //Thumbnail xml path
                    StreamReader rdr = new StreamReader(txtPath);// @"Thumbnails.xml");

                    string fld = rdr.ReadToEnd();

                    olist.Fields.AddFieldAsXml(fld, true, SPAddFieldOptions.AddToDefaultContentType);

                    olist.Update();

                }
            }
            MessageBox.Show("ok");
        }
        #region 测试
        private void GetSplookUpList()
        {
            SPSite site = new SPSite("http://localhost");
            SPWeb myWeb = site.AllWebs[this.txtSubWeb.Text.Trim()];
            SPList subList = myWeb.Lists[this.txtListName.Text.Trim()];
            SPFieldLookup task = subList.Fields.GetFieldByInternalName("PlanID") as SPFieldLookup;
            SPList taskList = myWeb.Lists[new Guid(task.LookupList)];
        }
        #endregion

        private void btnTotal_Click(object sender, EventArgs e)
        {
            SPSite site = new SPSite("http://localhost");
            SPWeb myWeb = site.AllWebs[this.txtSubWeb.Text.Trim()];
            SPList myList = myWeb.Lists[this.txtListName.Text.Trim()];
            foreach (SPListItem item in myList.Items)
            {
                int iWordNum = 0;
                if (item["正文"] != null)
                {
                    iWordNum = GetHanNumFromString(item["正文"].ToString());
                    item["Flag"] = iWordNum;
                }
                else
                {
                    item["Flag"] = 0;
                }
                string appraiseState = item["审批状态"].ToString();
                item.SystemUpdate();
                if (appraiseState == "0")
                {
                    item.ModerationInformation.Status = SPModerationStatusType.Approved;
                }
                else
                {
                    item.ModerationInformation.Status = SPModerationStatusType.Denied;
                }
                item.SystemUpdate();
            }
            MessageBox.Show("ok!");
        }

        private void btnWrite_Click(object sender, EventArgs e)
        {
            SPSite site = new SPSite("http://localhost");
            SPWeb myWeb = site.AllWebs[this.txtSubWeb.Text.Trim()];
            SPList myList = myWeb.Lists[this.txtListName.Text.Trim()];
            SPList mySubList = myWeb.Lists[this.txtApproveT.Text.Trim()];
            foreach (SPListItem item in myList.Items)
            {
                if ((item.ModerationInformation == null) || (item.ModerationInformation.Status == SPModerationStatusType.Approved))
                {
                    string[] name;
                    string creaName = item["创意名称"].ToString();
                    switch (int.Parse(item["Group"].ToString()))
                    {
                        case 1:
                            name = new string[] { "huangweizu", "huangrenjie", "huangweihua" };
                            break;

                        case 2:
                            name = new string[] { "09275", "09256", "31079" };
                            break;

                        case 3:
                            name = new string[] { "08433", "06834", "31042" };
                            break;

                        case 4:
                            name = new string[] { "10013", "09411", "09372" };
                            break;

                        default:
                            name = new string[] { "40833", "09371" };
                            break;
                    }
                    string strAction = this.txtAction.Text;
                    this.WriteApproveT(item.ID.ToString(), creaName, name, strAction);
                }
            }
            MessageBox.Show("ok!");
        }

        private void btnXueHao_Click(object sender, EventArgs e)
        {
            SPSite site = new SPSite("http://localhost");
            SPWeb myWeb = site.AllWebs[this.txtSubWeb.Text.Trim()];
            SPList myList = myWeb.Lists[this.txtListName.Text.Trim()];
            StreamWriter w = new StreamWriter(this.txtListName.Text + "重复名单.txt", false, Encoding.Default);
            DataTable dt = new DataTable();
            DataRow dr = null;
            DataColumn dc = new DataColumn("Account", typeof(string));
            dt.Columns.Add(dc);
            foreach (SPListItem item in myList.Items)
            {
                dr = dt.NewRow();
                dr["Account"] = item["学号/工号"];
                dt.Rows.Add(dr);
            }
            DataTable disDt = dt.DefaultView.ToTable(true, new string[] { "Account" });
            int tCount = 0;
            foreach (DataRow dr1 in disDt.Rows)
            {
                if (dt.Select("Account='" + dr1["Account"].ToString() + "'").Length > 1)
                {
                    w.WriteLine(dr1["Account"].ToString());
                    tCount++;
                }
            }
            w.Close();
            MessageBox.Show("ok!" + tCount.ToString() + "  人重复");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SPSite site = new SPSite("http://localhost");
            SPWeb myWeb = site.AllWebs[this.txtSubWeb.Text.Trim()];
            SPList myList = myWeb.Lists[this.txtListName.Text.Trim()];
            int i = 0;
            foreach (SPListItem item in myList.Items)
            {
                string txtTitle = item["角色"].ToString();
                string txtGroup = item["单位名称"].ToString();
                if ((txtTitle.Contains("学生") && (item["Group"] == null)) && (item["审批状态"].ToString() == "0"))
                {
                    item["Group"] = 3;
                    i++;
                    try
                    {
                        SPModerationStatusType appraiseState = (SPModerationStatusType)item["审批状态"];
                        item.SystemUpdate();
                        item.ModerationInformation.Status = appraiseState;
                        item.SystemUpdate();
                    }
                    catch
                    {
                        item.SystemUpdate();
                    }
                    if (i == 12)
                    {
                        break;
                    }
                }
            }
            MessageBox.Show("ok!");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            SPSite site = new SPSite("http://localhost");
            SPWeb myWeb = site.AllWebs[this.txtSubWeb.Text.Trim()];
            SPList list = myWeb.Lists["项目文档"];
            foreach (SPListItem item in list.Items)
            {
                SPFile file = item.File;
                foreach (DictionaryEntry entry in file.Properties)
                {
                    Console.WriteLine(entry.Key + ":" + entry.Value);
                }
                break;
            }
        }
        //二等奖写入创意子表
        private void button11_Click(object sender, EventArgs e)
        {
            SPSite site = new SPSite("http://localhost");
            SPWeb myWeb = site.AllWebs[this.txtSubWeb.Text.Trim()];
            SPList myList = myWeb.Lists[this.txtListName.Text.Trim()];
            SPList mySubList = myWeb.Lists[this.txtApproveT.Text.Trim()];
            SPQuery oQuery = new SPQuery
            {
                Query = "<Where><Eq><FieldRef Name='Flag0'/><Value Type='Number'>2</Value></Eq></Where>"
            };
            SPListItemCollection collListItems = myList.GetItems(oQuery);
            foreach (SPListItem item in collListItems)
            {
                SPListItem newITem = mySubList.AddItem();
                newITem["创意名称"] = item.ID;
                newITem["动作"] = "答辩";
                newITem["作品状态"] = "答辩状态";
                newITem["结果"] = "二等奖";
                newITem.Update();
            }
            MessageBox.Show("ok!");
        }
        //一等奖
        private void button12_Click(object sender, EventArgs e)
        {
            SPSite site = new SPSite("http://localhost");
            SPWeb myWeb = site.AllWebs[this.txtSubWeb.Text.Trim()];
            SPList myList = myWeb.Lists[this.txtListName.Text.Trim()];
            SPList mySubList = myWeb.Lists[this.txtApproveT.Text.Trim()];
            string[] num1 = new string[] { "003579", "009438", "009456", "031103", "031247", "1610560", "1670790", "1700818", "20153552", "20154536", "20163412", "20164795", "20165294", "20170330", "20170351" };//, "20170374" };//一等奖
            string[] num3 = new string[] { "20154641", "20160120", "20161728", "20170279", "20170363", "20170374", "009055", "009639", "010010" };//三等奖

            SPQuery oQuery = new SPQuery
            {
                Query = "<Where><Eq><FieldRef Name='Flag0'/><Value Type='Number'>2</Value></Eq></Where>"
            };
            SPListItemCollection collListItems = myList.GetItems(oQuery);
            foreach (SPListItem item in collListItems)
            {
                string lng = item["学号/工号"].ToString();

                if (num1.Contains<string>(lng))
                {
                    item["Flag0"] = 3;
                    item.Update();
                    item.SystemUpdate();
                    item.ModerationInformation.Status = SPModerationStatusType.Approved;
                    item.SystemUpdate();
                }
                else if (((IList)num3).Contains(lng))
                {
                    item["Flag0"] = 1;
                    item.Update();
                    item.SystemUpdate();
                    item.ModerationInformation.Status = SPModerationStatusType.Approved;
                    item.SystemUpdate();
                }

            }
            string idNum = "20165246";//二等奖
            oQuery = new SPQuery
            {
                Query = "<Where><Eq><FieldRef Name='IDNum'/><Value Type='Text'>" + idNum + "</Value></Eq></Where>"
            };
            collListItems = myList.GetItems(oQuery);
            foreach (SPListItem item in collListItems)
            {
                item["Flag0"] = 2;
                item.Update();
                item.SystemUpdate();
                item.ModerationInformation.Status = SPModerationStatusType.Approved;
                item.SystemUpdate();
            }
            MessageBox.Show("ok!");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int[] arr = this.getRandomNum(6, 1, 20);
            int i = 0;
            string temp = "";
            while (i <= (arr.Length - 1))
            {
                temp = temp + arr[i].ToString() + "\t";
                i++;
            }
            Console.WriteLine(temp);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SPSite site = new SPSite("http://localhost");
            SPWeb myWeb = site.AllWebs[this.txtSubWeb.Text.Trim()];
            SPList myList = myWeb.Lists[this.txtApproveT.Text.Trim()];
            foreach (SPListItem item in myList.Items)
            {
                item["时间"] = item["修改时间"];
                try
                {
                    SPModerationStatusType appraiseState = item.ModerationInformation.Status;
                    item.SystemUpdate();
                    item.ModerationInformation.Status = appraiseState;
                    item.SystemUpdate();
                }
                catch
                {
                    item.SystemUpdate();
                }
            }
            MessageBox.Show("ok!");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SPSite site = new SPSite("http://localhost");
            SPWeb myWeb = site.AllWebs[this.txtSubWeb.Text.Trim()];
            SPList myList = myWeb.Lists[this.txtMtable.Text.Trim()];
            SPList subList = myWeb.Lists[this.txtSubT.Text.Trim()];
            foreach (SPListItem item in myList.Items)
            {
                string[] authors = Regex.Split(item["作者"].ToString().TrimEnd(new char[] { ';' }), ";");
                SPFieldUserValueCollection author1 = new SPFieldUserValueCollection();
                DirectoryEntry adUser = GetDirectoryEntryByName(authors[0].Trim());
                if (adUser != null)
                {
                    SPUser user = myWeb.EnsureUser(@"ccc\" + adUser.Properties["sAMAccountName"].Value.ToString());
                    SPFieldUserValue fUser = new SPFieldUserValue(myWeb, user.ID, user.LoginName);
                    item["创建者"] = fUser;
                    item.Update();
                    try
                    {
                        item.ModerationInformation.Status = SPModerationStatusType.Approved;
                        item.SystemUpdate();
                    }
                    catch
                    {
                    }
                }
            }
            MessageBox.Show("ok!");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SPSite site = new SPSite("http://localhost");
            SPWeb myWeb = site.AllWebs[this.txtSubWeb.Text.Trim()];
            SPList myList = myWeb.Lists[this.txtMtable.Text.Trim()];
            SPList subList = myWeb.Lists[this.txtSubT.Text.Trim()];
            DataTable dt = myList.Items.GetDataTable();
            StringBuilder txt = new StringBuilder();
            foreach (SPField f in myList.Fields)
            {
                txt.AppendLine(f.Title + "    " + f.InternalName);
            }
            this.txtResult.Text = txt.ToString();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SPSite site = new SPSite("http://localhost");
            SPWeb myWeb = site.AllWebs[this.txtSubWeb.Text.Trim()];
            SPList myList = myWeb.Lists[this.txtListName.Text.Trim()];
            float[] scores = new float[] { 79.9f };
            int[] ids = new int[] { 0x70 };
            string strAction = this.txtAction.Text.Trim();
            for (int i = 0; i < ids.Length; i++)
            {
                SPListItem item = myList.GetItemById(ids[i]);
                item[this.txtScoreField.Text.Trim()] = scores[i];
                SPModerationStatusType appraiseState = item.ModerationInformation.Status;
                item.SystemUpdate();
                item.ModerationInformation.Status = appraiseState;
                item.SystemUpdate();
            }
            MessageBox.Show("ok!");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            using (SPSite site = new SPSite("http://localhost"))
            {
                SPWeb myWeb = site.AllWebs[this.txtSubWeb.Text.Trim()];
                SPList myList = myWeb.Lists[this.txtListName.Text.Trim()];
                SPQuery oQuery = new SPQuery
                {
                    Query = "<OrderBy><FieldRef Name='Score' Ascending='False'></FieldRef></OrderBy><Where><Eq><FieldRef Name='Group' /><Value Type='Number'>" + 5 + "</Value></Eq></And></Where>"
                };
                SPListItemCollection collListItems = myList.GetItems(oQuery);
                foreach (SPListItem oListItem in collListItems)
                {
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            SPSite site = new SPSite("http://localhost");
            SPWeb myWeb = site.AllWebs[this.txtSubWeb.Text.Trim()];
            SPList myList = myWeb.Lists[this.txtListName.Text.Trim()];
            StringBuilder txt = new StringBuilder();
            foreach (SPField f in myList.Fields)
            {
                txt.AppendLine(f.Title + "   " + f.InternalName);
            }
            this.txtResult.Text = txt.ToString();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            SPSite site = new SPSite("http://localhost");
            SPWeb myWeb = site.AllWebs[this.txtSubWeb.Text.Trim()];
            SPList myList = myWeb.Lists[this.txtListName.Text.Trim()];
            SPList mySubList = myWeb.Lists[this.txtApproveT.Text.Trim()];
            SPQuery oQuery = new SPQuery
            {
                Query = "<Where><Eq><FieldRef Name='Flag0'/><Value Type='Number'>1</Value></Eq></Where>"
            };
            StreamReader r = new StreamReader(this.txtFilePath.Text, Encoding.Default);
            for (string txtResult = r.ReadLine(); (txtResult != null) && (txtResult != ""); txtResult = r.ReadLine())
            {
                string xueHao = Regex.Split(txtResult, "\t")[2];
            }
            r.Close();
            SPListItemCollection collListItems = myList.GetItems(oQuery);
            foreach (SPListItem item in collListItems)
            {
                SPListItem newITem = mySubList.AddItem();
                newITem["创意名称"] = item.ID;
                newITem["动作"] = "微信投票";
                newITem["作品状态"] = "微信投票";
                newITem.Update();
            }
            MessageBox.Show("ok!");
        }

        private bool CheckBlog(string xueHao)
        {
            string siteUrl = "http://localhost/personal/" + xueHao + "/Blog";
            CamlQuery oQuery = new CamlQuery
            {
                ViewXml = "<View><Query><Where></Where></Query></View>"
            };
            try
            {
                ClientContext clientContext = new ClientContext(siteUrl);
                ListItemCollection lstItems = clientContext.Web.Lists.GetByTitle("Posts").GetItems(oQuery);
                clientContext.Load(lstItems, items => items.Include(item => item["Title"], item => item.Id));
                clientContext.ExecuteQuery();
                return (lstItems.Count >= 0);
            }
            catch
            {
                return false;
            }
        }

        private bool CheckBlogNew(string xueHao)
        {
            string siteUrl = "http://localhost/personal/" + xueHao + "/Blog";
            try
            {
                SPSite site = new SPSite(siteUrl);
                return (site != null);
            }
            catch
            {
                return false;
            }
        }



        private void EnumerateUserProfile()
        {
            SPSecurity.RunWithElevatedPrivileges(delegate
            {
                SPSite sc = new SPSite(SPContext.Current.Site.ID);
                UserProfileManager profileManager = new UserProfileManager(SPServiceContext.GetContext(sc));
                foreach (UserProfile uprofile in profileManager)
                {
                }
                IEnumerator profileEnum = profileManager.GetEnumerator();
                DataTable dtUserProfile = new DataTable();
                while (profileEnum.MoveNext())
                {
                    UserProfile up = (UserProfile)profileEnum.Current;
                    if ((((up["FirstName"] != null) && (up["FirstName"].Value != null)) && !string.IsNullOrEmpty(up["FirstName"].Value.ToString())) && ((up.PublicUrl != null) && !string.IsNullOrEmpty(up.PublicUrl.ToString())))
                    {
                        DataRow drUserProfile = dtUserProfile.NewRow();
                        drUserProfile["DisplayName"] = up.DisplayName;
                        drUserProfile["FirstName"] = up["FirstName"].Value;
                        drUserProfile["LastName"] = up["LastName"].Value;
                        drUserProfile["Department"] = up["Department"].Value;
                        drUserProfile["Location"] = up["SPS-Location"].Value;
                        drUserProfile["ContactNumber"] = up["Office"].Value;
                        drUserProfile["MySiteUrl"] = up.PublicUrl;
                        dtUserProfile.Rows.Add(drUserProfile);
                    }
                }
            });
        }

        private SPListItem getAtraivementItem(string subList, string strCreativity, string userName)
        {
            using (SPSite site = new SPSite("http://localhost"))
            {
                using (SPWeb spWeb = site.AllWebs[this.txtSubWeb.Text.Trim()])
                {
                    try
                    {
                        SPList spList = spWeb.Lists.TryGetList(subList);
                        if (spList != null)
                        {
                            SPQuery qry = new SPQuery
                            {
                                Query = "<Where><And><Eq><FieldRef Name='AuthorName' /> <Value Type='User'>" + userName + " </Value></Eq><Eq><FieldRef Name='PaperTitle' /><Value Type='Lookup'>" + strCreativity + "</Value></Eq></And></Where>"
                            };
                            SPListItemCollection listItems = spList.GetItems(qry);
                            if (listItems.Count > 0)
                            {
                                return listItems[0];
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }
            }
            return null;
        }

        public static DirectoryEntry GetDirectoryEntryByAccount(string sAMAccountName)
        {
            DirectoryEntry de = new DirectoryEntry(ADPath);
            DirectorySearcher deSearch = new DirectorySearcher(de)
            {
                Filter = "(&(&(objectCategory=person)(objectClass=user))(sAMAccountName=" + sAMAccountName + "))",
                SearchScope = SearchScope.Subtree
            };
            try
            {
                return new DirectoryEntry(deSearch.FindOne().Path);
            }
            catch
            {
                return null;
            }
        }

        public static DirectoryEntry GetDirectoryEntryByName(string displayName)
        {
            DirectoryEntry de = new DirectoryEntry(ADPath);
            DirectorySearcher deSearch = new DirectorySearcher(de)
            {
                Filter = "(&(&(objectCategory=person)(objectClass=user))(name=" + displayName + "))",
                SearchScope = SearchScope.Subtree
            };
            try
            {
                SearchResultCollection results = deSearch.FindAll();
                if (results.Count == 1)
                {
                    return new DirectoryEntry(results[0].Path);
                }
                foreach (SearchResult result in results)
                {
                    if (result.Path.Contains("教师"))
                    {
                        return new DirectoryEntry(result.Path);
                    }
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public static DirectoryEntry GetDirectoryEntryOfOU(string entryPath, string ouName)
        {
            if (entryPath == "")
            {
                entryPath = ADPath;
            }
            DirectoryEntry de = new DirectoryEntry(entryPath);
            DirectorySearcher deSearch = new DirectorySearcher(de)
            {
                Filter = "(&(objectClass=organizationalUnit)(OU=" + ouName + "))",
                SearchScope = SearchScope.Subtree
            };
            try
            {
                return new DirectoryEntry(deSearch.FindOne().Path);
            }
            catch
            {
                return null;
            }
        }

        private SPFieldUserValue GetFieldUserValue(string userName)
        {
            SPSite site = new SPSite("http://localhost");
            SPWeb myWeb = site.AllWebs[this.txtSubWeb.Text.Trim()];
            DirectoryEntry adUser = GetDirectoryEntryByAccount(userName);
            if (adUser != null)
            {
                SPUser user = myWeb.EnsureUser(@"ccc\" + adUser.Properties["sAMAccountName"].Value.ToString());
                return new SPFieldUserValue(myWeb, user.ID, user.LoginName);
            }
            return null;
        }

        private string GetGroup(string strCreate)
        {
            StreamReader r = new StreamReader(this.txtFilePath.Text, Encoding.Default);
            string txtResult = r.ReadLine();
            string txtGroup = "";
            while ((txtResult != null) && (txtResult != ""))
            {
                string[] paras = Regex.Split(txtResult, "\t");
                if (paras[0] == strCreate)
                {
                    txtGroup = paras[2];
                    break;
                }
            }
            r.Close();
            return txtGroup;
        }

        public static int GetHanNumFromString(string str)
        {
            int count = 0;
            Regex regex = new Regex(@"^[\u4E00-\u9FA5]{0,}$");
            for (int i = 0; i < str.Length; i++)
            {
                char c = str[i];
                if (regex.IsMatch(c.ToString()))
                {
                    count++;
                }
            }
            return count;
        }

        public int getNum(int[] arrNum, int tmp, int minValue, int maxValue, Random ra)
        {
            for (int n = 0; n <= (arrNum.Length - 1); n++)
            {
                if (arrNum[n] == tmp)
                {
                    tmp = ra.Next(minValue, maxValue);
                    this.getNum(arrNum, tmp, minValue, maxValue, ra);
                }
            }
            return tmp;
        }

        public int[] getRandomNum(int num, int minValue, int maxValue)
        {
            Random ra = new Random((int)DateTime.Now.Ticks);
            int[] arrNum = new int[num];
            int tmp = 0;
            for (int i = 0; i <= (num - 1); i++)
            {
                tmp = ra.Next(minValue, maxValue);
                arrNum[i] = this.getNum(arrNum, tmp, minValue, maxValue, ra);
            }
            return arrNum;
        }

        private float GetScore(int creaID, string strAction)
        {
            using (SPSite site = new SPSite("http://localhost"))
            {
                SPWeb myWeb = site.AllWebs[this.txtSubWeb.Text.Trim()];
                SPList myList = myWeb.Lists[this.txtApproveT.Text.Trim()];
                SPQuery oQuery = new SPQuery
                {
                    Query = string.Concat(new object[] { "<Where><And><Eq><FieldRef Name='creativity' LookupId='True' /><Value Type='Lookup'>", creaID, "</Value></Eq><Eq><FieldRef Name='Title' /><Value Type='Text'>", strAction, "</Value></Eq></And></Where>" })
                };
                SPListItemCollection collListItems = myList.GetItems(oQuery);
                float score = 0f;
                int cTotal = 0;
                foreach (SPListItem oListItem in collListItems)
                {
                    if (oListItem[this.txtScoreField.Text.Trim()] != null)
                    {
                        score += float.Parse(oListItem[this.txtScoreField.Text.Trim()].ToString());
                        cTotal++;
                    }
                }
                if (score > 0f)
                {
                    score /= (float)cTotal;
                }
                site.Dispose();
                return (float)Math.Round((double)score, 1);
            }
        }



        private void ReadExcel(string fileName)
        {
        }

        private void ReadStudent(string fileName)
        {
            StreamReader r = new StreamReader(this.txtFilePath.Text, Encoding.Default);
            StreamWriter w = new StreamWriter(this.txtFilePath.Text.Replace(".txt", "move.txt"), false, Encoding.Default);
            string txtResult = r.ReadLine();
            int j = 0;
            while ((txtResult != null) && (txtResult != ""))
            {
                string[] infos = Regex.Split(txtResult, "\t");
                string xueHao = infos[3];
                string bj = infos[2];
                DirectoryEntry stu = GetDirectoryEntryByAccount(xueHao);
                DirectoryEntry stuBj = GetDirectoryEntryOfOU("", bj);
                if (stu == null)
                {
                    w.WriteLine(infos[1] + "\t" + infos[2] + "\t" + infos[3] + "\t" + infos[4] + "\t未注册");
                }
                else if (stuBj != null)
                {
                    try
                    {
                        stu.MoveTo(stuBj);
                        j++;
                    }
                    catch (Exception ex)
                    {
                        w.WriteLine(ex.ToString());
                    }
                }
                txtResult = r.ReadLine();
            }
            w.WriteLine("共移动    " + j.ToString() + "   个学生");
            r.Close();
            w.Close();
            MessageBox.Show("ok");
        }

        private void ReadTxt(string fileName)
        {
            StreamReader r = new StreamReader(this.txtFilePath.Text, Encoding.Default);
            StreamWriter w = new StreamWriter(this.txtFilePath.Text.Replace(".txt", "result.txt"), false, Encoding.Default);
            for (string txtResult = r.ReadLine(); (txtResult != null) && (txtResult != ""); txtResult = r.ReadLine())
            {
                string[] infos = Regex.Split(txtResult, "\t");
                string xueHao = infos[3];
                if (GetDirectoryEntryByAccount(xueHao) == null)
                {
                    w.WriteLine(infos[1] + "\t" + infos[2] + "\t" + infos[3] + "\t" + infos[4] + "\t未注册");
                }
                else if (!this.CheckBlogNew(xueHao))
                {
                    w.WriteLine(infos[1] + "\t" + infos[2] + "\t" + infos[3] + "\t" + infos[4] + "\t未创建博客");
                }
            }
            r.Close();
            w.Close();
            MessageBox.Show("ok");
        }

        public bool SendMail(string fromEmail, string fromDisplayName, string pwd, string[] toMail, string toSubject, string toBody)
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
                Host = "smtp.neu.edu.cn",
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

        private void WriteApproveT(string creativityID, string strCreativity, string[] names, string strAction)
        {
            SPSite site = new SPSite("http://localhost");
            SPWeb myWeb = site.AllWebs[this.txtSubWeb.Text.Trim()];
            SPList myList = myWeb.Lists[this.txtApproveT.Text.Trim()];
            foreach (string userName in names)
            {
                SPFieldUserValue fUser = this.GetFieldUserValue(userName);
                SPQuery oQuery = new SPQuery
                {
                    Query = string.Concat(new object[] { "<Where><And><And><Eq><FieldRef Name='Author' LookupId='True' /> <Value Type='Integer'>", fUser.User.ID, "</Value></Eq><Eq><FieldRef Name='Title' /><Value Type='Text'>", strAction, "</Value></Eq></And><Eq><FieldRef Name='creativity' LookupId='True' /><Value Type='Lookup'>", creativityID, "</Value></Eq></And></Where>" })
                };
                if (myList.GetItems(oQuery).Count == 0)
                {
                    SPListItem listItem = myList.AddItem();
                    listItem["创意名称"] = creativityID + ";#" + strCreativity;
                    listItem["评审人"] = fUser;
                    listItem["动作"] = strAction;
                    listItem.Update();
                }
            }
        }

        // Properties
        private static string ADPath
        {
            get
            {
                DirectoryEntry root = new DirectoryEntry("LDAP://rootDSE");
                return ("LDAP://" + root.Properties["defaultNamingContext"].Value);
            }
        }

        private void btnTestCode_Click_1(object sender, EventArgs e)
        {
        
            List<int> ps = new List<int> { 1, 2, 3, 4, 5 };
            ps.Reverse();

            SPSite site = new SPSite("http://localhost");
            SPWeb myWeb = site.AllWebs["blog"];
            string listName = "本科教学2";// "项目任务";"申报材料";//"个人学习助手结果";// 
            SPList myList = myWeb.Lists[listName ];
            SPListItem itm = myList.Items[0];
            itm["action"] = new SPFieldLookupValueCollection("9;#分析;#2;#学习");
            itm.Update();
            SPQuery qry = new SPQuery();
            //当前用户创建的文档
            qry.ViewFields = "<FieldRef Name='ID' /><FieldRef Name='Author' /><FieldRef Name='FileRef' /><FieldRef Name='Action' /><FieldRef Name='Title' />";
            //qry.Query = @"<Where><Eq><FieldRef Name='Author' LookupId='True' /><Value Type='Lookup'>" + user.ID + "</Value></Eq></Where>";
            qry.Folder = myList.RootFolder; ;
            qry.ViewAttributes = "Scope=\"RecursiveAll\"";
            SPListItemCollection docITems = myList.GetItems(qry);
            //SPFieldLookupValue f = new SPFieldLookupValue();
            

            //foreach (SPListItem imte in myList.Items )
            //{
            //    Console.WriteLine(imte.ID + "    " + imte.File.ServerRelativeUrl);
            //}
            //DataTable dt = docITems.GetDataTable();
            //DataRow[] flds;
            //foreach (SPListItem  fld in myList.Folders  )
            //{
            //    flds=dt.Select("FileRef='"+fld["FileRef"]+"'");
            //    if (flds.Length > 0)
            //        dt.Rows.Remove(flds[0]);
                 

            //}
            //foreach (DataRow dr in dt.Rows )
            //{
            //    Console.WriteLine(dr["ID"]+"    "+dr["FileRef"]);
            //}
            return;
            string[] lFieds = new string[] { "ItemChildCount", "UniqueId", "Last_x0020_Modified", "SortBehavior", "FileRef", "AppAuthor" };
            SPField f;
            string cate = myList.Items[0]["Cate"].ToString();
            cate=cate.TrimStart(";#".ToCharArray());
            cate = cate.TrimEnd(";#".ToCharArray());
            
            string[] cates=System.Text.RegularExpressions.Regex.Split(cate,";#");
            SPFieldLookup f2;
   StringBuilder txt = new StringBuilder();
            foreach (string fName in lFieds)
            {
                f = myList.Fields.GetFieldByInternalName(fName);
                f2 = f as SPFieldLookup;
                if (f2.LookupList == "")
                    txt.AppendLine("    "+f2.LookupField + "    " + f2.Title);
                else
                    try
                    {
 myList = myWeb.Lists[new Guid(f2.LookupList)];
                txt.AppendLine(myList.Title + "   " + f.Title);
                    }
                    catch
                    {
                        txt.AppendLine(f2.LookupList );
                    }
                   

            }
            txtResult.Text = txt.ToString();
 return;
            //SPQuery qry = new SPQuery();
            qry.Query = "";
            
           SPListItemCollection all = myList.GetItems(qry);
            txt.AppendLine(all.Count.ToString ());
            foreach (SPListItem item in myList.Items)
                txt.AppendLine(item["父 ID"] == null ? "null" : item["父 ID"].ToString());
            SPField fs = myList.Fields.GetFieldByInternalName("ParentID");
            if (fs.Type == SPFieldType.Lookup)
                txt.AppendLine("ok");
            SPFieldLookup fl = fs as SPFieldLookup;
            txt.AppendLine(fl.LookupField + " lookupList:" + fl.LookupList);
            SPList lookList = myWeb.Lists[new Guid(fl.LookupList)];
            txt.AppendLine(lookList.Title );
            //foreach (SPField f in myList.Fields)
            //    txt.AppendLine(f.InternalName + "   " + f.TypeAsString);
            txtResult.Text = txt.ToString();

        }

        private void delString()
        {
            string thumbnail = "http://va.neu.edu.cn/Courses/CollegeEnglish/SmartLL/DocLib7/1%202016%E5%9B%BD%E5%AE%B6%E7%B2%BE%E5%93%81%E8%B5%84%E6%BA%90%E5%85%B1%E4%BA%AB%E8%AF%BE%E7%A8%8B.jpg";
            string extName = thumbnail.Substring(thumbnail.LastIndexOf("."));//获取扩展名
            string fileName = thumbnail.Substring(thumbnail.LastIndexOf("/")).Replace(extName, "");
            string fName = thumbnail.Substring(0, thumbnail.LastIndexOf("/")) + "/_t";
            fName = fName + fileName + extName.Replace(".", "_") + extName;
        }
        public interface iMyTest
        { string showMsg(); }
        public class test : iMyTest
        {
            public string showMsg()
            {
                throw new NotImplementedException();
            }
        }

        private void btnQueryOrder_Click(object sender, EventArgs e)
        {
            SPSite site = new SPSite("http://localhost");
            SPWeb myWeb = site.OpenWeb("blog123");
            string listName = "个人学习助手结果";// "项目任务";"申报材料";//"个人学习助手结果";// 
            SPList sList = myWeb.Lists[listName];
            SPQuery qry = new SPQuery();
            qry.ViewFields = "<FieldRef Name='ID' /><FieldRef Name='Author' /><FieldRef Name='Result' /><FieldRef Name='AssistantID' /><FieldRef Name='Title' /><FieldRef Name='Created' />";

            //根据时间获取新建项目的结果，根据用户和时间
            qry.Query = @"<Where><And><Eq><FieldRef Name='Author' LookupId='True' /><Value Type='Lookup'>8</Value></Eq><Eq><FieldRef Name='AssistantID'/><Value Type='Number'>0</Value></Eq></And></Where><OrderBy><FieldRef Name = 'Created' Ascending = 'FALSE' /></OrderBy>";

            SPListItemCollection docITems = sList.GetItems(qry);
            if (docITems.Count > 0)//
            {
                DateTime dtNew = (DateTime)docITems[0]["创建时间"];
            }
        }
    }
}
