using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace DeviceManager.CustomForm
{
    public partial class Account : Form
    {
        const string save = "userInfo";
        public Account()
        {
            InitializeComponent();
            AccountRoot ar = Utils.FromXMLFile<AccountRoot>("");
            if (ar != null)
            { }
            else
            { }
        }

        public void Del(AccountModel user)
        {

        }
    }

    [XmlRoot("Root")]
    public class AccountRoot
    {
        [XmlElement("User")]
        public List<AccountModel> Users { get; set; }
    }

    public class AccountModel
    {
        [XmlAttribute("Level")]
        public int Level { get; set; }
        [XmlAttribute("UserName")]
        public string UserName { get; set; }
        [XmlAttribute("PassWord")]
        public string PassWord { get; set; }

    }
}
