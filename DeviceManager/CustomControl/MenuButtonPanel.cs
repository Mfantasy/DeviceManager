using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DeviceManager.CustomControl
{
    public static class AppContextRecord
    {
        private static MenuButtonPanel lastMenuItem = null;

        public static void ClickMenuButton(MenuButtonPanel sender)
        {
            if (lastMenuItem == null)
            {
                lastMenuItem = sender;
            }
            else if (lastMenuItem != sender)
            {
                lastMenuItem.ShowDefaultImage(null, null);
                lastMenuItem.IsDown = false;
                lastMenuItem = sender;
            }
        }
    }

    public class MenuButtonPanel : Panel
    {
        public MenuButtonPanel()
        {
            this.BackgroundImageLayout = ImageLayout.Stretch;
            //this.Dock = DockStyle.Fill;
            this.Margin = new Padding(10);
            this.CaptionLabel = new Label();
            this.CaptionLabel.AutoSize = true;
            this.CaptionLabel.BackColor = Color.Transparent;
            this.CaptionLabel.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Regular);
            this.CaptionLabel.ForeColor = Color.Black;
            this.Controls.Add(CaptionLabel);
            this.CaptionText = "Caption";
            this.SizeChanged += MenuButtonPanel_SizeChanged;
            this.MouseDown += MenuButtonPanel_MouseDown;
            this.MouseEnter += MenuButtonPanel_MouseEnter;
            this.MouseLeave += MenuButtonPanel_MouseLeave;
            CaptionLabel.MouseDown += MenuButtonPanel_MouseDown;
            CaptionLabel.MouseEnter += MenuButtonPanel_MouseEnter;
            CaptionLabel.MouseLeave += MenuButtonPanel_MouseLeave;
            this.DoubleBuffered = true;
            if (this.Parent != null)
            {
                this.BackColor = this.Parent.BackColor;
            }
        }

        private void MenuButtonPanel_MouseLeave(object sender, EventArgs e)
        {
            if (!IsDown)
            {
                ShowDefaultImage(null, null);
            }
        }

        private void MenuButtonPanel_MouseEnter(object sender, EventArgs e)
        {
            ShowCheckedImage(null, null);
        }

        public bool IsDown = false;
        private void MenuButtonPanel_MouseDown(object sender, MouseEventArgs e)
        {
            ShowCheckedImage(sender, e);
            AppContextRecord.ClickMenuButton(this);
            Control content = this.Panel;
            IsDown = true;
            if (content != null)
            {
                content.BringToFront();
            }
        }

        public void ShowCheckedImage(object sender, MouseEventArgs e)
        {
            if (CheckedImage != null)
            {
                this.BackgroundImage = CheckedImage;
            }
        }

        public void ShowDefaultImage(object sender, MouseEventArgs e)
        {
            if (DefaultImage != null)
            {
                this.BackgroundImage = DefaultImage;
            }
        }

        private void MenuButtonPanel_SizeChanged(object sender, EventArgs e)
        {
            this.CaptionLabel.Location = new Point((this.Width - CaptionLabel.Width) / 2, (this.Height - CaptionLabel.Height) - 10);
            if (this.Width > this.Height)
            {
                this.Width = this.Height;
            }
            else
            {
                this.Height = this.Width;
            }
        }

        [Browsable(true)]
        public Control Panel { get; set; }


        public Label CaptionLabel { get; set; }

        [Browsable(true)]
        public string CaptionText
        {
            get { return this.CaptionLabel.Text; }
            set { this.CaptionLabel.Text = value; }
        }
        [Browsable(true)]
        public Image CheckedImage
        {
            get; set;
        }
        [Browsable(true)]
        public Image DefaultImage
        {
            get;
            set;
        }
    }
}
