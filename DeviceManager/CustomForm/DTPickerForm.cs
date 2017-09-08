using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DeviceManager.CustomForm
{
    public partial class DTPickerForm : Form
    {
        public DTPickerForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            dateTimePicker1.Value = DateTime.Now;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.RestoreDirectory = true;
            sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            //string fileName = "";
            sfd.Filter = "Excel文件(*.xls)|*.xls";
            sfd.FileName = string.Format("{0}榆社化石博物馆传感监测典型数据统计表{1}.xls", dateTimePicker1.Value.ToString("H_mm"), dateTimePicker1.Value.ToString("yyyy.M.d"));
            //string fileName = string.Format("{0}榆社化石博物馆传感监测典型数据统计表{1}.xls", dt.ToString("H_mm"), dt.ToString("yyyy.M.d"));
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                Utils.FmtExcel(dateTimePicker1.Value, sfd.FileName);
                MessageBox.Show("保存成功!");
            }
            this.DialogResult = DialogResult.OK;
        }
    }
}
