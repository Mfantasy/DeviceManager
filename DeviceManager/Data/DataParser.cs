using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceManager
{
    class DataParser
    {      
        public static void ParseJObj(JObject jobj)
        {
            JEnumerable<JToken> jdata = jobj["data"].Children();
            string nodeid = "";
            string uid = "";
            string portid = ""; 
            string key = "converted";
            foreach (JToken jt in jdata)
            {
                string name = jt["name"].ToString();
                switch (name)
                {
                    case "nodeid":
                        nodeid = jt[key].ToString();
                        break;
                    case "uid":
                        uid = jt["raw"].ToString().Remove(0,2);
                        break;
                    case "port":
                        portid = jt[key].ToString();
                        break;
                }             
            }
            List<Sensor> sensors = ConfigData.AllSensors.Sensors.FindAll(ss => ss.Uid == uid && ss.NodeId == nodeid);
            if (sensors == null || sensors.Count==0)
            {
                return;
            }
            Sensor sensor;
            if (sensors.Count == 1)
            {
                sensor = sensors[0];
            }
            else
            {
                sensor = sensors.Find(ss => ss.PortId == portid);
                if (sensor == null)
                    return;
            } 
            foreach (JToken jt in jdata)
            {
                string name = jt["name"].ToString();
                foreach (Field field in sensor.Model.Fields)
                {
                    if (!field.Realtime)
                    {
                        continue;
                    }
                    if (name == field.Name)
                    {
                        if(name=="uid")
                            field.Value = jt["raw"].ToString().Remove(0, 2);
                        else
                            field.Value = jt[key].ToString();
                        field.Label.Invoke(new Action(() => field.Label.Text = field.LabelText));
                    }                   
                }
            }
        }
    }
}
