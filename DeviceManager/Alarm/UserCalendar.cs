using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DeviceManager.Alarm;
using System.IO;

namespace DeviceManager.Alarm
{
    public partial class UserCalendar : UserControl
    {
        public UserCalendar()
        {
            InitializeComponent();
            tableLayoutPanel1.GetType().GetProperty("DoubleBuffered",
 System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(tableLayoutPanel1,
 true, null);
            dateTimePicker1.ValueChanged += DateTimePicker1_ValueChanged;


        }
        public AlarmChart Ac;  //表格控件
        public event EventHandler ACShown;
        public Sensor currentSensor;//当前选择的传感器
        string db = Path.Combine(Utils.GetUserPath(), "alarm.db");

        public Sensor CurrentSensor
        {
            get { return this.currentSensor; }
            set
            {
                if (this.currentSensor != value)
                {
                    this.currentSensor = value;
                    DateTimePicker1_ValueChanged(null, null);
                }
            }
        }
        //初始化策略列表
        public void Init()
        {
            dateTimePicker1.Value = DateTime.Now;
            RefreshComb();
        }
        //刷新Comb(策略列表)
        public void RefreshComb()
        {
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(ConfigData.AllStrategy.ToArray());
            if (comboBox1.Items.Count == 0)
            {
                comboBox1.Items.Add("无");
            }
            comboBox1.SelectedIndex = 0;
        }


        public void DateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            int weekNum = Convert.ToInt16(DateTime.Parse(dateTimePicker1.Value.ToString("yyyy年MM月01日")).DayOfWeek);
            dataGridView1.Rows.Clear();
            if (weekNum == 0)
            {
                weekNum = 6;
            }
            else
            {
                weekNum -= 1;
            }
            //根据星期和每月天数计算行数
            //31天并且1号是周六日 或 30天一号为周日  6行
            //剩下均为5行
            //边界高度为1所以 计算行高的时候应该先减上下边界为2再减列头高度减2得出结果再减1
            int rowC = 0;
            int monthDayCount = DateTime.DaysInMonth(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month);
            if ((monthDayCount == 30 && weekNum == 6) || monthDayCount == 31 && weekNum >= 5)
                rowC = 6;
            else
                rowC = 5;
            int rowHeight = (this.Size.Height - 2 - dataGridView1.ColumnHeadersHeight - 2) / rowC - 1;

            dataGridView1.RowTemplate.Height = rowHeight; //必须先设置模板行高再添加行,否则不生效
            dataGridView1.Rows.Add(rowC);

            int num = 0;
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                if (i > 0) weekNum = 0;
                for (int j = weekNum; j < dataGridView1.ColumnCount; j++)
                {
                    num++;
                    if (num > monthDayCount)
                        break;
                    DateCell dc = new DateCell();
                    dc.ColIndex = weekNum;
                    dc.RowIndex = i;
                    dc.Year = dateTimePicker1.Value.Year;
                    dc.Month = dateTimePicker1.Value.Month;
                    dc.Day = num;
                    if (CurrentSensor != null && CurrentSensor.AlarmDic.ContainsKey(dc.YYYYMMDD))
                    {
                        dc.AS = CurrentSensor.AlarmDic[dc.YYYYMMDD];
                    }
                    dataGridView1.Rows[i].Cells[j].Value = dc;
                    if (num == DateTime.Now.Day && dateTimePicker1.Value.Month == DateTime.Now.Month && dateTimePicker1.Value.Year == DateTime.Now.Year)
                    {
                        dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.LightSkyBlue;
                    }
                }
            }
            dataGridView1.CurrentCell = null;
        }


        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)//双击列头
                return;
            //object obj = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            //if (obj != null && obj.ToString().Length > 2)
            //{
            //}
            //this.Visible = false;
            ////this.SendToBack();            
            button4_Click(null, null);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            //新建            
            Ac.CreateNew();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //设定 comb1
            if (dataGridView1.CurrentCell != null)
            {
                if (comboBox1.SelectedItem is AlarmStrategy)
                {
                    AlarmStrategy cas = comboBox1.SelectedItem as AlarmStrategy;
                    //此处还应该操作数据库 使用helper+手写sql语句
                    if (dataGridView1.CurrentCell.Value is DateCell && (dataGridView1.CurrentCell.Value as DateCell).AS != cas)
                    {
                        DateCell dc = dataGridView1.CurrentCell.Value as DateCell;
                        dc.AS = cas;
                        dataGridView1.Refresh();
                        if (CurrentSensor.AlarmDic.ContainsKey(dc.YYYYMMDD))
                        {
                            CurrentSensor.AlarmDic[dc.YYYYMMDD] = cas;
                            //UPDATE SQL
                            string updateSql = string.Format("UPDATE T_ALARM_SENSOR_MAP SET aname='{0}' WHERE date='{1}' AND uid='{2}' AND node='{3}' AND port='{4}'", cas.Name, dc.YYYYMMDD, CurrentSensor.Uid, CurrentSensor.NodeId, CurrentSensor.PortId);
                            SqlLiteHelper.ExecuteNonQuery(db, updateSql);
                        }
                        else
                        {
                            CurrentSensor.AlarmDic.Add(dc.YYYYMMDD, cas);
                            //insert
                            string insertSql = string.Format("INSERT INTO T_ALARM_SENSOR_MAP (aname,date,uid,node,port) VALUES('{0}','{1}','{2}','{3}','{4}')", cas.Name, dc.YYYYMMDD, CurrentSensor.Uid, CurrentSensor.NodeId, CurrentSensor.PortId);
                            SqlLiteHelper.ExecuteNonQuery(db, insertSql);
                        }
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell != null)
            {
                //删除Cell
                if (dataGridView1.CurrentCell.Value is DateCell)
                {
                    DateCell dc = dataGridView1.CurrentCell.Value as DateCell;
                    AlarmStrategy ast = dc.AS;
                    if (ast != null)
                    {
                        string dsql = string.Format("DELETE FROM T_ALARM_SENSOR_MAP WHERE date='{0}' AND aname ='{1}' AND uid='{2}' AND node='{3}' AND port='{4}'", dc.YYYYMMDD, ast.Name, CurrentSensor.Uid, CurrentSensor.NodeId, CurrentSensor.PortId);
                        SqlLiteHelper.ExecuteNonQuery(db, dsql);
                        dataGridView1.Refresh();
                        if (CurrentSensor.AlarmDic.ContainsKey(dc.YYYYMMDD))
                        {
                            CurrentSensor.AlarmDic.Remove(dc.YYYYMMDD);
                            DateTimePicker1_ValueChanged(null, null);
                        }
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //编辑
            if (dataGridView1.CurrentCell.Value is DateCell)
            {
                DateCell dc = dataGridView1.CurrentCell.Value as DateCell;
                if (dc.AS != null)
                {
                    Ac.EditStrategy(dc.AS);
                }
            }
        }
    }
}
class DateCell//自定义日期单元格
{
    public int Month { get; set; }
    public int Year { get; set; }
    public int Day { get; set; }

    public int ColIndex { get; set; }
    public int RowIndex { get; set; }

    public AlarmStrategy AS { get; set; }

    public string YYYYMMDD
    {
        get { return Year.ToString() + (Month.ToString().Length == 1 ? "0" + Month.ToString() : Month.ToString()) + (Day.ToString().Length == 1 ? "0" + Day.ToString() : Day.ToString()); }
    }

    public override string ToString()
    {
        if (AS != null)
        {
            return AS.Name + "\r\n" + Day.ToString();
        }
        else
        {
            return Day.ToString();
        }
    }

}