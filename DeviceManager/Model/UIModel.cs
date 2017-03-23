using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DeviceManager.Model
{
    [XmlRoot("UI")]
    public class UIModel
    {   
        [XmlElement("element")]
        public List<Element> Elements { get; set; }
    }

    public class Element
    {
        [XmlAttribute("name")]
        public string Name { get; set; }
        [XmlElement("property")]
        public List<Property> Properties { get; set; }
        [XmlElement("child")]
        public List<Child> Children { get; set; }
        [XmlElement("element")]
        public List<Element> Elements { get; set; }
    }

    public class Child
    {
        [XmlAttribute("name")]
        public string Name { get; set; }
        [XmlElement("property")]
        public List<Property> Properties { get; set; }
    }

    public class Property
    {
        [XmlAttribute("name")]
        public string Name { get; set; }
        [XmlAttribute("value")]
        public string StrValue { get; set; }

        public object Value
        {
            get
            {
                if (Name.Contains("Image"))
                {
                    return Image.FromFile(StrValue);
                }
                else if(Name.Contains("Color"))
                {
                    return Color.FromName(StrValue);
                }
                return StrValue;
            }
        }
    }

}
