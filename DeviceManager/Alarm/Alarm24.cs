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
            bool bname = ast.Name == this.Name;
            if (!bname)
            {
                sql += string.Format(@"UPDATE T_ALARM SET name='{0}' WHERE name='{1}';
                    UPDATE T_ALARM_SENSOR_MAP SET aname = '{0}' WHERE aname = '{1}';", ast.Name, this.Name);
            }
            //如果存在任何不一样的地方,则全局进行更新操作            
            bool ba24 = true;
            for (int i = 0; i < this.A24s.Count; i++)
            {
                if (ast.A24s[i].Field != this.A24s[i].Field || ast.A24s[i].Warn != this.A24s[i].Warn)
                {
                    ba24 = false;
                    break;
                }

                for (int j = 0; j < this.A24s[i].Hs.Length; j++)
                {
                    if (ast.A24s[i].Hs[j].Low != this.A24s[i].Hs[j].Low || ast.A24s[i].Hs[j].Top != this.A24s[i].Hs[j].Top)
                    {
                        ba24 = false;
                        break;
                    }
                }
                if (!ba24)
                {
                    
                    //此处就不能做全部更新了, 因为其实field的顺序是固定不变的,并且只能增加不会减少,而且目前还没有策略列表的管理界面.
                    sql += string.Format(@"UPDATE T_ALARM SET field='{0}',warn='{1}',h0t='{2}',h0l='{3}',h1t='{4}',h1l='{5}',h2t='{6}',h2l='{7}',h3t='{8}',h3l='{9}',h4t='{10}',h4l='{11}',h5t='{12}',h5l='{13}',h6t='{14}',h6l='{15}',h7t='{16}',h7l='{17}',h8t='{18}',h8l='{19}',
                        h9t ='{20}',h9l ='{21}',h10t ='{22}',h10l ='{23}',h11t ='{24}',h11l ='{25}',h12t ='{26}',h12l ='{27}',h13t ='{28}',h13l ='{29}',h14t ='{30}',h14l ='{31}',h15t ='{32}',h15l ='{33}',h16t ='{34}',h16l ='{35}',h17t ='{36}',h17l ='{37}',h18t ='{38}',h18l ='{39}',
                        h19t ='{40}',h19l ='{41}',h20t ='{42}',h20l ='{43}',h21t ='{44}',h21l ='{45}',h22t ='{46}',h22l ='{47}',h23t ='{48}',h23l ='{49}' WHERE name='{50}' AND field = '{51}';", ast.A24s[i].Field, ast.A24s[i].Warn, ast.A24s[i].Hs[0].Top, ast.A24s[i].Hs[0].Low, ast.A24s[i].Hs[1].Top, ast.A24s[i].Hs[1].Low,
                        ast.A24s[i].Hs[2].Top, ast.A24s[i].Hs[2].Low, ast.A24s[i].Hs[3].Top, ast.A24s[i].Hs[3].Low, ast.A24s[i].Hs[4].Top, ast.A24s[i].Hs[4].Low, ast.A24s[i].Hs[5].Top, ast.A24s[i].Hs[5].Low, ast.A24s[i].Hs[6].Top, ast.A24s[i].Hs[6].Low, ast.A24s[i].Hs[7].Top, ast.A24s[i].Hs[7].Low, ast.A24s[i].Hs[8].Top, ast.A24s[i].Hs[8].Low, ast.A24s[i].Hs[9].Top, ast.A24s[i].Hs[9].Low,
                        ast.A24s[i].Hs[10].Top, ast.A24s[i].Hs[10].Low, ast.A24s[i].Hs[11].Top, ast.A24s[i].Hs[11].Low, ast.A24s[i].Hs[12].Top, ast.A24s[i].Hs[12].Low, ast.A24s[i].Hs[13].Top, ast.A24s[i].Hs[13].Low, ast.A24s[i].Hs[14].Top, ast.A24s[i].Hs[14].Low, ast.A24s[i].Hs[15].Top, ast.A24s[i].Hs[15].Low, ast.A24s[i].Hs[16].Top, ast.A24s[i].Hs[16].Low, ast.A24s[i].Hs[17].Top, ast.A24s[i].Hs[17].Low,
                        ast.A24s[i].Hs[18].Top, ast.A24s[i].Hs[18].Low, ast.A24s[i].Hs[19].Top, ast.A24s[i].Hs[19].Low, ast.A24s[i].Hs[20].Top, ast.A24s[i].Hs[20].Low, ast.A24s[i].Hs[21].Top, ast.A24s[i].Hs[21].Low, ast.A24s[i].Hs[22].Top, ast.A24s[i].Hs[22].Low, ast.A24s[i].Hs[23].Top, ast.A24s[i].Hs[23].Low, ast.Name, this.A24s[i].Field);                        
                }
                ba24 = true;
            }
            if (ast.A24s.Count > this.A24s.Count)
            {
                for (int i = this.A24s.Count; i < ast.A24s.Count; i++)
                {
                    sql += string.Format(@"INSERT INTO T_ALARM(name,field,warn,h0t,h0l,h1t,h1l,h2t,h2l,h3t,h3l,h4t,h4l,h5t,h5l,h6t,h6l,h7t,h7l,h8t,h8l,h9t,h9l,h10t,h10l,h11t,h11l,h12t,h12l,h13t,h13l,h14t,h14l,h15t,h15l,h16t,h16l,h17t,h17l,h18t,h18l,h19t,h19l,h20t,h20l,h21t,h21l,h22t,h22l,h23t,h23l) VALUES
('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}','{29}','{30}','{31}','{32}','{33}','{34}','{35}','{36}','{37}','{38}','{39}','{40}','{41}','{42}','{43}','{44}','{45}','{46}','{47}','{48}','{49}','{50}');",
ast.Name,ast.A24s[i].Field,ast.A24s[i].Warn, ast.A24s[i].Hs[0].Top, ast.A24s[i].Hs[0].Low, ast.A24s[i].Hs[1].Top, ast.A24s[i].Hs[1].Low,ast.A24s[i].Hs[2].Top, ast.A24s[i].Hs[2].Low, ast.A24s[i].Hs[3].Top, ast.A24s[i].Hs[3].Low, ast.A24s[i].Hs[4].Top, ast.A24s[i].Hs[4].Low, ast.A24s[i].Hs[5].Top, ast.A24s[i].Hs[5].Low, ast.A24s[i].Hs[6].Top, ast.A24s[i].Hs[6].Low, ast.A24s[i].Hs[7].Top, ast.A24s[i].Hs[7].Low, ast.A24s[i].Hs[8].Top, ast.A24s[i].Hs[8].Low, ast.A24s[i].Hs[9].Top, ast.A24s[i].Hs[9].Low,
                        ast.A24s[i].Hs[10].Top, ast.A24s[i].Hs[10].Low, ast.A24s[i].Hs[11].Top, ast.A24s[i].Hs[11].Low, ast.A24s[i].Hs[12].Top, ast.A24s[i].Hs[12].Low, ast.A24s[i].Hs[13].Top, ast.A24s[i].Hs[13].Low, ast.A24s[i].Hs[14].Top, ast.A24s[i].Hs[14].Low, ast.A24s[i].Hs[15].Top, ast.A24s[i].Hs[15].Low, ast.A24s[i].Hs[16].Top, ast.A24s[i].Hs[16].Low, ast.A24s[i].Hs[17].Top, ast.A24s[i].Hs[17].Low,
                        ast.A24s[i].Hs[18].Top, ast.A24s[i].Hs[18].Low, ast.A24s[i].Hs[19].Top, ast.A24s[i].Hs[19].Low, ast.A24s[i].Hs[20].Top, ast.A24s[i].Hs[20].Low, ast.A24s[i].Hs[21].Top, ast.A24s[i].Hs[21].Low, ast.A24s[i].Hs[22].Top, ast.A24s[i].Hs[22].Low, ast.A24s[i].Hs[23].Top, ast.A24s[i].Hs[23].Low);
                }
            }
            return sql;
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
