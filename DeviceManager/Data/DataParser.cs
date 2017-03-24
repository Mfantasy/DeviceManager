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
#warning 处理Json数据
            IEnumerable<JToken> jdata = jobj["data"].Children();
            Sensor sensor = new Sensor();
            string sensorModel = jobj["parser"].ToString();            
            foreach (JToken jt in jdata)
            {
                string name = jt["name"].ToString();
                switch (name)
                {
                    //case "nodeid":
                        //sensor.NodeId = 
                 
                }
            }
        }
    }
}
