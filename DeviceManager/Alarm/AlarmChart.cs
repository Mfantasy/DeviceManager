using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DeviceManager.Alarm
{
    public partial class AlarmChart : UserControl
    {
        public AlarmChart()
        {
            InitializeComponent();
            tx.Parent = this;        
            lb.Parent = this;
            lb.AutoSize = true;
            lb.BackColor = Color.White;
            tx.BorderStyle = BorderStyle.FixedSingle;
            tx.Width = 33;
            tx.BringToFront();
            lb.BringToFront();
        }

        TextBox tx = new TextBox();
        Label lb = new Label();
        private void chart1_MouseClick(object sender, MouseEventArgs e)
        {
            System.Windows.Forms.DataVisualization.Charting.HitTestResult Result = new System.Windows.Forms.DataVisualization.Charting.HitTestResult();
            Result = chart1.HitTest(e.X, e.Y);

            if (Result.Series != null)
            {
                string x = Result.Series.Points[Result.PointIndex].XValue.ToString();
                string y = Result.Series.Points[Result.PointIndex].YValues[0].ToString();
                
                lb.Text = x;
                tx.Text = y;
                lb.Location = new Point(e.X+2, e.Y-18);
                tx.Location = new Point(e.X+20,e.Y-20);
                lb.Visible = true;
                tx.Visible = true;            
            }
            
        }

        public Alarm24 A24 { get; set; }
    }
}
