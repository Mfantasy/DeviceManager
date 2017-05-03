namespace DeviceManager.CustomControl
{
    partial class PanelAlarmSet
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
            this.添加一个配置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripTextBox2 = new System.Windows.Forms.ToolStripTextBox();
            this.确定ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.删除一个配置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripComboBox1 = new System.Windows.Forms.ToolStripComboBox();
            this.确定ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.保存配置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.添加一个配置ToolStripMenuItem,
            this.删除一个配置ToolStripMenuItem,
            this.保存配置ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(690, 25);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 添加一个配置ToolStripMenuItem
            // 
            this.添加一个配置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripTextBox2,
            this.确定ToolStripMenuItem1});
            this.添加一个配置ToolStripMenuItem.Name = "添加一个配置ToolStripMenuItem";
            this.添加一个配置ToolStripMenuItem.Size = new System.Drawing.Size(92, 21);
            this.添加一个配置ToolStripMenuItem.Text = "添加一个配置";
            // 
            // toolStripTextBox2
            // 
            this.toolStripTextBox2.Name = "toolStripTextBox2";
            this.toolStripTextBox2.Size = new System.Drawing.Size(100, 23);
            // 
            // 确定ToolStripMenuItem1
            // 
            this.确定ToolStripMenuItem1.Name = "确定ToolStripMenuItem1";
            this.确定ToolStripMenuItem1.Size = new System.Drawing.Size(160, 22);
            this.确定ToolStripMenuItem1.Text = "确定";
            this.确定ToolStripMenuItem1.Click += new System.EventHandler(this.确定ToolStripMenuItem1_Click);
            // 
            // 删除一个配置ToolStripMenuItem
            // 
            this.删除一个配置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripComboBox1,
            this.确定ToolStripMenuItem});
            this.删除一个配置ToolStripMenuItem.Name = "删除一个配置ToolStripMenuItem";
            this.删除一个配置ToolStripMenuItem.Size = new System.Drawing.Size(92, 21);
            this.删除一个配置ToolStripMenuItem.Text = "删除一个配置";
            // 
            // toolStripComboBox1
            // 
            this.toolStripComboBox1.Name = "toolStripComboBox1";
            this.toolStripComboBox1.Size = new System.Drawing.Size(121, 25);
            this.toolStripComboBox1.Click += new System.EventHandler(this.toolStripComboBox1_Click);
            // 
            // 确定ToolStripMenuItem
            // 
            this.确定ToolStripMenuItem.Name = "确定ToolStripMenuItem";
            this.确定ToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.确定ToolStripMenuItem.Text = "确定";
            this.确定ToolStripMenuItem.Click += new System.EventHandler(this.确定ToolStripMenuItem_Click);
            // 
            // 保存配置ToolStripMenuItem
            // 
            this.保存配置ToolStripMenuItem.Name = "保存配置ToolStripMenuItem";
            this.保存配置ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.保存配置ToolStripMenuItem.Text = "保存配置";
            this.保存配置ToolStripMenuItem.Click += new System.EventHandler(this.保存配置ToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(690, 587);
            this.panel1.TabIndex = 2;
            // 
            // PanelAlarmSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.Name = "PanelAlarmSet";
            this.Size = new System.Drawing.Size(690, 612);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 添加一个配置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除一个配置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 保存配置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 确定ToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox2;
        private System.Windows.Forms.ToolStripMenuItem 确定ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox1;
        private System.Windows.Forms.Panel panel1;
    }
}
