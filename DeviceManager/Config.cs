using DeviceManager.Alarm;
using DeviceManager.Model;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
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
        public static List<GroupConfig2> allG2 = new List<GroupConfig2>();
        public static List<GroupConfig3> allG3 = new List<GroupConfig3>();

        public static List<AlarmStrategy> AllStrategy = new List<AlarmStrategy>();

        public static SensorModelRoot SensorModelRoot
        {
            get
            {
                if (sensorModelCfg == null)
                {
                    string fileName = ConfigurationManager.AppSettings["传感器模型配置文件"];
                    //string path = Path.Combine(Utils.GetUserPath(), fileName);
                    
                    if (!File.Exists(fileName))
                    {
                        sensorModelCfg = new SensorModelRoot();
                        sensorModelCfg.SensorModels = new List<SensorModel>();
                    }
                    else
                        sensorModelCfg = Utils.FromXMLFile<SensorModelRoot>(fileName);
                }
                return sensorModelCfg;
            }
        }
        public static GroupConfigRoot GroupConfigRoot
        {
            get
            {
                if (groupCfg == null)
                {
                    string fileName = ConfigurationManager.AppSettings["传感器列表"];                    
                    if (!File.Exists(fileName))
                    {
                        groupCfg = new GroupConfigRoot();
                        groupCfg.GroupConfig1s = new List<GroupConfig1>();         
                    }
                    else
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
                   
                    if (!File.Exists(fileName))
                    {
                        alarmCfg = new AlarmConfigRoot();
                        alarmCfg.AlarmConfigs = new List<AlarmConfig>();
                    }
                    else
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
                    allG2.Add(itemgc2);
                    foreach (var itemgc3 in itemgc2.GroupConfigs)
                    {
                        allG3.Add(itemgc3);
                        foreach (var ss in itemgc3.Sensors)
                        {
                            allSensors.Add(ss);
                            ss.CurrentGroup3 = itemgc3;
                            ss.GroupName = itemgc1.Name + " " + itemgc2.Name + " " + itemgc3.Name;
                            foreach (var field in ss.Model.Fields)
                            {
                                allFields.Add(field);
                                field.CurrentSensor = ss;
                                field.InitChart();
                            }
                        }
                    }
                }
            }
            //Chart初始化
        }
    }

    public static class Config
    {
        public static bool IsShowLogon = true;
        //0 未初始化 1 管理员 2 访问者
        public static int UserLevel = 0;
        
    }
    

}
