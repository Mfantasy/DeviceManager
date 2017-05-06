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
    public partial class CustomSensorDataView : UserControl
    {
        public CustomSensorDataView()
        {
            InitializeComponent();
        }

        public GroupConfig3 G3 { get; set; }
        public CustomSensorDataView(GroupConfig3 g3)
        {
            InitializeComponent();  
            groupBox1.Text = g3.Name;
            dataGridView1.DragEnter += DataGridView1_DragEnter;
            dataGridView1.DragDrop += CustomSensorDataView_DragDrop;
            
        }

        private void CustomSensorDataView_DragDrop(object sender, DragEventArgs e)
        {
            Sensor ss = e.Data.GetData(typeof(Sensor)) as Sensor;
            
        }

        private void DataGridView1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(Sensor)))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }
    }
}
