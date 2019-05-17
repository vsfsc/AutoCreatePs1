using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace AutoCreatePPT
{
    public class common
    {
        // Methods
        public void changeName(string strPath, string strSrcFileName, string strDesFileName)
        {
            try
            {
                DirectoryInfo info = new DirectoryInfo(strPath);
                foreach (FileInfo info2 in info.GetFiles())
                {
                    string fullName = info2.FullName;
                    string name = info2.Name;
                    if (name.IndexOf(strSrcFileName) > -1)
                    {
                        info2.MoveTo(fullName.Replace(name, name.Replace(strSrcFileName, strDesFileName)));
                    }
                    else
                    {
                        info2.MoveTo(fullName.Replace(name, name.Replace(" ", "")));
                    }
                }
                MessageBox.Show("文件改名成功");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        public static bool FileExits(string fileName)
        {
            FileInfo info = new FileInfo(fileName);
            return info.Exists;
        }

        //[STAThread]
        //private static void Main()
        //{
        //    Application.Run(new frmNewCommon ());
        //}
    }


}
