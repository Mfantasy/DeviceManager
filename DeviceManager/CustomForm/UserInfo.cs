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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            (this.ParentForm as Account).Del(User);
        }

        public AccountModel User { get; set; }
    }
}
