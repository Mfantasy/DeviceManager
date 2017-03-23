using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DeviceManager
{
    public class Sensor
    {    
        public string Uid { get; set; }
        public string NodeId { get; set; }
        public string Comment { get; set; }
        public string GroupLevel0 { get; set; }
        public string GroupLevel1 { get; set; }
        public string GroupLevel2 { get; set; }
        //0正常 1接近告警 2报警
        public int State { get; set; }
        
        public SensorModel Model { get; set; }

    }

    [XmlRoot("Sensors")]
    public class SensorRoot
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
    }
}
