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
using Microsoft.Office.Server.UserProfiles;
using Microsoft.Office.Server.Social;

namespace AutoCreatePs1
{
    public partial class FrmFollow : Form
    {
        public FrmFollow()
        {
            InitializeComponent();
        }

        private void btnEnum_Click(object sender, EventArgs e)
        {
            SPSite site = new SPSite("http://localhost");
            try
            {
                SPServiceContext serviceContext = SPServiceContext.GetContext(site);
                UserProfileManager upm = new UserProfileManager(serviceContext);
                string accountName = "ccc\\" + txtUser.Text.ToString();//xueqingxia";// SPContext.Current.Web.CurrentUser.LoginName;
                //accountName = accountName.Substring(accountName.LastIndexOf("|") + 1);
                //if (accountName == "sharepoint\\system")
                //    accountName = "ccc\\xueqingxia";
                if (upm.UserExists(accountName))
                {
                    UserProfile u = upm.GetUserProfile(accountName);
                    //Response.Write(u[PropertyConstants.PictureUrl].Value);
                    SPSocialFollowingManager follow = new SPSocialFollowingManager(u, serviceContext);
                    SPSocialActorInfo socialInfo = new SPSocialActorInfo();
                    StringBuilder txt = new StringBuilder();
                    //des.Text = follow.GetFollowedCount(SPSocialActorTypes.Sites).ToString();
                    foreach (SPSocialActor spFollow in follow.GetFollowed(SPSocialActorTypes.Sites))
                    {
                        //spFollow.Status = SPSocialStatusCode.InternalError;
                        socialInfo.ContentUri = spFollow.Uri;
                        //socialInfo.AccountName = u.AccountName;
                        socialInfo.ActorType = SPSocialActorType.Site;
                        //follow.StopFollowing(socialInfo);
                        //des.Text += "ok! ";
                        txt.AppendLine(spFollow.Uri.ToString());
                    }
                    this.txtResult.Text = txt.ToString();
                }
            }
            catch (Exception ex)
            {
                this.txtResult.Text =ex.ToString ();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string emails = this.txtResult.Text.Replace("；", ";").Trim().TrimEnd(new char[] { ';' })+"\rfdsfd";
            emails = emails.Replace(" ", "");
            emails = emails.Replace("\r\n", "");
            emails = emails.Replace("\n", "");
            emails = emails.Replace("\r", "");

            txtResult.Text = emails;
        }
    }
}
