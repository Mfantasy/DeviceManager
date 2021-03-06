﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace DeviceManager.CustomControl
{
    public class CustomChart:Chart
    {
        public CustomChart()
        {
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
        
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            legend1.Font = new System.Drawing.Font("微软雅黑", 16, System.Drawing.FontStyle.Bold);
            title1.Font = new System.Drawing.Font("微软雅黑", 16,System.Drawing.FontStyle.Bold);            
            legend1.TitleFont = new System.Drawing.Font("微软雅黑", 16, System.Drawing.FontStyle.Bold); 
            this.Legends.Add(legend1);
            this.Titles.Add(title1);
        
            chartArea1.AxisX.MajorTickMark.Enabled = false;
            chartArea1.AxisY.MajorTickMark.Enabled = true;            
            chartArea1.AxisX.MajorGrid.Enabled = false;
            chartArea1.AxisY.MajorGrid.Enabled = false;
            chartArea1.AxisY.IsStartedFromZero = false;            
            chartArea1.Name = "ChartArea1";
            chartArea1.CursorX.AutoScroll = true;
            chartArea1.CursorX.AxisType = AxisType.Primary;
            //chartArea1.CursorX.IsUserEnabled = true;
            chartArea1.CursorX.IntervalOffsetType = DateTimeIntervalType.Auto;
            chartArea1.CursorX.IntervalType = DateTimeIntervalType.Auto;
            chartArea1.CursorX.IsUserSelectionEnabled = true;
        
            ChartAreas.Add(chartArea1);
            
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series1.IsValueShownAsLabel = true;
            //series1.MarkerBorderColor = System.Drawing.Color.DarkOrange;
            series1.MarkerSize = 6;            
            //series1.MarkerBorderWidth = 1;
            //series1.MarkerColor = System.Drawing.Color.WhiteSmoke;
            series1.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            series1.Name = "Series1";
            series1.XValueType = ChartValueType.Time;        
            series1.IsXValueIndexed = true;           
            Series.Add(series1);
            this.Dock = System.Windows.Forms.DockStyle.Fill;
        }
    }
}
