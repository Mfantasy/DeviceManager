using NPOI.HSSF.UserModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
            ConfigParser.ParseSensor(panelLeft);
            ConfigParser.ParseUI(this);            
        }

        private void LoadUI()
        {
            Panel pt = (Panel)this.Controls.Find("panelTop",false)[0];
            pt.BackgroundImage = Image.FromFile("a.jpg");
            pt.GetType().GetProperty("BackColor").SetValue(pt, Color.FromName("red"));
            Label lb = (Label)pt.Controls.Find("labelTitle", false)[0];
            lb.GetType().GetProperty("Text").SetValue(lb, "测试成功了呀");
        }

        private void button1_Click(object sender, EventArgs e)
        {

            DataSubscribe.StartSubscribe();



        }

        private void glassButton1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(glassButton1.ButtonText);
        }

        private void glassButtonAll_Click(object sender, EventArgs e)
        {

        }
    }

}
