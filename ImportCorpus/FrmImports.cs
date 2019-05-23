using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImportCorpus
{
    public partial class FrmImports : Form
    {
        public FrmImports()
        {
            InitializeComponent();
            if (FSCDLL.DAL.DataProvider.ConnectionString == "")
                FSCDLL.DAL.DataProvider.ConnectionString = GetConnString("SqlProviderFSC");
            if (dtCorpus == null)
                dtCorpus = FSCDLL.DAL.Corpus.GetCorpus().Tables[0];
            lbMsg.Text = "";
        }
        //打开文件夹
        private void btnOpen_Click(object sender, EventArgs e)
        {
            txtPath.Text = ChooseFolderPath();
        }
        #region 
        /// <summary>
        /// 对话框形式选择文件夹路径
        /// </summary>
        /// <returns>返回所选择的文件夹路径</returns>
        public string ChooseFolderPath()
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.RootFolder = System.Environment.SpecialFolder.MyComputer;
            fbd.Description = "请选择目录";
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                return fbd.SelectedPath.ToString();
            }
            else
            {
                return "";

            }
        }
        #endregion

        private void btnImport_Click(object sender, EventArgs e)
        {
            if (txtPath.Text == "")
            {
                lbMsg.Text = "文件夹不能为空！";
                return;
            }
            string txt = Text;
            Text="正在导入……";
            int total = 0;
            StreamWriter w = new StreamWriter(@"c:\importCorpus.txt", true);
            DirectoryInfo dir = new DirectoryInfo(txtPath.Text);
            DealFiles(dir, ref total);
            foreach (DirectoryInfo subDir in dir.GetDirectories())
            {
                DealFiles(subDir, ref total);
            }
            w.Write(DateTime.Now.ToString ()+"导入文件夹："+txtPath.Text );
            w.WriteLine(";本次导入条："+ total );
            w.Close();
            Text = txt;
            MessageBox.Show ( "导入完成！");
        }
        private void DealFiles(DirectoryInfo dir,ref int total)
        {
            //遍历文件夹的文件
            foreach (FileInfo file in dir.GetFiles("*.txt"))
            {
                string fileName = file.Name;//.Replace(file.Extension, "");//去掉文件扩展名
                //处理非数字部分
                string ignore = "[.a-zA-Z_-]";//
                fileName = Regex.Replace(fileName  , ignore, "");
                string fileContent;
                 if (rbLC.Checked && fileName.Length == 8)
                {
                    string gradeIDs = split + fileName.Substring(0, 1) + split;
                    string genreIDs = split + fileName.Substring(1, 1) + split;
                    string topicIDs = split + fileName.Substring(2, 1) + split;
                    StreamReader read = file.OpenText();
                    fileContent = read.ReadToEnd().Trim();
                    Save(fileName, fileContent, topicIDs, genreIDs, gradeIDs);
                    total += 1;
                }
            }
        }
       
        //数据分隔符，用来分隔外键的ID值
        private const string split = ";";
        private static DataTable dtCorpus;

        private void Save(string title, string corpusText, string topicIds, string genreIds, string gradeIds)
        {
            DataRow dr = dtCorpus.NewRow();
            long corpusID = 0;
            //更改为标题唯一
            DataRow[] drs = dtCorpus.Select("Title='" + title + "'");// and OriginalText='" + corpusText + "'");//通过标题和正文判断唯一性
            if (drs.Length > 0)
            {
                dr = drs[0];
                corpusID = Convert.ToInt64(dr["CorpusID"].ToString());
            }
            else
            {
                dr["Created"] = DateTime.Now;
                dr["Author"] = 0;
                dr["Flag"] = 1;
                dr["Title"] = title;
                dr["TopicID"] = topicIds;
                dr["GenreID"] = genreIds;
                dr["GradeID"] = gradeIds;
                //用来保存语料库的类别
                dr["Source"] = rbLC.Checked ? rbLC.Text : rbAC.Text;

            }
            if (rbUntagged.Checked)
                dr["OriginalText"] = corpusText;
            else
                dr["CodedText"] = corpusText;
            if (corpusID == 0)
            {
                FSCDLL.DAL.Corpus.InsertCorpus(null, dr);
                dtCorpus.Rows.Add(dr.ItemArray);
            }

            else
            {
                FSCDLL.DAL.Corpus.UpdateCorpus(null, dr);
                dtCorpus.AcceptChanges();
            }
        }
        /// <summary>
        /// 获取配置文件中的连接字符串
        /// </summary>
        /// <param name="connStrName">字符串的名称</param>
        /// <returns></returns>
        public string GetConnString(string connStrName)
        {
            string connectionString = ConfigurationManager.ConnectionStrings[connStrName].ConnectionString;
            return connectionString;
        }

    }
}
