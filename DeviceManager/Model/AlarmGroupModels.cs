using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DeviceManager.Model
{
    [XmlRoot("GroupConfig")]
    public class GroupConfigRoot
    {
        [XmlElement("config1")]
        public List<GroupConfig1> GroupConfig1s { get; set; }
    }

    public class GroupConfig1
    {
        [XmlElement("config2")]
        public List<GroupConfig2> GroupConfigs { get; set; }
      
        [XmlAttribute("name")]
        public string Name { get; set; }

    }

    public class GroupConfig2
    {
        [XmlElement("config3")]
        public List<GroupConfig3> GroupConfigs { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }
    }

    public class GroupConfig3
    {
        [XmlElement("sensor")]
        public List<Sensor> Sensors { get; set; }
        [XmlAttribute("name")]
        public string Name { get; set; }
    }


    [XmlRoot("AlarmConfigs")]
    public class AlarmConfigRoot
    {
        [XmlElement("config")]
        public List<AlarmConfig> AlarmConfigs { get; set; }
    }

    public class AlarmConfig
    {
        [XmlAttribute("name")]
        public string Name { get; set; }
        [XmlElement("field")]
        public List<AlarmField> AlarmField { get; set; }
        [XmlAttribute("enable")]
        public bool Enable { get; set; }
        [XmlAttribute("startdate")]
        public int Startdate { get; set; }
        [XmlAttribute("enddate")]
        public int Enddate { get; set; }
        [XmlAttribute("starttime")]
        public int Starttime { get; set; }
        [XmlAttribute("endtime")]
        public int Endtime { get; set; }
    }

    public class AlarmField
    {     
        [XmlAttribute("model")]
        public string Model { get; set; }
        [XmlAttribute("name")]
        public string Name { get; set; }
        [XmlAttribute("up")]
        public double Up { get; set; }
        [XmlAttribute("low")]
        public double Low { get; set; }
        [XmlAttribute("around")]
        public double Around { get; set; }


    }
}
