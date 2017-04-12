namespace DeviceManager.CustomControl
{
    partial class PanelHistory
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dateTimePickerRetrieveEnd = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerRetrieveBegin = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.customDataView1 = new DeviceManager.CustomControl.CustomDataView();
            this.glassButton1 = new FOF.UserControlModel.GlassButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.glassButton2 = new FOF.UserControlModel.GlassButton();
            ((System.ComponentModel.ISupportInitialize)(this.customDataView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dateTimePickerRetrieveEnd
            // 
            this.dateTimePickerRetrieveEnd.CustomFormat = "yyyy年M月d日 HH:mm";
            this.dateTimePickerRetrieveEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerRetrieveEnd.Location = new System.Drawing.Point(217, 19);
            this.dateTimePickerRetrieveEnd.MaxDate = new System.DateTime(2111, 11, 11, 0, 0, 0, 0);
            this.dateTimePickerRetrieveEnd.MinDate = new System.DateTime(2015, 9, 15, 0, 0, 0, 0);
            this.dateTimePickerRetrieveEnd.Name = "dateTimePickerRetrieveEnd";
            this.dateTimePickerRetrieveEnd.Size = new System.Drawing.Size(161, 21);
            this.dateTimePickerRetrieveEnd.TabIndex = 4;
            this.dateTimePickerRetrieveEnd.Value = new System.DateTime(2016, 6, 28, 0, 0, 0, 0);
            // 
            // dateTimePickerRetrieveBegin
            // 
            this.dateTimePickerRetrieveBegin.CustomFormat = "yyyy年M月d日 HH:mm";
            this.dateTimePickerRetrieveBegin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerRetrieveBegin.Location = new System.Drawing.Point(8, 19);
            this.dateTimePickerRetrieveBegin.MaxDate = new System.DateTime(2111, 11, 11, 0, 0, 0, 0);
            this.dateTimePickerRetrieveBegin.MinDate = new System.DateTime(2015, 9, 15, 0, 0, 0, 0);
            this.dateTimePickerRetrieveBegin.Name = "dateTimePickerRetrieveBegin";
            this.dateTimePickerRetrieveBegin.Size = new System.Drawing.Size(161, 21);
            this.dateTimePickerRetrieveBegin.TabIndex = 3;
            this.dateTimePickerRetrieveBegin.Value = new System.DateTime(2016, 6, 28, 0, 0, 0, 0);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "开始时间";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(215, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "结束时间";
            // 
            // customDataView1
            // 
            this.customDataView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.customDataView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.customDataView1.BackgroundColor = System.Drawing.Color.White;
            this.customDataView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.customDataView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.customDataView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.customDataView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.customDataView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.customDataView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.customDataView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.customDataView1.GridColor = System.Drawing.Color.SkyBlue;
            this.customDataView1.Location = new System.Drawing.Point(0, 0);
            this.customDataView1.Name = "customDataView1";
            this.customDataView1.ReadOnly = true;
            this.customDataView1.RowHeadersVisible = false;
            this.customDataView1.RowTemplate.Height = 23;
            this.customDataView1.Size = new System.Drawing.Size(596, 518);
            this.customDataView1.TabIndex = 8;
            // 
            // glassButton1
            // 
            this.glassButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.glassButton1.BackColor = System.Drawing.Color.Transparent;
            this.glassButton1.BaseColor = System.Drawing.Color.LightSalmon;
            this.glassButton1.ButtonColor = System.Drawing.Color.DimGray;
            this.glassButton1.ButtonText = "导出数据";
            this.glassButton1.CornerRadius = 0;
            this.glassButton1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.glassButton1.ForeColor = System.Drawing.Color.Black;
            this.glassButton1.HighlightColor = System.Drawing.Color.SeaShell;
            this.glassButton1.Location = new System.Drawing.Point(663, 5);
            this.glassButton1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.glassButton1.Name = "glassButton1";
            this.glassButton1.Size = new System.Drawing.Size(116, 35);
            this.glassButton1.TabIndex = 9;
            this.glassButton1.Click += new System.EventHandler(this.glassButton1_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(0, 47);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.customDataView1);
            this.splitContainer1.Size = new System.Drawing.Size(785, 518);
            this.splitContainer1.SplitterDistance = 185;
            this.splitContainer1.TabIndex = 10;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.ShowLines = false;
            this.treeView1.ShowNodeToolTips = true;
            this.treeView1.ShowRootLines = false;
            this.treeView1.Size = new System.Drawing.Size(185, 518);
            this.treeView1.TabIndex = 0;
            // 
            // glassButton2
            // 
            this.glassButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.glassButton2.BackColor = System.Drawing.Color.Transparent;
            this.glassButton2.BaseColor = System.Drawing.Color.LightSalmon;
            this.glassButton2.ButtonColor = System.Drawing.Color.DimGray;
            this.glassButton2.ButtonText = "开始查询";
            this.glassButton2.CornerRadius = 0;
            this.glassButton2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.glassButton2.ForeColor = System.Drawing.Color.Black;
            this.glassButton2.HighlightColor = System.Drawing.Color.SeaShell;
            this.glassButton2.Location = new System.Drawing.Point(531, 4);
            this.glassButton2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.glassButton2.Name = "glassButton2";
            this.glassButton2.Size = new System.Drawing.Size(116, 35);
            this.glassButton2.TabIndex = 11;
            this.glassButton2.Click += new System.EventHandler(this.glassButton2_Click);
            // 
            // PanelHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.glassButton2);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.glassButton1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateTimePickerRetrieveEnd);
            this.Controls.Add(this.dateTimePickerRetrieveBegin);
            this.Name = "PanelHistory";
            this.Size = new System.Drawing.Size(785, 565);
            ((System.ComponentModel.ISupportInitialize)(this.customDataView1)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateTimePickerRetrieveEnd;
        private System.Windows.Forms.DateTimePicker dateTimePickerRetrieveBegin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private CustomDataView customDataView1;
        private FOF.UserControlModel.GlassButton glassButton1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeView1;
        private FOF.UserControlModel.GlassButton glassButton2;
    }
}
