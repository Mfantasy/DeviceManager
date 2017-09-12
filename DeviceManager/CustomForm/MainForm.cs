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
using DeviceManager.CustomForm;
using DeviceManagerO;

namespace DeviceManager
{
    public partial class MainForm : Form
    {
        
        //测试
        private void button1_Click(object sender, EventArgs e)
        {           
            glassButton1_Click(null, null);
            Screen screen = Screen.FromControl(this);
            int sh = screen.Bounds.Height;
            int sw = screen.Bounds.Width;
        }

        const int reConnectInterval = 10 * 1000;//10s
        bool success = false;
        void MonitorStatus()
        {
            while (true)
            {
                if (success)
                {
                    this.Invoke(new Action(() => { toolStripStatusLabel2.Text = "已连接"; }));                 
                    return;
                }
                Thread.Sleep(reConnectInterval);
            }
        }
        void ConnectMethod()
        {
            success = false;
            try
            {
                DataSubscribe.StartSubscribe(out success);
            }
            catch (Exception ex)
            {                
                this.Invoke(new Action(() => { toolStripStatusLabel2.Text = "正在连接"; }));
                Thread.Sleep(reConnectInterval);
                Thread thCon = new Thread(ConnectMethod);
                thCon.IsBackground = true;
                thCon.Start();                
            }
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            //测试                                
            Thread thStatus = new Thread(MonitorStatus);
            thStatus.IsBackground = true;
            thStatus.Start();
            Thread thMonitor = new Thread(ConnectMethod);
            thMonitor.IsBackground = true;
            thMonitor.Start();

            //this.Visible = false;
            //new TestForm().Show();
        }
    
        PanelHistory ph = new PanelHistory();
        PanelAlarmRecord par = new PanelAlarmRecord();
        PanelAlarmSet paset = new PanelAlarmSet();
        PanelGroupSensors pgs = new PanelGroupSensors();
        PanelDrag pdg = new PanelDrag();
        
