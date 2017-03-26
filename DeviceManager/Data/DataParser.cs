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
            //string sensorModel = jobj["parser"].ToString();
            //SensorModel smodel = ConfigData.SensorModelCfg.Sensors.Find(model => model.Name == sensorModel);
            //if (smodel == null)
            //{ return; }
            //smodel.Fields
            string key = "converted";
            foreach (JToken jt in jdata)
            {
                string name = jt["name"].ToString();
                switch (name)
                {
                    //我在这个位置取到传感器的UID+NODEID。然后从·ALLSensors中找到这个玩意~然后把ALLsensor的内个的label的文字改成这个玩意~
                    case "nodeid":
                        nodeid = jt[key].ToString();
                        break;
                    case "uid":
                        uid = jt["raw"].ToString().Remove(0,2);
                        break;
                }             
            }
            Sensor sensor = ConfigData.AllSensors.Sensors.Find(ss => ss.Uid == uid && ss.NodeId == nodeid);
            if (sensor == null)
            {
                return;
            }
            Console.WriteLine("find!");
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
                    }
                    field.Label.Invoke(new Action(()=>field.Label.Text = field.LabelText));
                }
            }
        }
    }
}
