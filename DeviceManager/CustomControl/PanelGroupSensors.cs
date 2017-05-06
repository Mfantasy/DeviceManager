using System;
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
            dataGridView1.RowHeaderMouseDoubleClick += DataGridView1_RowHeaderMouseDoubleClick;
            dataGridView2.RowHeaderMouseDoubleClick += DataGridView2_RowHeaderMouseDoubleClick;
        }

        private void DataGridView2_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            
        }

        private void DataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
           
            e.RowIndex
            //e.RowIndex
        }

        List<Sensor> listS = new List<Sensor>();
        private void DataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            listS.RemoveAt(e.RowIndex);
            //e.RowIndex
        }

        void GetListS()
        {
            // < sensor model = "MXN820" uid = "81817F01E524226D" nodeid = "1"  portid = "0" comment = "状态包" />
            string dateTime = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd HH:mm");
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
                        listS.Add(ss);                        
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
        }

        private void SetForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("是否保存", "提醒", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                string fileName = ConfigurationManager.AppSettings["传感器列表"];
                Utils.ToFile(fileName, ConfigData.GroupConfigRoot);
            }
        }

        void AfterSourceChange(DataGridView dgv)
        {
            dgv.Columns["Model"].Visible = false;
            dgv.Columns["GroupName"].Visible = false;
            dgv.Columns["CurrentGroup3"].Visible = false;
            dgv.Columns["HisColumnStr"].Visible = false;            
        }

        public void Init()
        {
            GetListS();
            dataGridView1.DataSource = listS;
            AfterSourceChange(dataGridView1);
            List<GroupConfig1> groups = ConfigData.GroupConfigRoot.GroupConfig1s;
            foreach (GroupConfig1 g1 in groups)
            {
                TreeNode tn2 = treeView1.Nodes.Add(g1.Name);
                foreach (GroupConfig2 g2 in g1.GroupConfigs)
                {
                    TreeNode tn3 = tn2.Nodes.Add(g2.Name);
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
            GroupConfig3 g3 = e.Node.Tag as GroupConfig3;
            dataGridView2.DataSource = null;
            dataGridView2.DataSource = g3.Sensors;
            AfterSourceChange(dataGridView2);
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
    }
}
