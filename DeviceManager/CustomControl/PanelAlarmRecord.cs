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
using System.IO;

namespace DeviceManager.CustomControl
{
    public partial class PanelAlarmRecord : UserControl
    {
        public PanelAlarmRecord()
        {
            InitializeComponent();
        }
        DataTable dt;
        CustomDataView cdv;
        string db = "null";
        public void Init()
        {
            db = Path.Combine(Utils.GetUserPath(),"alarm.db");          
            string sql = "SELECT COUNT(*) FROM sqlite_master where type = 'table' and name = 'record'";
            object obj = SqlLiteHelper.ExecuteScalar(db, sql);
            if (Convert.ToInt32(obj) == 0)
            {
                string createSql = "CREATE TABLE record(time varchar(50),field varchar(50),groupname varchar(50),comment varchar(50),state varchar(50))";
                int res = SqlLiteHelper.ExecuteNonQuery(db, createSql);
            }

            foreach (Field field in ConfigData.allFields)
            {
                field.StateChanged += Field_StateChanged;
            }

            cdv = new CustomDataView();          
            cdv.Font = new Font("微软雅黑", 15);
            cdv.Parent = this;
            cdv.Dock = DockStyle.Fill;
            dt = SqlLiteHelper.ExecuteReader(db, "SELECT * FROM record");                    
            cdv.DataSource = dt;            
            cdv.Columns["time"].HeaderText = "时间";
            cdv.Columns["field"].HeaderText = "监测项";
            cdv.Columns["groupname"].HeaderText = "位置";
            cdv.Columns["comment"].HeaderText = "备注";
            cdv.Columns["state"].HeaderText = "状态";
            cdv.CellFormatting += Cdv_CellFormatting;
        }

        private void Cdv_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value != null)
            {
                switch (e.Value.ToString())
                {
                    case state0:
                        cdv.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.GreenYellow;
                        break;
                    case state1:
                        cdv.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Yellow;
                        break;
                    case state2:
                        cdv.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red;
                        break;
                }
            }
        }
        
       const string state0 = "恢复正常数值";
       const string state1 = "接近预警临界值";
       const string state2 = "超过预警值";
        private void Field_StateChanged(object sender, EventArgs e)
        {
            Field field = sender as Field;
            AlarmInfo ai = new AlarmInfo();
            ai.Time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            ai.Field = field.Alias;
            ai.GroupName = field.CurrentSensor.GroupName;
            ai.Comment = field.CurrentSensor.Comment;          
            switch (field.State)
            {
                case 0:
                    ai.State = state0;
                    break;
                case 1:
                    ai.State = state1;
                    break;
                case 2:
                    ai.State = state2;
                    break;
            }            
            dt.Rows.Add(ai.Time,ai.Field,ai.GroupName,ai.Comment,ai.State);
            string insertSql = string.Format("INSERT INTO record(time,field,groupname,comment,state) VALUES('{0}','{1}','{2}','{3}','{4}')",ai.Time,ai.Field,ai.GroupName,ai.Comment,ai.State);
            try
            {
                SqlLiteHelper.ExecuteNonQuery(db, insertSql);
            }
            catch (Exception ex)
            {
                Utils.WriteEX(ex);
            }
        }
    }

    public class AlarmInfo
    {
        public string Time { get; set; }
        public string Field { get; set; }
        public string GroupName { get; set; }
        public string Comment { get; set; }
        public string State { get; set; }
    }
}
