using System;
using System.IO;
using Microsoft.SharePoint;
using System.Windows.Forms;
using System.Threading;

namespace AutoCreatePs1
{
    public partial class FrmFillData : Form
    {
        public FrmFillData()
        {
            InitializeComponent();
            lblMsg.Text = "";
            //string txtUrl = "http://xqx2012/blog";
            //SPSite site = new SPSite(txtUrl);
            //Console.WriteLine(site.OpenWeb ().Url );
        }
        #region 进度条相关
        private delegate void SetPos(int ipos,int total);
        private void SetTextMessage(int ipos,int total)

        {

            if (this.InvokeRequired)

            {
                SetPos setpos = new SetPos(SetTextMessage);
                this.Invoke(setpos, new object[] { ipos,total });
            }
            else
            {
                this.lblMsg.Text = ipos.ToString () +" / " +total.ToString ();// ipos.ToString() + " %";
                this.progressBar1.Value = Convert.ToInt32(ipos*100/total);
                
            }
        }
            private void SleepT()

         {
            //lblMsg.Text = "正在进行，请稍等……";
            string fldType = "ActivityType";//活动类型内部字段名
            string fldCustAction = "CustAction";//活动操作内部 字段名称
            string fldAction = "Action";//操作内部字段名称
            string siteUrl = "http://localhost";
            string webUrl = txtSubWeb.Text;
            StreamWriter w = new StreamWriter("c:\\action.txt", false );
            using (SPSite spSite = new SPSite(siteUrl)) //找到网站集
            {
                using (SPWeb spWeb = spSite.OpenWeb(webUrl))
                {
                    SPList lstAssistant = spWeb.Lists.TryGetList(txtListName.Text);
                    if (lstAssistant == null) return;
                    SPFieldLookup fld = lstAssistant.Fields.GetFieldByInternalName(fldCustAction) as SPFieldLookup;//活动操作
                    string fldActionTitle = lstAssistant.Fields.GetFieldByInternalName(fldAction).Title;//操作，后来的用活动操作替换了
                    string fldTypeTitle = lstAssistant.Fields.GetFieldByInternalName(fldType).Title;//活动类型，活动操作对应的类型
                    string lstID = fld.LookupList;
                    SPList lstType = spWeb.Lists[new Guid(lstID)];//查阅项所在的列表
                    string fldActionTypeDispName = "";//获取查阅项表中的类别字段显示名称，即活动操作的上级
                    foreach (SPField fldType1 in lstType.Fields)
                    {
                        if (fldType1.Type == SPFieldType.Lookup && !fldType1.Hidden)
                        {

                            fldActionTypeDispName = fldType1.Title;
                            break;
                        }
                    }
                    SPListItem itemType = null;
                    string txtAction;
                    string actiType;
                    int i = 1;
                    SPListItem item;
                    this.Text = lstAssistant.ItemCount.ToString();
                    for (int j = int.Parse(txtStart.Text); j < lstAssistant.ItemCount; j++)//遍历所有的活动记录each (SPListItem item in lstAssistant.Items)
                    {
                        item = lstAssistant.Items[j];
                        if (item[fld.Title] == null)//进行填充
                        {
                            if (item[fldActionTitle] == null) continue;
                            txtAction = item[fldActionTitle].ToString();
                            SPQuery qry = new SPQuery();
                            qry = new SPQuery();
                            //qry.ViewFields = "<FieldRef Name='ID' /><FieldRef Name='Title' />";
                            qry.Query = @"<Where><Eq><FieldRef Name='Title' /><Value Type='Text'>" + txtAction +
                        "</Value></Eq></Where>";

                            SPListItemCollection myItems = lstType.GetItems(qry);
                            if (myItems.Count > 0)
                            {
                                itemType = myItems[0];
                                item[fld.Title] = itemType.ID;

                            }
                            else
                                w.Write(txtAction);
                        }
                        else
                        {
                            SPFieldLookupValue custID = item[fld.Title] as SPFieldLookupValue;//获取自定义字段的值 
                            if (custID == null)
                            {
                                custID = (item[fld.Title] as SPFieldLookupValueCollection)[0];
                            }

                            try
                            {
                                itemType = lstType.GetItemById(custID.LookupId);//查阅项数据行
                            }
                            catch (Exception ex)
                            {
                                w.Write("itemIDitem.ID" + " :lookupID: " + custID.LookupId + ":value:" + custID.LookupValue);
                                itemType = null;
                            }

                        }
                        if (itemType != null)
                        {
                            actiType = GetActivityType(itemType, fldActionTypeDispName);
                            item[fldTypeTitle] = actiType;
                            item.SystemUpdate();
                        }
                        System.Threading.Thread.Sleep(1);//没什么意思，单纯的执行延时
                        i = j + 1;
                        SetTextMessage(i, lstAssistant.ItemCount);// 100 * i / lstAssistant.ItemCount );

                        w.WriteLine(i.ToString());

                    }
                }
            }
            w.Close();
            w.Dispose();
            
            
            }
        #endregion
        #region 事件
       
        private void btnFillData_Click(object sender, EventArgs e)
        {

            //Thread fThread = new Thread(new ThreadStart(SleepT));//开辟一个新的线程
            //fThread.Start();
            //lblMsg.Text = "完成填充！";
            SleepT();
            MessageBox.Show("ok");
            //btnFillData.Enabled = true;
        }
        #endregion
        #region 方法
        private string GetActivityType(SPListItem itemType, string fldActionTypeDispName)
        {
            string actiType;
            SPFieldLookupValue fldValue = itemType[fldActionTypeDispName] as SPFieldLookupValue;
            if (fldValue == null)
            {
                fldValue = new SPFieldLookupValue(itemType[fldActionTypeDispName].ToString());
            }

            actiType = fldValue.LookupValue;
            return actiType;
        }
        #endregion
    }

}
