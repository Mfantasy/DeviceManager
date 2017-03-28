using DeviceManager.Model;
using FOF.UserControlModel;
using System.Collections.Generic;
using System.Configuration;
using System.Windows.Forms;
using System.Xml;
using static DeviceManager.Model.GroupConfig;
using System;
using DeviceManager.UserControl;
using System.Data;

namespace DeviceManager
{
    public static class ConfigData
    {
        public static SensorModelRoot SensorModelCfg = null;
        public static SensorRoot AllSensors = null;
        public static GroupConfigRoot GroupCfg = null;
        public static AlarmConfigRoot AlarmCfg = null;
    }

    public static class Config
    {
        public static bool IsShowLogon = false;
        //public static bool IsMySql = true;
    }

    public class ConfigSaver
    {        
        void Test()
        {
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load("");
            xdoc.ChildNodes[1].Attributes[0].Value = "";
            xdoc.Save("");
        }
    }

    public static class ConfigParser
    {
        #region 分组配置
        public static Button btnAll = new Button();
        public static void ParseGroups(Panel panelAll)
        {        
            string fileName = ConfigurationManager.AppSettings["分组配置文件"];
            GroupConfigRoot gcfg = Utils.FromXMLFile<GroupConfigRoot>(fileName);
            ConfigData.GroupCfg = gcfg;
            //载入功能
            TreeView tv = new TreeView();
            tv.Visible = false;
            tv.Dock = DockStyle.Fill;
            tv.NodeMouseDoubleClick += Tv_NodeMouseClick;                    
            btnAll.Dock = DockStyle.Top;
            btnAll.Text = "全部";
            btnAll.Click += BtnAll_Click;
            btnAll.Tag = tv;
            panelAll.Parent.Controls.Add(tv);
            panelAll.Parent.Controls.Add(btnAll);
                                 
            foreach (GroupConfig cfg in gcfg.GroupConfigs)
            {
                TreeNode n1 = tv.Nodes.Add(cfg.Name);
                if (cfg.GroupConfigs != null)
                {
                    foreach (GroupConfig cfgp in cfg.GroupConfigs)
                    {
                        TreeNode n2 = n1.Nodes.Add(cfgp.Name);
                        if (cfgp.GroupConfigs != null)
                        {
                            foreach (GroupConfig cfgc in cfgp.GroupConfigs)
                            {
                                TreeNode n3 = n2.Nodes.Add(cfgc.Name);                                
                                TableLayoutPanel tlp = new TableLayoutPanel();
                                tlp.DoubleClick += Tlp_DoubleClick;
                                tlp.CellBorderStyle = TableLayoutPanelCellBorderStyle.InsetDouble;
                                tlp.MinimumSize = new System.Drawing.Size(415, 0);
                                tlp.ColumnCount = 2;
                                tlp.Name = cfgc.Key;                                              
                                tlp.Dock = DockStyle.Top;
                                tlp.AutoSize = true;
                                tlp.Margin = new Padding(3);
                                n3.Tag = tlp;                           
                                tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,50));
                                tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,50));
                                panelAll.Controls.Add(tlp);
                                Control tlpCD = GetCD(tlp.Parent.Parent, cfgc.Sensors, cfg.Name + " " + cfgp.Name + " " + cfgc.Name);
                                tlp.Tag = tlpCD;
                                //tlp.BringToFront();                                
                                Label lbtitle = new Label();
                                lbtitle.Margin = new Padding(3);
                                lbtitle.Text = cfg.Name+" "+ cfgp.Name+" "+cfgc.Name;
                                lbtitle.AutoSize = true;
                                lbtitle.Parent = tlp;
                                tlp.SetColumnSpan(lbtitle, 2);
                                foreach (Sensor sensor in cfgc.Sensors)
                                {
                                    foreach (Field field in sensor.Model.Fields)
                                    {
                                        if (!field.Realtime)
                                            continue;
                                        Label lb = new Label();
                                        lb.Margin = new Padding(3);
                                        field.Label = lb;
                                        lb.AutoSize = true;
                                        lb.Parent = tlp;
                                        lb.Font = new System.Drawing.Font("宋体", 11);
                                        field.Label.Text = field.LabelText;                                                                         
                                    }                                                                        
                                }
                            }
                        }
                    }
                } 
            }
        }

        private static DataGridView GetCD(Control parent, List<Sensor> sensors,string name)
        {
            CustomDataView cdv = new CustomDataView();            
            cdv.Parent = parent;
            cdv.Dock = DockStyle.Fill;
            cdv.Name = name;
            DataTable table = new DataTable();            
            table.Columns.Add("监测项");
            table.Columns.Add("数值");
            table.Columns.Add("数据时间");
            table.Columns.Add("备注");                        
            foreach (Sensor ss in sensors)
            {
                foreach (Field item in ss.Model.Fields)
                {
                    if (item.Realtime)
                    {
                        DataRow row = table.NewRow();
                        row.ItemArray =new object[] { item.Alias, item.Value, ss.Time, ss.Comment };
                        item.Row = row;
                        table.Rows.Add(row);
                        //table.Rows.Add(item.Alias, item.Value, ss.Time, ss.Comment);
                    }
                }
            }
            cdv.DataSource = table;
            cdv.Font = new System.Drawing.Font("宋体", 12);
            //cdv.Columns["备注"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            return cdv;
        }

        private static void Tlp_DoubleClick(object sender, System.EventArgs e)
        {
            Control ctrl = sender as Control;
            Control tlpCP = (sender as Control).Tag as Control;
            tlpCP.BringToFront();
            btnAll.Text = (ctrl.Tag as Control).Name;
            btnTxt = btnAll.Text;
        }

        private static void Tv_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode n3 = e.Node;
            if(n3.Tag is TableLayoutPanel)
            {
                TableLayoutPanel tlp = n3.Tag as TableLayoutPanel;
                Control dgvCD = tlp.Tag as Control;
                dgvCD.BringToFront();
                BtnAll_Click(null, null);                
                btnAll.Text = n3.Parent.Parent.Text+" "+n3.Parent.Text+" "+ n3.Text;
                btnTxt = btnAll.Text;
            }
        }

        public static string btnTxt = "";
        private static void BtnAll_Click(object sender, System.EventArgs e)
        {            
            TreeView tv = btnAll.Tag as TreeView;
            if (tv.Visible)
            {
                tv.Visible = false;
                btnAll.Text = btnTxt;          
            }
            else
            {
                tv.Visible = true;
                tv.BringToFront();
                btnAll.Text = "收起";
            }
        }
     
        #endregion

        #region 报警配置
        public static void ParseAlarms()
        {
            string fileName = ConfigurationManager.AppSettings["报警配置文件"];
            AlarmConfigRoot acfg = Utils.FromXMLFile<AlarmConfigRoot>(fileName);
            ConfigData.AlarmCfg = acfg;
        }
        #endregion

        #region 传感器列表
        public static void ParseSensors()
        {
            string fileName = ConfigurationManager.AppSettings["传感器列表配置文件"];
            SensorRoot sensors = Utils.FromXMLFile<SensorRoot>(fileName);
            ConfigData.AllSensors = sensors;
        }
        #endregion

        #region 界面配置
        public static void ParseUI(MainForm form)
        {
            string fileName = ConfigurationManager.AppSettings["界面配置文件"];
            UIModel ui = Utils.FromXMLFile<UIModel>(fileName);
            SetElement(form, ui.Elements);
        }

        static void SetElement(Control parent, List<Element> elements)
        {
            foreach (Element element in elements)
            {
                Control ctrlP = parent.Controls.Find(element.Name, false)[0];
                if (element.Properties != null)
                {
                    SetProperty(ctrlP, element.Properties);
                }
                if (element.Children != null)
                {
                    foreach (Child child in element.Children)
                    {                
                        Control ctrlC = ctrlP.Controls.Find(child.Name, false)[0];
                        if (child.Properties != null)
                        {
                            SetProperty(ctrlC, child.Properties);
                        }
                    }
                }
                if (element.Elements != null)
                {
                    SetElement(ctrlP, element.Elements);
                }
            }
        }

        static void SetProperty(Control ctrl, List<Property> properties)
        {
            foreach (Property property in properties)
            {
                ctrl.GetType().GetProperty(property.Name).SetValue(ctrl, property.Value);
            }
        }
        #endregion

        #region 传感器模型配置
        public static void ParseSensorModel(Panel panelLeft,Panel panelItem)
        {
            string fileName = ConfigurationManager.AppSettings["传感器模型配置文件"];
            SensorModelRoot sroot = Utils.FromXMLFile<SensorModelRoot>(fileName);
            ConfigData.SensorModelCfg = sroot;
            foreach (SensorModel smodel in sroot.Sensors)
            {
                FlowLayoutPanel flp = new FlowLayoutPanel();
                flp.Dock = DockStyle.Fill;
                flp.Parent = panelItem;
                flp.Tag = smodel;
                GlassButton gbtn = new GlassButton();
                gbtn.Name = smodel.Name;
                gbtn.ButtonText = smodel.Title;
                gbtn.Font = new System.Drawing.Font("宋体", 11, System.Drawing.FontStyle.Regular);
                gbtn.Size = new System.Drawing.Size(160, 36);
                gbtn.Tag = flp;
                gbtn.Click += Gbtn_Click;
                panelLeft.Controls.Add(gbtn);
            }
        }
        
        private static void Gbtn_Click(object sender, System.EventArgs e)
        {
            GlassButton gbtn = sender as GlassButton;

            //读XML配置文件,并将XML备份到User目录下
#warning 左侧按钮点击事件,秀出设备
        }
        #endregion

    }
}
