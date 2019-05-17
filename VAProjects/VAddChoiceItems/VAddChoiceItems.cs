using System;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.Workflow;

namespace VAProjects.VAddChoiceItems
{
    /// <summary>
    /// 列表项事件
    /// </summary>
    public class VAddChoiceItems : SPItemEventReceiver
    {
        /// <summary>
        /// 正在添加项.
        /// </summary>
        public override void ItemAdding(SPItemEventProperties properties)
        {
            base.ItemAdding(properties);
        }

        /// <summary>
        /// 正在更新项.
        /// </summary>
        public override void ItemUpdating(SPItemEventProperties properties)
        {
            base.ItemUpdating(properties);
        }

        /// <summary>
        /// 已添加项.
        /// </summary>
        public override void ItemAdded(SPItemEventProperties properties)
        {
            base.ItemAdded(properties);
            //AddValues(properties);
        }

        /// <summary>
        /// 已更新项.
        /// </summary>
        public override void ItemUpdated(SPItemEventProperties properties)
        {
            base.ItemUpdated(properties);
            //AddValues(properties);
        }

        private void AddValues(SPItemEventProperties properties)
        {

            base.ItemAdded(properties);
            this.EventFiringEnabled = false;
            SPSecurity.RunWithElevatedPrivileges(delegate
            {
                using (SPSite site = new SPSite(properties.Site.ID))
                {
                    using (SPWeb web = site.OpenWeb(properties.Web.ID))
                    {
                        SPList list = web.Lists[properties.ListId];
                        SPListItem item = list.GetItemById(properties.ListItemId);
                        web.AllowUnsafeUpdates = true;
                        try
                        {
                            #region SPFieldChoice 
                            SPFieldChoice choices = new SPFieldChoice(item.Fields, "Action");
                            //choices.Choices.Clear();
                            if (!choices.Choices.Contains(properties.UserDisplayName))
                                choices.Choices.Add(properties.UserDisplayName);
                            //需要提升权限，否则 会出现权限错误
                            choices.Update();
                        }
                        catch (Exception ex)
                        { }
                        #endregion
                        web.AllowUnsafeUpdates = false;
                    }
                }
            });
            this.EventFiringEnabled = true;
        }
    }
}