using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;
using DeviceManager.Model;
using DeviceManager.CustomForm;

namespace DeviceManager.CustomControl
{
    public partial class PanelDrag : UserControl
    {
        public PanelDrag()
        {
            InitializeComponent();
            this.BackgroundImageLayout = ImageLayout.Stretch;
            tableLayoutPanel1.BackColor =  Color.FromArgb(80, Color.Black);
            button2.BackColor = Color.FromArgb(100, Color.White);
            button1.BackColor = Color.FromArgb(100, Color.White);
            button2_Click(null, null);                       
        }
        ToolTip tip = new ToolTip();
        XRoot xroot;
        Point p;
        const string path = "dragConfig.xml";
        public void Init()
        {
 
            xroot = Utils.FromXMLFile<XRoot>(path);
            foreach (var item in xroot.Pages)
            {
                PictureBox picTop = new PictureBox();
                tip.SetToolTip(picTop, item.Name);
                picTop.ImageLocation = item.Pic;
                picTop.SizeMode = PictureBoxSizeMode.StretchImage;
                picTop.Tag = item;
                picTop.Dock = DockStyle.Left;             
                picTop.Click += PicTop_Click;
                tableLayoutPanel1.ColumnCount++;
                tableLayoutPanel1.Controls.Add(picTop);
                if (item.Sensors.Count > 0)
                {
                    foreach (var ss in item.Sensors)
                    {
                        NewPb(item, ss);
                        ss.Sensor = ConfigData.allSensors.Find(s =>  s.Uid==ss.Uid&&s.NodeId==ss.Node&&s.PortId==ss.Port );
                    }
                }
                else
                {
                    //匹配组名取出sensor
                    switch (item.Level)
                    {
                        case "1":
                            GroupConfig1 g1 = ConfigData.GroupConfigRoot.GroupConfig1s.Find(g => g.Name == item.Name);
                            if (g1 != null)
                            {
                                foreach (var g12 in g1.GroupConfigs)
                                {
                                    foreach (var g123 in g12.GroupConfigs)
                                    {
                                        foreach (var s in g123.Sensors)
                                        {
                                            NewSs(item, s);
                                        }
                                    }
                                }
                            }
                            break;
                        case "2":
                            GroupConfig2 g2 = ConfigData.allG2.Find(g => g.Name == item.Name);
                            if (g2 != null)
                            {
                                foreach (var g23 in g2.GroupConfigs)
                                {
                                    foreach (var s in g23.Sensors)
                                    {
                                        NewSs(item, s);
                                    }                                    
                                }
                            }
                            break;
                        case "3":
                            GroupConfig3 g3 = ConfigData.allG3.Find(g => g.Name == item.Name);
                            if (g3 != null)
                            {
                                foreach (var s in g3.Sensors)
                                {
                                    NewSs(item, s);
                                }
                            }
                            break;
                    }
                }
            }
            if (xroot.Pages.Count > 0)
            {
                this.BackgroundImage =Image.FromFile(xroot.Pages[0].Pic);
                ShowPb(xroot.Pages[0]);
                label1.Visible = true;
                label1.Text = xroot.Pages[0].Name;
            }
        }

        void NewPb(Page item,SensorS ss)
        {
            PicBox pb = new PicBox(ss);
            pb.Cursor = Cursors.Hand;
            pb.Parent = this;            
            pb.Location = new Point(ss.X, ss.Y);            
            item.Pbs.Add(pb);
            pb.DoubleClick += Pb_DoubleClick;
            pb.MouseEnter += Pb_MouseEnter;            
            pb.MouseDown += Pb_MouseDown;
            pb.MouseUp += Pb_MouseUp;
            pb.MouseMove += Pb_MouseMove;
        }

        private void Pb_DoubleClick(object sender, EventArgs e)
        {
            TableLayoutPanel flp = new TableLayoutPanel();
            flp.ColumnCount = 1;
            flp.BackColor = System.Drawing.Color.Transparent;
            flp.AutoScroll = true;            
            flp.Dock = DockStyle.Fill;
            SensorModel sm = (sender as PicBox).Ss.Sensor.Model;
            foreach (Field item in sm.Fields)
            {
                if (item.Realtime)
                {
                    flp.Controls.Add(item.ChartPanel);                    
                }
            }

            ChartForm cf = new ChartForm();
            flp.Parent = cf;
            cf.ShowDialog();
        }

