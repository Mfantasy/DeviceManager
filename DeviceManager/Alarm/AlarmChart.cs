using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace DeviceManager.Alarm
{
    public partial class AlarmChart : UserControl
    {    
        public AlarmChart()
        {
            InitializeComponent();
            tx.Parent = this;        
            lb.Parent = this;
            tx.Visible = false;           
            lb.Visible = false;
            lb.AutoSize = true;
            lb.BackColor = Color.Black;
            lb.ForeColor = Color.White;
            tx.BorderStyle = BorderStyle.FixedSingle;
            tx.Width = 33;
            tx.TextChanged += Tx_TextChanged;            
            tx.BringToFront();
            lb.BringToFront();                      
        }
        string db = Path.Combine(Utils.GetUserPath(), "alarm.db");
        TextBox tx = new TextBox(); //点击出现的txtbox
        Label lb = new Label();    //点击出现的label
        int h = 0; //小时
        string l = "Top"; //表中线名

        public UserCalendar userCalendar;
        public Alarm24 CurrentA24 { get; set; }   //当前选中的预警字段
        public AlarmStrategy CAS { get; set; }  //当前策略
        public AlarmStrategy CASCopy { get; set; } //编辑缓存

        //刷新字段列表
        void RefreshCombox()
        {
            comboBox1.Items.Clear();
            foreach (var item in ConfigData.SensorModelRoot.SensorModels)
            {
                foreach (var f in item.Fields)
                {
                    if (f.Realtime)
                    {
                        comboBox1.Items.Add(f);
                    }
                }
            }
            if (comboBox1.Items.Count == 0)
            {
                comboBox1.Items.Add("无");
            }
            comboBox1.SelectedIndex = 0;
        }

   
        //切换A24
        public void NewA(Alarm24 a24)
        {            
            CurrentA24 = a24;
            chart1.Series["Top"].Points.Clear();
            chart1.Series["Low"].Points.Clear();
            warnNum.Value = a24.Warn;
            for (int i = 0; i < CurrentA24.Hs.Length; i++)
            {
                chart1.Series["Top"].Points.AddXY(i, CurrentA24.Hs[i].Top);
                chart1.Series["Low"].Points.AddXY(i, CurrentA24.Hs[i].Low);
            }
            chart1.Series["Top"].Points.AddXY(24, CurrentA24.Hs[CurrentA24.Hs.Length - 1].Top);
            chart1.Series["Low"].Points.AddXY(24, CurrentA24.Hs[CurrentA24.Hs.Length - 1].Low);
        }
             
        private void Tx_TextChanged(object sender, EventArgs e)
        {
            if (this.CurrentA24 != null)
            {
                double test = 0;
                if (double.TryParse(tx.Text, out test))
                {
                    chart1.Series[l].Points[h].SetValueY(test);
                    if (l == "Top")
                    {
                        CurrentA24.Hs[h].Top = test;
                    }
                    else if (l == "Low")
                    {
                        CurrentA24.Hs[h].Low = test;
                    }
                    if (h == 23)
                    {
                        chart1.Series[l].Points[24].SetValueY(test);
                    }
                }             
            }
        }


        public bool SetName()
        {
            CAS.Name = textBox1.Text;
            if (ConfigData.AllStrategy.Exists(cas => cas.Name == CAS.Name))
            {
                MessageBox.Show(CAS.Name + "已经存在,请重新命名!");
                textBox1.Focus();
                return false;
            }
            else
            {
                return true;
            }
        }

        //编辑当前策略
        public void EditStrategy(AlarmStrategy cas)
        {
            CAS = cas;
            //copy一个副本,然后通过对比生成sql
        }

        //新建一个策略
        public void CreateNew()
        {                                                         
            textBox1.Text = "未命名";
            RefreshCombox();
            //CAS = new AlarmStrategy();
            //if (comboBox1.Items[0] is Field)
            //{
            //    comboBox1.SelectedIndex = 0;
            //    button1_Click(null, null);
            //}

            this.BringToFront();
            this.Visible = true;
        }

        private void chart1_MouseClick(object sender, MouseEventArgs e)
        {
            System.Windows.Forms.DataVisualization.Charting.HitTestResult Result = new System.Windows.Forms.DataVisualization.Charting.HitTestResult();
            Result = chart1.HitTest(e.X, e.Y);
            if (Result.Series != null)
            {
                string x = Result.Series.Points[Result.PointIndex].XValue.ToString();                
                string y = Result.Series.Points[Result.PointIndex].YValues[0].ToString();
                h = int.Parse(x);
                if (h == 24)
                {
                    lb.Visible = false;
                    tx.Visible = false;
                    return;
                }
                l = Result.Series.Name;
                lb.Text = x;
                tx.Text = y;
                lb.Location = new Point(e.X + 2, e.Y - 18);
                tx.Location = new Point(e.X + 20, e.Y - 20);
                lb.Visible = true;
                tx.Visible = true;
            }
            else
            {
                lb.Visible = false;
                tx.Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //添加
            if (comboBox1.SelectedItem is Field)
            {
                Field f = comboBox1.SelectedItem as Field;
                comboBox1.Items.Remove(f);
                if (comboBox1.Items.Count == 0)
                {
                    comboBox1.Items.Add("无");
                }
                comboBox1.SelectedIndex = 0;
                Alarm24 a24 = new Alarm24(f);
                CAS.A24s.Add(a24);                
                RadioButton rb = new RadioButton();
                rb.Parent = flowLayoutPanel1;
                rb.Visible = true;
                rb.CheckedChanged += (S, E) =>
                {
                    if (rb.Checked)
                    {
                        NewA(a24);
                    }
                };
                rb.Checked = true;

            }
            else
            {
                return;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //保存
            if (CAS.Name == null)
            {
                if (!SetName())
                {
                    return;
                }
            }
            if (ConfigData.AllStrategy.Contains(CAS))
            {
                //编辑保存 //更新Alarm和Map
                //UpdateSql
                
            }
            else //新建保存
            {
                foreach (var item in CAS.A24s)
                {
                    string insertSql = @"INSERT INTO T_ALARM(name,field,warn,h0t,h0l,h1t,h1l,h2t,h2l,h3t,h3l,h4t,h4l,h5t,h5l,h6t,h6l,h7t,h7l,
                    h8t,h8l,h9t,h9l,h10t,h10l,h11t,h11l,h12t,h12l,h13t,h13l,h14t,h14l,h15t,h15l,h16t,h16l,h17t,h17l,h18t,h18l,h19t,h19l,h20t,h20l,h21t,h21l,h22t,h22l,h23t,h23l) 
                    VALUES('" + CAS.Name + "','" + item.Field + "','" + item.Warn + "','" + item.Model + "','"+ item.Hs[0].Top + "','" + item.Hs[0].Low + "','" + item.Hs[1].Top + "','" + item.Hs[1].Low + "','" + item.Hs[2].Top + "','" + item.Hs[2].Low + "','" + item.Hs[3].Top + "','" + item.Hs[3].Low + "','" + item.Hs[4].Top + "','" + item.Hs[4].Low + "','" + item.Hs[5].Top + "','" + item.Hs[5].Low + "','" + item.Hs[6].Top + "','" + item.Hs[6].Low + "','" + item.Hs[7].Top + "','" + item.Hs[7].Low +
                    "','" + item.Hs[8].Top + "','" + item.Hs[8].Low + "','" + item.Hs[9].Top + "','" + item.Hs[9].Low + "','" + item.Hs[10].Top + "','" + item.Hs[10].Low + "','" + item.Hs[11].Top + "','" + item.Hs[11].Low + "','" + item.Hs[12].Top + "','" + item.Hs[12].Low + "','" + item.Hs[13].Top + "','" + item.Hs[13].Low + "','" + item.Hs[14].Top + "','" + item.Hs[14].Low + "','" + item.Hs[15].Top + "','" + item.Hs[15].Low + "','" + item.Hs[16].Top + "','" + item.Hs[16].Low + "','" + item.Hs[17].Top + "','" + item.Hs[17].Low +
                    "','" + item.Hs[18].Top + "','" + item.Hs[18].Low + "','" + item.Hs[19].Top + "','" + item.Hs[19].Low + "','" + item.Hs[20].Top + "','" + item.Hs[20].Low + "','" + item.Hs[21].Top + "','" + item.Hs[21].Low + "','" + item.Hs[22].Top + "','" + item.Hs[22].Low + "','" + item.Hs[23].Top + "','" + item.Hs[23].Low + "')";
                    SqlLiteHelper.ExecuteNonQuery(db,insertSql);
                }
                ConfigData.AllStrategy.Add(CAS);
                userCalendar.RefreshComb();
            }
            
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //取消
            this.Visible = false;
            this.SendToBack();
            
        }

     
    }
}
