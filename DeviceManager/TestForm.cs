using DeviceManager;
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

            string x = listView1.SelectedItems[0].Text;
            MessageBox.Show(x);
        }
    }

    }

   

