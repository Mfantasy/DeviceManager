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
        //增加 //经常使用
//        string insertAlarmSql = @"INSERT INTO AlarmConfig(name,model,field,warn,h0t,h1t,h2t,h3t,h4t,h5t,h6t,h7t,h8t,h9t,h10t,h11t,h12t,h13t,h14t,h15t,h16t,h17t,h18t,h19t,h20t,h21t,h22t,h23t,h24t,
//h0l,h1l,h2l,h3l,h4l,h5l,h6l,h7l,h8l,h9l,h10l,h11l,h12l,h13l,h14l,h15l,h16l,h17l,h18l,h19l,h20l,h21l,h22l,h23l,h24l
//) VALUES('{0}','{1}','{2}','{3}'  ,'{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}','{29}',
//'{30}','{31}','{32}','{33}','{34}','{35}','{36}','{37}','{38}','{39}','{40}','{41}','{42}','{43}','{44}','{45}','{46}','{47}','{48}','{49}','{50}','{51}','{52}','{53}')";
//        string insertMapSql = "INSERT INTO AlarmMap(name,model,field,gate,node,port,date) VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}')";
        //修改
        string updateAlarmSql = @"UPDATE AlarmConfig SET name='{0}',h0t='{1}',h0l='{2}',h1t='{3}',h1l='{4}',h2t='{5}',h2l='{6}',h3t='{7}',h3l='{8}',h4t='{9}',h4l='{10}',h5t='{11}',h5l='{12}',h6t='{13}',h6l='{14}',h7t='{15}',h7l='{16}',
h8t ='{17}',h8l ='{18}',h9t ='{19}',h9l ='{20}',h10t ='{21}',h10l ='{22}',h11t ='{23}',h11l ='{24}',h12t ='{25}',h12l ='{26}',h13t='{27}',h13l='{28}',h14t='{29}',h14l='{30}',h15t='{31}',h15l='{32}',h16t='{33}',h16l='{34}',h17t='{35}',h17l='{36}',h18t='{37}',h18l='{38}',
h19t='{39}',h19l='{40}',h20t='{41}',h20l='{42}',h21t='{43}',h21l='{44}',h22t='{45}',h22l='{46}',h23t='{47}',h23l='{48}',h24t='{49}',h24l='{50},warn='{51}'' WHERE name='{52}' AND model='{53}' AND field='{54}'";
        string updateMapSql = "UPDATE AlarmMap SET name='{0}' WHERE name='{1}' AND model='{2}' AND field= '{3}'";
        //删除
        string deleteAlarmSql = "DELETE FROM AlarmConfig WHERE name='{0}' AND model='{1}' AND field='{2}'";
        string deleteMapSql = "DELETE FROM AlarmMap WHERE name='{0}' AND model='{1}' AND field='{2}' ";        
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
                a24.Model = r["model"].ToString();
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
