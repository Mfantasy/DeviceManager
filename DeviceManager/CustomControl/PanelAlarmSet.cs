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

namespace DeviceManager.CustomControl
{
    public partial class PanelAlarmSet : UserControl
    {
        public PanelAlarmSet()
        {
            InitializeComponent();
        }
        public void Init()
        {
            Scheduler scheduler = new Scheduler();
            scheduler.DateChanged += Scheduler_DateChanged;
            scheduler.TimeChanged += Scheduler_TimeChanged;
        }

        List<AlarmConfig> current = null;

        private void Scheduler_TimeChanged(object sender, EventArgs e)
        {
            Scheduler s = sender as Scheduler;
            if (s.Time == 1324)
            {
                //先处理开始
                current = ConfigData.AlarmConfigRoot.AlarmConfigs.FindAll(alac => alac.Enable && alac.Startdate == s.Date);
                //ConfigData.a
            }
        }

        private void Scheduler_DateChanged(object sender, EventArgs e)
        {
            Scheduler s = sender as Scheduler;
            if (s.Date == 2234)
            {
                //先处理开始
                AlarmConfig ac = ConfigData.AlarmConfigRoot.AlarmConfigs.Find(alac => alac.Enable && alac.Startdate == s.Date)
                //ConfigData.a
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
