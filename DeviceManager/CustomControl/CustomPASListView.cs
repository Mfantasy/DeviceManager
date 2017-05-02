using DeviceManager.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DeviceManager.CustomControl
{
    public partial class CustomPASListView : ListView
    {
        public GroupConfig1 G1 { get; set; }

        public CustomPASListView(GroupConfig1 g1)
        {
            InitializeComponent();
            G1 = g1;
            this.ContextMenuStrip = contextMenuStrip1;
            //重命名分组
            if (g1.GroupConfigs == null)
            {
                g1.GroupConfigs = new List<GroupConfig2>();
            }
            this.toolStripComboBox3.Items.AddRange(g1.GroupConfigs.ToArray());
            //添加设备
            this.toolStripComboBox1.Items.AddRange(g1.GroupConfigs.ToArray());
            toolStripComboBox1.SelectedIndexChanged += ToolStripComboBox1_SelectedIndexChanged;
          
            toolStripMenuItem3.Click += ToolStripMenuItem3_Click;
            确定ToolStripMenuItem.Click += 确定ToolStripMenuItem_Click;
            确定ToolStripMenuItem1.Click += 确定ToolStripMenuItem1_Click;
            确定ToolStripMenuItem2.Click += 确定ToolStripMenuItem2_Click;
        }

        private void 确定ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            //添加设备 toolStripComboBox1 g2 // toolStripComboBox2 g3
        }

        private void  ToolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            toolStripComboBox2.Items.Clear();
            if ((toolStripComboBox1.SelectedItem as GroupConfig2).GroupConfigs != null)
            {
                toolStripComboBox2.Items.AddRange((toolStripComboBox1.SelectedItem as GroupConfig2).GroupConfigs.ToArray());
            }
        }

        private void 确定ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //添加地点
            //toolStripTextBox2
        }

        private void 确定ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //添加组(楼层)
            if (String.IsNullOrWhiteSpace(toolStripTextBox1.Text))
            {
                GroupConfig2 g2 = new GroupConfig2();
                g2.Name = toolStripTextBox1.Text;
                g2.GroupConfigs = new List<GroupConfig3>();
                this.toolStripComboBox3.Items.Add(g2);
                this.toolStripComboBox1.Items.Add(g2);
                G1.GroupConfigs.Add(g2);
            }
        }

        private void ToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            //重命名分组
            if (String.IsNullOrWhiteSpace(toolStripTextBox3.Text))
            {
                ((GroupConfig2)toolStripComboBox3.SelectedItem).Name = toolStripTextBox3.Text;
            }
        }

        private void ToolStripMenuItem1_Click(object sender, System.EventArgs e)
        {
            //编辑此设备
            if (SelectedItems != null)
            {
                Sensor ss = SelectedItems[0].Tag as Sensor;
                if (MessageBox.Show("确认编辑["+ss.GroupName+" "+ss.ModelKey+" "+ss.Comment+" "+"]?","提醒", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    
                }
            }
        }

    }
}
