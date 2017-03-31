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
using Newtonsoft.Json.Linq;
using FOF.UserControlModel;

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
            InitializeUI();
            ConfigParser.ParseSensorModel(panelLeft,panelItem);            
            ConfigParser.ParseSensors();
            ConfigParser.ParseAlarms();
            ConfigParser.ParseGroups(panelAll);          
            ConfigParser.ParseUI(this);
            this.Text = labelTitle.Text;
            InitialModelClickSensors();
        }

        void InitialModelClickSensors()
        {
            for (int i = 1; i < panelLeft.Controls.Count; i++)
            {
                if (panelLeft.Controls[i] is GlassButton)
                {
                    GlassButton gb = panelLeft.Controls[i] as GlassButton;
                    FlowLayoutPanel flp = gb.Tag as FlowLayoutPanel;
                    List<Sensor> sensors = ConfigData.AllSensors.Sensors.FindAll(ss => ss.ModelKey == gb.Name);
                    foreach (Sensor ss in sensors)
                    {
                        FlowLayoutPanel spanel = new FlowLayoutPanel();                        
                        spanel.Parent = flp;
                        spanel.BackColor = Color.Green;
                        foreach (Field field in ss.Model.Fields)
                        {
                            if (!field.Realtime)
                                continue;
                            Label lb = new Label();
                            lb.Margin = new Padding(2);
                            lb.AutoSize = true;
                            lb.Parent = spanel;
                            spanel.SetFlowBreak(lb,true);
                            lb.Text = field.LabelText;
                        }                      
                    }
                }
            }
        }

        void InitializeUI()
        {            
            panelAllP.Name = "panelAllP";
            panelAllP.Dock = DockStyle.Fill;
            panelAllP.AutoScroll = true;
            panelAll.Name = "panelAll";
            panelAll.Dock = DockStyle.Fill;
            panelAll.AutoScroll = true;
            panelItem.AutoScroll = true;
            panelItem.Dock = DockStyle.Fill;
            panelItem.Name = "panelItem";             
            panelRight.Controls.Add(panelAllP);
            panelRight.Controls.Add(panelItem);
            panelAllP.Controls.Add(panelAll);
            panelAllP.BringToFront();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Thread th = new Thread(DataSubscribe.StartSubscribe);
            th.IsBackground = true;
            th.Start();
        }

        bool b = false;
        private void glassButton1_Click(object sender, EventArgs e)
        {
            //测试
            string jstr;
            if (b)
            {
                jstr = "{\"state\":\"Stream\",\"parser\":\"MXS1501\",\"raw\":\"7E000B7D1A000001000000330A4081817F01E524226D050100000100B01C00\",\"data\":[{\"name\":\"nodeid\",\"alias\":\"节点编号\",\"type\":\"uint16\",\"raw\":\"0x0100\",\"converted\":\"1\"},{\"name\":\"uid\",\"alias\":\"网关唯一号\",\"type\":\"raw\",\"raw\":\"0x81817F01E524226D\"},{\"name\":\"parent\",\"alias\":\"父级节点\",\"type\":\"uint16\",\"raw\":\"0x0000\",\"converted\":\"0\"},{\"name\":\"port\",\"alias\":\"采集通道\",\"type\":\"uint8\",\"raw\":\"0x01\",\"converted\":\"1\"},{\"name\":\"light\",\"alias\":\"太阳光照(lux)\",\"type\":\"uint32\",\"raw\":\"0x00B01C00\",\"converted\":\"188006.4\"}]}";
                b = false;
            }
            else
            {
                jstr = "{\"state\":\"Stream\",\"parser\":\"MXN820\",\"raw\":\"7E000B7D1A000001000000330A5E81817F01E524226DFC000000000000E70D\",\"data\":[{\"name\":\"nodeid\",\"alias\":\"节点编号\",\"type\":\"uint16\",\"raw\":\"0x0100\",\"converted\":\"1\"},{\"name\":\"uid\",\"alias\":\"网关唯一号\",\"type\":\"hex\",\"raw\":\"0x81817F01E524226D\",\"converted\":\"81817F01E524226D\"},{\"name\":\"parent\",\"alias\":\"父级节点\",\"type\":\"uint16\",\"raw\":\"0x0000\",\"converted\":\"0\"},{\"name\":\"port\",\"alias\":\"采集通道\",\"type\":\"uint8\",\"raw\":\"0x00\",\"converted\":\"0\"},{\"name\":\"chargeVol\",\"alias\":\"充电电压(mv)\",\"type\":\"uint16\",\"raw\":\"0x0000\",\"converted\":\"0\"},{\"name\":\"battVol\",\"alias\":\"电池电压(mv)\",\"type\":\"uint16\",\"raw\":\"0xE70D\",\"converted\":\"3559\"}]}";
                b = true;
            }
            JObject jobj = JObject.Parse(jstr);
            DataParser.ParseJObj(jobj);
        }

        //模型对应Panel
        Panel panelItem = new Panel();
        //全部
        FlowLayoutPanel panelAll = new FlowLayoutPanel();
        Panel panelAllP = new Panel();
        private void glassButton4_Click(object sender, EventArgs e)
        {                        
            panelAllP.BringToFront();
            panelAll.BringToFront();
            ConfigParser.btnAll.Text = "全部";
            ConfigParser.btnTxt = "全部";
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
