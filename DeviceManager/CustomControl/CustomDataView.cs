using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DeviceManager.CustomControl
{
    public class CustomDataView : DataGridView
    {
        public CustomDataView()
        {                                  
            this.BorderStyle = BorderStyle.Fixed3D;
            this.CellBorderStyle = DataGridViewCellBorderStyle.SingleVertical;
            this.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            this.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.GridColor = Color.CadetBlue;
            this.BackgroundColor = Color.LightSteelBlue;
            this.RowHeadersVisible = false;
            this.ReadOnly = true;
            this.Dock = DockStyle.Fill;
            this.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.AllowUserToResizeColumns = false;
        }
    }
}
