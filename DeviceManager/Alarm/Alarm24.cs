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

        //比较并生成更新SQL
        public string CompareTo(AlarmStrategy ast)
        {
            string sql = "";
            if (ast.Name != this.Name)
            {
                sql += string.Format(@"UPDATE T_ALARM SET name='{0}' WHERE name='{1}';
                    UPDATE T_ALARM_SENSOR_MAP SET aname = '{0}' WHERE aname = '{1}';",ast.Name,this.Name);
            }
            foreach (var item in A24s)
            {
                //item
            }

 
        }

        public AlarmStrategy Copy()
        {
            AlarmStrategy acopy = new AlarmStrategy();
            acopy.Name = this.Name;
            acopy.A24s = new List<Alarm24>();
            foreach (var item in this.A24s)
            {
                Alarm24 a24 = new Alarm24();
                a24.Field = item.Field;
                
                a24.Warn = item.Warn;
                for (int i = 0; i < a24.Hs.Length; i++)
                {
                    a24.Hs[i].Low = item.Hs[i].Low;
                    a24.Hs[i].Top = item.Hs[i].Top;
                }
                acopy.A24s.Add(a24);
            }
            return acopy;
        }

    }

    public class Alarm24
    {
        public Alarm24()
        { }

        public Alarm24(Field f)
        {            
            this.Field = f.Name;            
            Warn = 100;
        }
           
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
