using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DeviceManager
{
    public partial class UC : UserControl
    {
        public UC()
        {
            InitializeComponent();
        }

        //当前Field
        public Field CurrentField { get; set; }
    }
}
