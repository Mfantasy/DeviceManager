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
using System.Configuration;

namespace DeviceManager.CustomControl
{
    public partial class CustomAlarmSetControl : UserControl
    {     
        public CustomAlarmSetControl()
        {
            InitializeComponent();
        }


        public AlarmConfig AlarmConfig { get; set; }
    
        public CustomAlarmSetControl(AlarmConfig ac)
        {
            InitializeComponent();
            groupBox1.Text = ac.Name;
            AlarmConfig = ac;         
            Init();
        }

        DateTime GetTimeValue(int time)
        {
            int H = time / 100;
            int M = time % 100;
            string dtStr = string.Format("{0}:{1}:0", H, M);
            return DateTime.Parse(dtStr);
        }
        DateTime GetDateValue(int date)
        {
            int M = date / 100;
            int d = date % 100;
            string dtStr = string.Format("2017/{0}/{1}",M,d);
            return DateTime.Parse(dtStr);
        }
        private void Init()
        {                             
            //时间
            if (AlarmConfig.AllTime)
            {
                radioButton1.Checked = true;           
            }
            else
            {
                radioButton2.Checked = true;
                dateTimePicker1.Value = GetTimeValue(AlarmConfig.Starttime);
                dateTimePicker2.Value = GetTimeValue(AlarmConfig.Endtime);
            }
            //日期
            if (AlarmConfig.AllDate)
            {
                radioButton4.Checked = true;           
            }
            else
            {
                radioButton3.Checked = true;
                dateTimePicker4.Value = GetDateValue(AlarmConfig.Startdate);
                dateTimePicker3.Value = GetDateValue(AlarmConfig.Enddate);
            }
            foreach (SensorModel model in ConfigData.SensorModelRoot.SensorModels)
            {
                foreach (Field field in model.Fields)
                {
                    if (field.Realtime && !AlarmConfig.AlarmField.Exists(fd => fd.Name == field.Name))
                    {
                        AlarmConfig.AlarmField.Add(new AlarmField() { Name = field.Name, NameChnName = field.Alias, Model = model.Name, ModelChnName = model.Title });
                    }
                }
            }           
         
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.DataSource = AlarmConfig.AlarmField;
            dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;
            AfterDataSourceUpdate();
            dataGridView1.CellEndEdit += DataGridView1_CellEndEdit;
            dateTimePicker1.ValueChanged += DateTimePicker1_ValueChanged;
            dateTimePicker2.ValueChanged += DateTimePicker2_ValueChanged;
            dateTimePicker3.ValueChanged += DateTimePicker3_ValueChanged;
            dateTimePicker4.ValueChanged += DateTimePicker4_ValueChanged;
            radioButton1.CheckedChanged += RadioButton1_CheckedChanged;
            radioButton4.CheckedChanged += RadioButton4_CheckedChanged;                                
        }

        private void DateTimePicker4_ValueChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                AlarmConfig.Startdate = dateTimePicker4.Value.Day + dateTimePicker4.Value.Month * 100;
                Utils.ToFile(ConfigurationManager.AppSettings["预警配置文件"], ConfigData.AlarmConfigRoot);
            }
        }

        private void DateTimePicker3_ValueChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                AlarmConfig.Enddate = dateTimePicker3.Value.Day + dateTimePicker3.Value.Month * 100;
                Utils.ToFile(ConfigurationManager.AppSettings["预警配置文件"], ConfigData.AlarmConfigRoot);
            }
        }

        private void DateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                AlarmConfig.Endtime = dateTimePicker2.Value.Minute + dateTimePicker2.Value.Hour * 100;
                Utils.ToFile(ConfigurationManager.AppSettings["预警配置文件"], ConfigData.AlarmConfigRoot);
            }
        }

        private void DateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                AlarmConfig.Starttime = dateTimePicker1.Value.Minute + dateTimePicker1.Value.Hour * 100;
                Utils.ToFile(ConfigurationManager.AppSettings["预警配置文件"], ConfigData.AlarmConfigRoot);
            }
        }

        private void RadioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked)
            {
                AlarmConfig.AllDate = true;                
            }
            else
            {
                AlarmConfig.AllDate = false;
                AlarmConfig.Startdate = dateTimePicker4.Value.Day + dateTimePicker4.Value.Month * 100;
                AlarmConfig.Enddate = dateTimePicker3.Value.Day + dateTimePicker3.Value.Month * 100;
            }
            Utils.ToFile(ConfigurationManager.AppSettings["预警配置文件"], ConfigData.AlarmConfigRoot);
        }

        private void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                AlarmConfig.AllTime = true;                
            }
            else
            {
                AlarmConfig.AllTime = false;
                AlarmConfig.Starttime = dateTimePicker1.Value.Minute + dateTimePicker1.Value.Hour * 100;
                AlarmConfig.Endtime = dateTimePicker2.Value.Minute + dateTimePicker2.Value.Hour * 100;
            }
            Utils.ToFile(ConfigurationManager.AppSettings["预警配置文件"], ConfigData.AlarmConfigRoot);
        }


        private void DataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {            
            //string pathAC = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), fileNameAC);
            // string pathAC = Path.Combine(Utils.GetUserPath(), fileNameAC);
            Utils.ToFile(ConfigurationManager.AppSettings["预警配置文件"], ConfigData.AlarmConfigRoot);
        }

        void AfterDataSourceUpdate()
        {            
            dataGridView1.Columns["Model"].HeaderText = "型号";
            dataGridView1.Columns["ModelChnName"].HeaderText = "型号名称";
            dataGridView1.Columns["Name"].HeaderText = "监测项";
            dataGridView1.Columns["NameChnName"].HeaderText = "监测名称";
            dataGridView1.Columns["Up"].HeaderText = "上限值";
            dataGridView1.Columns["Low"].HeaderText = "下限值";
            dataGridView1.Columns["Around"].HeaderText = "阈值";            
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
