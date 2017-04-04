using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DeviceManager.Model;

namespace DeviceManager.CustomControl
{
    public partial class PanelHistory : UserControl
    {
        public PanelHistory()
        {
            InitializeComponent();
       
            
        }

        public void Init()
        {
            List<GroupConfig> groups = ConfigData.GroupCfg.GroupConfigs;
            foreach (GroupConfig gcfg in groups)
            {                
                BuildTree(gcfg, treeView1.Nodes);
            }
            treeView1.ExpandAll();
            treeView1.NodeMouseClick += TreeView1_NodeMouseClick;
            dateTimePickerRetrieveEnd.Value = DateTime.Now;
            dateTimePickerRetrieveBegin.Value = DateTime.Now.AddDays(-1);
        }

        private void TreeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode node = e.Node;
            if (node.Tag is Sensor)
            {                
                Sensor sensor = node.Tag as Sensor;
                SelectData(sensor);
                return;                                                                    
            }
        }

        void BuildTree(GroupConfig cfg,TreeNodeCollection nodes)
        {
            TreeNode tnode = nodes.Add(cfg.Name);
            if (cfg.GroupConfigs != null && cfg.GroupConfigs.Count !=0)
            {
                foreach (GroupConfig item in cfg.GroupConfigs)
                {
                    BuildTree(item, tnode.Nodes);
                }
            }
            else
            {
                foreach (Sensor ss in cfg.Sensors)
                {
                    TreeNode tn = tnode.Nodes.Add(ss.Comment);
                    tn.ToolTipText = ss.Model.Title;
                    tn.Tag = ss;
                }
            }
        }


        private void glassButton2_Click(object sender, EventArgs e)
        {
            if(treeView1.SelectedNode.Tag is Sensor)
            {
                Sensor sensor = treeView1.SelectedNode.Tag as Sensor;
                SelectData(sensor);
            }
        }
        void SelectData(Sensor sensor)
        {
            //DataTable dt= SqlLiteHelper.ExecuteReader("SELECT * FROM SCity_MX8100_result");
            string sql = string.Format("SELECT {0} FROM {1}_result WHERE time < '{2}' and time >'{3}' and nodeid = {4} ", sensor.HisColumnStr, sensor.Model.Sname, dateTimePickerRetrieveEnd.Value.ToString("yyyy-MM-dd HH-mm"), dateTimePickerRetrieveBegin.Value.ToString("yyyy-MM-dd HH-mm"), sensor.NodeId);
            try
            {
                DataTable dt = SqlLiteHelper.ExecuteReader(sql);
                var c1 = dt.Columns.Add("传感器类型");
                var c2 = dt.Columns.Add("传感器位置");
                var c3 = dt.Columns.Add("备注");
                foreach (DataRow row in dt.Rows)
                {
                    row[c1] = sensor.Model.Title;
                    row[c2] = sensor.GroupName;
                    row[c3] = sensor.Comment;
                }
                customDataView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                if (ex.HResult == -2147467259)
                {
                    MessageBox.Show(string.Format("{0}({1})暂无数据", sensor.Model.Title, sensor.Comment));
                }
                else
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void glassButton1_Click(object sender, EventArgs e)
        {
            if (customDataView1.DataSource is DataTable)
            {
                DataTable dt = customDataView1.DataSource as DataTable;
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Excel文件|.xlsx";
                sfd.RestoreDirectory = true;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    Utils.ExportToXls(sfd.FileName, dt);
                }
            }
            else
            {
                MessageBox.Show("没有数据");
            }
            
        }
    }
}
