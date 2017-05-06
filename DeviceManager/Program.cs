using DeviceManagerO;
using System;
using System.Collections.Generic;
using System.Configuration;
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
           // Application.Run(new TestForm());
           //return;
            Application.EnableVisualStyles();                    
            Application.SetCompatibleTextRenderingDefault(false);
            if (Config.IsShowLogon)
            {
                Logon logon = new Logon();
                DialogResult dialogResult = logon.ShowDialog();
                if (dialogResult != DialogResult.OK)
                {
                    return;
                }
            }
           mainForm = new MainForm();
           Application.Run(mainForm);            
        }
        public static MainForm mainForm;
    }
}
