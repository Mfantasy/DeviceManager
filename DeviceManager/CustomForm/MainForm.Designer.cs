namespace DeviceManager
{
    partial class MainForm
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.button1 = new System.Windows.Forms.Button();
            this.panelTop = new System.Windows.Forms.Panel();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.panelRuntime = new System.Windows.Forms.Panel();
            this.panelLeft = new System.Windows.Forms.FlowLayoutPanel();
            this.panelRight = new System.Windows.Forms.Panel();
            this.panelBotttom = new System.Windows.Forms.Panel();
            this.glassButtonAll = new FOF.UserControlModel.GlassButton();
            this.menuButtonPanel5 = new DeviceManager.CustomControl.MenuButtonPanel();
            this.menuButtonPanel4 = new DeviceManager.CustomControl.MenuButtonPanel();
            this.menuButtonPanel3 = new DeviceManager.CustomControl.MenuButtonPanel();
            this.menuButtonPanel2 = new DeviceManager.CustomControl.MenuButtonPanel();
            this.menuButtonPanel1 = new DeviceManager.CustomControl.MenuButtonPanel();
            this.button4 = new System.Windows.Forms.Button();
            this.panelTop.SuspendLayout();
            this.panelRuntime.SuspendLayout();
            this.panelLeft.SuspendLayout();
            this.panelBotttom.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(707, 55);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "测试";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.Transparent;
            this.panelTop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelTop.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelTop.Controls.Add(this.button4);
            this.panelTop.Controls.Add(this.button3);
            this.panelTop.Controls.Add(this.button2);
            this.panelTop.Controls.Add(this.menuButtonPanel5);
            this.panelTop.Controls.Add(this.menuButtonPanel4);
            this.panelTop.Controls.Add(this.menuButtonPanel3);
            this.panelTop.Controls.Add(this.menuButtonPanel2);
            this.panelTop.Controls.Add(this.menuButtonPanel1);
            this.panelTop.Controls.Add(this.button1);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Margin = new System.Windows.Forms.Padding(0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1024, 125);
            this.panelTop.TabIndex = 1;
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button3.BackgroundImage")));
            this.button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.button3.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Location = new System.Drawing.Point(888, 11);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(60, 60);
            this.button3.TabIndex = 14;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button2.BackgroundImage")));
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Location = new System.Drawing.Point(954, 10);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(56, 56);
            this.button2.TabIndex = 13;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // panelRuntime
            // 
            this.panelRuntime.BackColor = System.Drawing.Color.Transparent;
            this.panelRuntime.Controls.Add(this.panelLeft);
            this.panelRuntime.Controls.Add(this.panelRight);
            this.panelRuntime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRuntime.Location = new System.Drawing.Point(0, 0);
            this.panelRuntime.Name = "panelRuntime";
            this.panelRuntime.Size = new System.Drawing.Size(1020, 641);
            this.panelRuntime.TabIndex = 6;
            // 
            // panelLeft
            // 
            this.panelLeft.BackColor = System.Drawing.Color.Transparent;
            this.panelLeft.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelLeft.Controls.Add(this.glassButtonAll);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeft.Location = new System.Drawing.Point(0, 0);
            this.panelLeft.Margin = new System.Windows.Forms.Padding(0);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Padding = new System.Windows.Forms.Padding(3);
            this.panelLeft.Size = new System.Drawing.Size(181, 641);
            this.panelLeft.TabIndex = 4;
            // 
            // panelRight
            // 
            this.panelRight.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelRight.BackColor = System.Drawing.Color.Transparent;
            this.panelRight.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelRight.Location = new System.Drawing.Point(181, 0);
            this.panelRight.Margin = new System.Windows.Forms.Padding(0);
            this.panelRight.Name = "panelRight";
            this.panelRight.Padding = new System.Windows.Forms.Padding(1);
            this.panelRight.Size = new System.Drawing.Size(839, 641);
            this.panelRight.TabIndex = 5;
            // 
            // panelBotttom
            // 
            this.panelBotttom.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelBotttom.BackColor = System.Drawing.Color.Transparent;
            this.panelBotttom.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelBotttom.Controls.Add(this.panelRuntime);
            this.panelBotttom.Location = new System.Drawing.Point(0, 124);
            this.panelBotttom.Margin = new System.Windows.Forms.Padding(0);
            this.panelBotttom.Name = "panelBotttom";
            this.panelBotttom.Size = new System.Drawing.Size(1024, 645);
            this.panelBotttom.TabIndex = 7;
            // 
            // glassButtonAll
            // 
            this.glassButtonAll.BackColor = System.Drawing.Color.Transparent;
            this.glassButtonAll.ButtonText = "全部";
            this.glassButtonAll.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.glassButtonAll.Location = new System.Drawing.Point(8, 8);
            this.glassButtonAll.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.glassButtonAll.Name = "glassButtonAll";
            this.glassButtonAll.Size = new System.Drawing.Size(160, 47);
            this.glassButtonAll.TabIndex = 3;
            this.glassButtonAll.Click += new System.EventHandler(this.glassButtonAll_Click);
            // 
            // menuButtonPanel5
            // 
            this.menuButtonPanel5.BackColor = System.Drawing.Color.Transparent;
            this.menuButtonPanel5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.menuButtonPanel5.CaptionText = "设备管理";
            this.menuButtonPanel5.CheckedImage = global::DeviceManager.Properties.Resources.设备管理_副本;
            this.menuButtonPanel5.DefaultImage = global::DeviceManager.Properties.Resources.设备管理;
            this.menuButtonPanel5.Location = new System.Drawing.Point(534, 11);
            this.menuButtonPanel5.Margin = new System.Windows.Forms.Padding(10);
            this.menuButtonPanel5.Name = "menuButtonPanel5";
            this.menuButtonPanel5.Panel = null;
            this.menuButtonPanel5.Size = new System.Drawing.Size(101, 101);
            this.menuButtonPanel5.TabIndex = 12;
            // 
            // menuButtonPanel4
            // 
            this.menuButtonPanel4.BackColor = System.Drawing.Color.Transparent;
            this.menuButtonPanel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.menuButtonPanel4.CaptionText = "预警配置";
            this.menuButtonPanel4.CheckedImage = global::DeviceManager.Properties.Resources.预警设置_副本;
            this.menuButtonPanel4.DefaultImage = global::DeviceManager.Properties.Resources.预警设置;
            this.menuButtonPanel4.Location = new System.Drawing.Point(405, 11);
            this.menuButtonPanel4.Margin = new System.Windows.Forms.Padding(10);
            this.menuButtonPanel4.Name = "menuButtonPanel4";
            this.menuButtonPanel4.Panel = null;
            this.menuButtonPanel4.Size = new System.Drawing.Size(101, 101);
            this.menuButtonPanel4.TabIndex = 11;
            // 
            // menuButtonPanel3
            // 
            this.menuButtonPanel3.BackColor = System.Drawing.Color.Transparent;
            this.menuButtonPanel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.menuButtonPanel3.CaptionText = "预警记录";
            this.menuButtonPanel3.CheckedImage = global::DeviceManager.Properties.Resources.预警记录_副本;
            this.menuButtonPanel3.DefaultImage = global::DeviceManager.Properties.Resources.预警记录;
            this.menuButtonPanel3.Location = new System.Drawing.Point(276, 11);
            this.menuButtonPanel3.Margin = new System.Windows.Forms.Padding(10);
            this.menuButtonPanel3.Name = "menuButtonPanel3";
            this.menuButtonPanel3.Panel = null;
            this.menuButtonPanel3.Size = new System.Drawing.Size(101, 101);
            this.menuButtonPanel3.TabIndex = 10;
            // 
            // menuButtonPanel2
            // 
            this.menuButtonPanel2.BackColor = System.Drawing.Color.Transparent;
            this.menuButtonPanel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.menuButtonPanel2.CaptionText = "数据查询";
            this.menuButtonPanel2.CheckedImage = global::DeviceManager.Properties.Resources.历史数据_副本;
            this.menuButtonPanel2.DefaultImage = global::DeviceManager.Properties.Resources.历史数据;
            this.menuButtonPanel2.Location = new System.Drawing.Point(147, 11);
            this.menuButtonPanel2.Margin = new System.Windows.Forms.Padding(10);
            this.menuButtonPanel2.Name = "menuButtonPanel2";
            this.menuButtonPanel2.Panel = null;
            this.menuButtonPanel2.Size = new System.Drawing.Size(101, 101);
            this.menuButtonPanel2.TabIndex = 9;
            // 
            // menuButtonPanel1
            // 
            this.menuButtonPanel1.BackColor = System.Drawing.Color.Transparent;
            this.menuButtonPanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.menuButtonPanel1.CaptionText = "实时数据";
            this.menuButtonPanel1.CheckedImage = global::DeviceManager.Properties.Resources.实时数据1;
            this.menuButtonPanel1.DefaultImage = global::DeviceManager.Properties.Resources.实时数据;
            this.menuButtonPanel1.Location = new System.Drawing.Point(18, 11);
            this.menuButtonPanel1.Margin = new System.Windows.Forms.Padding(10);
            this.menuButtonPanel1.Name = "menuButtonPanel1";
            this.menuButtonPanel1.Panel = this.panelRuntime;
            this.menuButtonPanel1.Size = new System.Drawing.Size(101, 101);
            this.menuButtonPanel1.TabIndex = 8;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(707, 30);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 15;
            this.button4.Text = "连接";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.HotTrack;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.panelBotttom);
            this.Controls.Add(this.panelTop);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1024, 768);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "山西省榆社县博物馆环境监测系统";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panelTop.ResumeLayout(false);
            this.panelRuntime.ResumeLayout(false);
            this.panelLeft.ResumeLayout(false);
            this.panelBotttom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panelTop;
        private FOF.UserControlModel.GlassButton glassButtonAll;
        private System.Windows.Forms.FlowLayoutPanel panelLeft;
        private System.Windows.Forms.Panel panelRight;
        private System.Windows.Forms.Panel panelRuntime;
        private System.Windows.Forms.Panel panelBotttom;
        private CustomControl.MenuButtonPanel menuButtonPanel1;
        private CustomControl.MenuButtonPanel menuButtonPanel5;
        private CustomControl.MenuButtonPanel menuButtonPanel4;
        private CustomControl.MenuButtonPanel menuButtonPanel3;
        private CustomControl.MenuButtonPanel menuButtonPanel2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
    }
}

