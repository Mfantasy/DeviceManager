using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using DeviceManager.Model;
using System.IO;
using System.Configuration;

namespace DeviceManager.CustomControl
{
    public partial class PanelAlarmSet : UserControl
    {
        #region 控制策略生效

        AlarmConfig currentAlarmCfg = null;
        public AlarmConfig CurrentAlarmCfg
        {
            get { return currentAlarmCfg; }
            set
            {
                if (currentAlarmCfg != value)
                {
                    currentAlarmCfg = value;
                    SetAlarm(currentAlarmCfg);
                }
            }
        }
      
        private void Scheduler_TimeChanged(object sender, EventArgs e)
        {
            Scheduler s = sender as Scheduler;
            foreach (var item in ConfigData.AlarmConfigRoot.AlarmConfigs)
            {
                if (s.Time >= item.Starttime && s.Time <= item.Endtime)
                {
                    item.InTime = true;
                }
                else
                {
                    item.InTime = false;
                }                
            }
            List<AlarmConfig> currentAlarms = ConfigData.AlarmConfigRoot.AlarmConfigs.FindAll(ala => ala.InDate && ala.InTime);
            if (currentAlarms != null && currentAlarms.Count > 0)
            {
                CurrentAlarmCfg = currentAlarms[currentAlarms.Count - 1];
            }
            else
            {
                SetAlarmNull();
            }
        }

        private void Scheduler_DateChanged(object sender, EventArgs e)
        {
            Scheduler s = sender as Scheduler;           
            foreach (var item in ConfigData.AlarmConfigRoot.AlarmConfigs)
            {
                if (s.Date >= item.Startdate && s.Date < item.Enddate)
                {
                    item.InDate = true;
                }
                else
                {
                    item.InDate = false;
                }                
            }         
        }

        void SetAlarm(AlarmConfig ac)
        {
            foreach (var fd in ConfigData.allFields)
            {
                fd.Alarm = ac.AlarmField.Find(af => af.Name == fd.Name);
            }
            foreach (ToolStripMenuItem item in menuStrip1.Items)
            {
                if (InvokeRequired)
                {
                    this.Invoke(new Action(() =>
                    {
                        if (item.Name == ac.Name)
                        {
                            item.BackColor = Color.BlueViolet;
                            item.ForeColor = Color.White;
                            TsmItem_Click(item, null);
                        }
                        else
                        {
                            item.BackColor = Color.Transparent;
                            item.ForeColor = Color.Black;
                        }
                    }));
                }
                else
                {
                    if (item.Name == ac.Name)
                    {
                        item.BackColor = Color.BlueViolet;
                        item.ForeColor = Color.White;
                        TsmItem_Click(item, null);
                    }
                    else
                    {
                        item.BackColor = Color.Transparent;
                        item.ForeColor = Color.Black;
                    }
                }
            }
        }

        void SetAlarmNull()
        {       
            foreach (var fd in ConfigData.allFields)
            {
                fd.Alarm = null;
            }
        }

        #endregion
        public PanelAlarmSet()
        {
            InitializeComponent();
        }
        
        string configPath;
        List<AlarmConfig> listAlarm = null;
        public CustomAlarmSetControl CurrentAlarmConfigUI { get; set; }
      
        public void Init()
        {
            //设置策略UI
            string fileName = ConfigurationManager.AppSettings["预警配置文件"];
            configPath = Path.Combine(Utils.GetUserPath(), fileName);
            if (ConfigData.AlarmConfigRoot.AlarmConfigs == null)
            {
                ConfigData.AlarmConfigRoot.AlarmConfigs = new List<AlarmConfig>();
            }
            listAlarm = ConfigData.AlarmConfigRoot.AlarmConfigs;
            foreach (var item in listAlarm)
            {              
                NewItem(item);
            }
            //删除配置列表
            toolStripComboBox1.Items.AddRange(listAlarm.ToArray());
            
            //使策略生效            
            Scheduler scheduler = new Scheduler();
            scheduler.DateChanged += Scheduler_DateChanged;
            scheduler.TimeChanged += Scheduler_TimeChanged;                        
        }

        private void NewItem(AlarmConfig item)
        {
            ToolStripMenuItem tsmItem = new ToolStripMenuItem(item.Name);            
            tsmItem.Name = item.Name;                      
            menuStrip1.Items.Insert(0, tsmItem);
            CustomAlarmSetControl scg = new CustomAlarmSetControl(item);
                    
            scg.Parent = panel1;
            scg.Dock = DockStyle.Fill;       
            tsmItem.Tag = scg;
            tsmItem.Click += TsmItem_Click;            
        }
  
        private void TsmItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmItem = sender as ToolStripMenuItem;
            var scg = tsmItem.Tag as CustomAlarmSetControl;            
            scg.BringToFront();    
        }


        
        private void 确定ToolStripMenuItem1_Click(object sender, EventArgs e)
        {        
            string name = null;
            if (!string.IsNullOrWhiteSpace(toolStripTextBox2.Text))
            {
                name = toolStripTextBox2.Text;
            }
            else
            {
                return;
            }
            AlarmConfig ac = new AlarmConfig();
            ac.AlarmField = new List<AlarmField>();
            ac.AllDate = true;
            ac.AllTime = true;
            ac.Name = name;           
            listAlarm.Add(ac);
            NewItem(ac);
            toolStripComboBox1.Items.Add(ac);
            Utils.ToFile(ConfigurationManager.AppSettings["预警配置文件"], ConfigData.AlarmConfigRoot);
        }

      

        private void 确定ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (toolStripComboBox1.SelectedItem != null)
            {
                if (MessageBox.Show(string.Format("确定删除{0}?", ((AlarmConfig)toolStripComboBox1.SelectedItem).Name), "提醒", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {                    
                    listAlarm.Remove(toolStripComboBox1.SelectedItem as AlarmConfig);
                    menuStrip1.Items.RemoveByKey((toolStripComboBox1.SelectedItem as AlarmConfig).Name);
                    toolStripComboBox1.Items.Remove(toolStripComboBox1.SelectedItem);
                    Utils.ToFile(ConfigurationManager.AppSettings["预警配置文件"], ConfigData.AlarmConfigRoot);
                }
            }
        }
    
    }




    public class Scheduler
    {
        // const int interval = 30 * 60 * 1000; //30分钟     
        const int interval = 60 * 1000; //30秒     
        public event EventHandler DateChanged;
        public event EventHandler TimeChanged;

        private int date;
        /// <summary>
        /// Format MMdd
        /// </summary>
        public int Date
        {
            get { return date; }
            set
            {
                if (date != value)
                {
                    date = value;
                    if (DateChanged != null)
                        DateChanged(this, null);
                }
            }
        }

        private int time;
        /// <summary>
        /// Format HHmm
        /// </summary>
        public int Time
        {
            get { return time; }
            set
            {
                if (time != value)
                {
                    time = value;
                    if (TimeChanged != null)
                        TimeChanged(this, null);
                }
            }
        }

        public Scheduler()
        {
            Thread thMonitor = new Thread(Monitoring);
            thMonitor.IsBackground = true;
            thMonitor.Start();
        }

        public void Monitoring()
        {
            while (true)
            {
                Date = int.Parse(DateTime.Now.ToString("Mdd"));
                Time = int.Parse(DateTime.Now.ToString("Hmm"));
                Thread.Sleep(interval);
            }
        }
    }
}