        private void Pb_MouseEnter(object sender, EventArgs e)
        {
            var pb = sender as PicBox;
            string cap = "";
            foreach (var item in pb.Ss.Sensor.Model.Fields)
            {                
                cap += item.LabelText + "\r\n";
            }
            tip.SetToolTip(pb, cap);
        }

        private void Pb_MouseMove(object sender, MouseEventArgs e)
        {
            var pb = sender as PicBox;
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                pb.Location = new Point(pb.Left + (e.X - p.X), pb.Top + (e.Y - p.Y));

            }
        }

        private void Pb_MouseUp(object sender, MouseEventArgs e)
        {
            var pb = sender as PicBox;
            pb.Ss.X = pb.Location.X;
            pb.Ss.Y = pb.Location.Y;
            this.Cursor = Cursors.WaitCursor;
            Utils.ToFile(path, xroot);
            this.Cursor = Cursors.Default;
        }

        private void Pb_MouseDown(object sender, MouseEventArgs e)
        {
            p = e.Location;
        }

        void NewSs(Page item, Sensor s)
        {
            SensorS ss = new SensorS();
            ss.X = 64;
            ss.Y = 64;
            ss.Uid = s.Uid;
            ss.Node = s.NodeId;
            ss.Port = s.PortId;
            ss.Sensor = s;
            item.Sensors.Add(ss);
            NewPb(item,ss);
        }

        private void PicTop_Click(object sender, EventArgs e)
        {
            PictureBox picTop = sender as PictureBox;            
            this.BackgroundImage = picTop.Image;
            Page page = picTop.Tag as Page;
            label1.Text = page.Name;
            ShowPb(page);
        }

        void ShowPb(Page page)
        {
            foreach (var item in xroot.Pages)
            {
                if (page == item)
                {
                    foreach (var pb in item.Pbs)
                    {
                        pb.Visible = true;
                    }
                }
                else
                {
                    foreach (var pb in item.Pbs)
                    {
                        pb.Visible = false;
                    }
                }
            }
        }
                   
        private void button1_Click(object sender, EventArgs e)
        {                        
            button2.Visible = true;
            tableLayoutPanel1.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.Visible = false;
            tableLayoutPanel1.Visible = true;
        }
    }

    public class PicBox : Panel
    {
        public PicBox(SensorS s)
        {
            this.Size = new Size(64, 64);
            this.BackgroundImage = Image.FromFile("ico.png");//Properties.Resources.ico;//
            this.BackColor = Color.Transparent;
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.Ss = s;                        
            this.Label = new Label();
            this.Label.Parent = this;
            this.Label.Text = s.Node;
            //this.Label.ForeColor = Color.White;
            this.Label.Anchor = AnchorStyles.Bottom & AnchorStyles.Right;
            this.Label.AutoSize = true;
            this.Label.Location = new Point(this.Width - this.Label.Width-4, this.Size.Height - this.Label.Height-5);            
            //this.Label.Location = new Point(32, 32);
        }
        public Label Label { get; set; }
        public SensorS Ss { get; set; }
    }


    [XmlRoot("root")]
    public class XRoot
    {
        [XmlElement("page")]
        public List<Page> Pages { get; set; }
    }

    public class Page
    {
        [XmlElement("sensor")]
        public List<SensorS> Sensors { get; set; }
        [XmlAttribute("name")]
        public string Name { get; set; }
        [XmlAttribute("level")]
        public string Level { get; set; }
        [XmlAttribute("pic")]
        public string Pic { get; set; }

        
        
        private List<PicBox> pbs= new List<PicBox>();
        [XmlIgnore]
        public List<PicBox> Pbs
        {
            get { return pbs; }
            set { pbs = value; }
        }

    }

    public class SensorS
    {
        [XmlAttribute("uid")]
        public string Uid { get; set; }
        [XmlAttribute("node")]
        public string Node { get; set; }

        [XmlAttribute("port")]
        public string Port { get; set; }

        [XmlAttribute("x")]
        public int X { get; set; }

        [XmlAttribute("y")]
        public int Y { get; set; }

        [XmlIgnore]
        public Sensor Sensor { get; set; }
    }
}
