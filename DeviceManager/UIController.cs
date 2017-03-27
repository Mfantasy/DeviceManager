using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DeviceManager
{
    public static class UIController
    {
        public static Panel panelRight;

        public static void ShowAllTplClickPanel(object sensorsTag)
        {
            if(panelRight.Controls.ContainsKey("tplCP"))
                Console.WriteLine("");
            List<Sensor> sensors = (List<Sensor>)sensorsTag;
            TableLayoutPanel allTplClickPanel = new TableLayoutPanel();
            allTplClickPanel.Name = "tplCP";
        }

    }
}
