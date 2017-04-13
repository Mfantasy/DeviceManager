namespace DeviceManager.CustomControl
{
    partial class PASListView
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
            this.c1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.c2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.c3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.c4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.c5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.c6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.c7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // c1
            // 
            this.c1.Text = "地点";
            this.c1.Width = 70;
            // 
            // c2
            // 
            this.c2.Text = "备注";
            this.c2.Width = 64;
            // 
            // c3
            // 
            this.c3.Text = "类型";
            // 
            // c4
            // 
            this.c4.Text = "预警配置";
            // 
            // c5
            // 
            this.c5.Text = "Uid";
            // 
            // c6
            // 
            this.c6.Text = "NodeId";
            this.c6.Width = 110;
            // 
            // c7
            // 
            this.c7.Text = "PortId";
            // 
            // PASListView
            // 
            this.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.c1,
            this.c2,
            this.c3,
            this.c4,
            this.c5,
            this.c6,
            this.c7});
            this.FullRowSelect = true;
            this.GridLines = true;
            this.Location = new System.Drawing.Point(105, 215);
            this.MultiSelect = false;
            this.Name = "listView1";
            this.Size = new System.Drawing.Size(543, 331);
            this.TabIndex = 6;
            this.UseCompatibleStateImageBehavior = false;
            this.View = System.Windows.Forms.View.Details;
            this.ResumeLayout(false);

        }
        private System.Windows.Forms.ColumnHeader c1;
        private System.Windows.Forms.ColumnHeader c2;
        private System.Windows.Forms.ColumnHeader c3;
        private System.Windows.Forms.ColumnHeader c4;
        private System.Windows.Forms.ColumnHeader c5;
        private System.Windows.Forms.ColumnHeader c6;
        private System.Windows.Forms.ColumnHeader c7;

        #endregion
    }
}
