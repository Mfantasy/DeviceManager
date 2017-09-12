using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DeviceManager.CustomForm
{
    public partial class UserInfo : UserControl
    {        
        public UserInfo(AccountModel user)
        {
            InitializeComponent();
            tableLayoutPanel1.GetType().GetProperty("DoubleBuffered",
     System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(tableLayoutPanel1,
     true, null);
            this.User = user;
            comboBox1.SelectedIndex = User.Level - 1;
            textBox1.Text = User.UserName;
            textBox2.Text = User.PassWord;            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            (this.ParentForm as Account).Del(this);
        }        

        public AccountModel User { get; set; }

        public void Save()
        {
            User.Level = comboBox1.SelectedIndex + 1;
            User.UserName = textBox1.Text;
            User.PassWord = textBox2.Text;
        }

        public bool Judge()
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
      
    }
}
