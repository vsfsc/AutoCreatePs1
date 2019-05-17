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
using System.DirectoryServices;

namespace AutoCreatePs1
{
    public partial class FrmCreative : Form
    {
        public FrmCreative()
        {
            InitializeComponent();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            if (txtSourceListName.Text == "" || txtDesListName.Text == "")
            {
                MessageBox.Show("导入导出的列表名称不能为空");
                return;
            }
            if (txtSourceCol.Text == "" || txtDesCol.Text == "")
            {
                MessageBox.Show("导入导出的列表列不能为空");
                return;
            }
            string txtNames = txtSourceCol.Text.Trim().Replace("；", ";");
            txtNames = txtNames.Replace(" ", "").TrimEnd(';');

            string[] srcCols = txtNames.Split(';');

            txtNames = txtDesCol.Text.Trim().Replace("；", ";");
            txtNames = txtNames.Replace(" ", "").TrimEnd(';');
            string[] desCols = txtNames.Split(';');

            if (srcCols.Length != desCols.Length)
            {
                MessageBox.Show("导入导出的列表列个数不同");
                return;
            }
            using (SPSite mySite = new SPSite("http://localhost"))
            {
                SPWeb myWeb = mySite.AllWebs[txtWebSourceUrl.Text];
                SPList mySourceList = myWeb.Lists[txtSourceListName.Text];
                SPWeb desWeb = mySite.AllWebs[txtWebDesUrl.Text];  
                SPList myDesList = desWeb.Lists[txtDesListName.Text];//查阅项
                //DirectoryEntry de= AdHelper.GetDirectoryEntryByAccount("xueqingxia");
                SPUser user = myWeb.EnsureUser ("ccc\\xueqingxia");
                SPFieldUserValue fUser = new SPFieldUserValue(myWeb, user.ID, user.LoginName);
                
                foreach (SPListItem myItem in mySourceList.Items)
                {
                    SPListItem newItem = myDesList.AddItem();
                    for (int i = 0; i < srcCols.Length; i++)
                    {
                        newItem[srcCols[i]] = myItem.ID + ";#" + myItem[desCols[i]];
                        newItem["评审人"] = fUser;
                    }

                    newItem.Update();

                }
                MessageBox.Show("ok");
            }
        }
        //完全替换
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtSourceListName.Text == "" || txtDesListName.Text == "")
            {
                MessageBox.Show("导入导出的列表名称不能为空");
                return;
            }
            if (txtSourceCol.Text == "" || txtDesCol.Text == "")
            {
                MessageBox.Show("导入导出的列表列不能为空");
                return;
            }
            string txtNames = txtSourceCol.Text.Trim().Replace("；", ";");
            txtNames = txtNames.Replace(" ", "").TrimEnd(';');

            string[] srcCols = txtNames.Split(';');

            txtNames = txtDesCol.Text.Trim().Replace("；", ";");
            txtNames = txtNames.Replace(" ", "").TrimEnd(';');
            string[] desCols = txtNames.Split(';');

            if (srcCols.Length != desCols.Length)
            {
                MessageBox.Show("导入导出的列表列个数不同");
                return;
            }
            using (SPSite mySite = new SPSite("http://localhost"))
            {
                SPWeb myWeb = mySite.AllWebs[txtWebSourceUrl.Text];
                SPList mySourceList = myWeb.Lists[txtSourceListName.Text];
                SPWeb desWeb = mySite.AllWebs[txtWebDesUrl.Text];
                SPList myDesList = desWeb.Lists[txtDesListName.Text];//查阅项
                 SPField  fldEqual = mySourceList.Fields[txtFldEqual.Text ] ;
                string fldName = fldEqual.InternalName;
                SPQuery qry;
                string fldType = fldEqual.TypeAsString; 
                foreach (SPListItem desItem in myDesList.Items)
                {
                    qry = new SPQuery();
                    qry.Query = @"<Where><Eq><FieldRef Name='" + fldName + "'/><Value Type='Text'>" + desItem[fldName] + "</Value></Eq></Where>";
                    SPListItemCollection listItems = mySourceList.GetItems(qry);
                    if (listItems.Count > 0)
                    {
                        for (int i = 0; i < desCols.Length; i++)
                        {
                            desItem[desCols[i]] = listItems[0][srcCols[i]];
                        }

                        desItem.Update();
                    }

                }
                MessageBox.Show("ok");

            }
        }
       
    }
}
