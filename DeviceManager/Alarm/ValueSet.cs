using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DeviceManager.Alarm
{
    public partial class ValueSet : UserControl
    {
        public ValueSet()
        {
            InitializeComponent();
        }

        //画图时23点之后接入0点的数据
        private void button1_Click(object sender, EventArgs e)
        {
            label1.Text = "0";
            label2.Text = "1";
            label3.Text = "2";
            label4.Text = "3";
            label5.Text = "4";
            label6.Text = "5";
            label7.Text = "6";
            label8.Text = "7";
            label9.Text = "8";
            label10.Text = "9";
            label11.Text = "10";
            label12.Text = "11";

        }

        private void button2_Click(object sender, EventArgs e)
        {
            label1.Text = "12";
            label2.Text = "13";
            label3.Text = "14";
            label4.Text = "15";
            label5.Text = "16";
            label6.Text = "17";
            label7.Text = "18";
            label8.Text = "19";
            label9.Text = "20";
            label10.Text = "21";
            label11.Text = "22";
            label12.Text = "23";
        }
    }
}

