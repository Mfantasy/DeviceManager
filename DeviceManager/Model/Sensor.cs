using DeviceManager.Model;
using System;
using System.Collections.Generic;
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
        //频率(分钟)
        [XmlAttribute("interval")]
        public int Interval { get; set; }                
        [XmlAttribute("model")]
        public string ModelKey { get; set; }
        [XmlAttribute("alarmConfig")]
        public string AlarmConfigKey { get; set; }
        [XmlAttribute("groupConfig")]
        public string GroupConfigKey { get; set; }
         
        [XmlIgnore]
        public DateTime Time { get; set; }
        //0正常 1接近告警 2报警
        [XmlIgnore]
        public int State
        { get {
#warning 根据模型中的所有字段进行匹配做报警处理          
                return 0;
            } }

        SensorModel model = null;      
        [XmlIgnore]
        public SensorModel Model { get
            {
                if (model == null)
                {
                    model = ConfigData.SensorModelCfg.Sensors.Find(md => md.Name == ModelKey);
                }
                return model;
            } }
        [XmlIgnore]
        public AlarmConfig AlarmConfig
        {
            get
            {
                AlarmConfig cfg = ConfigData.AlarmCfg.AlarmConfigs.Find(acfg => acfg.Name == AlarmConfigKey);
                return cfg;
            }
        }
        [XmlIgnore]
        public GroupConfig GroupConfig { get { GroupConfig cfg = ConfigData.GroupCfg.GroupConfigs.Find(gcfg => gcfg.Key == GroupConfigKey);return cfg; } }
    }

    [XmlRoot("Sensors")]
    public class SensorModelRoot
    {
        [XmlElement("sensor")]
        public List<SensorModel> Sensors { get; set; }
    }

    public class SensorModel
    {        
        [XmlAttribute("name")]
        public string Name { get; set; }
        [XmlAttribute("title")]
        public string Title { get; set; }
        [XmlElement("field")]
        public List<Field> Fields { get; set; }
    }
    public class Field
    {
        [XmlAttribute("name")]
        public string Name { get; set; }
        [XmlAttribute("alias")]
        public string Alias { get; set; }
        [XmlAttribute("realtime")]
        public bool Realtime { get; set; }
        [XmlAttribute("history")]
        public bool History { get; set; }
        [XmlIgnore]
        public string Value { get; set; }
        [XmlIgnore]
        public Label Label { get; set; }
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
