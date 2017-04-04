using DeviceManager.Model;
using System;
using System.Collections.Generic;
using System.Data;
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
                    model = ConfigData.SensorModelCfg.SensorModels.Find(md => md.Name == ModelKey);
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

        [XmlElement("field")]
        public List<Field> Fields { get; set; }
    }
    public class Field
    {
        [XmlIgnore]
        public string ModelName { get; set; }
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
        //0正常 1接近告警 2报警
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
                StateChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        
        [XmlIgnore]
        public string Value {
            get { return _value; }
            set {
                _value = value;
                ValueUpdated?.Invoke(this, EventArgs.Empty);
                if (Row != null)
                {
                    Row[1] = Value;
                    Row[2] = DateTime.Now;
                }
            }
        }
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
                return Alias + " : " + Value;
            }
        }
    }
}
