﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using DeviceManager.Model;

namespace DeviceManager.CustomControl
{
    public partial class PanelGroupSensors : UserControl
    {
        public PanelGroupSensors()
        {
            InitializeComponent();
            this.BorderStyle = BorderStyle.Fixed3D;           
                       
            treeView1.AfterLabelEdit += TreeView1_AfterLabelEdit;
            
         
            dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;
            dataGridView2.EditMode = DataGridViewEditMode.EditProgrammatically;
            dataGridView1.RowHeaderMouseDoubleClick += DataGridView1_RowHeaderMouseDoubleClick;
            dataGridView2.RowHeaderMouseDoubleClick += DataGridView2_RowHeaderMouseDoubleClick;
        }

        private void TreeView1_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Node.Tag is GroupConfig1)
            {
                var g1 = e.Node.Tag as GroupConfig1;
                g1.Name = e.Label;
                if (e.Node.Nodes.Count == 0)
                {
                    TreeNode node = new TreeNode("未命名节点");
                    GroupConfig2 g2 = new GroupConfig2();
                    g2.GroupConfigs = new List<GroupConfig3>();
                    ((GroupConfig1)(e.Node.Tag)).GroupConfigs.Add(g2);
                    node.Tag = g2;
                    e.Node.Nodes.Add(node);
                    node.Parent.ExpandAll();
                }
            }
            else if (e.Node.Tag is GroupConfig2)
            {
                var g2 = e.Node.Tag as GroupConfig2;
                g2.Name = e.Label;
                if (e.Node.Nodes.Count == 0)
                {
                    TreeNode node = new TreeNode("未命名节点");                  
                    GroupConfig3 g3 = new GroupConfig3();
                    g3.Sensors = new List<Sensor>();
                    ((GroupConfig2)(e.Node.Tag)).GroupConfigs.Add(g3);
                    node.Tag = g3;
                    e.Node.Nodes.Add(node);
                    node.Parent.ExpandAll();
                }
            }
            else if (e.Node.Tag is GroupConfig3)
            {
                var g3 = e.Node.Tag as GroupConfig3;
                g3.Name = e.Label;
            }
        }

        private void DataGridView2_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (G3 != null && G3.Sensors.Count >0)
            {
                listS.Add(G3.Sensors[e.RowIndex]);
                dataGridView1.DataSource = null;
                BindingSource bindingSource = new BindingSource();
                bindingSource.DataSource = listS;
                dataGridView1.DataSource = bindingSource; ;
                G3.Sensors.RemoveAt(e.RowIndex);
                dataGridView2.DataSource = null;
                BindingSource bindingSource1 = new BindingSource();
                bindingSource1.DataSource = G3.Sensors;
                dataGridView2.DataSource = bindingSource1;
                AfterSourceChange(dataGridView1);
                AfterSourceChange(dataGridView2);
            }
        }       

        List<Sensor> listS = new List<Sensor>();
        private void DataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (G3 != null)
            {
                G3.Sensors.Add(listS[e.RowIndex]);
                listS.RemoveAt(e.RowIndex);
                dataGridView1.DataSource = null;
                BindingSource bindingSource = new BindingSource();
                bindingSource.DataSource = listS;
                dataGridView1.DataSource = bindingSource;
                dataGridView2.DataSource = null;
                BindingSource bindingSource1 = new BindingSource();
                bindingSource1.DataSource = G3.Sensors;
                dataGridView2.DataSource = bindingSource1;
                AfterSourceChange(dataGridView1);
                AfterSourceChange(dataGridView2);
            }
            
        }

        void GetListS()
        {
            // < sensor model = "MXN820" uid = "81817F01E524226D" nodeid = "1"  portid = "0" comment = "状态包" />
            string dateTime = DateTime.Now.AddDays(-60).ToString("yyyy-MM-dd HH:mm");
            string dataSource = ConfigurationManager.AppSettings["dbPath"];
            foreach (SensorModel model in ConfigData.SensorModelRoot.SensorModels)
            {
                string sql = string.Format("SELECT distinct uid,nodeid,port FROM {0}_result WHERE time > '{1}'", model.Sname, dateTime);
                try
                {
                    DataTable dt = SqlLiteHelper.ExecuteReader(dataSource, sql);
                    foreach (DataRow row in dt.Rows)
                    {
                        Sensor ss = new Sensor();
                        ss.Comment = "";
                        ss.ModelKey = model.Name;
                        ss.NodeId = row["nodeid"].ToString();
                        ss.PortId = row["port"].ToString();
                        ss.Uid = row["uid"].ToString();
                        if (ConfigData.allSensors.Exists(sensor => sensor.Uid == ss.Uid && sensor.NodeId == ss.NodeId && sensor.PortId == ss.PortId))
                        {
                            continue;
                        }
                        listS.Add(ss);                        
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
        }

        void AfterSourceChange(DataGridView dgv)
        {       
            dgv.Columns["Model"].Visible = false;
            dgv.Columns["GroupName"].Visible = false;
            dgv.Columns["CurrentGroup3"].Visible = false;
            dgv.Columns["HisColumnStr"].Visible = false;
            dgv.Columns["Uid"].HeaderText = "网关号";
            dgv.Columns["NodeId"].HeaderText = "节点";
            dgv.Columns["PortId"].HeaderText = "通道";
            dgv.Columns["Comment"].HeaderText = "备注";
            dgv.Columns["ModelKey"].HeaderText = "型号";          
        }

        public void Init()
        {
            GetListS();
            BindingSource bindingSource = new BindingSource();
            bindingSource.DataSource = listS;
            dataGridView1.DataSource = bindingSource;
            AfterSourceChange(dataGridView1);
            List<GroupConfig1> groups = ConfigData.GroupConfigRoot.GroupConfig1s;
            foreach (GroupConfig1 g1 in groups)
            {
                TreeNode tn2 = treeView1.Nodes.Add(g1.Name);
                tn2.Tag = g1;
                foreach (GroupConfig2 g2 in g1.GroupConfigs)
                {
                    TreeNode tn3 = tn2.Nodes.Add(g2.Name);
                    tn3.Tag = g2;
                    foreach (GroupConfig3 g3 in g2.GroupConfigs)
                    {
                        TreeNode tns = tn3.Nodes.Add(g3.Name);
                        tns.Tag = g3;
                    }
                }
            }
            treeView1.ExpandAll();
            treeView1.NodeMouseClick += TreeView1_NodeMouseClick;
        }

        public GroupConfig3 G3 { get; set; }

        private void TreeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                GroupConfig3 g3 = e.Node.Tag as GroupConfig3;
                if (g3 != null)//&& g3.Sensors.Count>0)
                {
                    G3 = g3;
                    groupBox4.Text = g3.Name;
                    BindingSource bindingSource = new BindingSource();
                    bindingSource.DataSource = g3.Sensors;
                    dataGridView2.DataSource = null;
                    dataGridView2.DataSource = bindingSource;
                    AfterSourceChange(dataGridView2);
                }
            }
        }

        private void 编辑ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridView dgv = contextMenuStrip1.SourceControl as DataGridView;
            if (dgv.SelectedCells != null && dgv.SelectedCells.Count > 0 && new string[] { "Comment" }.Contains(dgv.SelectedCells[0].OwningColumn.Name))
            {
                dgv.BeginEdit(true);
            }
            else
            {
                if (dgv.SelectedCells != null && dgv.SelectedCells.Count > 0)
                {
                    MessageBox.Show(string.Format("{0}列不允许编辑", dgv.SelectedCells[0].OwningColumn.HeaderText), "提醒", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string fileName = ConfigurationManager.AppSettings["传感器列表"];
            Utils.ToFile(fileName, ConfigData.GroupConfigRoot);
            MessageBox.Show("保存成功", "提醒", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void 添加根级分组ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode != null)
            {
                treeView1.SelectedNode.BeginEdit();
            }
        }

        private void 添加次级分组ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode != null)
            {
                if (treeView1.SelectedNode.Tag is GroupConfig1)
                {
                    var node = treeView1.Nodes.Add("未命名");
                    GroupConfig1 g1 = new GroupConfig1();
                    ConfigData.GroupConfigRoot.GroupConfig1s.Add(g1);
                    g1.GroupConfigs = new List<GroupConfig2>();
                    node.Tag = g1;
                    node.BeginEdit();                    
                }
                else if (treeView1.SelectedNode.Tag is GroupConfig2)
                {
                    var node = treeView1.SelectedNode.Parent.Nodes.Add("未命名");
                    GroupConfig2 g2 = new GroupConfig2();
                    g2.GroupConfigs = new List<GroupConfig3>();
                    ((GroupConfig1)(treeView1.SelectedNode.Parent.Tag)).GroupConfigs.Add(g2);
                    node.Tag = g2;
                    node.BeginEdit();
                }
                else if (treeView1.SelectedNode.Tag is GroupConfig3)
                {
                    var node = treeView1.SelectedNode.Parent.Nodes.Add("未命名");
                    GroupConfig3 g3 = new GroupConfig3();
                    g3.Sensors = new List<Sensor>();
                    ((GroupConfig2)(treeView1.SelectedNode.Parent.Tag)).GroupConfigs.Add(g3);
                    node.Tag = g3;
                    node.BeginEdit();
                }
            }
        }
    }
}
