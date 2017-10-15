using DeviceManager;
using DeviceManager.Alarm;
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
            //UserCalendar uc = new UserCalendar();
            //uc.Parent = this;
            //uc.Visible = true;
            //uc.Dock = DockStyle.Fill;
        }

        private void TestForm_FormClosing(object sender, FormClosingEventArgs e)
        {


        }

        private void TestForm_Load(object sender, EventArgs e)
        {
            AlarmStrategy a1 = new AlarmStrategy();
            a1.Name = "a1";
            AlarmStrategy a2 = new AlarmStrategy();
            a2.Name = "a2";
            AlarmStrategy A1 = a1;
            List<AlarmStrategy> la = new List<AlarmStrategy>();
            AlarmStrategy a3 = new AlarmStrategy();
            a3.Name = "a3";
            la.Add(a1);
            la.Add(a2);            
            a1 = a3;
            Console.WriteLine(A1.Name);
            Console.WriteLine(la[0]);
            //uc1.Init();
            //this.userCalendar1.Init();
           // MainForm mf = new MainForm(3);
           // uc1.Init();
            //mf.Show();
            //mf.Visible = false;
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

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
          
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
        }
    }

}

