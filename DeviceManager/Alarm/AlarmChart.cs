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
        }

        private void chart1_MouseClick(object sender, MouseEventArgs e)
        {
            System.Windows.Forms.DataVisualization.Charting.HitTestResult Result = new System.Windows.Forms.DataVisualization.Charting.HitTestResult();
            Result = chart1.HitTest(e.X, e.Y);

            if (Result.Series != null)
            {
                string Text1 = Result.Series.Points[Result.PointIndex].XValue.ToString();
                Console.WriteLine(Result.Series.Points[Result.PointIndex].YValues[0].ToString());
            }
            //MessageBox.Show("X轴:" + Result.Series.Points[Result.PointIndex].XValue.ToString() + "Y轴:" + Result.Series.Points[Result.PointIndex].YValues[0].ToString());

        }
    }
}
