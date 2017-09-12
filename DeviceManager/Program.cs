using DeviceManager.CustomForm;
using DeviceManagerO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace DeviceManager
{   
  
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //string sql = "SELECT * FROM sqlite_master";
            //DataTable dt = SqlLiteHelper.ExecuteReader(ConfigurationManager.AppSettings["dbPath"], sql);
            //Application.Run(new TestForm());
            //Console.WriteLine(dt.ToString());
            //Application.Run(new Account());
            //return;
            Application.EnableVisualStyles();                    
            Application.SetCompatibleTextRenderingDefault(false);

            Logon logon = new Logon();
            DialogResult dialogResult = logon.ShowDialog();
            if (dialogResult != DialogResult.OK)
            {
                return;
            }

           mainForm = new MainForm(logon.logonResult);            
           Application.Run(mainForm);      
                 
        }
        public static MainForm mainForm;
    }
}
