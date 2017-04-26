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
            this.夏季策略ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.启用ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.停用ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.冬季策略ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.启用ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.停用ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.添加一个配置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除一个配置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.保存配置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.夏季策略ToolStripMenuItem,
            this.冬季策略ToolStripMenuItem,
            this.添加一个配置ToolStripMenuItem,
            this.删除一个配置ToolStripMenuItem,
            this.保存配置ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(690, 25);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 夏季策略ToolStripMenuItem
            // 
            this.夏季策略ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.启用ToolStripMenuItem,
            this.停用ToolStripMenuItem});
            this.夏季策略ToolStripMenuItem.Name = "夏季策略ToolStripMenuItem";
            this.夏季策略ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.夏季策略ToolStripMenuItem.Text = "夏季策略";
            // 
            // 启用ToolStripMenuItem
            // 
            this.启用ToolStripMenuItem.Name = "启用ToolStripMenuItem";
            this.启用ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.启用ToolStripMenuItem.Text = "启用";
            // 
            // 停用ToolStripMenuItem
            // 
            this.停用ToolStripMenuItem.Name = "停用ToolStripMenuItem";
            this.停用ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.停用ToolStripMenuItem.Text = "停用";
            // 
            // 冬季策略ToolStripMenuItem
            // 
            this.冬季策略ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.启用ToolStripMenuItem1,
            this.停用ToolStripMenuItem1});
            this.冬季策略ToolStripMenuItem.Name = "冬季策略ToolStripMenuItem";
            this.冬季策略ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.冬季策略ToolStripMenuItem.Text = "冬季策略";
            // 
            // 启用ToolStripMenuItem1
            // 
            this.启用ToolStripMenuItem1.Name = "启用ToolStripMenuItem1";
            this.启用ToolStripMenuItem1.Size = new System.Drawing.Size(100, 22);
            this.启用ToolStripMenuItem1.Text = "启用";
            // 
            // 停用ToolStripMenuItem1
            // 
            this.停用ToolStripMenuItem1.Name = "停用ToolStripMenuItem1";
            this.停用ToolStripMenuItem1.Size = new System.Drawing.Size(100, 22);
            this.停用ToolStripMenuItem1.Text = "停用";
            // 
            // 添加一个配置ToolStripMenuItem
            // 
            this.添加一个配置ToolStripMenuItem.Name = "添加一个配置ToolStripMenuItem";
            this.添加一个配置ToolStripMenuItem.Size = new System.Drawing.Size(92, 21);
            this.添加一个配置ToolStripMenuItem.Text = "添加一个配置";
            this.添加一个配置ToolStripMenuItem.Click += new System.EventHandler(this.添加一个配置ToolStripMenuItem_Click);
            // 
            // 删除一个配置ToolStripMenuItem
            // 
            this.删除一个配置ToolStripMenuItem.Name = "删除一个配置ToolStripMenuItem";
            this.删除一个配置ToolStripMenuItem.Size = new System.Drawing.Size(92, 21);
            this.删除一个配置ToolStripMenuItem.Text = "删除一个配置";
            // 
            // 保存配置ToolStripMenuItem
            // 
            this.保存配置ToolStripMenuItem.Name = "保存配置ToolStripMenuItem";
            this.保存配置ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.保存配置ToolStripMenuItem.Text = "保存配置";
            this.保存配置ToolStripMenuItem.Click += new System.EventHandler(this.保存配置ToolStripMenuItem_Click);
            // 
            // PanelAlarmSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
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
        private System.Windows.Forms.ToolStripMenuItem 夏季策略ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 冬季策略ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 添加一个配置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 启用ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 停用ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 启用ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 停用ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 删除一个配置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 保存配置ToolStripMenuItem;
    }
}
