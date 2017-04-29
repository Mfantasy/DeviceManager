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
using DeviceManager.CustomForm;

namespace DeviceManager.CustomControl
{
    public partial class PanelAllSensors : UserControl
    {
        public PanelAllSensors()
        {
            InitializeComponent();
            ToolStripItem tsc = new ToolStripButton("重命名");
            tsc.Click += Tsc_Click;
            ToolStripItem tsd = new ToolStripButton("删除");
            tsd.Click += Tsi_Click;
            cs.Items.Add(tsc);
            cs.Items.Add(tsd);                        
            tabControl1.ContextMenuStrip = cs;
        }
        ContextMenuStrip cs = new ContextMenuStrip();

        private void Tsc_Click(object sender, EventArgs e)
        {            
            InputForm inf = new InputForm();
            if (inf.ShowDialog() == DialogResult.OK)
            {
                string name = inf.StrValue;
                if (!string.IsNullOrWhiteSpace(name))
                {
                    MessageBox.Show(string.Format("确定将[{0}]重命名为[{1}]?",tabControl1.SelectedTab.Text,name), "提醒", MessageBoxButtons.YesNo);
                    tabControl1.SelectedTab.Text = name;
                    return;                    
                }
            }
        }

        private void Tsi_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("确定删除?", "提醒", MessageBoxButtons.OKCancel)== DialogResult.OK)
            {

            }
        }
     
        public void Init()
        {            
            List<GroupConfig1> groups = ConfigData.GroupConfigRoot.GroupConfig1s;
            foreach (GroupConfig1 g1 in groups)
            {
                TabPage tabpage = new TabPage(g1.Name);
                tabpage.Tag = g1;         
                tabControl1.TabPages.Add(tabpage);
                CustomPASListView paslv = new CustomPASListView(g1);
                paslv.Dock = DockStyle.Fill;
                paslv.Parent = tabpage;
                foreach (GroupConfig2 g2 in g1.GroupConfigs)
                {
                    ListViewGroup lvg = new ListViewGroup(g2.Name);
                    paslv.Groups.Add(lvg);
                    foreach (GroupConfig3 g3 in g2.GroupConfigs)
                    {
                        foreach (Sensor ss in g3.Sensors)
                        {
                            ListViewItem lvi = new ListViewItem(lvg);
                            paslv.Items.Add(lvi);
                            lvi.Group = lvg;
                            lvi.Text = ss.GroupName;
                            lvi.SubItems.AddRange(new ListViewItem.ListViewSubItem[] {                           
                                new ListViewItem.ListViewSubItem(lvi,ss.Model.Title),
                                new ListViewItem.ListViewSubItem(lvi,ss.Comment),                                
                                new ListViewItem.ListViewSubItem(lvi,ss.Uid),
                                new ListViewItem.ListViewSubItem(lvi,ss.NodeId),
                                new ListViewItem.ListViewSubItem(lvi,ss.PortId)
                                });
                        }
                    }
                }
                paslv.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                paslv.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            }
            TabPage tbAdd = new TabPage(" 十");
            tabControl1.TabPages.Add(tbAdd);
            tbAdd.Name = "tbAdd";
            tabControl1.Selected += TabControl1_Selected;                      
        }

        private void TabControl1_Selected(object sender, TabControlEventArgs e)
        {            
            if (e.TabPage == tabControl1.TabPages["tbAdd"])
            {
                InputForm inf = new InputForm();
                if (inf.ShowDialog() == DialogResult.OK)
                {
                    string name = inf.StrValue;
                    if (!string.IsNullOrWhiteSpace(name))
                    {
                        tabControl1.TabPages.Insert(tabControl1.TabPages.Count - 1, name, name);
                        GroupConfig1 gc1 = new GroupConfig1();
                        gc1.Name = name;
                        tabControl1.TabPages[name].Tag = gc1;
                        return;
                    }
                }                                
                    tabControl1.SelectedTab = tabControl1.TabPages[0];                                    
            }                
        }       
    }
}
