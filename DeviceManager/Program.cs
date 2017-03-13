using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

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
           Application.Run(new MainForm());
            
        }
    }
}
