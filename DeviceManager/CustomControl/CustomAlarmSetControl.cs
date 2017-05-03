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
using System.IO;

namespace DeviceManager.CustomControl
{
    public partial class CustomAlarmSetControl : UserControl
    {     
        public CustomAlarmSetControl()
        {
            InitializeComponent();
        }


        public AlarmConfig AlarmConfig { get; set; }

        public CustomAlarmSetControl(string name,AlarmConfig ac)
        {
            InitializeComponent();
            groupBox1.Text = name;
            AlarmConfig = ac;
            AlarmConfig.Name = name;
            Init();
        }

        public CustomAlarmSetControl(AlarmConfig ac)
        {
            InitializeComponent();
            groupBox1.Text = ac.Name;
            AlarmConfig = ac;
            Init();
        }       

        private void Init()
        {
            //测试
            ConfigData.InitConfig();
            foreach (var item in ConfigData.allFields)
            {
                if ( item.Realtime && !AlarmConfig.AlarmField.Exists(field=>field.Name == item.Name))
                {
                    AlarmConfig.AlarmField.Add(new AlarmField() { Name = item.Name, NameChnName = item.Alias, Model = item.CurrentSensor.Model.Name, ModelChnName = item.CurrentSensor.Model.Title });
                }
            }

            DataGridViewButtonColumn btnCol = new DataGridViewButtonColumn() {HeaderText="",Name="del", Text = "删除", UseColumnTextForButtonValue = true };                                                                 
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.DataSource = AlarmConfig.AlarmField;
            dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;
            dataGridView1.CellClick += DataGridView1_CellClick;            
            dataGridView1.Columns.Add(btnCol);            
            AfterDataSourceUpdate();
            
        }
  
        void AfterDataSourceUpdate()
        {
            dataGridView1.Columns["del"].DisplayIndex = dataGridView1.Columns.Count - 1;
            dataGridView1.Columns["Model"].HeaderText = "型号";
            dataGridView1.Columns["ModelChnName"].HeaderText = "型号名称";
            dataGridView1.Columns["Name"].HeaderText = "监测项";
            dataGridView1.Columns["NameChnName"].HeaderText = "监测名称";
            dataGridView1.Columns["Up"].HeaderText = "上限值";
            dataGridView1.Columns["Low"].HeaderText = "下限值";
            dataGridView1.Columns["Around"].HeaderText = "阈值";            
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 0)
            {
                if (MessageBox.Show("确定删除?","提醒", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    dataGridView1.DataSource = null;
                    AlarmConfig.AlarmField.RemoveAt(e.RowIndex);
                    dataGridView1.DataSource = AlarmConfig.AlarmField;
                    AfterDataSourceUpdate();
                }
            }
        }

        void AddModel(SensorModel sm)
        {
            foreach (Field fd in sm.Fields)
            {
                if (fd.Realtime)
                {
                    AlarmField af = new AlarmField();
                    AlarmConfig.AlarmField.Add(af);
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = AlarmConfig.AlarmField;
                    dataGridView1.Columns[0].DisplayIndex = 8;
                }
            }
        }

     

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            //添加 soure=null;source=list; dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;
            //if (e.RowIndex >= 0 && e.ColumnIndex == 0)
            //{
            //    dataGridView1.DataSource = null;
            //    testList.RemoveAt(e.RowIndex);
            //    dataGridView1.DataSource = testList;
            //    dataGridView1.Columns[0].DisplayIndex = 2;                                
            //}

        }

        private void 编辑ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells != null && dataGridView1.SelectedCells.Count>0 && new string[] { "ModelChnName","NameChnName", "Up", "Low", "Around" }.Contains(dataGridView1.SelectedCells[0].OwningColumn.Name))
            {
                dataGridView1.BeginEdit(true);
            }
            else
            {
                if (dataGridView1.SelectedCells != null && dataGridView1.SelectedCells.Count > 0)
                {
                    MessageBox.Show(string.Format("{0}列不允许编辑", dataGridView1.SelectedCells[0].OwningColumn.HeaderText), "提醒", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            
        }
    }

}
