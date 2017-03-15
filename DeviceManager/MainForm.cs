using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace DeviceManager
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            //manager dataControl there is a question . 
            //Format . table格式.Now I should test data . 模仿设备进行数据上行.
            //today I should have a smeshserver . then 查询数据格式,配置数据库

        }

        private void button1_Click(object sender, EventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            string path = Path.GetFullPath("../../SensorsConfig.xml");
            doc.Load(path);
            string x = doc.FirstChild.SelectSingleNode("Section").InnerText="QAQ";
            string y = doc.FirstChild.SelectSingleNode("Section").Attributes[0].InnerText="OvO";
            doc.Save(path);
            Console.WriteLine(x);
            Console.WriteLine(y);
        }
    }
}
