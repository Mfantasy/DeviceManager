using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace DeviceManager
{
    public partial class Logon : Form
    {
        public Logon()
        {
            InitializeComponent();
            SetUser();           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int logonResult = DataAccess.Logon(textBox1.Text, textBox2.Text);
            if (logonResult == 1)
            {
                RecordUser();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }        
            else
            {
                MessageBox.Show("用户名或密码有误!");
            }
        }

        private void SetUser()
        {            
            if (File.Exists(fileName))
            {
                try
                {
                    string[] strs = File.ReadAllLines(fileName);
                    if (strs.Length > 2)
                    {
                        return;
                    }
                    if (strs.Length > 0)
                    {
                        string acc = Utils.Decode(strs[0]);
                        textBox1.Text = acc;
                    }
                    if (strs.Length > 1)
                    {
                        string pwd = Utils.Decode(strs[1]);
                        textBox2.Text = pwd;
                    }
                }
                catch { }
            }        
        }
        string fileName = "logonCache.txt";
        private void RecordUser()
        {
            try
            {
                string[] strs;
                string acc = Utils.Encode(textBox1.Text);
                if (checkBox2.Checked)
                {
                    string pwd = Utils.Encode(textBox2.Text);
                    strs = new string[]{ acc,pwd};
                }
                else
                {
                    strs = new string[] { acc };
                }
                File.WriteAllLines(fileName, strs);
            }
            catch { }   
        }

       
    }
}
