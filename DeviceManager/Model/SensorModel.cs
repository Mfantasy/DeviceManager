using DeviceManager.CustomControl;
using DeviceManager.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using static DeviceManager.Model.GroupConfig;

namespace DeviceManager
{
    [XmlRoot("Sensors")]
    public class SensorRoot
    {
        [XmlElement("sensor")]
        public List<Sensor> Sensors { get; set; }
    }

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
        [XmlAttribute("groupConfig")]
        public string GroupConfigKey { get; set; }
        
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

        [XmlIgnore]
        public DateTime Time { get; set; }
     

        SensorModel model = null;      
        [XmlIgnore]
        public SensorModel Model { get
            {
                if (model == null)
                {
                    SensorModel orign = ConfigData.SensorModelCfg.SensorModels.Find(md => md.Name == ModelKey);
                    model = orign.Copy();
                }
                return model;
            } }
            
        [XmlIgnore]
        public GroupConfig GroupConfig { get { GroupConfig cfg = ConfigData.GroupCfg.GroupConfigs.Find(gcfg => gcfg.Key == GroupConfigKey);return cfg; } }
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
        [XmlAttribute("alarm")]
        public string AlarmName { get; set; }
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

            for (int i = 0; i < Fields.Count; i++)
            {
                sm.Fields[i].Alarm = Fields[i].Alarm;
            }
                        
            return sm;
            //SensorModel smodel = new SensorModel();
            //smodel.Name = Name;
            //smodel.Title = Title;
            //smodel.Sname = Sname;
            //smodel.AlarmName = AlarmName;
            //smodel.Fields = Fields.            
        }


    }
    public class Field
    {
        [XmlIgnore]
        public AlarmField Alarm { get; set; }
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
                state = value;
                StateChanged?.Invoke(state, EventArgs.Empty);
                if (state == 0)
                {
                    Label.BackColor = System.Drawing.Color.LightGreen;
                    ClickLabel.BackColor = System.Drawing.Color.LightGreen;
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
                latestTen.Add(DateTime.Now, double.Parse(_value));
                if (latestTen.Count > 10)
                {
                    latestTen.Remove(latestTen.ElementAt(0).Key);
                }
                if (chart != null)
                {
                    chart.Series[0].Points.AddXY(DateTime.Now, double.Parse(_value));
                    if (chart.Series[0].Points.Count > 10)
                    {
                        chart.Series[0].Points.RemoveAt(0);
                    }
                    if (chart.ChartAreas[0].AxisY.Maximum < double.Parse(_value))
                    {
                        chart.ChartAreas[0].AxisY.Maximum = double.Parse(_value);
                    }
                }
                ValueUpdated?.Invoke(this, EventArgs.Empty);
                if (Row != null)
                {
                    Row[1] = Value;
                    Row[2] = DateTime.Now;
                }
             
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
            }
        }

        private CustomChart chart;

        public CustomChart  Chart
        {
            get
            {
                if (chart == null)
                {
                    chart = new CustomChart();
                    chart.Legends[0].Title = this.Alias;
                    chart.Series[0].Name = this.Unit;
                    foreach (var item in LatestTen)
                    {
                        chart.Series[0].Points.AddXY(item.Key, item.Value);
                    }                    
                }
                return chart;
            }
            
        }


        Dictionary<DateTime, double> latestTen = new Dictionary<DateTime, double>();
        [XmlIgnore]
        public Dictionary<DateTime,double> LatestTen { get { return latestTen; } }
        string _value = "";
        [XmlIgnore]
        public DataRow Row { get; set; }
        [XmlIgnore]
        public Label Label { get; set; }
        [XmlIgnore]
        public Label ClickLabel { get; set; }
        [XmlIgnore]
        public string LabelText
        {
            get
            {               
                return Alias + " : " + Value+" "+Unit;
            }
        }
    }
}
