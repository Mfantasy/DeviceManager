using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DeviceManager
{
    public static class UIUtils
    {
        public static Label NewLabel(float size)
        {
            Label lb = new Label();
            lb.Margin = new Padding(3);
            lb.AutoSize = true;            
            lb.Font = new System.Drawing.Font("宋体", size);
            return lb;
        }
    }
}
