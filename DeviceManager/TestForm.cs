using DeviceManager;
using DeviceManager.CustomControl;
using DeviceManager.CustomForm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
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
            this.FormClosing += TestForm_FormClosing;
            //ContextMenu = new ContextMenu();            
        }

        private void TestForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }
    
        
        private void button2_Click(object sender, EventArgs e)
        {
            new Account().ShowDialog();
        }

     

        private void TestForm_Load(object sender, EventArgs e)
        {
        
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Utils.ExcelTest();
        }

        private void 数据报表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DTPickerForm df = new DTPickerForm();
            df.ShowDialog();
        }
    }

    }

   

