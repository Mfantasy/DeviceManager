namespace DeviceManager.CustomForm
{
    partial class DateTimePickerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePickerRetrieveBegin = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerRetrieveEnd = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(23, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "开始日期";
            // 
            // dateTimePickerRetrieveBegin
            // 
            this.dateTimePickerRetrieveBegin.CustomFormat = "yyyy年M月d日 HH:mm";
            this.dateTimePickerRetrieveBegin.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dateTimePickerRetrieveBegin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerRetrieveBegin.Location = new System.Drawing.Point(143, 13);
            this.dateTimePickerRetrieveBegin.Name = "dateTimePickerRetrieveBegin";
            this.dateTimePickerRetrieveBegin.Size = new System.Drawing.Size(286, 33);
            this.dateTimePickerRetrieveBegin.TabIndex = 1;
            // 
            // dateTimePickerRetrieveEnd
            // 
            this.dateTimePickerRetrieveEnd.CustomFormat = "yyyy年M月d日 HH:mm";
            this.dateTimePickerRetrieveEnd.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dateTimePickerRetrieveEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerRetrieveEnd.Location = new System.Drawing.Point(143, 71);
            this.dateTimePickerRetrieveEnd.Name = "dateTimePickerRetrieveEnd";
            this.dateTimePickerRetrieveEnd.Size = new System.Drawing.Size(286, 33);
            this.dateTimePickerRetrieveEnd.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(23, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 25);
            this.label2.TabIndex = 2;
            this.label2.Text = "结束日期";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(531, 68);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(108, 42);
            this.button1.TabIndex = 4;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // DateTimePickerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 122);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dateTimePickerRetrieveEnd);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dateTimePickerRetrieveBegin);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "DateTimePickerForm";
            this.Text = "请选择时间";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.DateTimePicker dateTimePickerRetrieveBegin;
        public System.Windows.Forms.DateTimePicker dateTimePickerRetrieveEnd;
    }
}