using DeviceManager;
using DeviceManager.CustomControl;
using DeviceManager.CustomForm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace DeviceManagerO
{
    public partial class TestForm : Form
    {
        public TestForm()
        {
            InitializeComponent();
            //ContextMenu = new ContextMenu();            
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
        int i = 0;
        private void button2_Click(object sender, EventArgs e)
        {           
            //string x = new tex
            //添加一个节点
            //treeView1.Nodes.Add(i.ToString());
            //i ++;
        }

        private void 测试1ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            
        }

        private void TestForm_Load(object sender, EventArgs e)
        {
            customChart1.Parent = panel1;
            customChart1.Dock = DockStyle.Fill;
            System.Windows.Forms.Timer tm = new System.Windows.Forms.Timer();
            tm.Interval = 1 * 1000;
            tm.Tick += new EventHandler(tm_Tick);
            tm.Start();
        }
        Random r = new Random();
        int y = 5678;
        CustomChart customChart1 = new CustomChart();
        private void tm_Tick(object sender, EventArgs e)
        {
            //y = r.Next(1, 1111111);
            y += 10;
            customChart1.Series[0].Points.AddXY(DateTime.Now, y);
            if (customChart1.Series[0].Points.Count > 10)
                customChart1.Series[0].Points.RemoveAt(0);
        }
    }

    }

   

