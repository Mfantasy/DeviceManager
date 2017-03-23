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
            this.button1 = new System.Windows.Forms.Button();
            this.panelTop = new System.Windows.Forms.Panel();
            this.labelTitle = new System.Windows.Forms.Label();
            this.panelTopBtn = new System.Windows.Forms.Panel();
            this.glassButton3 = new FOF.UserControlModel.GlassButton();
            this.glassButton1 = new FOF.UserControlModel.GlassButton();
            this.glassButton2 = new FOF.UserControlModel.GlassButton();
            this.glassButton4 = new FOF.UserControlModel.GlassButton();
            this.panelLeft = new System.Windows.Forms.FlowLayoutPanel();
            this.panelTop.SuspendLayout();
            this.panelTopBtn.SuspendLayout();
            this.panelLeft.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(921, 695);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panelTop
            // 
            this.panelTop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelTop.Controls.Add(this.panelTopBtn);
            this.panelTop.Controls.Add(this.labelTitle);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1008, 100);
            this.panelTop.TabIndex = 1;
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.BackColor = System.Drawing.Color.Transparent;
            this.labelTitle.Font = new System.Drawing.Font("宋体", 42F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelTitle.Location = new System.Drawing.Point(197, -1);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(248, 56);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "测试文字";
            // 
            // panelTopBtn
            // 
            this.panelTopBtn.BackColor = System.Drawing.Color.Transparent;
            this.panelTopBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelTopBtn.Controls.Add(this.glassButton3);
            this.panelTopBtn.Controls.Add(this.glassButton1);
            this.panelTopBtn.Controls.Add(this.glassButton2);
            this.panelTopBtn.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelTopBtn.Location = new System.Drawing.Point(0, 54);
            this.panelTopBtn.Name = "panelTopBtn";
            this.panelTopBtn.Padding = new System.Windows.Forms.Padding(3);
            this.panelTopBtn.Size = new System.Drawing.Size(1006, 44);
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
            // 
            // glassButton4
            // 
            this.glassButton4.BackColor = System.Drawing.Color.Transparent;
            this.glassButton4.ButtonText = "全部";
            this.glassButton4.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.glassButton4.Location = new System.Drawing.Point(6, 6);
            this.glassButton4.Name = "glassButton4";
            this.glassButton4.Size = new System.Drawing.Size(160, 47);
            this.glassButton4.TabIndex = 3;
            // 
            // panelLeft
            // 
            this.panelLeft.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelLeft.Controls.Add(this.glassButton4);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeft.Location = new System.Drawing.Point(0, 100);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Padding = new System.Windows.Forms.Padding(3);
            this.panelLeft.Size = new System.Drawing.Size(175, 630);
            this.panelLeft.TabIndex = 4;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1008, 730);
            this.Controls.Add(this.panelLeft);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.button1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MinimumSize = new System.Drawing.Size(1024, 768);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelTopBtn.ResumeLayout(false);
            this.panelLeft.ResumeLayout(false);
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
        private FOF.UserControlModel.GlassButton glassButton4;
        private System.Windows.Forms.FlowLayoutPanel panelLeft;
    }
}

