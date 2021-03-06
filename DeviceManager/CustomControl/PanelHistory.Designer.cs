﻿namespace DeviceManager.CustomControl
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.glassButton2 = new FOF.UserControlModel.GlassButton();
            this.glassButton1 = new FOF.UserControlModel.GlassButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.customDataView1 = new DeviceManager.CustomControl.CustomDataView();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.customDataView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dateTimePickerRetrieveEnd
            // 
            this.dateTimePickerRetrieveEnd.CustomFormat = "yyyy年M月d日 HH:mm";
            this.dateTimePickerRetrieveEnd.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dateTimePickerRetrieveEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerRetrieveEnd.Location = new System.Drawing.Point(391, 6);
            this.dateTimePickerRetrieveEnd.MaxDate = new System.DateTime(2111, 11, 11, 0, 0, 0, 0);
            this.dateTimePickerRetrieveEnd.MinDate = new System.DateTime(2015, 9, 15, 0, 0, 0, 0);
            this.dateTimePickerRetrieveEnd.Name = "dateTimePickerRetrieveEnd";
            this.dateTimePickerRetrieveEnd.Size = new System.Drawing.Size(204, 29);
            this.dateTimePickerRetrieveEnd.TabIndex = 4;
            this.dateTimePickerRetrieveEnd.Value = new System.DateTime(2016, 6, 28, 0, 0, 0, 0);
            // 
            // dateTimePickerRetrieveBegin
            // 
            this.dateTimePickerRetrieveBegin.CustomFormat = "yyyy年M月d日 HH:mm";
            this.dateTimePickerRetrieveBegin.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dateTimePickerRetrieveBegin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerRetrieveBegin.Location = new System.Drawing.Point(78, 6);
            this.dateTimePickerRetrieveBegin.MaxDate = new System.DateTime(2111, 11, 11, 0, 0, 0, 0);
            this.dateTimePickerRetrieveBegin.MinDate = new System.DateTime(2015, 9, 15, 0, 0, 0, 0);
            this.dateTimePickerRetrieveBegin.Name = "dateTimePickerRetrieveBegin";
            this.dateTimePickerRetrieveBegin.Size = new System.Drawing.Size(204, 29);
            this.dateTimePickerRetrieveBegin.TabIndex = 3;
            this.dateTimePickerRetrieveBegin.Value = new System.DateTime(2016, 6, 28, 0, 0, 0, 0);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(3, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 21);
            this.label1.TabIndex = 6;
            this.label1.Text = "开始时间";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(314, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 21);
            this.label2.TabIndex = 7;
            this.label2.Text = "结束时间";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.glassButton2);
            this.panel1.Controls.Add(this.glassButton1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.dateTimePickerRetrieveBegin);
            this.panel1.Controls.Add(this.dateTimePickerRetrieveEnd);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(410, 44);
            this.panel1.TabIndex = 1;
            // 
            // glassButton2
            // 
            this.glassButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.glassButton2.BackColor = System.Drawing.Color.Transparent;
            this.glassButton2.BaseColor = System.Drawing.Color.Navy;
            this.glassButton2.ButtonColor = System.Drawing.Color.MidnightBlue;
            this.glassButton2.ButtonText = "开始查询";
            this.glassButton2.CornerRadius = 0;
            this.glassButton2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.glassButton2.HighlightColor = System.Drawing.Color.SeaShell;
            this.glassButton2.Location = new System.Drawing.Point(153, 5);
            this.glassButton2.Margin = new System.Windows.Forms.Padding(4);
            this.glassButton2.Name = "glassButton2";
            this.glassButton2.Size = new System.Drawing.Size(116, 35);
            this.glassButton2.TabIndex = 11;
            this.glassButton2.Click += new System.EventHandler(this.glassButton2_Click);
            // 
            // glassButton1
            // 
            this.glassButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.glassButton1.BackColor = System.Drawing.Color.Transparent;
            this.glassButton1.BaseColor = System.Drawing.Color.Navy;
            this.glassButton1.ButtonColor = System.Drawing.Color.MidnightBlue;
            this.glassButton1.ButtonText = "导出数据";
            this.glassButton1.CornerRadius = 0;
            this.glassButton1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.glassButton1.HighlightColor = System.Drawing.Color.SeaShell;
            this.glassButton1.Location = new System.Drawing.Point(286, 5);
            this.glassButton1.Margin = new System.Windows.Forms.Padding(4);
            this.glassButton1.Name = "glassButton1";
            this.glassButton1.Size = new System.Drawing.Size(116, 35);
            this.glassButton1.TabIndex = 9;
            this.glassButton1.Click += new System.EventHandler(this.glassButton1_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(410, 240);
            this.tableLayoutPanel1.TabIndex = 12;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Location = new System.Drawing.Point(3, 47);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.customDataView1);
            this.splitContainer1.Size = new System.Drawing.Size(404, 190);
            this.splitContainer1.SplitterDistance = 51;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 10;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.ShowLines = false;
            this.treeView1.ShowNodeToolTips = true;
            this.treeView1.ShowRootLines = false;
            this.treeView1.Size = new System.Drawing.Size(47, 186);
            this.treeView1.TabIndex = 0;
            // 
            // customDataView1
            // 
            this.customDataView1.AllowUserToResizeColumns = false;
            this.customDataView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.customDataView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.customDataView1.BackgroundColor = System.Drawing.Color.White;
            this.customDataView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.customDataView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.customDataView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.customDataView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.customDataView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
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
            this.customDataView1.Size = new System.Drawing.Size(348, 186);
            this.customDataView1.TabIndex = 8;
            // 
            // PanelHistory
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "PanelHistory";
            this.Size = new System.Drawing.Size(410, 240);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.customDataView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateTimePickerRetrieveEnd;
        private System.Windows.Forms.DateTimePicker dateTimePickerRetrieveBegin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private FOF.UserControlModel.GlassButton glassButton1;
        private FOF.UserControlModel.GlassButton glassButton2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeView1;
        private CustomDataView customDataView1;
    }
}
