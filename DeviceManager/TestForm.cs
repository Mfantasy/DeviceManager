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

        Random r = new Random();
        private void button1_Click(object sender, EventArgs e)
        {
            int i=r.Next(0, 10);
            customChart2.Series[0].Points.AddXY(DateTime.Now.ToString("HH:MM:ss"), i);
        }
        
        private void button2_Click(object sender, EventArgs e)
        {           
            //加上按钮控制属性 
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
         
        }

        private void button3_Click(object sender, EventArgs e)
        {
            customChart2.ChartAreas[0].CursorX.IsUserSelectionEnabled = false;

            
        }
    }

    }

   

