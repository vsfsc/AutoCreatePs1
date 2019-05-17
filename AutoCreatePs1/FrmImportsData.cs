using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoCreatePs1
{
    public partial class FrmImportsData : Form
    {
        string connectionString;
        public FrmImportsData()
        {
            InitializeComponent();
            string dbName = ConfigurationSettings.AppSettings["VsDB"];
            this.connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + dbName + ";Persist Security Info=False;";
            //this.connectionString = "Provider=microsoft.jet.oledb.4.0;Data Source=" + dbName + ";Persist Security Info=False;";
            OleDbHelp.ConnectionString = this.connectionString;
        }

        private void btn2016_Click(object sender, EventArgs e)
        {
            DataSet dsTmp = OleDbHelp.ExecuteDataset("SELECT  操作,计量方式 ,计量单位,说明 from " + txtListName.Text);
            using (SPSite mySite = new SPSite("http://localhost"))
            {
                SPWeb myWeb = mySite.AllWebs[txtSubWeb.Text];
                SPList myLst = myWeb.Lists[txtListName.Text];
                SPListItem item;
                //foreach (DataRow dr in dsTmp.Tables[0].Rows)
                //{
                //    item = myLst.AddItem();
                //    item["操作"] = dr["操作"];
                //    item["计量方式"] = dr["计量方式"];
                //    item["计量单位"] = dr["计量单位"];
                //    item["说明"] = dr["说明"];
                //    item.SystemUpdate();
                //}
                MessageBox.Show("ok");
            }
        }
    }
}
