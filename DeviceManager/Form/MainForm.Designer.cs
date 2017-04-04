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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.panelTopBtn = new System.Windows.Forms.Panel();
            this.glassButton3 = new FOF.UserControlModel.GlassButton();
            this.glassButton1 = new FOF.UserControlModel.GlassButton();
            this.glassButton2 = new FOF.UserControlModel.GlassButton();
            this.labelTitle = new System.Windows.Forms.Label();
            this.panelLeft = new System.Windows.Forms.FlowLayoutPanel();
            this.glassButtonAll = new FOF.UserControlModel.GlassButton();
            this.panelRight = new System.Windows.Forms.Panel();
            this.panelRuntime = new System.Windows.Forms.Panel();
            this.panelBotttom = new System.Windows.Forms.Panel();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panelTopBtn.SuspendLayout();
            this.panelLeft.SuspendLayout();
            this.panelRuntime.SuspendLayout();
            this.panelBotttom.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(369, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "开始";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panelTop
            // 
            this.panelTop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelTop.Controls.Add(this.pictureBox1);
            this.panelTop.Controls.Add(this.pictureBox2);
            this.panelTop.Controls.Add(this.panelTopBtn);
            this.panelTop.Controls.Add(this.labelTitle);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1024, 100);
            this.panelTop.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(932, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(42, 42);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(980, 0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(42, 42);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 6;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Visible = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // panelTopBtn
            // 
            this.panelTopBtn.BackColor = System.Drawing.Color.Transparent;
            this.panelTopBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelTopBtn.Controls.Add(this.glassButton3);
            this.panelTopBtn.Controls.Add(this.button1);
            this.panelTopBtn.Controls.Add(this.glassButton1);
            this.panelTopBtn.Controls.Add(this.glassButton2);
            this.panelTopBtn.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelTopBtn.Location = new System.Drawing.Point(0, 54);
            this.panelTopBtn.Name = "panelTopBtn";
            this.panelTopBtn.Padding = new System.Windows.Forms.Padding(3);
            this.panelTopBtn.Size = new System.Drawing.Size(1022, 44);
            this.panelTopBtn.TabIndex = 4;
            // 
            // glassButton3
            // 
            this.glassButton3.BackColor = System.Drawing.Color.Transparent;
            this.glassButton3.ButtonText = null;
            this.glassButton3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.glassButton3.Location = new System.Drawing.Point(241, 4);
            this.glassButton3.Name = "glassButton3";
            this.glassButton3.Padding = new System.Windows.Forms.Padding(5);
            this.glassButton3.Size = new System.Drawing.Size(112, 36);
            this.glassButton3.TabIndex = 3;
            // 
            // glassButton1
            // 
            this.glassButton1.BackColor = System.Drawing.Color.Transparent;
            this.glassButton1.ButtonText = "测试文字";
            this.glassButton1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.glassButton1.Location = new System.Drawing.Point(5, 4);
            this.glassButton1.Name = "glassButton1";
            this.glassButton1.Size = new System.Drawing.Size(112, 36);
            this.glassButton1.TabIndex = 1;
            this.glassButton1.Click += new System.EventHandler(this.glassButton1_Click);
            // 
            // glassButton2
            // 
            this.glassButton2.BackColor = System.Drawing.Color.Transparent;
            this.glassButton2.ButtonText = null;
            this.glassButton2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.glassButton2.Location = new System.Drawing.Point(123, 4);
            this.glassButton2.Name = "glassButton2";
            this.glassButton2.Padding = new System.Windows.Forms.Padding(5);
            this.glassButton2.Size = new System.Drawing.Size(112, 36);
            this.glassButton2.TabIndex = 2;
            this.glassButton2.Click += new System.EventHandler(this.glassButton2_Click);
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.BackColor = System.Drawing.Color.Transparent;
            this.labelTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelTitle.Font = new System.Drawing.Font("宋体", 42F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelTitle.Location = new System.Drawing.Point(0, 0);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(248, 56);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "测试文字";
            // 
            // panelLeft
            // 
            this.panelLeft.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelLeft.Controls.Add(this.glassButtonAll);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeft.Location = new System.Drawing.Point(0, 0);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Padding = new System.Windows.Forms.Padding(3);
            this.panelLeft.Size = new System.Drawing.Size(179, 668);
            this.panelLeft.TabIndex = 4;
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
            this.glassButtonAll.Click += new System.EventHandler(this.glassButton4_Click);
            // 
            // panelRight
            // 
            this.panelRight.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelRight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelRight.Location = new System.Drawing.Point(185, 0);
            this.panelRight.Name = "panelRight";
            this.panelRight.Size = new System.Drawing.Size(839, 668);
            this.panelRight.TabIndex = 5;
            // 
            // panelRuntime
            // 
            this.panelRuntime.Controls.Add(this.panelLeft);
            this.panelRuntime.Controls.Add(this.panelRight);
            this.panelRuntime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRuntime.Location = new System.Drawing.Point(0, 0);
            this.panelRuntime.Name = "panelRuntime";
            this.panelRuntime.Size = new System.Drawing.Size(1024, 668);
            this.panelRuntime.TabIndex = 6;
            // 
            // panelBotttom
            // 
            this.panelBotttom.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelBotttom.Controls.Add(this.panelRuntime);
            this.panelBotttom.Location = new System.Drawing.Point(0, 101);
            this.panelBotttom.Name = "panelBotttom";
            this.panelBotttom.Size = new System.Drawing.Size(1024, 668);
            this.panelBotttom.TabIndex = 7;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.panelBotttom);
            this.Controls.Add(this.panelTop);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MinimumSize = new System.Drawing.Size(1024, 768);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "环境监测系统";
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.panelTopBtn.ResumeLayout(false);
            this.panelLeft.ResumeLayout(false);
            this.panelRuntime.ResumeLayout(false);
            this.panelBotttom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label labelTitle;
        private FOF.UserControlModel.GlassButton glassButton1;
        private FOF.UserControlModel.GlassButton glassButton2;
        private FOF.UserControlModel.GlassButton glassButton3;
        private System.Windows.Forms.Panel panelTopBtn;
        private FOF.UserControlModel.GlassButton glassButtonAll;
        private System.Windows.Forms.FlowLayoutPanel panelLeft;
        private System.Windows.Forms.Panel panelRight;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Panel panelRuntime;
        private System.Windows.Forms.Panel panelBotttom;
    }
}

