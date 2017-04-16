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
using DeviceManager.CustomControl;
using DeviceManager.Model;
using System.Configuration;

namespace DeviceManager
{
    public partial class MainForm : Form
    {
        //测试
        private void button1_Click(object sender, EventArgs e)
        {           
            glassButton1_Click(null, null);
            //Thread th = new Thread(DataSubscribe.StartSubscribe);
            //th.IsBackground = true;
            //th.Start();
        }
      

        PanelAllSensors pas = new PanelAllSensors();
        PanelHistory ph = new PanelHistory();
        public MainForm()
        {
            InitializeComponent();          
            InitializeUI();
            ConfigParser.ParseSensorModel(panelLeft,panelItem);            
            ConfigParser.ParseSensors();
            ConfigParser.ParseAlarms();
            InitialAlarms();
            ConfigParser.ParseGroups(panelAll);          
            //ConfigParser.ParseUI(this);
            InitializeUIEnd();            
            InitialModelClickSensors();
            
        }

        void InitializeUIEnd()
        {
            ph.Init();
            pas.Init();
            pas.Parent = panelBotttom;
            pas.Dock = DockStyle.Fill;
            ph.Parent = panelBotttom;
            ph.Dock = DockStyle.Fill;
            this.Text = ConfigurationManager.AppSettings["软件名称"];
            if (menuButtonPanel1.DefaultImage != null)
            {
                menuButtonPanel1.ShowDefaultImage(null, null);
            }
            if (menuButtonPanel2.DefaultImage != null)
            {
                menuButtonPanel2.ShowDefaultImage(null, null);
            }
            if (menuButtonPanel3.DefaultImage != null)
            {
                menuButtonPanel3.ShowDefaultImage(null, null);
            }
            if (menuButtonPanel4.DefaultImage != null)
            {
                menuButtonPanel4.ShowDefaultImage(null, null);
            }
            if (menuButtonPanel5.DefaultImage != null)
            {
                menuButtonPanel5.ShowDefaultImage(null, null);
            }
            menuButtonPanel1.Panel = panelRuntime;
            menuButtonPanel2.Panel = ph;
            //menuButtonPanel3.Panel = 
            //menuButtonPanel4.Panel
            menuButtonPanel5.Panel = pas;
        }
        void InitialAlarms()
        {
            //为每个传感器类型匹配正确的报警设置1.首先获取传感器模型列表 2.然后循环alm列表 3.然后匹配模型,匹配字段
            foreach (SensorModel smodel in ConfigData.SensorModelCfg.SensorModels)
            {
                if (string.IsNullOrWhiteSpace(smodel.AlarmName))
                {
                    continue;
                }
                else
                {
                    AlarmConfig acfg = ConfigData.AlarmCfg.AlarmConfigs.Find(cfg => cfg.Name == smodel.AlarmName);
                    if (acfg == null)
                    {
                        continue;
                    }
                    foreach (Field field in smodel.Fields)
                    {
                        field.Alarm = acfg.AlarmField.Find(af => af.Name == field.Name);
                    }
                }                                
            }             
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
                        spanel.MinimumSize = new Size(200, 128);
                        spanel.Parent = flp;
                        spanel.BackColor = Color.LightGreen;
                        Label lbGroup = UIUtils.NewLabel(19);
                        lbGroup.BackColor = Color.Blue;
                        lbGroup.Dock = DockStyle.Top;
                        lbGroup.ForeColor = Color.White;
                        lbGroup.Parent = spanel;
                        spanel.SetFlowBreak(lbGroup,true);
                        lbGroup.Text = ss.GroupName;                                                                                                
                        foreach (Field field in ss.Model.Fields)
                        {
                            if (!field.Realtime)
                                continue;                            
                            Label lb = new Label();
                            field.ClickLabel = lb;
                            lb.Font = new Font("宋体", 20);
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
      


        //模型对应Panel
        Panel panelItem = new Panel();
        //全部
        FlowLayoutPanel panelAll = new FlowLayoutPanel();
        Panel panelAllP = new Panel();
        private void glassButtonAll_Click(object sender, EventArgs e)
        {                        
            panelAllP.BringToFront();
            panelAll.BringToFront();
            ConfigParser.btnAll.Text = "全部";
            ConfigParser.btnTxt = "全部";
        }
     

        //测试
        bool b = false;
        int value1 = 11098;
        int value2 = 6640;
        private void glassButton1_Click(object sender, EventArgs e)
        {
            panelRuntime.BringToFront();
            //测试
            string jstr;
            if (b)
            {
                jstr = "{\"state\":\"Stream\",\"parser\":\"MXS1501\",\"raw\":\"7E000B7D1A000001000000330A4081817F01E524226D050100000100B01C00\",\"data\":[{\"name\":\"nodeid\",\"alias\":\"节点编号\",\"type\":\"uint16\",\"raw\":\"0x0100\",\"converted\":\"1\"},{\"name\":\"uid\",\"alias\":\"网关唯一号\",\"type\":\"raw\",\"raw\":\"0x81817F01E524226D\"},{\"name\":\"parent\",\"alias\":\"父级节点\",\"type\":\"uint16\",\"raw\":\"0x0000\",\"converted\":\"0\"},{\"name\":\"port\",\"alias\":\"采集通道\",\"type\":\"uint8\",\"raw\":\"0x01\",\"converted\":\"1\"},{\"name\":\"light\",\"alias\":\"太阳光照(lux)\",\"type\":\"uint32\",\"raw\":\"0x00B01C00\",\"converted\":\"" + value1 + "\"}]}";
                b = false;
                value1++;
            }
            else
            {
                jstr = "{\"state\":\"Stream\",\"parser\":\"MXN820\",\"raw\":\"7E000B7D1A000001000000330A5E81817F01E524226DFC000000000000E70D\",\"data\":[{\"name\":\"nodeid\",\"alias\":\"节点编号\",\"type\":\"uint16\",\"raw\":\"0x0100\",\"converted\":\"1\"},{\"name\":\"uid\",\"alias\":\"网关唯一号\",\"type\":\"hex\",\"raw\":\"0x81817F01E524226D\",\"converted\":\"81817F01E524226D\"},{\"name\":\"parent\",\"alias\":\"父级节点\",\"type\":\"uint16\",\"raw\":\"0x0000\",\"converted\":\"0\"},{\"name\":\"port\",\"alias\":\"采集通道\",\"type\":\"uint8\",\"raw\":\"0x00\",\"converted\":\"0\"},{\"name\":\"chargeVol\",\"alias\":\"充电电压(mv)\",\"type\":\"uint16\",\"raw\":\"0x0000\",\"converted\":\"0\"},{\"name\":\"battVol\",\"alias\":\"电池电压(mv)\",\"type\":\"uint16\",\"raw\":\"0xE70D\",\"converted\":\"" + value2 + "\"}]}";
                b = true;
                value2++;
            }
            JObject jobj = JObject.Parse(jstr);
            DataParser.ParseJObj(jobj);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }

}
