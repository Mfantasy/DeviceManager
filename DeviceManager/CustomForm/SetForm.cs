using DeviceManager.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DeviceManager.CustomForm
{
    public partial class SetForm : Form
    {
        public SetForm()
        {
            InitializeComponent();
            this.FormClosing += SetForm_FormClosing;
            dataGridView1.RowHeaderMouseDoubleClick += DataGridView1_RowHeaderMouseDoubleClick;
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
            foreach (SensorModel model in ConfigData.SensorModelRoot.SensorModels)
            {
                string sql = string.Format("SELECT distinct uid,nodeid,portid FROM {0}");
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

        public void Init()
        {
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

        private void TreeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
