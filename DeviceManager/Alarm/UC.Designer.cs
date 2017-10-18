namespace DeviceManager
{
    partial class UC
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripComboBox1 = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripComboBox2 = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripComboBox3 = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripComboBox4 = new System.Windows.Forms.ToolStripComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.userCalendar1 = new DeviceManager.Alarm.UserCalendar();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripComboBox1,
            this.toolStripComboBox2,
            this.toolStripComboBox3,
            this.toolStripComboBox4});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(0);
            this.menuStrip1.Size = new System.Drawing.Size(785, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripComboBox1
            // 
            this.toolStripComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBox1.Margin = new System.Windows.Forms.Padding(0);
            this.toolStripComboBox1.Name = "toolStripComboBox1";
            this.toolStripComboBox1.Size = new System.Drawing.Size(128, 25);
            // 
            // toolStripComboBox2
            // 
            this.toolStripComboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBox2.Name = "toolStripComboBox2";
            this.toolStripComboBox2.Size = new System.Drawing.Size(128, 25);
            // 
            // toolStripComboBox3
            // 
            this.toolStripComboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBox3.Name = "toolStripComboBox3";
            this.toolStripComboBox3.Size = new System.Drawing.Size(128, 25);
            // 
            // toolStripComboBox4
            // 
            this.toolStripComboBox4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBox4.Name = "toolStripComboBox4";
            this.toolStripComboBox4.Size = new System.Drawing.Size(138, 25);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.userCalendar1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(785, 494);
            this.panel1.TabIndex = 1;
            // 
            // userCalendar1
            // 
            this.userCalendar1.CurrentSensor = null;
            this.userCalendar1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userCalendar1.Location = new System.Drawing.Point(0, 0);
            this.userCalendar1.Name = "userCalendar1";
            this.userCalendar1.Size = new System.Drawing.Size(785, 494);
            this.userCalendar1.TabIndex = 0;
            // 
            // UC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.Name = "UC";
            this.Size = new System.Drawing.Size(785, 519);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox1;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox2;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox3;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox4;
        private System.Windows.Forms.Panel panel1;
        public Alarm.UserCalendar userCalendar1;
    }
}
