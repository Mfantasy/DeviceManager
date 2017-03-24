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
using System.Runtime.InteropServices;

namespace DeviceManager
{
    public partial class MainForm : Form
    {
        //拖动
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd,
                         int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
             
        public MainForm()
        {
            InitializeComponent();
            ConfigParser.ParseSensorModel(panelLeft);
            ConfigParser.ParseUI(this);
            ConfigParser.ParseGroups(panelAll);
            UITEST();
        }

        void UITEST()
        {
            panelAll.Dock = DockStyle.Fill;
            panelAll.AutoScroll = true;
            panelRight.Controls.Add(panelAll);
        }
        private void button1_Click(object sender, EventArgs e)
        {

            DataSubscribe.StartSubscribe();



        }

        private void glassButton1_Click(object sender, EventArgs e)
        {
            
        }

        private void glassButtonAll_Click(object sender, EventArgs e)
        {

        }

        Panel panelAll = new Panel();
        private void glassButton4_Click(object sender, EventArgs e)
        {
            //ConfigData.GroupCfg.GroupConfigs[0].
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
             
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
    }

}
