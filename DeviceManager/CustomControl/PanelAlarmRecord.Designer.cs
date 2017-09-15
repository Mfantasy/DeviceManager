namespace DeviceManager.CustomControl
{
    partial class PanelAlarmRecord
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
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.全部预警ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.最近三天ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.全部预警ToolStripMenuItem,
            this.最近三天ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(125, 48);
            // 
            // 全部预警ToolStripMenuItem
            // 
            this.全部预警ToolStripMenuItem.Name = "全部预警ToolStripMenuItem";
            this.全部预警ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.全部预警ToolStripMenuItem.Text = "全部预警";
            this.全部预警ToolStripMenuItem.Click += new System.EventHandler(this.全部预警ToolStripMenuItem_Click);
            // 
            // 最近三天ToolStripMenuItem
            // 
            this.最近三天ToolStripMenuItem.Name = "最近三天ToolStripMenuItem";
            this.最近三天ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.最近三天ToolStripMenuItem.Text = "最近三天";
            this.最近三天ToolStripMenuItem.Click += new System.EventHandler(this.最近三天ToolStripMenuItem_Click);
            // 
            // PanelAlarmRecord
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Name = "PanelAlarmRecord";
            this.Size = new System.Drawing.Size(675, 416);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 全部预警ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 最近三天ToolStripMenuItem;
    }
}