        public MainForm(int userLevel)
        {
            InitializeComponent();
            if (userLevel == 2)
            {
                menuButtonPanel4.Visible = false;
                menuButtonPanel5.Visible = false;
                用户管理ToolStripMenuItem.Visible = false;
            }
            else if (userLevel == 1)
            {
                用户管理ToolStripMenuItem.Visible = false;
            }
            
            if (Screen.PrimaryScreen.Bounds.Width > 1500)
            {
                panelAll.ColumnCount = 3;
                panelAll.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,34));
                panelAll.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,33));
                panelAll.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));
            }
            else
            {
                panelAll.ColumnCount = 2;
                panelAll.ColumnStyles.Add(new ColumnStyle(SizeType.Percent));
                panelAll.ColumnStyles.Add(new ColumnStyle(SizeType.Percent));
            }
            Utils.SetDoubleBuffer(panelAll);
            ConfigData.InitConfig();
            //通用界面逻辑
            InitializeUI();
            //左侧功能按钮
            InitializeSModel();
            //实时数据
            InitRealtime();
            //实时数据中左侧按钮点击            
            InitialModelClickSensors();
            InitializeUIEnd();
            this.WindowState = FormWindowState.Maximized;                                             
        }
       
        #region 模型按钮
        private void InitializeSModel()
        {
            foreach (SensorModel smodel in ConfigData.SensorModelRoot.SensorModels)
            {
                TableLayoutPanel tlp = new TableLayoutPanel();
                tlp.AutoScroll = true;
                tlp.Dock = DockStyle.Fill;
                tlp.Parent = panelItem;
                tlp.Padding = new Padding(8);
                tlp.ColumnCount = 2;                
                tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 11));
                tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 89));
                tlp.Tag = panelItem;
                //FlowLayoutPanel flp = new FlowLayoutPanel();
                //flp.AutoScroll = true;
                //flp.Dock = DockStyle.Fill;
                //flp.Parent = panelItem;
                //flp.Padding = new Padding(8);
                //flp.Tag = panelItem;
                GlassButton gbtn = new GlassButton();
                gbtn.CornerRadius = 0;
                gbtn.Name = smodel.Name;
                gbtn.ButtonText = smodel.Title;
                gbtn.Font = new System.Drawing.Font("幼圆", 12, System.Drawing.FontStyle.Regular);
                gbtn.Size = new System.Drawing.Size(182, 40);
                gbtn.Margin = new Padding(6);
                gbtn.Tag = tlp;
                gbtn.Click += Gbtn_Click;
                panelLeft.Controls.Add(gbtn);
            }
        }
        private void Gbtn_Click(object sender, EventArgs e)
        {
            GlassButton gbtn = sender as GlassButton;            
            TableLayoutPanel flp = gbtn.Tag as TableLayoutPanel;
            //FlowLayoutPanel flp = gbtn.Tag as FlowLayoutPanel;
            Panel panel = flp.Tag as Panel;
            panel.BringToFront();
            flp.BringToFront();
            foreach (Control item in flp.Controls)
            {
                if (item is TableLayoutPanel)
                {
                    foreach (var lb in item.Controls)
                    {
                        if (lb is Label)
                        {
                            Label l = lb as Label;
                            if (l.Tag is Field)
                            {
                                (l.Tag as Field).RefreshPanel2();
                            }
                        }
                    }
                }
            }

        }
        #endregion
        #region 实时数据
        //模型对应Panel
        Label lbAll = new Label();
        public Panel panelItem = new Panel();
        //全部
        public TableLayoutPanel panelAll = new TableLayoutPanel();
        public Panel panelAllP = new Panel();
        public void glassButtonAll_Click(object sender, EventArgs e)
        {
            panelAllP.BringToFront();
            panelAll.BringToFront();
            lbAll.Text = "全部";
        }    
        private void InitRealtime()
        {   
            //指示说明label                     
            //lbAll.Dock = DockStyle.Top;
            //lbAll.ForeColor = Color.White;
            //lbAll.Font = new System.Drawing.Font("微软雅黑", 14);
            //lbAll.Text = "全部";
            //lbAll.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //lbAll.Click += BtnAll_Click;
            //panelAllP.Controls.Add(lbAll);
            //加载group显示模块
            foreach (GroupConfig1 g1 in ConfigData.GroupConfigRoot.GroupConfig1s)
            {
                foreach (GroupConfig2 g2 in g1.GroupConfigs)
                {
                    foreach (GroupConfig3 g3 in g2.GroupConfigs)
                    {
                        //组显示模块
                        TableLayoutPanel tlp = new TableLayoutPanel();                        
                        tlp.BackColor = System.Drawing.Color.FromArgb(255, 83, 100, 132);
                        tlp.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;                        
                        //tlp.MinimumSize = new System.Drawing.Size(, 0);
                        tlp.ColumnCount = 1;
                        tlp.Dock = DockStyle.Top;
                        tlp.AutoSize = true;
                        tlp.Margin = new Padding(8);
                        tlp.Padding = new Padding(0);
                        panelAll.Controls.Add(tlp);
                        //每个模块的标题
                        Button lbtitle = new Button();
                        lbtitle.FlatStyle = FlatStyle.Flat;
                        lbtitle.Cursor = Cursors.Hand;
                        lbtitle.BackgroundImage = Properties.Resources.博物馆副本;
                        lbtitle.BackgroundImageLayout = ImageLayout.Stretch;
                        lbtitle.Click += Lbtitle_DoubleClick;
                        lbtitle.TextAlign = ContentAlignment.MiddleLeft;
                        //lbtitle.DoubleClick += Lbtitle_DoubleClick;
                        lbtitle.Font = new System.Drawing.Font("幼圆", 14, System.Drawing.FontStyle.Bold);
                        lbtitle.BackColor = System.Drawing.Color.FromArgb(255, 13, 5, 5);
                        lbtitle.ForeColor = System.Drawing.Color.White;
                        lbtitle.Margin = new Padding(0);
                        lbtitle.Dock = DockStyle.Fill;
                        lbtitle.Text = "        " + g1.Name + " " + g2.Name + " " + g3.Name;
                        lbtitle.AutoSize = true;
                        lbtitle.Parent = tlp;
                        TableLayoutPanel flp = new TableLayoutPanel();
                        flp.ColumnCount = 1;
                        flp.BackColor = System.Drawing.Color.Transparent;
                        flp.AutoScroll = true;
                        flp.Parent = panelAllP;
                        flp.Dock = DockStyle.Fill;
                        lbtitle.Tag = flp;
                        flp.Tag = g3.Sensors;
                        //每个组中的Field
                        foreach (Sensor sensor in g3.Sensors)
                        {                            
                            foreach (Field field in sensor.Model.Fields)
                            {
                                if (!field.Realtime)
                                    continue;
                                Label lb = new Label();
                                   lb.BackColor = System.Drawing.Color.Transparent;
                                lb.ForeColor = System.Drawing.Color.GreenYellow;
                                lb.Dock = DockStyle.Fill;
                                lb.TextAlign = ContentAlignment.MiddleLeft;
                                lb.Margin = new Padding(3);
                                field.Label = lb;
                                lb.AutoSize = true;
                                lb.Parent = tlp;

                                lb.Font = new System.Drawing.Font("幼圆", 12, System.Drawing.FontStyle.Regular);
                                field.Label.Text = field.LabelText;
                            }
                        }
                    }
                }
            }                      
        }
        //组模块标题label点击事件
        private void Lbtitle_DoubleClick(object sender, EventArgs e)
        {
            Control lbtitle = sender as Control;
            //lbAll.Text = lbtitle.Text;            
            TableLayoutPanel flp = lbtitle.Tag as TableLayoutPanel;
            List<Sensor> sensors = (List<Sensor>)flp.Tag;
            foreach (Sensor ss in sensors)
            {
                foreach (Field item in ss.Model.Fields)
                {
                    if (item.Realtime)
                    {
                   
                        flp.Controls.Add(item.ChartPanel);
                        //flp.Controls.Add(item.Chart);
                    }
                }
            }
            flp.BringToFront();
        }     

        private void BtnAll_Click(object sender, EventArgs e)
        {
            glassButtonAll_Click(null, null);
        }

        ToolTip toolTip1 = new ToolTip();
        void InitialModelClickSensors()
        {
            for (int i = 1; i < panelLeft.Controls.Count; i++)
            {
                if (panelLeft.Controls[i] is GlassButton)
                {
                    GlassButton gb = panelLeft.Controls[i] as GlassButton;
                    //FlowLayoutPanel flp = gb.Tag as FlowLayoutPanel;
                    TableLayoutPanel flp = gb.Tag as TableLayoutPanel;
                    List<Sensor> sensors = ConfigData.allSensors.FindAll(ss => ss.ModelKey == gb.Name);
                    foreach (Sensor ss in sensors)
                    {                  
                        foreach (Field field in ss.Model.Fields)
                        {
                            if (!field.Realtime)
                                continue;
                            TableLayoutPanel spanel = new TableLayoutPanel();
                            spanel.Margin = new Padding(5);
                            spanel.MinimumSize = new Size(168, 168);
                            spanel.MaximumSize = new Size(168, 168);
                            spanel.ColumnCount = 1;
                            spanel.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
                            spanel.Parent = flp;
                            flp.Controls.Add(field.ChartPanel2);
                            spanel.ForeColor = Color.Black;
                            spanel.BackColor = Color.White;
                            spanel.AutoScroll = true;
                            Label lb = new Label();
                            lb.BackColor = Color.GreenYellow;
                            toolTip1.SetToolTip(lb, ss.GroupName);
                            field.ClickLabel = lb;
                            //if (ss.Model.Fields.FindAll(f => f.Realtime).Count == 1)
                            //{
                                lb.Font = new Font("微软雅黑", 30, FontStyle.Bold);
                            //}
                            //else if (ss.Model.Fields.FindAll(f => f.Realtime).Count == 2)
                            //{
                            //    lb.Font = new Font("微软雅黑", 20, FontStyle.Bold);
                            //}
                            //else
                            //{
                            //    lb.Font = new Font("微软雅黑", 10, FontStyle.Bold);
                            //}
                            lb.Margin = new Padding(2);
                            spanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
                            lb.Parent = spanel;
                            lb.Dock = DockStyle.Fill;
                            lb.Text = field.CLabelText;
                            lb.Tag = field;
                            //lb.DoubleClick += Lb_DoubleClick;
                        }
                    }
                }
            }
        }

        private void Lb_DoubleClick(object sender, EventArgs e)
        {
            Field field = (Field)((sender as Label).Tag);
            ChartForm cf = new ChartForm();
            field.ChartPanel.Parent = cf;
            cf.ShowDialog();
        }
        #endregion

        void InitializeUIEnd()
        {
            ph.Init();       
            par.Init();
            paset.Init();
            pgs.Init();
            pdg.Init();
            pdg.Parent = panelBotttom;
            pdg.Dock = DockStyle.Fill;
            ph.Parent = panelBotttom;
            ph.Dock = DockStyle.Fill;
            par.Parent = panelBotttom;
            par.Dock = DockStyle.Fill;
            paset.Parent = panelBotttom;
            paset.Dock = DockStyle.Fill;
            pgs.Parent = panelBotttom;
            pgs.Dock = DockStyle.Fill;
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
            if (menuButtonPanel6.DefaultImage != null)
            {
                menuButtonPanel6.ShowDefaultImage(null, null);
            }
            menuButtonPanel1.Panel = panelRuntime;
            menuButtonPanel2.Panel = ph;
            menuButtonPanel3.Panel = par;
            menuButtonPanel4.Panel = paset;
            menuButtonPanel5.Panel = pgs;
            menuButtonPanel6.Panel = pdg;
        }  

        void InitializeUI()
        {
            panelAll.Padding = new Padding(5);
            panelAll.AutoScroll = true;
            panelAllP.Dock = DockStyle.Fill;
            panelAllP.AutoScroll = true;            
            panelAll.Dock = DockStyle.Fill;            
            panelItem.AutoScroll = true;
            panelItem.Dock = DockStyle.Fill;                      
            panelRight.Controls.Add(panelAllP);
            panelRight.Controls.Add(panelItem);
            panelAllP.Controls.Add(panelAll);
            panelAllP.BringToFront();
        }           
     
        //测试
        bool b = false;
        int value1 = 1226;
        int value2 = 6666;
        Random r = new Random();
        private void glassButton1_Click(object sender, EventArgs e)
        {
            //panelRuntime.BringToFront();
            //测试
            int add = r.Next(-20, 20);
            string jstr;
            if (b)
            {
                jstr = "{\"state\":\"Stream\",\"parser\":\"MXS1501\",\"raw\":\"7E000B7D1A000001000000330A4081817F01E524226D050100000100B01C00\",\"data\":[{\"name\":\"nodeid\",\"alias\":\"节点编号\",\"type\":\"uint16\",\"raw\":\"0x0100\",\"converted\":\"1\"},{\"name\":\"uid\",\"alias\":\"网关唯一号\",\"type\":\"raw\",\"raw\":\"0x81817F01E524226D\"},{\"name\":\"parent\",\"alias\":\"父级节点\",\"type\":\"uint16\",\"raw\":\"0x0000\",\"converted\":\"0\"},{\"name\":\"port\",\"alias\":\"采集通道\",\"type\":\"uint8\",\"raw\":\"0x01\",\"converted\":\"1\"},{\"name\":\"light\",\"alias\":\"太阳光照(lux)\",\"type\":\"uint32\",\"raw\":\"0x00B01C00\",\"converted\":\"" + value1 + "\"}]}";
                b = false;
                value1+=add;
            }
            else
            {
                jstr = "{\"state\":\"Stream\",\"parser\":\"MXN820\",\"raw\":\"7E000B7D1A000001000000330A5E81817F01E524226DFC000000000000E70D\",\"data\":[{\"name\":\"nodeid\",\"alias\":\"节点编号\",\"type\":\"uint16\",\"raw\":\"0x0100\",\"converted\":\"1\"},{\"name\":\"uid\",\"alias\":\"网关唯一号\",\"type\":\"hex\",\"raw\":\"0x81817F01E524226D\",\"converted\":\"81817F01E524226D\"},{\"name\":\"parent\",\"alias\":\"父级节点\",\"type\":\"uint16\",\"raw\":\"0x0000\",\"converted\":\"0\"},{\"name\":\"port\",\"alias\":\"采集通道\",\"type\":\"uint8\",\"raw\":\"0x00\",\"converted\":\"0\"},{\"name\":\"chargeVol\",\"alias\":\"充电电压(mv)\",\"type\":\"uint16\",\"raw\":\"0x0000\",\"converted\":\"0\"},{\"name\":\"battVol\",\"alias\":\"电池电压(mv)\",\"type\":\"uint16\",\"raw\":\"0xE70D\",\"converted\":\"" + value2 + "\"}]}";
                b = true;
                value2+=add;
            }
            JObject jobj = JObject.Parse(jstr);
            DataParser.ParseJObj(jobj);
        }
       
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

       // SetForm sf = new SetForm();
        private void button4_Click(object sender, EventArgs e)
        {
            //设置窗口         
            new AboutBox1().ShowDialog();
        }

        private void 数据报表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DTPickerForm df = new DTPickerForm();
            df.ShowDialog();
        }

        private void 用户管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Account acc = new Account();
            acc.ShowDialog();
        }
    }

}
