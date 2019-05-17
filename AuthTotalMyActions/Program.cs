using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.SharePoint;


namespace AuthTotalMyActions
{
    class Program
    {
        //每天定时统计用户的操作
        static void Main(string[] args)
        {
            string webUrl = ConfigurationManager.AppSettings["webUrl"];
            string listMyAction = ConfigurationManager.AppSettings["listMyAction"];//我的操作
            string listAction = ConfigurationManager.AppSettings["listAction"];// 操作
            GetAllUsers(webUrl);
        }
        private static string siteUrl = "http://localhost";
        /// <summary>
        /// 将操作表中的数据 写入我的操作
        /// </summary>
        /// <param name="spWeb"></param>
        private static void WriteMyAction(SPWeb spWeb,int userID)
        {
            string listMyAction = ConfigurationManager.AppSettings["listMyAction"];//我的操作
            string listAction = ConfigurationManager.AppSettings["listAction"];// 操作
            SPList lstActions = spWeb.Lists.TryGetList(listAction);
            SPList lstMyActions = spWeb.Lists.TryGetList(listMyAction);
            if (lstActions !=null )
            {
                SPListItem newItem;
                foreach (SPListItem item in lstActions.Items  )
                {
                    SPQuery qry = new SPQuery();
                    qry.ViewFields = "<FieldRef Name='ID' /><FieldRef Name='User' /><FieldRef Name='ActionID' /><FieldRef Name='Frequency' />";

                    qry.Query = @"<Where><And><Eq><FieldRef Name='User' LookupId='True' /><Value Type='Integer'>" + userID + 
                        "</Value></Eq><Eq><FieldRef Name='ActionID' LookupId='True' /><Value Type='Lookup'>" + item["ID"].ToString () + "</Value></Eq></And></Where>";

                    SPListItemCollection myItems = lstMyActions.GetItems(qry);
                    if (myItems.Count ==0)//没有此操作则添加
                    {
                        newItem = lstMyActions.Items.Add();
                        newItem["ActionID"] = item.ID;
                        newItem["User"] = userID;
                        newItem["Frequency"] = 0;
                        newItem.Update();  
                    }

                }
            }

        }
        //统计我的操作的频次
        private static void CaculateMyAction(SPWeb spWeb, int userID)
        {
            string listMyAction = ConfigurationManager.AppSettings["listMyAction"];//我的操作
            string listAction = ConfigurationManager.AppSettings["listAction"];// 操作
            SPList lstActions = spWeb.Lists.TryGetList(listAction);
            SPList lstMyActions = spWeb.Lists.TryGetList(listMyAction);
            //获取当前指定用户的活动
            DataTable dtActivity = GetActivityByUser(userID, spWeb.ServerRelativeUrl );
            //dtActivity.Compute("count(Action)","");//只能汇总一个值
            SPQuery qry;
            SPItem newItem;
            var query = from t in dtActivity.AsEnumerable()
                        group t by new { t1 = t.Field<string>("Action") } into m
                        select new
                        {
                            action = m.Key.t1,
                            times = m.Count()
                        };
            if (query.ToList().Count > 0)
            {
                query.ToList().ForEach(q =>
                {
                   //遍历统计的结果 Console.WriteLine(q.action + "," + q.times);
                    qry = new SPQuery();
                    qry.ViewFields = "<FieldRef Name='ID' /><FieldRef Name='User' /><FieldRef Name='ActionID' /><FieldRef Name='Frequency' />";

                    qry.Query = @"<Where><And><Eq><FieldRef Name='User' LookupId='True' /><Value Type='Integer'>" + userID +
                        "</Value></Eq><Eq><FieldRef Name='ActionID'/><Value Type='Lookup'>" + q.action + "</Value></Eq></And></Where>";
                    SPListItemCollection myItems = lstMyActions.GetItems(qry);
                    if (myItems.Count == 0)//没有此操作则添加
                    {
                        qry = new SPQuery();
                        qry.ViewFields = "<FieldRef Name='ID' /><FieldRef Name='Title' />";
                        qry.Query = @"<Where><Eq><FieldRef Name='Title' /><Value Type='Text'>" + q.action +
                        "</Value></Eq></Where>";
                        SPListItemCollection itmActions = lstActions.GetItems(qry);
                        SPListItem itmActin;
                        if (itmActions.Count > 0)
                            itmActin = itmActions[0];
                        else//将没有的操作加到操作表中
                        {
                            itmActin = lstActions.Items.Add();
                            itmActin["Title"] = q.action;
                            itmActin.Update();
                        }
                        newItem = lstMyActions.Items.Add();
                        newItem["ActionID"] = itmActin.ID;
                        newItem["User"] = userID;
                        newItem["Frequency"] =q.times ;
                        newItem.Update();
                    }
                    else//更新频次
                    {
                        newItem = myItems[0];
                        newItem["Frequency"] = q.times;
                        newItem.Update();
                    }
                });
            }
           

        }
        /// <summary>
        /// //将所有操作写每入每个人的操作中
        /// 统计每个人的操作频次
        /// 将操作频次更新到我的操作
        /// </summary>
        /// <param name="webUrl"></param>
        private static void GetAllUsers(string webUrl)
        {
            string[] users = ConfigurationManager.AppSettings["userAccounts"].Split(';');// 操作
            using (SPSite spSite = new SPSite(siteUrl)) //找到网站集
            {
                using (SPWeb spWeb = spSite.OpenWeb(webUrl))
                {
                    foreach (string user in users)
                    {
                        int userID = GetAuthorID(user, spWeb);
                        WriteMyAction(spWeb, userID);//
                        CaculateMyAction(spWeb, userID);
                    }
                }
            }

        }
        private static DataTable GetAllUsers(DataTable dt)
        {
            DataTable dataTable = dt.Copy();

            DataView dataView = dataTable.DefaultView;

            DataTable dtUsers = dataView.ToTable(true, "Author");//注：其中ToTable（）的第一个参数为是否DISTINCT

            return dtUsers;
        }
        //获取用户的活动数据所有,按每个活动用户进行解析
        private static DataTable GetActivityByUser(  int userID, string webUrl)
        {
            string listName = ConfigurationManager.AppSettings["listMyActivity"];//活动列表

            string siteUrl = "http://localhost";
            DataTable retDataTable = null;
            using (SPSite spSite = new SPSite(siteUrl)) //找到网站集
            {
                using (SPWeb spWeb = spSite.OpenWeb(webUrl))
                {
                    SPList spList = spWeb.Lists.TryGetList(listName);

                    if (spList != null)
                    {
                        SPQuery qry = new SPQuery();
                        qry.ViewFields = "<FieldRef Name='Action' /><FieldRef Name='Title' /><FieldRef Name='ID' /><FieldRef Name='Author' />";
                        qry.Query = @"<Where>
      <Eq><FieldRef Name='Author' LookupId='True' /><Value Type='Integer'>" + userID + "</Value></Eq></Where>";
   
                        SPListItemCollectionPosition lstPosi;
                        retDataTable = spList.GetDataTable(qry, SPListGetDataTableOptions.RetrieveLookupIdsOnly, out lstPosi);

                    }
                }
            }
            return retDataTable;
        }
        private static int GetAuthorID(string account ,SPWeb myWeb)
        {
            int id = 0;
            try
            {
                SPUser s = myWeb.EnsureUser(account);
                id = s.ID;
            }
            catch
            {

            }
            return id;
        }
    }
}
