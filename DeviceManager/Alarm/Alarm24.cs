using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceManager.Alarm
{

    public class AlarmStrategy
    {
        public string Name { get; set; }
        public List<Alarm24> A24s = new List<Alarm24>();
        public override string ToString()
        {
            return this.Name;
        }
    }

    public class Alarm24
    {
        public Alarm24()
        { }

        public Alarm24(Field f)
        {            
            this.Field = f.Name;
            this.Model = f.GetModelKey();
            Warn = 100;
        }
        
        public string Model { get; set; }
        public string Field { get; set; }
        public int Warn { get; set; }

        public TLValue[] Hs = new TLValue[24]
        {new TLValue() { Top = 65535,Low=1000},new TLValue() { Top = 65535,Low=1000},new TLValue() { Top = 65535,Low=1000},new TLValue() { Top = 65535,Low=1000},new TLValue() { Top = 65535,Low=1000},new TLValue() { Top = 65535,Low=1000},
        new TLValue() { Top = 65535,Low=1000},new TLValue() { Top = 65535,Low=1000},new TLValue() { Top = 65535,Low=1000},new TLValue() { Top = 65535,Low=1000},new TLValue() { Top = 65535,Low=1000},new TLValue() { Top = 65535,Low=1000},
        new TLValue() { Top = 65535,Low=1000},new TLValue() { Top = 65535,Low=1000},new TLValue() { Top = 65535,Low=1000},new TLValue() { Top = 65535,Low=1000},new TLValue() { Top = 65535,Low=1000},new TLValue() { Top = 65535,Low=1000},
        new TLValue() { Top = 65535,Low=1000},new TLValue() { Top = 65535,Low=1000},new TLValue() { Top = 65535,Low=1000},new TLValue() { Top = 65535,Low=1000},new TLValue() { Top = 65535,Low=1000},new TLValue() { Top = 65535,Low=1000},};

    }
    //<AlarmMap aname="博物馆监测策略" date="20170608" uid = "01E6F5DC180000AE" node="60" port="1" />
    //<Alarms><am name="博物馆监测策略">
    // <a24 name="博物馆监测策略" model="" field="" warn="" h0t="" h0l="" />
    // <a24 name="博物馆监测策略" model="" field="" warn="" h0t="" h0l="" />


    public class TLValue
    {
        public double Top { get; set; }
        public double Low { get; set; }
    }

    public class AlarmMap
    {
        public string Name { get; set; }
        public string Model { get; set; }
        public string Field { get; set; }
        public string Date { get; set; }
        public string Gate { get; set; }
        public string Node { get; set; }
        public string Port { get; set; }
    }
}
