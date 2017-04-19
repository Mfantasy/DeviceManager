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
    public class MyClass
    {
        [XmlAttribute("name")]
        public string Name { get; set; }
        [XmlIgnore]
        public int Age { get; set; }
        public MyClass Clone()
        {
            MemoryStream ms = new MemoryStream();
            MyClass mc = null;
            XmlSerializer xSl = new XmlSerializer(typeof(MyClass));
            xSl.Serialize(ms, this);
            ms.Seek(0, SeekOrigin.Begin);                        
            mc = (MyClass)xSl.Deserialize(ms);
            ms.Close();
            return mc;
        }
    }

    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            var x1 = SqlLiteHelper.ExecuteReader(ConfigurationManager.AppSettings["alarmdb"], "SELECT * FROM record");
            string userPath = Utils.GetUserPath();
            bool x = File.Exists("../../alarm.db");
            Application.EnableVisualStyles();
           
            //Application.Run(new TestForm());
           // return;
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
