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
using DeviceManager.Alarm;
using System.IO;

namespace DeviceManager
{
    public partial class UC : UserControl
    {
        AlarmChart Ac = new AlarmChart();
        public UC()
        {
            InitializeComponent();
            Ac = new AlarmChart();
            Ac.Parent = panel1;
            Ac.Dock = DockStyle.Fill;
            Ac.Visible = false;
        }

        public void BeforeInit()
        {
            InitAlarm();
            userCalendar1.Ac = Ac;
            Ac.userCalendar = userCalendar1;
        }
        
        public void Init()
        {
            BeforeInit();
            toolStripComboBox1.SelectedIndexChanged += (S, E) =>
            {
                GroupConfig1 g1 = toolStripComboBox1.SelectedItem as GroupConfig1;
                toolStripComboBox2.Items.Clear();
                if (g1 != null && g1.GroupConfigs != null)
                {
                    toolStripComboBox2.Items.AddRange(g1.GroupConfigs.ToArray());
                    SetSelect0(toolStripComboBox2);
                }
            };
            toolStripComboBox2.SelectedIndexChanged += (S, E) =>
            {
                GroupConfig2 g2 = toolStripComboBox2.SelectedItem as GroupConfig2;
                toolStripComboBox3.Items.Clear();
                if (g2 != null && g2.GroupConfigs != null)
                {
                    toolStripComboBox3.Items.AddRange(g2.GroupConfigs.ToArray());
                    SetSelect0(toolStripComboBox3);
                }
            };
            toolStripComboBox3.SelectedIndexChanged += (S, E) =>
            {
                GroupConfig3 g3 = toolStripComboBox3.SelectedItem as GroupConfig3;
                toolStripComboBox4.Items.Clear();
                if (g3 != null && g3.Sensors != null)
                {
                    toolStripComboBox4.Items.AddRange(g3.Sensors.ToArray());
                    SetSelect0(toolStripComboBox4);
                }
            };
            toolStripComboBox4.SelectedIndexChanged += (S, E) =>
            {
                Sensor s = toolStripComboBox4.SelectedItem as Sensor;                                
                userCalendar1.CurrentSensor = s;                
            };
          
            toolStripComboBox1.Items.AddRange(ConfigData.GroupConfigRoot.GroupConfig1s.ToArray());
            SetSelect0(toolStripComboBox1);
            userCalendar1.Init();
        }
        //建表            
        string createAlarmSql = @"CREATE TABLE T_ALARM(name varchar(50),model varchar(50),field varchar(50),warn varchar(50),
h0t varchar(50),h1t varchar(50),h2t varchar(50),h3t varchar(50),h4t varchar(50),h5t varchar(50),h6t varchar(50),h7t varchar(50),h8t varchar(50),h9t varchar(50),
h10t varchar(50),h11t varchar(50),h12t varchar(50),h13t varchar(50),h14t varchar(50),h15t varchar(50),h16t varchar(50),h17t varchar(50),h18t varchar(50),h19t varchar(50),
h20t varchar(50),h21t varchar(50),h22t varchar(50),h23t varchar(50),
h0l varchar(50),h1l varchar(50),h2l varchar(50),h3l varchar(50),h4l varchar(50),h5l varchar(50),h6l varchar(50),h7l varchar(50),h8l varchar(50),h9l varchar(50),
h10l varchar(50),h11l varchar(50),h12l varchar(50),h13l varchar(50),h14l varchar(50),h15l varchar(50),h16l varchar(50),h17l varchar(50),h18l varchar(50),h19l varchar(50),
h20l varchar(50),h21l varchar(50),h22l varchar(50),h23l varchar(50))";
        string createMapSql = "CREATE TABLE T_ALARM_SENSOR_MAP(aname varchar(50),date varchar(50), uid varchar(50), node varchar(50), port varchar(50))";
        //查询 //只有初始化的时候使用
        string selectAlarmSql = "SELECT * FROM T_ALARM";
        string selectMapSql = "SELECT * FROM T_ALARM_SENSOR_MAP";
   
        string db = Path.Combine(Utils.GetUserPath(),"alarm.db");
        void InitAlarm()
        {
            //初始化策略 .  1.首先查询数据库,查询出所有的Strategy及其对应关系
            //ConfigData.AllStrategy
            string stA = "SELECT COUNT(*) FROM sqlite_master where type = 'table' and name = 'T_ALARM'";
            string stM = "SELECT COUNT(*) FROM sqlite_master where type = 'table' and name = 'T_ALARM_SENSOR_MAP'";
            object objA = SqlLiteHelper.ExecuteScalar(db, stA);
            if (Convert.ToInt32(objA) == 0)
            {
                int res = SqlLiteHelper.ExecuteNonQuery(db, createAlarmSql);
            }
            object objM = SqlLiteHelper.ExecuteScalar(db, stM);
            if (Convert.ToInt32(objM) == 0)
            {
                int res = SqlLiteHelper.ExecuteNonQuery(db, createMapSql);
            }
            DataTable dtA = SqlLiteHelper.ExecuteReader(db, selectAlarmSql);
            DataTable dtM = SqlLiteHelper.ExecuteReader(db, selectMapSql);
            //处理查询下来的数据表
            foreach (DataRow r in dtA.Rows)
            {
                AlarmStrategy als;
                if (ConfigData.AllStrategy.Exists(ast => ast.Name == r["name"].ToString()))
                {
                    als = ConfigData.AllStrategy.Find(ast => ast.Name == r["name"].ToString());
                }
                else
                {
                    als = new AlarmStrategy();
                    als.Name = r["name"].ToString();
                    ConfigData.AllStrategy.Add(als);
                }
                Alarm24 a24 = new Alarm24();
                als.A24s.Add(a24);
                a24.Field = r["field"].ToString();            
                int warnTest = 0;
                if (int.TryParse(r["warn"].ToString(), out warnTest))
                {
                    a24.Warn = warnTest;
                }
                for (int i = 0; i < a24.Hs.Length; i++)
                {
                    string ht = string.Format("h{0}t", i);
                    string hl = string.Format("h{0}l", i);
                    double tTest = 0;
                    double lTest = 0;
                    if (double.TryParse(r[ht].ToString(), out tTest))
                    {
                        a24.Hs[i].Top = tTest;
                    }
                    if (double.TryParse(r[hl].ToString(), out lTest))
                    {
                        a24.Hs[i].Low = lTest;
                    }
                }
            }
            //处理AlarmMap
            foreach (DataRow r in dtM.Rows)
            {
                //aname varchar(50), date varchar(50), uid varchar(50), node varchar(50), port varchar(50)
                string aname = r["aname"].ToString();
                string date = r["date"].ToString();
                string uid = r["uid"].ToString();
                string node = r["node"].ToString();
                string port = r["port"].ToString();
                Sensor sensor = ConfigData.allSensors.Find(s => s.Uid == uid && s.NodeId == node && s.PortId == port);
                AlarmStrategy ast = ConfigData.AllStrategy.Find(o => o.Name == aname);
                if (sensor != null && ast != null)
                {
                    if (!sensor.AlarmDic.ContainsKey(date))
                    {
                        sensor.AlarmDic.Add(date, ast);
                    }
                }
            }   
        }

        void SetSelect0(ToolStripComboBox stcb)
        {
            if (stcb.Items.Count > 0)
                stcb.SelectedIndex = 0;
        }                    
       

      
    }
}
