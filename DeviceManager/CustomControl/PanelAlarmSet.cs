﻿using System;
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

namespace DeviceManager.CustomControl
{
    public partial class PanelAlarmSet : UserControl
    {
        public PanelAlarmSet()
        {
            InitializeComponent();
        }
        const string fileName = "AlarmConfig.xml";
        string configPath;
        List<AlarmConfig> listAlarm = null;
        public CustomAlarmSetControl CurrentAlarmConfigUI { get; set; }
      
        public void Init()
        {
            //设置策略UI
            configPath = Path.Combine(Utils.GetUserPath(), fileName);
            listAlarm = ConfigData.AlarmConfigRoot.AlarmConfigs;
            if (listAlarm == null)
                listAlarm = new List<AlarmConfig>();
            foreach (var item in listAlarm)
            {
                //menuStrip1.
                NewItem(item);
            }
            //使策略生效
            SetCurrentAlarmConfig();
            Scheduler scheduler = new Scheduler();
            scheduler.DateChanged += Scheduler_DateChanged;
            scheduler.TimeChanged += Scheduler_TimeChanged;                        
        }

        private void NewItem(AlarmConfig item)
        {
            ToolStripMenuItem tsmItem = new ToolStripMenuItem(item.Name);
            CustomAlarmSetControl scg = new CustomAlarmSetControl(item);
            tsmItem.Tag = scg;
            tsmItem.Click += TsmItem_Click;
            ToolStripMenuItem enable = (ToolStripMenuItem)tsmItem.DropDownItems.Add("启用");
            enable.Tag = item;
            enable.Click += Enable_Click;                                    
        }

        private void TsmItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmItem = sender as ToolStripMenuItem;
            var scg = tsmItem.Tag as CustomAlarmSetControl;
            scg.BringToFront();    
        }
    
        private void Enable_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem enable = sender as ToolStripMenuItem;
            if (enable.Checked)
            {
                enable.Checked = false;
                (enable.Tag as AlarmConfig).Enable = false;
            }
            else
            {
                enable.Checked = true;
                (enable.Tag as AlarmConfig).Enable = true;
            }
        }

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
                int nowTime = int.Parse(DateTime.Now.ToString("HHmm"));
                int nowDate = int.Parse(DateTime.Now.ToString("MMdd"));
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
            AlarmConfig endAlarm = ConfigData.AlarmConfigRoot.AlarmConfigs.Find(alac => alac.Enable && alac.Enddate == s.Time && alac.Using);
            if (endAlarm != null)
            {                
                SetAlarmNull(endAlarm);
            }

            List<AlarmConfig> currentInTimeAlarms = ConfigData.AlarmConfigRoot.AlarmConfigs.FindAll(alac => alac.Enable && alac.Startdate == s.Time);
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
            AlarmConfig endAlarm = ConfigData.AlarmConfigRoot.AlarmConfigs.Find(alac => alac.Enable && alac.Enddate == s.Date && alac.Using);
            if (endAlarm != null)
            {
                SetAlarmNull(endAlarm);
            }
            //先处理开始日期,再处理结束
            List<AlarmConfig> currentInDateAlarms = ConfigData.AlarmConfigRoot.AlarmConfigs.FindAll(alac => alac.Enable && alac.Startdate == s.Date);
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

        private void 添加一个配置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AlarmConfig ac = new AlarmConfig();
            listAlarm.Add(ac);
            CustomAlarmSetControl casc = new CustomAlarmSetControl("name",ac);
        }

        private void 保存配置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Utils.ToFile<AlarmConfigRoot>(configPath, ConfigData.AlarmConfigRoot);
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
