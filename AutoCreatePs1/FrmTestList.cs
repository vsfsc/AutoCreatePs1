using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoCreatePs1
{
    public partial class FrmTestList : Form
    {
        public FrmTestList()
        {
            InitializeComponent();

        }
        

        private void btnTest_Click(object sender, EventArgs e)
        {
            //testCalendar(DateTime.Parse ( dateTimePicker1.Value.ToShortDateString()) );
            //dayEvent(DateTime.Parse(dateTimePicker1.Value.ToShortDateString()));
            //GetCurrentDateActivity(int.Parse (txtID.Text), DateTime.Parse(dateTimePicker1.Value.ToShortDateString()));
            WriteCalendar();
        }

        private void WriteCalendar()
        {
            using (SPSite site = new SPSite("http://localhost"))
            {
                using (SPWeb web = site.AllWebs[txtWebUrl.Text])
                {
                   SPList calendar = web.Lists[txtListName.Text.Trim()] ;
                    SPListItem item = calendar.GetItemById(9);
                    item["全天事件"] = true;
                    item["开始时间"] = DateTime.Today;
                    item["结束时间"] = DateTime.Today;
                    item.Update();

                    txtResult.Text = "ok";
                }
            }
        }
        private void dayEvent(DateTime dt)
        {
            using (SPSite site = new SPSite("http://localhost"))
            {
                using (SPWeb web = site.AllWebs[txtWebUrl.Text])
                {
                    //dt = dt.AddHours(12);
                    //string strNow = SPUtility.CreateISO8601DateTimeFromSystemDateTime(dt);
                    SPList calendar = web.Lists[txtListName.Text.Trim()];
                    SPQuery caml = new SPQuery();
                    int userId = int.Parse(txtID.Text);//<And>
                    caml.Query = @"<Where>
                                    <And>
                                     <Eq>
                                        <FieldRef Name='Author' LookupId='True' />
                                        <Value Type='Integer'>" + userId + @"</Value>
                                     </Eq>
                                        <DateRangesOverlap>
                                            <FieldRef Name='EventDate' />;
                                            <FieldRef Name='EndDate' />
                                            <FieldRef Name='RecurrenceID' />
                                            <Value Type='DateTime'><Today /></Value>
                                        </DateRangesOverlap>
                                    </And>
                                </Where>";

                    caml.ExpandRecurrence = true;
                    caml.CalendarDate = dt;//Now
                                           //caml.Query = @"<Where>
                                           //                <DateRangesOverlap>
                                           //                    <FieldRef Name='EventDate' />;
                                           //                    <FieldRef Name='EndDate' />
                                           //                    <FieldRef Name='RecurrenceID' />
                                           ////                    <Value Type='DateTime'><Month /></Value>
                                           //                </DateRangesOverlap>
                                           //            </Where>";
                    StringBuilder txt = new StringBuilder();//Year Week
                    SPListItemCollection items = calendar.GetItems(caml);
                    int i = 1;
                    if (items.Count == 0)
                        txt.Append("there is no data");
                    else
                    {
                        foreach (SPListItem item in items)
                        {
                            int t1 = DateTime.Compare((DateTime)item["EndDate"], dt);
                            int t2 = DateTime.Compare((DateTime)item["EventDate"], dt);
                            if (t1 >= 0)//2019/1/11 23:59会在1月12日中
                            {
                                txt.AppendLine(i.ToString() + "  " + item["EventDate"].ToString() + "  " + item["EndDate"].ToString()+" "+ item["Author"].ToString ());
                                i = i + 1;

                            }
                        }

                    }
                    txtResult.Text = txt.ToString();
                }
            }
        }
        private void testCalendar(DateTime dt)
        {
            using (SPSite mySite = new SPSite("http://localhost"))
            {
                SPWeb myWeb = mySite.AllWebs[txtWebUrl.Text];
                if (!myWeb.Exists)
                {
                    MessageBox.Show("子网站不存在");
                    return;
                }
                int userId = myWeb.CurrentUser.ID;
                SPList list = myWeb.Lists[txtListName.Text.Trim()];
                SPListItem myItem = list.Items[0];
                SPListItemCollection items = null;
                if (list != null)
                {
                    StringBuilder txt = new StringBuilder();
                    //foreach (SPListItem item in list.Items)
                    //{
                    //    txt.AppendLine(item["EventDate"].ToString());
                    //    txt.Append("重复:");
                    //    txt.Append(item["重复"].ToString() + "    type:" + list.Fields["重复"].TypeAsString);
                    //    txt.Append("事件类型:");
                    //    txt.Append(item["事件类型"].ToString() + "    type:" + list.Fields["事件类型"].TypeAsString);
                    //    txt.Append("工作区:");
                    //    txt.AppendLine("");
                    //    txt.Append(list.Items[0]["工作区"].ToString());

                    //}
                    //txtResult.Text = txt.ToString();
                    string strNow = SPUtility.CreateISO8601DateTimeFromSystemDateTime(dt.AddHours(12));

                    SPQuery qry = new SPQuery();
                    qry.Query = @"<Where>
                                     <And><Geq>
                                        <FieldRef Name='EndDate' />
                                        <Value Type='DateTime'>" + strNow + @"</Value>
                                     </Geq>
                                     <Leq>
                                        <FieldRef Name='EventDate' />
                                        <Value Type='DateTime'>" + strNow + @"</Value>
                                     </Leq></And>
                               </Where><OrderBy><FieldRef Name='Created' Ascending='true' /></OrderBy>";
                    items = list.GetItems(qry);
                    if (items.Count == 0)
                    {
                        txtResult.Text = "无数据";
                        return;
                    }
                    //foreach(SPField fld in list.Fields)
                    //{
                    //    txt.AppendLine(fld.InternalName+"   Title:  "+ fld.Title );
                    //    txt.Append("类型:");
                    //     txt.Append(fld.TypeAsString);
                    //    txt.AppendLine("");
                    //}
                    foreach (SPListItem item in items)
                    {
                        txt.AppendLine(item["EventDate"].ToString());
                        if (item["RecurrenceData"] != null)
                        {
                            txt.Append("RecurrenceID:");
                            txt.Append(item["RecurrenceID"].ToString());

                            txt.Append("RecurrenceData:");
                            txt.Append(item["RecurrenceData"].ToString());

                        }
                        txt.AppendLine("");

                    }
                    txtResult.Text = txt.ToString();
                }
            }
        }
        //测试活动相关的内容
        private void testActivity(DateTime dt)
        {
            using (SPSite mySite = new SPSite("http://localhost"))
            {
                SPWeb myWeb = mySite.AllWebs[txtWebUrl.Text];
                if (!myWeb.Exists)
                {
                    MessageBox.Show("子网站不存在");
                    return;
                }
                int userId = myWeb.CurrentUser.ID;
                SPList list = myWeb.Lists[txtListName.Text.Trim()];
                SPListItem myItem = list.Items[0];
                SPListItemCollection items = null;
                if (list != null)
                {
                    SPQuery qry = new SPQuery();
                    qry.Query = @"<Where>
                                     <Geq>
                                        <FieldRef Name='Date' />
                                        <Value Type='DateTime'>" + dt.ToString("yyyy-MM-dd") + @"</Value>
                                     </Geq>
                               </Where><OrderBy><FieldRef Name='Created' Ascending='true' /></OrderBy>";
                    items = list.GetItems(qry);
                    if (items.Count == 0)
                    {
                        txtResult.Text = "无数据";
                        return;
                    }
                    DataTable dtItems = items.GetDataTable();
                    StringBuilder txt = new StringBuilder();
                    txt.AppendLine(dtItems.Rows[0]["Date"].ToString());
                    txt.AppendLine(dtItems.Rows[0]["TaskID"].ToString());
                    txt.AppendLine(dtItems.Columns["TaskID"].DataType.ToString());
                    DataRow[] drs = dtItems.Select("Date='" + dt.ToString("yyyy-MM-dd") + "'");
                    txt.AppendLine("Date='" + dt.ToString("yyyy-MM-dd") + "' count: " + drs.Length);
                    txtResult.Text = txt.ToString();

                }

            }
        }

        private void btnUser_Click(object sender, EventArgs e)
        {
            using (SPSite mySite = new SPSite("http://localhost"))
            {
                SPWeb myWeb = mySite.AllWebs[txtWebUrl.Text];
                if (!myWeb.Exists)
                {
                    MessageBox.Show("子网站不存在");
                    return;
                }
                foreach (SPUser user in myWeb.Users)
                {
                    if (user.LoginName.Contains(txtName.Text))
                        txtID.Text = user.ID.ToString();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //SPSite mySite = new SPSite("http://localhost");

            //SPWeb myWeb = mySite.AllWebs[txtWebUrl.Text];
            //txtResult.Text = myWeb.Name;
            if (txtID.Text == "")
            {
                MessageBox.Show("Account不能为空");
                return;
            }
            txtResult.Text = "begin……";
            InitGridView();
        }

        #region 任务文档
        private SPListItemCollection GetCurrentDateActivity(int userId, DateTime time)
        {
            SPSite site = new SPSite("http://localhost");
            SPWeb web = site.OpenWeb (txtWebUrl.Text );
            SPList list = web.Lists.TryGetList(webObj.ActivityList);
            string dateFld = "Date";
            SPListItemCollection items = null;
            if (list != null)
            {
                SPQuery qry = new SPQuery();
                qry.Query = @"<Where>
                                  <And>
                                     <Eq>
                                        <FieldRef Name='Author' LookupId='True' />
                                        <Value Type='Integer'>" + userId + @"</Value>
                                     </Eq>
                                     <Eq>
                                        <FieldRef Name='" + dateFld + @"' />
                                        <Value Type='DateTime'>" + time.ToString("yyyy-MM-dd") + @"</Value>
                                     </Eq>
                                  </And>
                               </Where><OrderBy><FieldRef Name='Created' Ascending='true' /></OrderBy>";
                items = list.GetItems(qry);
                DataTable  dt = items.GetDataTable().Clone();
            }
            return items;
        }
        private List<SPListItem> GetCalendarActivity(string calendarList, int userId, DateTime dtCurrDate)
        {
            string strNow = SPUtility.CreateISO8601DateTimeFromSystemDateTime(dtCurrDate.AddHours(12));
            List<SPListItem> retItems = new List<SPListItem>();

            using (SPSite mySite = new SPSite("http://localhost"))
            {
                using (SPWeb myWeb = mySite.OpenWeb(txtWebUrl.Text ))
                {
                    SPList taskList = myWeb.Lists.TryGetList(calendarList);
                    SPQuery qry = new SPQuery();
                    qry.Query = @"<Where>
                                    <And>
                                     <Eq>
                                        <FieldRef Name='Author' LookupId='True' />
                                        <Value Type='Integer'>" + userId + @"</Value>
                                     </Eq>
                                        <DateRangesOverlap>
                                            <FieldRef Name='EventDate' />;
                                            <FieldRef Name='EndDate' />
                                            <FieldRef Name='RecurrenceID' />
                                            <Value Type='DateTime'><Today /></Value>
                                        </DateRangesOverlap>
                                    </And>
                                </Where>";

                    qry.ExpandRecurrence = true;
                    qry.CalendarDate = dtCurrDate;//Now
                    SPListItemCollection items = taskList.GetItems(qry);
                    List<string> types = new List<string>() { "节假日", "工作日" };
                    if (items.Count > 0 && items[0]["Category"] != null)
                    {
                        foreach (SPListItem item in items)
                        {
                            int t1 = DateTime.Compare((DateTime)item["EndDate"], dtCurrDate);
                            if (t1 >= 0 && item["Category"] != null && !types.Contains(item["Category"].ToString()))
                                retItems.Add(item);

                        }
                    }

                }
            }
            return retItems;
        }
    
        private void InitGridView()
        {
            DateTime currDate = DateTime.Parse ( dateTimePicker1.Value.ToShortDateString());
            List<DateTime> retDts = new List<DateTime>(); 
            int dtype = GetDaysType("日常任务",new List<DateTime> { currDate },0,  ref retDts );
            chkDayType.Checked = dtype== 1 ? true :false;
            chkDayType.Text = dtype== 1 ? "工作日" :"节假日";

           
                int userId = int.Parse (txtID.Text );
                DataTable dtSource;
                DataTable dtResult = CreateData();
                SPListItemCollection items = GetCurrentDateActivity(userId, currDate);
                SPFieldLookupValue actionID;
                List<string> actions = new List<string>();
                //int durings = 0;
                int icount = 1;
            foreach (SPListItem item in items)
            {
                DataRow dr = dtResult.Rows.Add();
                actionID = new SPFieldLookupValue(item["ActionID"].ToString());
                dr["ID"] = icount.ToString() + "_" + actionID.LookupId.ToString();
                dr["操作"] = actionID;
                actions.Add(actionID.LookupValue);
                dr["频次"] = 0;
                dr["说明"] = item["Desc"];
                if (item["Quantity"] != null)
                    dr["数量"] = item["Quantity"];
                if (item["During"] != null)
                    dr["时长"] = item["During"];
                //if (item["During"] != null)
                //    durings += int.Parse(item["During"].ToString ());
                if (item["TaskID"] != null)
                    dr["TaskID"] = new SPFieldLookupValue(item["TaskID"].ToString()).LookupId;
                else
                    dr["TaskID"] = 0;
                dr["Flag"] = 1;
                dr["ItemID"] = item.ID;
                dr["ActivityDate"] = item["创建时间"];
                icount = icount + 1;
            }
                dtResult.AcceptChanges();

                #region //读取日历中的活动
                if (dtResult.Rows.Count < 30)
                {
                    List<SPListItem> calendarItems = GetCalendarActivity(webObj.CalendarList, userId, currDate);
                    bool isNew = true;
                    foreach (SPListItem calenItem in calendarItems)
                    {
                        isNew = true;
                        TimeSpan subValue = ((DateTime)calenItem["EndDate"]).Subtract((DateTime)calenItem["EventDate"]);
                        int during = subValue.Minutes + subValue.Hours * 60;
                        if (items.Count > 0)//当日有活动
                        {
                            foreach (SPListItem tmp in items)
                            {
                                if (tmp["Flag"] != null && tmp["Flag"].ToString() == "1" && tmp["ActionID"].ToString() == calenItem["ActionID"].ToString())
                                {

                                    if (tmp["Desc"] != null && tmp["Desc"].ToString().Contains(((DateTime)calenItem["EventDate"]).ToShortTimeString()))
                                    {
                                        isNew = false;
                                        break;
                                    }

                                }

                            }
                         if (isNew)
                        {
                            if (calenItem["ActionID"] == null)//日历中活动的操作不能为空
                                continue;
                            DataRow dr = dtResult.Rows.Add();
                            actionID = new SPFieldLookupValue(calenItem["ActionID"].ToString());
                            dr["ID"] = icount.ToString() + "_" + actionID.LookupId.ToString();
                            dr["操作"] = actionID;
                            actions.Add(actionID.LookupValue);
                            dr["频次"] = calenItem.ID;
                            string des = "";
                            if (calenItem["Title"] != null)
                                des = des + calenItem["Title"];
                            if (calenItem["Location"] != null)
                                des = des + calenItem["Location"];
                            if (calenItem["Description"] != null)
                                des = des + calenItem["Description"];
                            dr["说明"] = des + ((DateTime)calenItem["EventDate"]).ToShortTimeString();
                            dr["时长"] = during;

                            dr["TaskID"] = GetTaskIDByAction(actionID.LookupId, currDate);
                            dr["Flag"] = 2;
                            dr["ItemID"] = 0;// calenItem.ID;
                            dr["ActivityDate"] = calenItem["创建时间"];
                            icount = icount + 1;
                        }
                    }
                }
                #endregion

                if (dtResult.Rows.Count < 30)
                {

                    DateTime dtFrom = currDate.AddDays(-webObj.BeforeDays - 7);//在基础上又延长7天
                    List<DateTime> retdts = new List<DateTime>();
                    //获取工作日的具体日期
                    GetDaysType("日常任务", new List<DateTime> { dtFrom ,currDate.AddDays(-1) }, chkDayType.Checked ? 1 : 0,ref retdts);
                    
                    dtSource = GetPreData(userId, retdts  );//操作不应该重复
                    if (dtSource != null)
                    {
                        DataTable dt = GetDistTable(dtSource, "ActionID", 30 - dtResult.Rows.Count, actions, currDate);
                        dt.DefaultView.Sort = " Date desc,ActivityDate ";
                        dt.DefaultView.RowFilter = "Flag=3";
                        dtResult.Merge(dt.DefaultView.ToTable());
                        dt.DefaultView.RowFilter = "Flag=4";
                        dtResult.Merge(dt.DefaultView.ToTable());
                    }
                }
                //DataBind(dtResult );
                //ViewState["dtResult"] = dtResult;
                ////计算时间和条数
                //Caculate();
                txtResult.Text = "ok" +"    "+dtResult.Rows.Count.ToString ();
            }
            else
            {
                //lblMsg.Text = "您尚未登录，无法快速录入活动！";
            }
        }
        private int GetDaysType(string calendarList, List<DateTime> dts, int retDayType, ref List<DateTime> retDt)
        {
            int dayType = -1;
            string strDt;
            List<DateTime> retDates = new List<DateTime>();
           
                 using (SPSite mySite = new SPSite("http://localhost"))
                 {
                     using (SPWeb myWeb = mySite.OpenWeb(txtWebUrl.Text ))
                     {
                         SPList taskList = myWeb.Lists.TryGetList(calendarList);
                         SPQuery qry = new SPQuery();
                         for (DateTime dt1 = dts[dts.Count - 1]; dt1 >= dts[0]; dt1 = dt1.AddDays(-1))
                         {
                             dayType = -1;
                             //strDt = SPUtility.CreateISO8601DateTimeFromSystemDateTime(dt1.AddHours(12));
                             qry = new SPQuery();
                             qry.Query = @"<Where>
                                        <DateRangesOverlap>
                                            <FieldRef Name='EventDate' />;
                                            <FieldRef Name='EndDate' />
                                            <FieldRef Name='RecurrenceID' />
                                            <Value Type='DateTime'><Today /></Value>
                                        </DateRangesOverlap>
                                     </Where>";

                             qry.ExpandRecurrence = true;
                             qry.CalendarDate = dt1;//Now
                             SPListItemCollection items = taskList.GetItems(qry);
                             foreach (SPListItem item in items)
                             {
                                 int t1 = DateTime.Compare((DateTime)item["EndDate"], dt1);
                                 if (t1 >= 0 && items[0]["Category"] != null)
                                 {
                                     if (items[0]["Category"].ToString() == "节假日")
                                     { dayType = 0; break; }
                                     else if (items[0]["Category"].ToString() == "工作日")
                                     { dayType = 1; break; }
                                 }
                             }

                             if (dayType == -1)//日历中没有标记
                             {
                                 if (dt1.DayOfWeek == DayOfWeek.Saturday || dt1.DayOfWeek == DayOfWeek.Sunday)//6,0
                                     dayType = 0;
                                 else
                                     dayType = 1;
                             }
                             if (dts.Count > 1 && dayType == retDayType)
                             {
                                 retDates.Add(dt1);
                                 if (retDates.Count == webObj.BeforeDays)
                                     break;
                             }

                         }
                     }
                 }
            
            retDt = retDates;
            return dayType;
        }
        private SPListItemCollection GetAllActions
        {
            get
            {

                SPSite mySite = new SPSite("http://localhost");
                SPWeb myWeb = mySite.OpenWeb(txtWebUrl.Text);
                SPList list = myWeb.Lists.TryGetList(webObj.ActionList);//先到我的操作表中去找
                SPListItemCollection items = list.Items;
                return items;
            }
        }
        private int GetTaskIDByAction(int actionID, DateTime currDate)
        {
            SPListItemCollection items = GetAllActions;
            SPListItem item = items.GetItemById(actionID);
            string typeName = "";
            if (item["TypeID"] != null)
            {
                SPFieldLookupValueCollection types = (SPFieldLookupValueCollection)item["TypeID"];
                foreach (SPFieldLookupValue type in types)
                    if (type.LookupValue != "项目")
                    {
                        if (type.LookupValue != "工作" && type.LookupValue != "学习")
                            typeName = "生活类";
                        else
                            typeName = type.LookupValue;
                    }
            }
            if (typeName == "")
                typeName = "生活类";
            string taskCurr = GetRoutineDocName(txtName.Text , typeName, currDate);
            int taskID = GetTaskID(taskCurr);
            return taskID;
        }
        private string GetRoutineDocName(string account, string typeName, DateTime dtCurrent)
        {

            GregorianCalendar gc = new GregorianCalendar();
            string titleName = account + dtCurrent.ToString("yyyy") + "年";
            if (typeName == "生活类")
            {
                titleName += dtCurrent.Month + "月";
            }
            else if (typeName == "工作" || typeName == "学习")
            {
                int weekOfYear = gc.GetWeekOfYear(dtCurrent, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
                if (weekOfYear == 53)
                {
                    titleName = account + dtCurrent.AddYears(1).ToString("yyyy") + "年";
                    weekOfYear = 1;//下一年的第一周
                }
                titleName += "第" + weekOfYear + "周";
            }
            return titleName + typeName;
        }
        private int GetTaskID(string taskName)
        {
            string listName = "任务文档";
            SPSite site = new SPSite("http://localhost");
            SPWeb web = site.OpenWeb (txtWebUrl.Text );
            SPList list = web.Lists.TryGetList(listName);
            SPQuery qry = new SPQuery();
            qry.ViewFields = @"<FieldRef Name='Title' /><FieldRef Name='ParentID' />";
            qry.Query = @"<Where><Eq><FieldRef Name='Title' /><Value Type='Text'>" + taskName + "</Value></Eq></Where>";
            SPListItemCollection subItems = list.GetItems(qry);
            if (subItems.Count > 0)
                return subItems[0].ID;
            else
                return 0;
        }
        private DataTable GetDistTable(DataTable dtSource, string distField, int TotalCount, List<string> actions, DateTime dtTime)
        {
            DataTable rTable = CreateData();
            //rTable.Columns.Add("地点");
            DataTable dt1 = dtSource.DefaultView.ToTable(true, distField);
            int rows = dt1.Rows.Count <= TotalCount ? dt1.Rows.Count : TotalCount;//行数不够指定数，以行数为准
            SPFieldLookupValue tValue;
            int taskID = 0;
            string taskTitle;
            if (dtSource.Columns.Contains("TaskID"))
                taskTitle = "TaskID";
            else
                taskTitle = "任务文档";
            //找所有的，之后按频次排序，推出的会超过给出的条数
            int iStart = 30- TotalCount + 1;//为了获取序号，操作允许有重复
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                tValue = new SPFieldLookupValue(dt1.Rows[i][distField].ToString());
                if (actions.Contains(tValue.LookupValue))//以有的操作不加
                    continue;
                else
                    actions.Add(tValue.LookupValue);
                DataRow dr = rTable.NewRow();
                dr["ID"] = iStart.ToString() + "_" + tValue.LookupId;
                dr["操作"] = dt1.Rows[i][distField];
                dr["频次"] = dtSource.Compute("count(" + distField + ")", distField + "='" + dt1.Rows[i][distField] + "'");
                object result = DBNull.Value;
                result = dtSource.Compute("avg(During)", distField + "='" + dt1.Rows[i][distField] + "'");
                if (result != DBNull.Value)
                {
                    decimal during = decimal.Round(decimal.Parse(result.ToString()));
                    during = during - during % 10;
                    dr["时长"] = during;

                }

                dr["Flag"] = 3;
                DataRow[] drs = dtSource.Select(distField + "='" + dt1.Rows[i][distField] + "'", "Created desc");
                tValue = new SPFieldLookupValue(drs[0][taskTitle].ToString());
                dr["TaskID"] = tValue.LookupId;
                dr["Date"] = drs[0]["Date"];
                taskID = GetActivityTaskIDByDate(tValue.LookupValue, dtTime);
                if (taskID > 0)
                    dr["TaskID"] = taskID;
                dr["ItemID"] = 0;
                dr["ActivityDate"] = drs[0]["Created"];
                rTable.Rows.Add(dr);
                iStart = iStart + 1;
            }
            rTable.DefaultView.Sort = "频次 desc";
            rTable = rTable.DefaultView.ToTable();
            if (rTable.Rows.Count > TotalCount)
            {
                for (int i = rTable.Rows.Count - 1; i >= TotalCount; i--)
                    rTable.Rows.RemoveAt(i);
                rTable.AcceptChanges();
            }

            if (rTable.Rows.Count < TotalCount)
            {
                DataTable dtActions = GetBindData(webObj.MyActionList, "");
                int iCount = 0;

                for (int i = rTable.Rows.Count; i < TotalCount; i++)
                {
                    while (dtActions.Rows.Count > iCount && actions.Contains(dtActions.Rows[iCount]["Title"].ToString()))
                        iCount = iCount + 1;
                    if (iCount == dtActions.Rows.Count)
                        break;
                    DataRow dr = rTable.NewRow();
                    dr["ID"] = iStart.ToString() + "_" + dtActions.Rows[iCount]["ID"];

                    dr["操作"] = dtActions.Rows[iCount]["ID"] + ";#" + dtActions.Rows[iCount]["Title"];//[distField];
                    //DataRow[] drs = dtSource.Select(distField + "='" + dt1.Rows[0][distField] + "'");

                    dr["频次"] = 0;
                    dr["时长"] = DBNull.Value;
                    dr["Flag"] = 4;//空的

                    dr["ItemID"] = 0;
                    dr["TaskID"] = GetTaskIDByAction((int)dtActions.Rows[iCount]["ID"], dtTime);
                    rTable.Rows.Add(dr);
                    iCount = iCount + 1;
                    iStart = iStart + 1;
                }
            }

            return rTable;
        }
        private DataTable GetBindData(string listName, string parentId, int userID = 0)
        {
            if (userID == 0)
                userID = int.Parse(txtID.Text );
            DataTable dt = null;
            SPSite site = new SPSite("http://localhost");
            SPWeb web = site.OpenWeb (txtWebUrl.Text );
            SPList list = web.Lists.TryGetList(listName);//先到我的操作表中去找
            SPQuery qry = new SPQuery
            {
                DatesInUtc = false
            };
            List<string> actions = new List<string>();
            if (list != null)
            {
                qry.Query = "<Where><Eq><FieldRef Name='Author' LookupId='True' /><Value Type='Integer'>" + userID + @"</Value></Eq></Where><OrderBy><FieldRef Name='Frequency' Ascending='FALSE' /></OrderBy>";
                dt = list.GetItems(qry).GetDataTable();
                SPListItemCollection items = list.GetItems(qry);
                SPListItem item;
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        item = items.GetItemById((int)dr["ID"]);
                        SPFieldLookupValue actionID = new SPFieldLookupValue(item["ActionID"].ToString());
                        dr["ID"] = actionID.LookupId;
                        dr["Title"] = actionID.LookupValue;
                        actions.Add(actionID.LookupValue);
                    }
                    dt.AcceptChanges();
                }
            }
            list = web.Lists.TryGetList(webObj.ActionList);
            if (dt == null || dt.Rows.Count < list.ItemCount)// 我的操作设置了部分操作
            {
                qry = new SPQuery();
                qry.Query = "<OrderBy><FieldRef Name='Frequency' Ascending='FALSE' /></OrderBy>";
                DataTable dtALL = list.GetItems(qry).GetDataTable();
                if (dt == null)
                    dt = dtALL.Copy();
                else
                {
                    foreach (DataRow drTemp in dtALL.Rows)
                    {
                        if (!actions.Contains(drTemp["Title"].ToString()))
                        {
                            DataRow dr = dt.NewRow();
                            dr["ID"] = drTemp["ID"];
                            dr["Title"] = drTemp["Title"];
                            dr["Frequency"] = drTemp["Frequency"];
                            dt.Rows.Add(dr);
                        }
                    }
                }
            }

            dt.DefaultView.Sort = "Frequency desc";
            dt = dt.DefaultView.ToTable();
            return dt;
        }
        private int GetActivityTaskIDByDate(string taskName, DateTime currDate)
        {
            int taskID = 0;

            if (taskName.StartsWith(txtName.Text ))//只处理Routine任务
            {
                string typeName = taskName.Substring(taskName.Length - 3);
                if (typeName != "生活类")
                    typeName = typeName.Substring(1);
                string taskCurr = GetRoutineDocName(txtName.Text , typeName, currDate);
                taskID = GetTaskID(taskCurr);
            }
            return taskID;
        }
        
        private DataTable CreateData()
        {
            DataTable rTable = new DataTable();
            rTable.Columns.Add("ID", typeof(string));
            rTable.Columns.Add("操作");
            rTable.Columns.Add("频次");
            rTable.Columns.Add("时长", typeof(double));
            rTable.Columns.Add("数量", typeof(double));
            rTable.Columns.Add("说明");
            rTable.Columns.Add("TaskID", typeof(int));
            rTable.Columns.Add("Flag", typeof(int)).DefaultValue = 0;
            rTable.Columns.Add("ItemID", typeof(int));
            rTable.Columns.Add("ActivityDate", typeof(DateTime));//同一天的按时间降序
            rTable.Columns.Add("Date", typeof(DateTime));//日期，
            return rTable;
        }
        private DataTable GetPreData(int userId, List<DateTime> dtDate)
        {
            DataTable dt = new DataTable();
            SPSite site = new SPSite("http://localhost");
                SPWeb web = site.OpenWeb (txtWebUrl.Text );
                SPList list = web.Lists.TryGetList("活动");

                if (list != null)
                {
                    //查找当前用户一周内的所有活动操作，去重并按照频率由高到低排序，选取前25个操作，不够25个选择全部
                    SPListItemCollection items;
                    //获取最小的日期
                    DateTime time = dtDate[dtDate.Count - 1];// dtDate .AddDays(-webObj.BeforeDays );将参数的日期改过日期列表，去除掉非工作日
                    string timeStr = SPUtility.CreateISO8601DateTimeFromSystemDateTime(time);
                    SPQuery qry = new SPQuery();
                    SPField field;
                    string dateFld = "ActualDate";
                    if (list.Fields.ContainsFieldWithStaticName("CustAction"))
                    {
                        field = list.Fields.GetFieldByInternalName("CustAction");
                        //ViewState["CustAction"] = field.InternalName; ;
                        dateFld = "ActualDate";
                    }
                    else if (list.Fields.ContainsFieldWithStaticName("ActionID"))
                    {
                        field = list.Fields.GetFieldByInternalName("ActionID");
                        //ViewState["CustAction"] = field.InternalName;//由Title改为InternalName
                        dateFld = "Date";
                    }
                    string viewFld = "<FieldRef Name='ID' /><FieldRef Name='Quantity' /><FieldRef Name='During' /><FieldRef Name='Date' /><FieldRef Name='Title' /><FieldRef Name='ActionID' /><FieldRef Name='Desc' /><FieldRef Name='TaskID' />";
                    qry.ViewFields = viewFld;
                    qry.Query = @"<Where>
                                  <And>
                                     <Eq>
                                        <FieldRef Name='Author' LookupId='True' />
                                        <Value Type='Integer'>" + userId + @"</Value>
                                     </Eq>
                                     <Geq>
                                        <FieldRef Name='" + dateFld + @"' />
                                        <Value Type='DateTime'>" + time.ToString("yyyy-MM-dd") + @"</Value>
                                     </Geq>
                                  </And>
                               </Where>";

                    items = list.GetItems(qry);
                    if (items.Count <= 0)//没有该用户的记录,则用所有用户的记录来代替
                    {
                        qry = new SPQuery();
                        qry.ViewFields = viewFld;
                        qry.Query = @"<Where>
                                         <Geq>
                                            <FieldRef Name='" + dateFld + @"' />
                                            <Value Type='DateTime'>" + time.ToString("yyyy-MM-dd") + @"</Value>
                                         </Geq>
                               </Where>";
                        items = list.GetItems(qry);
                    }
                    dt = items.GetDataTable();// GetCamlDataListRetTable(items);
                    if (dt.Rows.Count > 0)
                    {
                        DataTable dtTmp = dt.Clone();

                        DataRow[] drs;
                        string sql;
                        SPListItem item;
                        foreach (DateTime dateTmp in dtDate)
                        {
                            sql = dateFld + "='" + dateTmp.ToString("yyyy-MM-dd") + "'";
                            drs = dt.Select(sql, "Created");
                            foreach (DataRow dr in drs)
                            {
                                item = items.GetItemById((int)dr["ID"]);
                                dr["ActionID"] = item["ActionID"];
                                dr["TaskID"] = item["TaskID"];
                                dr.AcceptChanges();
                                dtTmp.ImportRow(dr);
                            }
                        }

                        dt = dtTmp.Copy();
                    }
                }
                else
                {
                    dt = null;
                }
           

            return dt;
        }
        #endregion

        private void button2_Click(object sender, EventArgs e)
        {
            using (SPSite mySite = new SPSite("http://localhost"))
            {
                SPWeb myWeb = mySite.AllWebs[txtWebUrl.Text];
                if (!myWeb.Exists)
                {
                    MessageBox.Show("子网站不存在");
                    return;
                }

                SPList list = myWeb.Lists[txtListName.Text.Trim()];
                //list.Delete();
                //MessageBox.Show("ok");
                //MessageBox.Show(list.DefaultViewUrl);
            }
        }

        private void btnSentences_Click(object sender, EventArgs e)
        {
            SeprateSentences(txtResult.Text);
            //SeperateWords(txtResult.Text);
            //test();
        }
        private void test()
        {
            string text = "a|e|s|v"; 
            Regex rx = new Regex(@"(e\S$)");//(a|e|s)

            MatchCollection matchs = rx.Matches(text);
            foreach (Match match in matchs )
            {
                int i = match.Index;
                Console.WriteLine(match.Value);
            }

            Console.WriteLine(matchs.Count );
        }
        private void  SeprateSentences(string strText)
        {
            string ignore = "[\r\n\t\"]";//
            strText = Regex.Replace(strText, ignore,"");



            Regex rx = new Regex(@"(\S.+?[.!?])(?=\s+|$)");
            foreach (Match match in rx.Matches(strText))
            {
                int i = match.Index;
                Console.WriteLine(match.Value);
            }
        }
        private void SeperateWords(string strText)
        {
             Regex reg = new  Regex(@"\b\w+\b");
             MatchCollection mc = reg.Matches(strText);
            foreach ( Match m in mc)
            {
                Console.WriteLine(m.Value);
            }

        }
    }
    public class webObj
    {
        public static int BeforeDays = 7;
        public static string MyActionList = "用户-操作";
        public static string CalendarList = "日常任务";
        public static string ActionList = "操作";
        public static string ActivityList = "活动";
    }
}
