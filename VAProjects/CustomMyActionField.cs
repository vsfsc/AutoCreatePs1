using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace VAProjects
{
    public class CustomMyActionField : SPFieldChoice
    {
        public CustomMyActionField(SPFieldCollection fields, string fieldName)
              : base(fields, fieldName)
        {
            InitValues();
            //InitActionField();
        }
        //此方法没有用
        public CustomMyActionField(SPFieldCollection fields, string typeName, string displayName)
             : base(fields, typeName, displayName)
        {
            InitValues();
            //InitActionField();
        }
        #region 测试
        //通过自定义的字段来更新操作字段，填写我的操作内容
        private void InitActionField()
        {
            string fldName = "Action";
            SPSecurity.RunWithElevatedPrivileges(delegate
            {
                using (SPSite site = new SPSite(this.ParentList.ParentWeb.Site.ID))
                {
                    using (SPWeb web = site.OpenWeb(this.ParentList.ParentWeb.ID))
                    {
                        SPList lstAction = web.Lists[this.ParentList.Title];
                        web.AllowUnsafeUpdates = true;
                        try
                        {
                            if (lstAction.Fields.ContainsFieldWithStaticName(fldName))
                            {
                                SPFieldChoice fldAction = lstAction.Fields.GetFieldByInternalName(fldName) as SPFieldChoice;
                                fldAction.Choices.Clear();
                                //fldAction.Choices.Add(SPContext.Current.Web.CurrentUser.Name + 1);
                                //fldAction.Choices.Add(SPContext.Current.Web.CurrentUser.Name + 2);
                                //fldAction.Choices.Add(SPContext.Current.Web.CurrentUser.Name + 3);
                                //fldAction.Choices.Add(SPContext.Current.Web.CurrentUser.Name + 4);
                                try
                                {
                                    SPListItemCollection splistItems = GetSPItems("我的操作");
                                    foreach (SPListItem item in splistItems)
                                    {
                                        string Action = item["操作"].ToString();//此时的数据格式为：“ID;#查阅字段值”，需要做拆分处理
                                        Action = Action.Split('#')[1];
                                        fldAction.AddChoice(Action);
                                    }
                                    //fldAction.Choices.Add(items.Count.ToString());
                                }
                                catch
                                { }
                                fldAction.Update();
                            }
                            #region SPFieldChoice 
                        }
                        catch (Exception ex)
                        { }
                        #endregion
                        web.AllowUnsafeUpdates = false;
                    }
                }
            });
        }
        private void InitValues()
        {
            this.Choices.Clear();
            //this.Choices.Add(SPContext.Current.Web.CurrentUser.Name + 1);
            //this.Choices.Add(SPContext.Current.Web.CurrentUser.Name + 2);
            //this.Choices.Add(SPContext.Current.Web.CurrentUser.Name + 3);
            //this.Choices.Add(SPContext.Current.Web.CurrentUser.Name + 4);
            SPListItemCollection splistItems = GetSPItems("我的操作");
            foreach (SPListItem item in splistItems)
            {
                string Action = item["操作"].ToString();//此时的数据格式为：“ID;#查阅字段值”，需要做拆分处理
                Action = Action.Split('#')[1];
                this.Choices.Add(Action);
            }
            this.FillInChoice = true;

        }
        #endregion
        public override string DefaultValue //设置字段的默认值
        {
            get
            {  
                //this.ParentList
                return "";
            }
        }
        #region 查询我的操作列表来对列表进行填充
        /// <summary>
        /// 查询一个列表，将其某一列数据作为另一个列表的选项列的选项
        /// </summary>
        /// <param name="subWebUrl">列表所在子网站</param>
        /// <param name="fromList">选项来源列表</param>
        /// <param name="ToList">写入选项的列表</param>
        private static void AddChoice(string subWebUrl, string srcList, string desList)
        {
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.ID))
                    {
                        using (SPWeb spWeb = site.OpenWeb(subWebUrl))
                        {
                            SPList list = spWeb.Lists[desList];
                            SPFieldChoice fieldchoice = (SPFieldChoice)list.Fields["操作"];
                            fieldchoice.Choices.Clear();
                            SPListItemCollection splistItems = GetSPItems(srcList);
                            if (splistItems != null)
                            {
                                spWeb.AllowUnsafeUpdates = true;
                                foreach (SPListItem item in splistItems)
                                {
                                    string Action = item["操作"].ToString();//此时的数据格式为：“ID;#查阅字段值”，需要做拆分处理
                                    Action = Action.Split('#')[1];
                                    fieldchoice.AddChoice(Action);
                                }
                                fieldchoice.Update();
                                spWeb.AllowUnsafeUpdates = false;
                            }
                        }
                    }
                });
            }
            catch (Exception)
            {
            }
        }

        private static SPListItemCollection GetSPItems(string fromList )
        {
            try
            {
                int usrID = SPContext.Current.Web.CurrentUser.ID;
                using (SPSite site = SPContext.Current.Site)
                {
                    using (SPWeb spWeb = site.OpenWeb(SPContext.Current.Web.ID ))
                    {
                        SPList spList = spWeb.Lists.TryGetList(fromList);
                        if (spList != null)
                        {
                            SPQuery qry = new SPQuery();

                            qry.Query = @"<Where><Eq><FieldRef Name='User' LookupId='True' /><Value Type='Integer'>" + usrID
                              + "</Value></Eq></Where><OrderBy><FieldRef Name='Frequency' Ascending='FALSE' /></OrderBy>";
                               qry.ViewFields = @"<FieldRef Name='ActionID' />";
                             
                            SPListItemCollection listItems = spList.GetItems(qry);
                            return listItems;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

        }
        #endregion
    }
}
