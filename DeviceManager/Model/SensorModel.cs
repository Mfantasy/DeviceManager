using DeviceManager.CustomControl;
using DeviceManager.CustomForm;
using DeviceManager.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace DeviceManager
{
    public class Sensor
    {    
        [XmlAttribute("uid")]
        public string Uid { get; set; }
        [XmlAttribute("nodeid")]
        public string NodeId { get; set; }
        [XmlAttribute("comment")]
        public string Comment { get; set; }
        [XmlAttribute("portid")]
        public string PortId { get; set; }
        [XmlAttribute("model")]
        public string ModelKey { get; set; }
               
        [XmlIgnore]
        public string GroupName { get; set; }

        string hisColStr = null;
        [XmlIgnore]
        public string HisColumnStr
        {
            get
            {
                if (hisColStr == null)
                {
                    for (int i = 0; i < Model.Fields.Count; i++)
                    {
                        if (Model.Fields[i].History)
                        {
                            if (hisColStr == null)
                            {
                                hisColStr = Model.Fields[i].Name + " as " +Model.Fields[i].Alias;
                            }
                            else if(i==Model.Fields.Count)
                            {
                                hisColStr += Model.Fields[i].Name + " as " + Model.Fields[i].Alias;
                            }
                            else
                            {
                                hisColStr+=","+ Model.Fields[i].Name + " as " + Model.Fields[i].Alias;
                            }
                        }
                    }
                    if (string.IsNullOrWhiteSpace(hisColStr))
                    {
                        hisColStr = "*";
                    }
                    if (hisColStr != "*")
                    {
                        hisColStr += ",time as 时间";
                    }
                }
                return hisColStr;
            }           
        }
           
        SensorModel model = null;      
        [XmlIgnore]
        public SensorModel Model { get
            {
                if (model == null)
                {
                    SensorModel orign = ConfigData.SensorModelRoot.SensorModels.Find(md => md.Name == ModelKey);
                    model = orign.Copy();
                }
                return model;
            } }
            
        [XmlIgnore]
        public GroupConfig3 CurrentGroup3 { get; set; }
    }

    [XmlRoot("Sensors")]
    public class SensorModelRoot
    {
        [XmlElement("sensor")]
        public List<SensorModel> SensorModels { get; set; }
    }

    public class SensorModel
    {        
        [XmlAttribute("name")]
        public string Name { get; set; }
        [XmlAttribute("title")]
        public string Title { get; set; }
        [XmlAttribute("sname")]
        public string Sname { get; set; }     
        [XmlElement("field")]
        public List<Field> Fields { get; set; }       

        public SensorModel Copy()
        {
            MemoryStream ms = new MemoryStream();
            SensorModel sm = null;            
            XmlSerializer xsl = new XmlSerializer(typeof(SensorModel));
            xsl.Serialize(ms, this);
            ms.Seek(0, SeekOrigin.Begin);
            sm = (SensorModel)xsl.Deserialize(ms);
            ms.Close();

            //for (int i = 0; i < Fields.Count; i++)
            //{
            //    if (Fields[i].Alarm != null)
            //    {
            //        sm.Fields[i].Alarm = Fields[i].Alarm;
            //    }
            //}
                        
            return sm;          
        }

        public override string ToString()
        {
            return Title;
        }
    }

    public class Field
    {
        private AlarmField alarm;       
        [XmlIgnore]
        public AlarmField Alarm
        {
            get { return alarm; }
            set
            {
                if(alarm != value)
                   alarm = value;
            }
        }
        [XmlIgnore]
        public Sensor CurrentSensor { get; set; }
    
        [XmlAttribute("unit")]
        public string Unit { get; set; }
        [XmlAttribute("name")]
        public string Name { get; set; }
        [XmlAttribute("alias")]
        public string Alias { get; set; }
        [XmlAttribute("realtime")]
        public bool Realtime { get; set; }
        [XmlAttribute("history")]
        public bool History { get; set; }

       

        public event EventHandler ValueUpdated;
        public event EventHandler StateChanged;
        int state = 0;
        //0正常 1接近警戒 2报警
        [XmlIgnore]
        public int State
        {
            get
            {
                return state;
            }
            set
            {
                if (state != value || (state==1||state==2))
                {
                    state = value;
                    StateChanged?.Invoke(this, EventArgs.Empty);
                    if (state == 0)
                    {
                        Label.BackColor = System.Drawing.Color.GreenYellow;
                        ClickLabel.BackColor = System.Drawing.Color.GreenYellow;
                    }
                    else if (state == 1)
                    {
                        Label.BackColor = System.Drawing.Color.Yellow;
                        ClickLabel.BackColor = System.Drawing.Color.Yellow;
                    }
                    else if (state == 2)
                    {
                        Label.BackColor = System.Drawing.Color.Red;
                        ClickLabel.BackColor = System.Drawing.Color.Red;
                    }
                }
            }
        }

        string _value = "";
        [XmlIgnore]
        public string Value {
            get { return _value; }
            set {
                _value = value;
                try
                {
                    double.Parse(_value);
                }
                catch
                { return; }
                
                Label.Text = LabelText;
                ClickLabel.Text = CLabelText;
                //latest20.Add(DateTime.Now, double.Parse(_value));
                //if (latest20.Count > 20)
                //{
                //    latest20.Remove(latest20.ElementAt(0).Key);
                //}
                if (chart != null)
                {
                    int index = chart.Series[0].Points.AddXY(DateTime.Now, double.Parse(_value));
                    chart.Series[0].Points[index].ToolTip = DateTime.Now.ToString("HH:mm:ss");
                    if (chart.Series[0].Points.Count > 20)
                    {
                        chart.Series[0].Points.RemoveAt(0);
                    }
                    if (chart.ChartAreas[0].AxisY.Minimum > double.Parse(_value))
                    {
                        chart.ChartAreas[0].AxisY.Minimum = double.Parse(_value)- (int)(double.Parse(_value)*0.01);
                    }
                    if (chart.ChartAreas[0].AxisY.Maximum < double.Parse(_value))
                    {
                        chart.ChartAreas[0].AxisY.Maximum = double.Parse(_value)+ (int)(double.Parse(_value) * 0.01);
                    }                  
                }
                ValueUpdated?.Invoke(this, EventArgs.Empty);
           
                if (Alarm != null)
                {
                    double db = double.Parse(_value);
                    //报警
                    bool mtUp = db > Alarm.Up + Alarm.Around;
                    bool ltLow = db < Alarm.Low - Alarm.Around;
                    //警戒
                    bool aroundUp = db >= Alarm.Up - Alarm.Around && db <= Alarm.Up + Alarm.Around;
                    bool aroundLow = db <= Alarm.Low + Alarm.Around && db >= Alarm.Low - Alarm.Around;                    
                    if (mtUp || ltLow)
                    {
                        State = 2;
                    }                    
                    else if (aroundUp || aroundLow)
                    {
                        State = 1;
                    }
                    else
                    {
                        State = 0;
                    }
                }
                else
                {
                    State = 0;
                }
            }
        }



        private ComboBox comboBox;
        [XmlIgnore]
        public ComboBox ComboBox
        {
            get
            {
                if (comboBox == null)
                {
                    comboBox = new ComboBox();
                    comboBox.Items.Add("最新数据");
                    comboBox.Items.Add("全部数据");
                    comboBox.Items.Add("历史数据");
                    comboBox.SelectedIndex = 0;                    
                }
                return comboBox;
            }
            
        }

        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox.SelectedIndex)
            {
                case 0:
                    Chart.BringToFront();
                    ComboBox.BringToFront();
                    break;
                case 1:
                    ChartAll.Series[0].Points.Clear();
                    string sqlAll = string.Format("SELECT {0} FROM {1}_result WHERE time nodeid = {2}  ", Name + ",time", CurrentSensor.Model.Sname, CurrentSensor.NodeId);
                    DataTable tableAll = null;
                    try
                    {
                        tableAll = SqlLiteHelper.ExecuteReader(ConfigurationManager.AppSettings["dbPath"], sqlAll);
                        foreach (DataRow row in tableAll.Rows)
                        {
                            ChartAll.Series[0].Points.AddXY(row["time"], row[Name]);
                        }
                        ChartAll.BringToFront();
                        ComboBox.BringToFront();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("查询数据失败");
                        ComboBox.SelectedIndex = 0;
                    }
                    
                    break;
                case 2:
                    DateTimePickerForm dtpf = new DateTimePickerForm();
                    if (dtpf.ShowDialog() == DialogResult.OK)
                    {
                        ChartHis.Series[0].Points.Clear();
                        string sqlHis = string.Format("SELECT {0} FROM {1}_result WHERE time nodeid = {2} and time < '{3}' and time >'{4}' ", Name+",time", CurrentSensor.Model.Sname, CurrentSensor.NodeId, dtpf.dateTimePickerRetrieveEnd.Value.ToString("yyyy-MM-dd HH-mm"), dtpf.dateTimePickerRetrieveBegin.Value.ToString("yyyy-MM-dd HH-mm"));
                        DataTable tableHis = null;
                        try
                        {
                            tableHis = SqlLiteHelper.ExecuteReader(ConfigurationManager.AppSettings["dbPath"], sqlHis);
                            foreach (DataRow row in tableHis.Rows)
                            {
                                ChartAll.Series[0].Points.AddXY(row["time"], row[Name]);
                            }
                            ChartHis.BringToFront();
                            ComboBox.BringToFront();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("查询数据失败");
                            ComboBox.SelectedIndex = 0;
                        }
                    }
                    else
                    {
                        ComboBox.SelectedIndex = 0;
                    }          
                    break;
            }
        }


        private Panel chartPanel;
        [XmlIgnore]
        public Panel ChartPanel
        {
            get { return chartPanel; }
            set { chartPanel = value; }
        }

        private CustomChart chart;
        [XmlIgnore]
        public CustomChart Chart
        {
            get
            {
                if (chart == null)
                {
                    chart = new CustomChart();
                    //chart.MinimumSize = new System.Drawing.Size(0, 400);
                    chart.Titles[0].Text = string.Format("{0} ( {1} )", CurrentSensor.GroupName, CurrentSensor.Comment);
                    chart.Legends[0].Title = this.Alias;
                    chart.Series[0].Name = this.Unit;
                    //foreach (var item in Latest20)
                    //{
                    //    chart.Series[0].Points.AddXY(item.Key, item.Value);
                    //}                    
                }
                return chart;
            }
        }
        private CustomChart chartHis;
        [XmlIgnore]
        public CustomChart ChartHis
        {
            get
            {
                if (chartHis == null)
                {
                    chartHis = new CustomChart();
                    chartHis.Series[0].IsValueShownAsLabel = false;                    
                    chartHis.Titles[0].Text = string.Format("{0} ( {1} )", CurrentSensor.GroupName, CurrentSensor.Comment);
                    chartHis.Legends[0].Title = this.Alias;
                    chartHis.Series[0].Name = this.Unit;
                    //foreach (var item in Latest20)
                    //{
                    //    chart.Series[0].Points.AddXY(item.Key, item.Value);
                    //}                    
                }
                return chartHis;
            }
        }
        private CustomChart chartAll;
        [XmlIgnore]
        public CustomChart  ChartAll
        {
            get
            {
                if (chartAll == null)
                {
                    chartAll = new CustomChart();
                    chartHis.Series[0].IsValueShownAsLabel = false;
                    chartAll.Titles[0].Text = string.Format("{0} ( {1} )", CurrentSensor.GroupName, CurrentSensor.Comment);
                    chartAll.Legends[0].Title = this.Alias;
                    chartAll.Series[0].Name = this.Unit;                    
                    //foreach (var item in Latest20)
                    //{
                    //    chart.Series[0].Points.AddXY(item.Key, item.Value);
                    //}                    
                }
                return chartAll;
            }                    
        }
        public void InitChart()
        {
            ComboBox.Location = new System.Drawing.Point(20, 20);
            ComboBox.Visible = true;
            ComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            chartPanel = new Panel();
            chartPanel.Dock = DockStyle.Fill;
            chartPanel.MinimumSize = new System.Drawing.Size(0, 400);
            chartPanel.Controls.Add(Chart);
            chartPanel.Controls.Add(ChartHis);
            chartPanel.Controls.Add(ChartAll);
            chartPanel.Controls.Add(ComboBox);
            Chart.BringToFront();
            ComboBox.BringToFront();
            //string sql = "SELECT * FROM SCity_MX8100_result WHERE nodeid=2000  order by time desc limit 5";
            //string sql = string.Format("SELECT {0} FROM {1}_result WHERE time nodeid = {2}  ", "*", CurrentSensor.Model.Sname, CurrentSensor.NodeId);
            //var dt = SqlLiteHelper.ExecuteReader(ConfigurationManager.AppSettings["dbPath"], sql);

            //foreach (DataRow row in dt.Rows)
            //{
            //    chart.Series[0].Points.AddXY(row["time"], row[Name]);
            //}
        }


         
        [XmlIgnore]
        public Label Label { get; set; }
        [XmlIgnore]
        public Label ClickLabel { get; set; }
        [XmlIgnore]
        public string LabelText
        {
            get
            {
                        
                return " "+Alias +  " : " + Value+" "+  Unit;
            }
        }
        [XmlIgnore]
        public string CLabelText { get { return Value + "\r\n" +"    "+ Unit; } }
        
    }
}
