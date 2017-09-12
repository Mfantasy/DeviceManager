using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
        string file;
        AccountRoot ar;
        public Account()
        {
            InitializeComponent();
            file = Path.Combine(Utils.GetUserPath(), save);
            if (File.Exists(file))
            {
                 ar = Utils.FromXMLFile<AccountRoot>(file);
            }
            else
            {
                ar = new AccountRoot();
                ar.Users = new List<AccountModel>();
                groupBox1.Text = "暂无用户数据";
            }
            if (ar != null)
            {
                foreach (var item in ar.Users)
                {
                    UserInfo ui = new UserInfo(item);
                    ui.Parent = panel2;
                    ui.Dock = DockStyle.Top;
                }
            }
            else
            { }
        }

        public void Del(UserInfo user)
        {
            panel2.Controls.Remove(user);
            if (ar.Users.Contains(user.User))
            {
                ar.Users.Remove(user.User);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //添加
            groupBox1.Text = "用户列表";
            AccountModel am = new AccountModel();
            am.Level = 1;
            am.UserName = "";
            am.PassWord = "";
            UserInfo uinfo = new UserInfo(am);
            uinfo.Parent = panel2;
            uinfo.Dock = DockStyle.Top;
            uinfo.BringToFront();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (UserInfo item in panel2.Controls)
            {
                if (!item.Judge())
                {
                    MessageBox.Show("用户名或密码为空,请核对");
                    return;
                }
            }
                //保存
            foreach (UserInfo item in panel2.Controls)
            {
                item.Save();
                if (!ar.Users.Contains(item.User))
                {
                    ar.Users.Add(item.User);
                }
            }
            if (ar.Users.Count > 0)
            {
                Utils.ToFile<AccountRoot>(file, ar);
            }
            this.DialogResult = DialogResult.OK;
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
        //0  1   2 //默认应为1
        [XmlAttribute("Level")]
        public int Level { get; set; } 
        [XmlAttribute("UserName")]
        public string UserName { get; set; }
        [XmlAttribute("PassWord")]
        public string PassWord { get; set; }


    }
}
