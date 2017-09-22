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
            
        }

     

        private void TestForm_Load(object sender, EventArgs e)
        {

//            Table name model field warn low high hour(0 - 24)

//    未命名.m.10.10.10
//Table smap

//    sensor gate, node, port, date, a_name, model
//	//sensor.alarm
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string db = Path.Combine(Utils.GetUserPath(), "alarm.db");
            string sql = "SELECT COUNT(*) FROM sqlite_master where type = 'table' and name = 'record'";
            object obj = SqlLiteHelper.ExecuteScalar(db, sql);
            if (Convert.ToInt32(obj) == 0)
            {
                string createSql = "CREATE TABLE record(time varchar(50),field varchar(50),groupname varchar(50),comment varchar(50),state varchar(50))";
                int res = SqlLiteHelper.ExecuteNonQuery(db, createSql);
            }
        }

        private void 数据报表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
        
        }
    }

    }

   

