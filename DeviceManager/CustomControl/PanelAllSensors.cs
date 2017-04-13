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
          
        }

        public void Init()
        {            
            List<GroupConfig> groups = ConfigData.GroupCfg.GroupConfigs;
            for (int i = 0; i < groups.Count; i++)
            {
                TabPage tp = new TabPage(groups[i].Name);              
                tabControl1.TabPages.Add(tp);
                PASListView paslv = new PASListView();
                paslv.Dock = DockStyle.Fill;             
                paslv.Parent = tp;                
                for (int j = 0; j < groups[i].GroupConfigs.Count; j++)
                {
                    ListViewGroup lvg = new ListViewGroup(groups[i].GroupConfigs[j].Name);
               
                    paslv.Groups.Add(lvg);
                    List<GroupConfig> items = groups[i].GroupConfigs[j].GroupConfigs;
                    for (int k = 0; k < items.Count; k++)
                    {
                        for (int l = 0; l < items[k].Sensors.Count; l++)
                        {                                                       
                            ListViewItem lvi = new ListViewItem(lvg);
                            paslv.Items.Add(lvi);
                            lvi.Group = lvg;                            
                            lvi.Text = items[k].Name;
                            lvi.SubItems.AddRange(new ListViewItem.ListViewSubItem[] {
                                new ListViewItem.ListViewSubItem(lvi,items[k].Sensors[l].Model.Title),
                                new ListViewItem.ListViewSubItem(lvi,items[k].Sensors[l].Comment),
                                new ListViewItem.ListViewSubItem(lvi,items[k].Sensors[l].Model.AlarmName),
                                new ListViewItem.ListViewSubItem(lvi,items[k].Sensors[l].Uid),
                                new ListViewItem.ListViewSubItem(lvi,items[k].Sensors[l].NodeId),
                                new ListViewItem.ListViewSubItem(lvi,items[k].Sensors[l].PortId)
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
                    string x = inf.StrValue;
                    MessageBox.Show(x);
                }
                else
                {
                    MessageBox.Show("GD");
                }
                tabControl1.TabPages.Insert(tabControl1.TabPages.Count - 1,"tbtest", "测试");
                
            }                
        }       
    }
}
