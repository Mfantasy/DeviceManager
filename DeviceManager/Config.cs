using DeviceManager.Model;
using System.Collections.Generic;
using System.Configuration;
using System.Xml;

namespace DeviceManager
{
    public static class ConfigData
    {
        static SensorModelRoot sensorModelCfg = null;        
        static GroupConfigRoot groupCfg = null;
        static AlarmConfigRoot alarmCfg = null;
        public static List<Sensor> allSensors = new List<Sensor>();
        public static List<Field> allFields = new List<Field>();

        public static SensorModelRoot SensorModelRoot
        { get { if (sensorModelCfg == null)
                {
                    string fileName = ConfigurationManager.AppSettings["传感器模型配置文件"];
                    sensorModelCfg = Utils.FromXMLFile<SensorModelRoot>(fileName);
                }
                return sensorModelCfg;
                        } }
        public static GroupConfigRoot GroupConfigRoot
        {
            get
            {
                if (groupCfg == null)
                {
                    string fileName = ConfigurationManager.AppSettings["传感器列表"];
                    groupCfg = Utils.FromXMLFile<GroupConfigRoot>(fileName);
                }
                return groupCfg;
            }
        }
        public static AlarmConfigRoot AlarmConfigRoot
        {
            get
            {
                if (alarmCfg == null)
                {
                    string fileName = ConfigurationManager.AppSettings["预警配置文件"];
                    alarmCfg = Utils.FromXMLFile<AlarmConfigRoot>(fileName);
                }
                return alarmCfg;
            }
        }

        public static void InitConfig()
        {
            //首先为模型匹配预警
            //foreach (SensorModel smodel in SensorModelRoot.SensorModels)
            //{
            //    if (string.IsNullOrWhiteSpace(smodel.AlarmName))
            //    {
            //        continue;
            //    }
            //    else
            //    {
            //        AlarmConfig acfg = AlarmConfigRoot.AlarmConfigs.Find(cfg => cfg.Name == smodel.AlarmName);
            //        if (acfg == null)
            //        {
            //            continue;
            //        }
            //        foreach (Field field in smodel.Fields)
            //        {
            //            field.Alarm = acfg.AlarmField.Find(af => af.Name == field.Name);
            //        }
            //    }
            //}
            //为各个传感器相应字段附加其所需值
            foreach (var itemgc1 in GroupConfigRoot.GroupConfig1s)
            {
                foreach (var itemgc2 in itemgc1.GroupConfigs)
                {
                    foreach (var itemgc3 in itemgc2.GroupConfigs)
                    {
                        foreach (var ss in itemgc3.Sensors)
                        {
                            allSensors.Add(ss);
                            ss.CurrentGroup3 = itemgc3;
                            ss.GroupName = itemgc1.Name + " " + itemgc2.Name + " " + itemgc3.Name;
                            foreach (var field in ss.Model.Fields)
                            {
                               allFields.Add(field);
                               field.CurrentSensor = ss;                               
                            }
                        }
                    }
                }
            }

        }
    }

    public static class Config
    {
        public static bool IsShowLogon = false;
        
    }

    public class ConfigSaver
    {        
        void Test()
        {
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load("");
            xdoc.ChildNodes[1].Attributes[0].Value = "";
            xdoc.Save("");
        }
    }

}
