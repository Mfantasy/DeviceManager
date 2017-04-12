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
        [XmlElement("config")]
        public List<GroupConfig> GroupConfigs { get; set; }
    }

    public class GroupConfig
    {
        [XmlElement("config")]
        public List<GroupConfig> GroupConfigs { get; set; }
        [XmlAttribute("order")]
        public int Order { get; set; }
        [XmlAttribute("key")]
        public string Key { get; set; }
        [XmlAttribute("name")]
        public string Name { get; set; }

        List<Sensor> sensors = null;
        [XmlIgnore]
        public List<Sensor> Sensors
        {
            get
            {
                if (sensors == null)
                {
                    sensors = ConfigData.AllSensors.Sensors.FindAll(sensor => sensor.GroupConfigKey == Key);
                }
                return sensors;
            }
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