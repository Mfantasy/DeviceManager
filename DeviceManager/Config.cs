using DeviceManager.Model;
using FOF.UserControlModel;
using System.Collections.Generic;
using System.Configuration;
using System.Windows.Forms;

namespace DeviceManager
{
    public static class Config
    {
        public static bool IsShowLogon = false;
        //public static bool IsMySql = true;
    }

    public static class ConfigParser
    {
        public static void ParseUI(MainForm form)
        {
            string fileName = ConfigurationManager.AppSettings["界面配置文件"];
            UIModel ui = Utils.FromXMLFile<UIModel>(fileName);
            SetElement(form, ui.Elements);            
        }
        
        static void SetElement(Control parent, List<Element> elements)
        {
            foreach (Element element in elements)
            {
                Control ctrlP = parent.Controls.Find(element.Name, false)[0];
                if (element.Properties != null)
                {
                    SetProperty(ctrlP, element.Properties);
                }
                if (element.Children != null)
                {
                    foreach (Child child in element.Children)
                    {
                        Control ctrlC = ctrlP.Controls.Find(child.Name, false)[0];
                        if (child.Properties != null)
                        {
                            SetProperty(ctrlC, child.Properties);
                        }
                    }
                }
                if (element.Elements != null)
                {
                    SetElement(ctrlP, element.Elements);
                }
            }
        }

        static void SetProperty(Control ctrl,List<Property> properties)
        {
            foreach (Property property in properties)
            {
                ctrl.GetType().GetProperty(property.Name).SetValue(ctrl, property.Value);
            }
        }

        public static void ParseSensor(Panel panelLeft)
        {
            string fileName = ConfigurationManager.AppSettings["传感器配置文件"];
            SensorRoot sroot = Utils.FromXMLFile<SensorRoot>(fileName);
            foreach (SensorModel smodel in sroot.Sensors)
            {
                GlassButton gbtn = new GlassButton();
                gbtn.Name = smodel.Name;
                gbtn.ButtonText = smodel.Title;
                gbtn.Font = new System.Drawing.Font("宋体", 11, System.Drawing.FontStyle.Regular);
                gbtn.Size = new System.Drawing.Size(160, 36);
                gbtn.Tag = smodel.Fields;
                gbtn.Click += Gbtn_Click;
                panelLeft.Controls.Add(gbtn);
            }
        }

        private static void Gbtn_Click(object sender, System.EventArgs e)
        {
            GlassButton gbtn = sender as GlassButton;
            //读XML配置文件,并将XML备份到User目录下
#warning 左侧按钮点击事件,秀出设备
        }
    }
}
