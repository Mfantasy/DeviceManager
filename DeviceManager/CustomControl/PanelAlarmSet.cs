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
        private void SetCurrentAlarmConfig()
        {
            AlarmConfig current = ConfigData.AlarmConfigRoot.AlarmConfigs.Find(ac =>
            {
                bool intime = false;
                bool indate = false;
                if (ac.IsAllTime)
                    intime = true;
                if (ac.IsAllDate)
                    indate = true;
                int nowTime = int.Parse(DateTime.Now.ToString("Hmm"));
                int nowDate = int.Parse(DateTime.Now.ToString("Mdd"));
                if (nowDate >= ac.Startdate && nowDate < ac.Enddate)
                    indate = true;
                if (nowTime >= ac.Starttime && nowTime < ac.Endtime)
                    intime = true;
                return (intime && indate);
            });
            if (current != null)
            {
                SetAlarm(current);
            }
        }

        private void Scheduler_TimeChanged(object sender, EventArgs e)
        {
            Scheduler s = sender as Scheduler;
            AlarmConfig endAlarm = ConfigData.AlarmConfigRoot.AlarmConfigs.Find(alac => alac.Enddate == s.Time && alac.Using);
            if (endAlarm != null)
            {
                SetAlarmNull(endAlarm);
            }

            List<AlarmConfig> currentInTimeAlarms = ConfigData.AlarmConfigRoot.AlarmConfigs.FindAll(alac => alac.Startdate == s.Time);
            foreach (AlarmConfig ac in currentInTimeAlarms)
            {
                ac.InTime = true;
                if (ac.InTime && ac.InDate)
                {
                    SetAlarm(ac);
                }
            }
        }
        private void Scheduler_DateChanged(object sender, EventArgs e)
        {
            Scheduler s = sender as Scheduler;
            //先处理结束,再处理开始(为了让下个开始能够顺利执行,避免时间冲突)
            AlarmConfig endAlarm = ConfigData.AlarmConfigRoot.AlarmConfigs.Find(alac => alac.Enddate == s.Date && alac.Using);
            if (endAlarm != null)
            {
                SetAlarmNull(endAlarm);
            }
            //先处理开始日期,再处理结束
            List<AlarmConfig> currentInDateAlarms = ConfigData.AlarmConfigRoot.AlarmConfigs.FindAll(alac => alac.Startdate == s.Date);
            foreach (AlarmConfig ac in currentInDateAlarms)
            {
                ac.InDate = true;
                if (ac.IsAllTime)
                {
                    SetAlarm(ac);
                }
            }
        }

        void SetAlarm(AlarmConfig ac)
        {
            ac.Using = true;
            foreach (var fd in ConfigData.allFields)
            {
                fd.Alarm = ac.AlarmField.Find(af => af.Name == fd.Name);
            }
            foreach (ToolStripMenuItem item in menuStrip1.Items)
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

        void SetAlarmNull(AlarmConfig ac)
        {
            ac.Using = false;
            foreach (var fd in ConfigData.allFields)
            {
                foreach (AlarmField af in ac.AlarmField)
                {
                    if (fd.Alarm == af)
                    {
                        fd.Alarm = null;
                    }
                }
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
            SetCurrentAlarmConfig();
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
            ac.Name = name;
            ac.AlarmField = new List<AlarmField>();
            listAlarm.Add(ac);
            NewItem(ac);
            Utils.ToFile(ConfigurationManager.AppSettings["预警配置文件"], ConfigData.AlarmConfigRoot);
        }

        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {

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
        const int interval = 30 * 60 * 1000; //30分钟     
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
                Date = int.Parse(DateTime.Now.ToString("MMdd"));
                Time = int.Parse(DateTime.Now.ToString("HHmm"));
                Thread.Sleep(interval);
            }
        }
    }
}
