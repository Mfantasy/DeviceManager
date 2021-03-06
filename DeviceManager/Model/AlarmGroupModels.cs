﻿using System;
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
        public override string ToString()
        {
            return Name;
        }

    }

    public class GroupConfig2
    {
        [XmlElement("config3")]
        public List<GroupConfig3> GroupConfigs { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }

    public class GroupConfig3
    {
        [XmlElement("sensor")]
        public List<Sensor> Sensors { get; set; }
        [XmlAttribute("name")]
        public string Name { get; set; }
        public override string ToString()
        {
            return Name;
        }
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
      
        [XmlAttribute("startdate")]
        public int Startdate { get; set; }
        [XmlAttribute("enddate")]
        public int Enddate { get; set; }
        [XmlAttribute("starttime")]
        public int Starttime { get; set; }
        [XmlAttribute("endtime")]
        public int Endtime { get; set; }

        [XmlAttribute("alldate")]
        public bool AllDate { get; set; }

        [XmlAttribute("alltime")]
        public bool AllTime { get; set; }


        private bool inDate;
        [XmlIgnore]
        public bool InDate
        {
            get
            {                
                if (AllDate)
                {
                    return true;
                }
                return inDate;
            }
            set
            {
                if(inDate != value)
                {                    
                    inDate = value;
                }
            }
        }

        private bool inTime;
        [XmlIgnore]
        public bool InTime
        {
            get
            {
                if (AllTime)
                {
                    return true;
                }
                   return inTime;
            }
            set
            {
                if (inTime != value)
                {
                    inTime = value;             
                }
            }
        }

        public override string ToString()
        {
            return Name;
        }

    }

    public class AlarmField
    {     
        [XmlAttribute("model")]
        public string Model { get; set; }
        [XmlAttribute("modelcn")]
        public string ModelChnName { get; set; }
        [XmlAttribute("name")]
        public string Name { get; set; }
        [XmlAttribute("namecn")]
        public string NameChnName { get; set; }
        [XmlAttribute("up")]
        public double Up { get; set; }
        [XmlAttribute("low")]
        public double Low { get; set; }
        [XmlAttribute("around")]
        public double Around { get; set; }
    }
}
