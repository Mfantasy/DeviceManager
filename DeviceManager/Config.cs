﻿using DeviceManager.Model;
using FOF.UserControlModel;
using System.Collections.Generic;
using System.Configuration;
using System.Windows.Forms;
using System.Xml;
using static DeviceManager.Model.GroupConfig;
using System;
using DeviceManager.CustomControl;
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
            tv.Font = new System.Drawing.Font("宋体", 11);
            tv.Visible = false;
            tv.Dock = DockStyle.Fill;
            tv.NodeMouseDoubleClick += Tv_NodeMouseClick;                    
            btnAll.Dock = DockStyle.Top;
            btnAll.FlatAppearance.BorderSize = 0;
            btnAll.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            btnAll.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;            
            btnAll.FlatStyle = FlatStyle.Flat;
            btnAll.ForeColor = System.Drawing.Color.White;
            btnAll.Font = new System.Drawing.Font("宋体", 11);
            btnAll.Text = "全部";
            btnAll.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            btnAll.Click += BtnAll_Click;
            btnAll.Tag = tv;
            panelAll.Parent.Controls.Add(tv);
            panelAll.Parent.Controls.Add(btnAll);
            panelAll.Padding = new Padding(5);            
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
                                //颜色
                                tlp.BackColor = System.Drawing.Color.AliceBlue;                                
                                tlp.CellBorderStyle = TableLayoutPanelCellBorderStyle.OutsetDouble;
                                tlp.MinimumSize = new System.Drawing.Size(840, 0);
                                tlp.ColumnCount = 1;
                                tlp.Name = cfgc.Key;                                              
                                tlp.Dock = DockStyle.Top;
                                tlp.AutoSize = true;
                                tlp.Margin = new Padding(4);
                                n3.Tag = tlp;                           
                                tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,100));
                                //tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,50));
                                panelAll.Controls.Add(tlp);
                                //Control tlpCD = GetCD(tlp.Parent.Parent, cfgc.Sensors, cfg.Name + " " + cfgp.Name + " " + cfgc.Name);                                
                                //tlp.BringToFront();                                
                                Label lbtitle = new Label();
                                lbtitle.Tag = cfgc.Sensors;
                                lbtitle.DoubleClick += Tlp_DoubleClick;
                                lbtitle.Font = new System.Drawing.Font("微软雅黑", 24, System.Drawing.FontStyle.Bold);
                                lbtitle.BackColor = System.Drawing.Color.Blue;
                                lbtitle.ForeColor = System.Drawing.Color.White;
                                lbtitle.Margin = new Padding(5);
                                lbtitle.Dock = DockStyle.Fill;
                                lbtitle.Text = cfg.Name+" "+ cfgp.Name+" "+cfgc.Name;
                                lbtitle.AutoSize = true;
                                lbtitle.Parent = tlp;
                                foreach (Sensor sensor in cfgc.Sensors)
                                {
                                    sensor.GroupName = cfg.Name + " " + cfgp.Name + " " + cfgc.Name;                                  
                                    foreach (Field field in sensor.Model.Fields)
                                    {
                                        if (!field.Realtime)
                                            continue;
                                        Label lb = new Label();
                                        lb.BackColor = System.Drawing.Color.LightGreen;
                                        lb.Dock = DockStyle.Fill;
                                        lb.Margin = new Padding(5);
                                        field.Label = lb;
                                        lb.AutoSize = true;
                                        lb.Parent = tlp;
                                        lb.Font = new System.Drawing.Font("宋体", 32,System.Drawing.FontStyle.Bold);
                                        field.Label.Text = field.LabelText;                                                                         
                                    }                                                                        
                                }
                            }
                        }
                    }
                } 
            }
        }

        private static Control GetCD(Control parent, List<Sensor> sensors,string name)
        {
            //点击GroupPanel发生
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
                        row.ItemArray = new object[] { item.Alias, item.Value, null, ss.Comment };
                        item.Row = row;
                        table.Rows.Add(row);
                        //table.Rows.Add(item.Alias, item.Value, ss.Time, ss.Comment);
                    }
                }
            }
            cdv.DataSource = table;
            cdv.Font = new System.Drawing.Font("宋体", 11);
            //cdv.Columns["备注"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            return cdv;
        }

        //显示图表
        private static void Tlp_DoubleClick(object sender, System.EventArgs e)
        {
            Control lbt = sender as Control;
            btnTxt = btnAll.Text;
            btnAll.Text = lbt.Text;
            List<Sensor> sensors = (List<Sensor>)lbt.Tag;
            Control parent = lbt.Parent.Parent.Parent;
            TableLayoutPanel flp = new TableLayoutPanel();
            flp.ColumnCount = 1;
            flp.BackColor = System.Drawing.Color.Transparent;
            flp.AutoScroll = true;
            flp.Parent = parent;
            flp.Dock = DockStyle.Fill;
            foreach (Sensor ss in sensors)
            {
                foreach (Field item in ss.Model.Fields)
                {
                    if (item.Realtime)
                    {
                        flp.Controls.Add(item.Chart);
                        item.Chart.Titles[0].Text = ss.Comment;
                        item.Chart.Dock = DockStyle.Fill;
                        item.Chart.Margin = new Padding(3);
                    }
                }
            }
            flp.BringToFront();
            
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

        public static string btnTxt = "全部";
        private static void BtnAll_Click(object sender, System.EventArgs e)
        {
            Program.mainForm.glassButtonAll_Click(null, null);
            //TreeView tv = btnAll.Tag as TreeView;
            //if (tv.Visible)
            //{
            //    tv.Visible = false;
            //    btnAll.Text = btnTxt;          
            //}
            //else
            //{
            //    tv.Visible = true;
            //    tv.BringToFront();
            //    btnAll.Text = "收起";
            //}
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
            foreach (SensorModel smodel in sroot.SensorModels)
            {
                FlowLayoutPanel flp = new FlowLayoutPanel();
                flp.Dock = DockStyle.Fill;
                flp.Parent = panelItem;
                flp.Padding = new Padding(3);
                flp.Tag = panelItem;
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
            FlowLayoutPanel flp = gbtn.Tag as FlowLayoutPanel;
            Panel panel = flp.Tag as Panel;
            panel.BringToFront();
            flp.BringToFront();            
        }
        #endregion

    }
}
